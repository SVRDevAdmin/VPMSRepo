using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class AnalyticsRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Customer Expenses Summary By Year
        /// </summary>
        /// <param name="sYear"></param>
        /// <param name="sPatientID"></param>
        /// <returns></returns>
        public static List<ExpensesSummaryModel> GetExpensesSummaryByYear(String sYear, long sPatientID)
        {
            List<ExpensesSummaryModel> sResultList = new List<ExpensesSummaryModel>();

            try
            {
                using (var ctx = new AnalyticsDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ServiceID, A.ServiceName, SUM(A.TotalValue) AS 'Total' " +
                                            "FROM txn_customer_expenses_summary AS A " +
                                            "WHERE A.TransDateInYear = '" + sYear + "' AND " + 
                                            "A.PatientID= '" + sPatientID + "' " + 
                                            "GROUP BY A.ServiceID, A.ServiceName " + 
                                            "ORDER BY A.ServiceID ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                ExpensesSummaryModel sSummarObj = new ExpensesSummaryModel();
                                sSummarObj.ServiceID = Convert.ToInt32(sReader["ServiceID"]);
                                sSummarObj.ServiceName = sReader["ServiceName"].ToString();
                                sSummarObj.Total = Convert.ToDecimal(sReader["Total"]);

                                sResultList.Add(sSummarObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("AnalyticsRepository >>> GetExpensesSummaryByYear >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Customer Expenses Summary Breakdown By Year
        /// </summary>
        /// <param name="sYear"></param>
        /// <param name="sPatientID"></param>
        /// <returns></returns>
        public static List<ExpensesSummaryBreakdownModel> GetExpensesSummaryBreakdown(String sYear, long sPatientID)
        {
            List<ExpensesSummaryBreakdownModel> sResultList = new List<ExpensesSummaryBreakdownModel>();

            try
            {
                using (var ctx = new AnalyticsDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.PetID, B.Name, A.ServiceID, A.ServiceName, " +
                                            "A.PetServicesTotal, D.PetTotal, " +
                                            "ROUND(((A.PetServicesTotal / D.PetTotal) * 100), 2) AS 'ServcExpsPercentage' " +
                                            "FROM ( " +

                                            "SELECT PetID, ServiceID, ServiceName, SUM(TotalValue) AS 'PetServicesTotal' " +
                                            "FROM txn_customer_expenses_summary " +
                                            "WHERE TransDateInYear = '" + sYear + "' AND PatientID = '" + sPatientID + "' " +
                                            "GROUP BY PetID, ServiceID, ServiceName " +
                                            "ORDER BY PetID, ServiceID, ServiceName " +

                                            ") AS A " +
                                            "LEFT JOIN ( " +

                                            "SELECT PetID, SUM(TotalValue) AS 'PetTotal' " +
                                            "FROM txn_customer_expenses_summary " +
                                            "WHERE TransDateInYear = '" + sYear + "' AND PatientID = '" + sPatientID + "' " +
                                            "GROUP BY PetID " +
                                            "ORDER BY PetID " +

                                            ") AS D ON D.PetID = A.PetID " +
                                            "INNER JOIN mst_pets AS B ON B.ID = A.PetID ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                ExpensesSummaryBreakdownModel sSummaryObject = new ExpensesSummaryBreakdownModel();
                                sSummaryObject.PetID = Convert.ToInt32(sReader["PetID"]);
                                sSummaryObject.PetName = sReader["Name"].ToString();
                                sSummaryObject.ServiceID = Convert.ToInt32(sReader["ServiceID"]);
                                sSummaryObject.ServiceName = sReader["ServiceName"].ToString();
                                sSummaryObject.PetServicesTotal = Convert.ToDecimal(sReader["PetServicesTotal"]);
                                sSummaryObject.PetTotal = Convert.ToDecimal(sReader["PetTotal"]);
                                sSummaryObject.ServcExpsPercentage = Convert.ToDecimal(sReader["ServcExpsPercentage"]);

                                sResultList.Add(sSummaryObject);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("AnalyticsRepository >>> GetExpensesSummaryBreakdown >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Insert Expenses Summary data
        /// </summary>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public static Boolean InsertExpensesSummary(List<AnalyticsModel> sModel)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AnalyticsDBContext())
                {
                    ctx.txn_customer_expenses_summary.AddRange(sModel);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("AnalyticsRepository >>> InsertExpensesSummary >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Remove Expenses Summary data
        /// </summary>
        /// <param name="sTargetDate"></param>
        /// <returns></returns>
        public static Boolean DeleteExpensesSummaryByDate(DateTime sTargetDate)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AnalyticsDBContext())
                {
                    var sResult = ctx.txn_customer_expenses_summary.Where(x => x.TransDate == sTargetDate.Date).ToList();
                    if (sResult.Count > 0)
                    {
                        ctx.txn_customer_expenses_summary.RemoveRange(sResult);
                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("AnalyticsRepository >>> DeleteExpensesSummaryByDate >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Insert Summary Log
        /// </summary>
        /// <param name="sLog"></param>
        /// <returns></returns>
        public static Boolean InsertExpensesSummaryLog(ExpensesSummaryLog sLog)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AnalyticsDBContext())
                {
                    ctx.txn_customer_expenses_summarylog.Add(sLog);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("AnalyticsRepository >>> InsertExpensesSummaryLog >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
