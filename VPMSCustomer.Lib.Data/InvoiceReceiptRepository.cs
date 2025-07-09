using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;
using Microsoft.Extensions.Configuration;

namespace VPMSCustomer.Lib.Data
{
    public class InvoiceReceiptRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Invoice / Receipts Listing By Type
        /// </summary>
        /// <param name="iViewType"></param>
        /// <param name="sPetIDs"></param>
        /// <param name="sInvReceiptNo"></param>
        /// <param name="sPetName"></param>
        /// <param name="sDoctorName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static List<InvoiceReceiptModel> GetInvoiceReceiptViewListing(int iViewType, List<long> sPetIDs, String sInvReceiptNo, String sPetName, String sDoctorName,
                                                                            int pageSize, int pageIndex, out int totalRecords)
        {
            List<InvoiceReceiptModel> sResultList = new List<InvoiceReceiptModel>();
            totalRecords = 0;

            String sPetListString = String.Join(",", sPetIDs);

            try
            {
                using (var ctx = new InvoiceReceiptDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER (ORDER BY A.CreatedDate) as 'SeqNo', " +
                                            "A.Id AS 'InvReceiptID', A.TreatmentPlanID, A.Branch, A.InvoiceNo, A.ReceiptNo, A.Fee, A.Tax, " +
                                            "A.GrandDiscount, A.CreatedDate, B.PetID, C.Name AS 'PetName', " +
                                            "COUNT(*) OVER () AS 'TotalRow' " +
                                            "FROM mst_invoicereceipt AS A " +
                                            "INNER JOIN txn_treatmentplan AS B ON B.ID = A.TreatmentPlanID " +
                                            "INNER JOIN mst_pets AS C ON C.ID = B.PetID " +
                                            "WHERE B.PetID in ('" + sPetListString + "') AND " +
                                            "(" +
                                            "(" + (iViewType == 0) + " AND A.Status IN (3) AND (" + (sInvReceiptNo == null) + " OR A.ReceiptNo = '" + sInvReceiptNo + "')) OR " +
                                            "(" + (iViewType == 1) + " AND A.Status IN (2, 4) AND (" + (sInvReceiptNo == null) + " OR A.InvoiceNo = '" + sInvReceiptNo + "')) " +
                                            ") " +
                                            "AND " +
                                            "((" + (sPetName == null) + ") OR C.Name = '" + sPetName + "') " +
                                            "ORDER BY A.CreatedDate ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                InvoiceReceiptModel sInvReceiptObj = new InvoiceReceiptModel();
                                sInvReceiptObj.SeqNo = Convert.ToInt32(sReader["SeqNo"]);
                                sInvReceiptObj.TreatmentPlanID = Convert.ToInt32(sReader["TreatmentPlanID"]);
                                sInvReceiptObj.InvReceiptID = Convert.ToInt32(sReader["InvReceiptID"]);
                                sInvReceiptObj.Branch = Convert.ToInt32(sReader["Branch"]);
                                sInvReceiptObj.InvoiceNo = sReader["InvoiceNo"].ToString();
                                sInvReceiptObj.ReceiptNo = sReader["ReceiptNo"].ToString();
                                sInvReceiptObj.Fee = Convert.ToDecimal(sReader["Fee"]);
                                sInvReceiptObj.Tax = Convert.ToDecimal(sReader["Tax"]);
                                sInvReceiptObj.GrandDiscount = Convert.ToDecimal(sReader["GrandDiscount"]);
                                sInvReceiptObj.PetID = Convert.ToInt32(sReader["PetID"]);
                                sInvReceiptObj.PetName = sReader["PetName"].ToString();
                                sInvReceiptObj.DoctorName = "";
                                sInvReceiptObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);

                                totalRecords = Convert.ToInt32(sReader["TotalRow"]);

                                sResultList.Add(sInvReceiptObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("InvoiceReceiptRepository >>> GetInvoiceReceiptViewListing >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Invoice / Receipts detail By ID
        /// </summary>
        /// <param name="iInvReceiptID"></param>
        /// <returns></returns>
        public static InvoiceReceiptDetailsObj GetInvoiceReceiptDetailByID(int iInvReceiptID)
        {
            List<InvoiceReceiptDetailsObj> sResultList = new List<InvoiceReceiptDetailsObj>();

            try
            {
                using (var ctx = new InvoiceReceiptDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ID, A.InvoiceNo, A.ReceiptNo, D2.Name AS 'OwnerName', D2.Address, D2.ContactNo, " +
                                            "B.PlanName, C.Name AS 'PetName', C.RegistrationNo, C.Species, A.CreatedDate, A.Tax, A.Fee " +
                                            "FROM mst_invoicereceipt AS A " +
                                            "INNER JOIN txn_treatmentplan AS B ON B.ID = A.TreatmentPlanID " + 
                                            "LEFT JOIN mst_pets AS C ON C.ID = B.PetID " +
                                            "LEFT JOIN mst_patients AS D ON D.ID = C.PatientID " +
                                            "LEFT JOIN mst_patients_owner AS D2 ON D2.PatientID = D.ID AND D2.IsPrimary = 1 " +
                                            "WHERE A.ID = '" + iInvReceiptID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                InvoiceReceiptDetailsObj sDetailObj = new InvoiceReceiptDetailsObj();
                                sDetailObj.ID = Convert.ToInt32(sReader["ID"]);
                                sDetailObj.InvoiceNo = sReader["InvoiceNo"].ToString();
                                sDetailObj.ReceiptNo = sReader["ReceiptNo"].ToString();
                                sDetailObj.OwnerName = sReader["OwnerName"].ToString();
                                sDetailObj.Address = sReader["Address"].ToString();
                                sDetailObj.ContactNo = sReader["ContactNo"].ToString();
                                sDetailObj.PetName = sReader["PetName"].ToString();
                                sDetailObj.RegistrationNo = sReader["RegistrationNo"].ToString();
                                sDetailObj.Species = sReader["Species"].ToString();
                                sDetailObj.PlanName = sReader["PlanName"].ToString();
                                sDetailObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sDetailObj.Tax = Convert.ToDecimal(sReader["Tax"]);
                                sDetailObj.Fee = Convert.ToDecimal(sReader["Fee"]);

                                sResultList.Add(sDetailObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("InvoiceReceiptRepository >>> GetInvoiceReceiptDetailByID >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Invoices / Receipts's Services
        /// </summary>
        /// <param name="iInvReceiptID"></param>
        /// <returns></returns>
        public static List<InvoiceReceiptServicesObj> GetInvoiceReceiptServicesList(int iInvReceiptID)
        {
            List<InvoiceReceiptServicesObj> sResultList = new List<InvoiceReceiptServicesObj>();

            try
            {
                using (var ctx = new InvoiceReceiptDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT B.ID, B.ServiceID, B.ServiceName, B.Price, B.Discount, B.TotalPrice " +
                                            "FROM mst_invoicereceipt AS A " +
                                            "INNER JOIN txn_treatmentplan_services AS B ON B.PlanID = A.TreatmentPlanID " +
                                            "WHERE A.ID = '" + iInvReceiptID + "' AND " +
                                            "B.IsDeleted = 0 ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                InvoiceReceiptServicesObj sServiceObj = new InvoiceReceiptServicesObj();
                                sServiceObj.ID = Convert.ToInt32(sReader["ID"]);
                                sServiceObj.ServiceID = Convert.ToInt32(sReader["ServiceID"]);
                                sServiceObj.ServiceName = sReader["ServiceName"].ToString();
                                sServiceObj.Price = Convert.ToDecimal(sReader["Price"]);
                                sServiceObj.Discount = Convert.ToDecimal(sReader["Discount"]);
                                sServiceObj.TotalPrice = Convert.ToDecimal(sReader["TotalPrice"]);

                                sResultList.Add(sServiceObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("InvoiceReceiptRepository >>> GetInvoiceReceiptServicesList >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Invoices / Receipts's Product
        /// </summary>
        /// <param name="iInvReceiptID"></param>
        /// <returns></returns>
        public static List<InvoiceReceiptProductsObj> GetInvoiceReceiptProductList(int iInvReceiptID)
        {
            List<InvoiceReceiptProductsObj> sResultList = new List<InvoiceReceiptProductsObj>();

            try
            {
                using (var ctx = new InvoiceReceiptDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT B.ID, B.ProductID, B.ProductName, B.Units, B.PricePerQty, B.Discount, B.TotalPrice " +
                                            "FROM mst_invoicereceipt AS A " +
                                            "INNER JOIN txn_treatmentplan_Products AS B ON B.PlanID = A.TreatmentPlanID " +
                                            "WHERE A.Id = '" + iInvReceiptID + "' AND " +
                                            "B.IsDeleted = 0 ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                InvoiceReceiptProductsObj sProductObj = new InvoiceReceiptProductsObj();
                                sProductObj.ID = Convert.ToInt32(sReader["ID"]);
                                sProductObj.ProductID = Convert.ToInt32(sReader["ProductID"]);
                                sProductObj.ProductName = sReader["ProductName"].ToString();
                                sProductObj.Units = Convert.ToInt32(sReader["Units"]);
                                sProductObj.PricePerQty = Convert.ToDecimal(sReader["PricePerQty"]);
                                sProductObj.Discount = Convert.ToDecimal(sReader["Discount"]);
                                sProductObj.TotalPrice = Convert.ToDecimal(sReader["TotalPrice"]);

                                sResultList.Add(sProductObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("InvoiceReceiptRepository >>> GetInvoiceReceiptProductList >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Total amount of Invoice / Receipts 
        /// </summary>
        /// <param name="iInvReceiptID"></param>
        /// <returns></returns>
        public static List<InvoiceReceiptTotalObj> GetInvoiceReceiptTotal(int iInvReceiptID)
        {
            List<InvoiceReceiptTotalObj> sResultList = new List<InvoiceReceiptTotalObj>();

            try
            {
                using (var ctx = new InvoiceReceiptDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT 'Total', T1.Fee " +
                                            "FROM mst_invoicereceipt AS T1 " +
                                            "WHERE ID = '" + iInvReceiptID + "' " +
                                            "UNION " +
                                            "SELECT 'Tax', T4.Tax " +
                                            "FROM mst_invoicereceipt AS T4 " +
                                            "WHERE ID = '" + iInvReceiptID + "' " +
                                            "UNION " +
                                            "SELECT 'Services', SUM(T2.TotalPrice) AS 'TotalPrice' " +
                                            "FROM (" +
                                            "SELECT B.ID, B.ServiceID, B.ServiceName, B.Price, B.Discount, B.TotalPrice " +
                                            "FROM mst_invoicereceipt AS A " +
                                            "INNER JOIN txn_treatmentplan_services AS B ON B.PlanID = A.TreatmentPlanID " +
                                            "WHERE A.ID = '" + iInvReceiptID +  "' and B.IsDeleted = 0" +
                                            ") AS T2 " +
                                            "GROUP BY 'Services' " +
                                            "UNION " +
                                            "SELECT 'Product', SUM(T3.TotalPrice) AS 'TotalPrice' " +
                                            "FROM (" +
                                            "SELECT B.ID, B.ProductID, B.ProductName, B.Units, B.PricePerQty, B.Discount, B.TotalPrice " +
                                            "FROM mst_invoicereceipt AS A " +
                                            "INNER JOIN txn_treatmentplan_Products AS B ON B.PlanID = A.TreatmentPlanID " +
                                            "WHERE A.Id = '" + iInvReceiptID + "' AND B.IsDeleted = 0" +
                                            ") AS T3 " +
                                            "GROUP BY 'Product' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                InvoiceReceiptTotalObj sTotalObj = new InvoiceReceiptTotalObj();
                                sTotalObj.ItemName = sReader["Total"].ToString();
                                sTotalObj.ItemTotalValue = Convert.ToDecimal(sReader["Fee"]);

                                sResultList.Add(sTotalObj);
                            }
                        }
                    }
                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("InvoiceReceiptRepository >>> GetInvoiceReceiptTotal >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Customer's Expenses Summary By Date
        /// </summary>
        /// <param name="sTransStartDate"></param>
        /// <param name="sTransEndDate"></param>
        /// <returns></returns>
        public static List<DailyExpenseSummObject> GetCustomerExpensesSummary(DateTime sTransStartDate, DateTime sTransEndDate)
        {
            List<DailyExpenseSummObject> sResultList = new List<DailyExpenseSummObject>();

            try
            {
                using (var ctx = new InvoiceReceiptDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.UpdatedDate AS 'InvoiceDate', M.PatientID, B.PetID, " +
                                            "C.ServiceID as 'ServiceID', C.ServiceName AS 'ServiceName', " +
                                            "C.TotalPrice AS 'ServicePrice', 'Service' AS 'EntityName' " +
                                            "FROM mst_invoicereceipt AS A " +
                                            "INNER JOIN txn_treatmentplan AS B ON B.ID = A.TreatmentPlanID " +
                                            "INNER JOIN txn_treatmentplan_services AS C ON C.PlanID = B.ID " +
                                            "INNER JOIN mst_pets AS M ON M.ID = B.PetID " +
                                            "WHERE (A.UpdatedDate >= '" + sTransStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                                            "A.UpdatedDate <= '" + sTransEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "') AND " +
                                            "A.Status = '3' " +
                                            "UNION " +
                                            "SELECT A.UpdatedDate AS 'InvoiceDate', M.PatientID, B.PetID, " +
                                            "C.ProductID AS 'ServiceID', C.ProductName AS 'ServiceName', " +
                                            "C.TotalPrice AS 'ServicePrice', 'Product' AS 'EntityName' " +
                                            "FROM mst_invoicereceipt AS A  " +
                                            "INNER JOIN txn_treatmentplan AS B ON B.ID = A.TreatmentPlanID " +
                                            "INNER JOIN txn_treatmentplan_Products AS C ON C.PlanID = B.ID " +
                                            "INNER JOIN mst_pets AS M ON M.ID = B.PetID " +
                                            "WHERE (A.UpdatedDate >= '" + sTransStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                                            "A.UpdatedDate <= '" + sTransEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "') AND " +
                                            "A.Status = '3' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                DailyExpenseSummObject sExpensesObj = new DailyExpenseSummObject();
                                sExpensesObj.InvoiceDate = Convert.ToDateTime(sReader["InvoiceDate"]);
                                sExpensesObj.PatientID = Convert.ToInt64(sReader["PatientID"]);
                                sExpensesObj.PetID = Convert.ToInt64(sReader["PetID"]);
                                sExpensesObj.ServiceID = Convert.ToInt32(sReader["ServiceID"]);
                                sExpensesObj.ServiceName = sReader["ServiceName"].ToString();
                                sExpensesObj.ServicePrice = Convert.ToDecimal(sReader["ServicePrice"]);
                                sExpensesObj.EntityName = sReader["EntityName"].ToString();

                                sResultList.Add(sExpensesObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("InvoiceReceiptRepository >>> GetCustomerExpensesSummary >>> " + ex.ToString());
                return null;
            }
        }
    }
}
