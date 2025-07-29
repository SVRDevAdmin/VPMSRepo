using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;
using VPMS.Lib.Data.DBContext;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace VPMS.Lib.Data
{
    public class TaxRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Insert new tax configuration
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public static Boolean insertTaxConfiguration(IConfiguration config, TaxModel sModel) 
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TaxDBContext(config))
                {
                    ctx.mst_tax_configuration.Add(sModel);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("TaxRepository >>> insertTaxConfiguration >>> ", ex);
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Check Is Tax type exists
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sTaxType"></param>
        /// <returns></returns>
        public static Boolean IsTaxConfigurationExists(IConfiguration config, String sTaxType)
        {
            try
            {
                using (var ctx = new TaxDBContext(config))
                {
                    return ctx.mst_tax_configuration.Where(x => x.TaxType == sTaxType).Any();
                }
            }
            catch (Exception ex)
            {
                logger.Error("TaxRepository >>> IsTaxConfigurationExists >>> ", ex);
                return false;
            }
        }

        /// <summary>
        /// Get Tax Configuration Listing
        /// </summary>
        /// <param name="config"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static List<TaxConfigurationListModel> GetTaxConfigurationListing(IConfiguration config, int pageSize, int pageIndex, out int totalRecords)
        {
            List<TaxConfigurationListModel> sResultList = new List<TaxConfigurationListModel>();
            totalRecords = 0;

            try
            {
                using (var ctx = new TaxDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', " +
                                            "A.ID, A.TaxType, A.Description, A.ChargesRate, A.Status, A.CreatedDate, A.CreatedBy, " +
                                            "B.CodeName AS 'TaxName', " +
                                            "COUNT(*) OVER() AS 'TotalRows' " +
                                            "FROM mst_tax_configuration AS A " +
                                            "INNER JOIN " +
                                            "( " +
                                            "SELECT * FROM mst_mastercodedata WHERE CodeGroup='TaxType' AND IsActive=1 " +
                                            ") " +
                                            "AS B ON B.CodeID = A.TaxType " +
                                            "WHERE A.Status = 1 " +
                                            "LIMIT " + pageSize + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                TaxConfigurationListModel sTaxConfigObj = new TaxConfigurationListModel();
                                sTaxConfigObj.SeqNo = Convert.ToInt32(sReader["row_num"]);
                                sTaxConfigObj.ID = Convert.ToInt32(sReader["ID"]);
                                sTaxConfigObj.TaxType = sReader["TaxType"].ToString();
                                sTaxConfigObj.Description = sReader["Description"].ToString();
                                sTaxConfigObj.ChargesRate = Convert.ToDecimal(sReader["ChargesRate"]);
                                sTaxConfigObj.Status = Convert.ToInt32(sReader["Status"]);
                                sTaxConfigObj.TaxName = sReader["TaxName"].ToString();
                                sTaxConfigObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sTaxConfigObj.CreatedBy = sReader["CreatedBy"].ToString();

                                sTaxConfigObj.StatusName = (sReader["Status"].ToString() == "1") ? "Active" : "Inactive";
                                sTaxConfigObj.CreatedDateInString = Convert.ToDateTime(sReader["CreatedDate"]).ToString("dd/MM/yyyy hh:mm:ss tt");

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);

                                sResultList.Add(sTaxConfigObj);
                            }
                        }
                    }

                    sConn.Close();

                    return sResultList;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Tax Configuration By ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="taxConfigurationID"></param>
        /// <returns></returns>
        public static TaxModel GetTaxConfigurationByID(IConfiguration config, int taxConfigurationID)
        {
            try
            {
                using (var ctx = new TaxDBContext(config))
                {
                    return ctx.mst_tax_configuration.Where(x => x.ID == taxConfigurationID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("TaxRepository >>> GetTaxConfigurationByID >>> ", ex);
                return null;
            }
        }

        /// <summary>
        /// Update Tax Configuration Details
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sID"></param>
        /// <param name="sDescription"></param>
        /// <param name="dChargesRate"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean UpdateTaxConfiguration(IConfiguration config, int sID, String sDescription, Decimal dChargesRate, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TaxDBContext(config))
                {
                    var sTaxObj = ctx.mst_tax_configuration.Where(x => x.ID == sID).FirstOrDefault();
                    if (sTaxObj != null)
                    {
                        Boolean isChanges = false;

                        if (sTaxObj.Description.Trim() != sDescription)
                        {
                            sTaxObj.Description = sDescription;

                            isChanges = true;
                        }

                        if (sTaxObj.ChargesRate != dChargesRate)
                        {
                            sTaxObj.ChargesRate = dChargesRate;

                            isChanges = true;
                        }

                        if (isChanges)
                        {
                            sTaxObj.UpdatedDate = DateTime.Now;
                            sTaxObj.UpdatedBy = sUpdatedBy;

                            ctx.SaveChanges();

                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("TaxRepository >>> GetTaxConfigurationByID >>> ", ex);
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
