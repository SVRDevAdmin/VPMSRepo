using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VPMS.Lib.Data
{
    public class TreatmentServicesRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Treatment Services list
        /// </summary>
        /// <param name="config"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public static List<TreatmentServicesModel> GetTreatmentServicesList(IConfiguration config, int organizationID, int branchID, int isSuperadmin)
        {
            List<TreatmentServicesModel> sResultList = new List<TreatmentServicesModel>();

            try
            {
                using (var ctx = new TreatmentServicesDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ID, A.CategoryID, A.Name, A.Prices, A.Duration, A.Status, " +
                                            "A.Description, A.Precaution, A.BranchID, A.DoctorInCharge, A.CreatedDate, " +
                                            "A.CreatedBy, A.UpdatedDate, A.UpdatedBy " +
                                            "FROM mst_services AS A " +
                                            "INNER JOIN mst_branch AS B ON B.ID = A.BranchID " +
                                            "INNER JOIN mst_organisation AS C ON C.ID = B.OrganizationID " +
                                            "WHERE A.Status = '1' AND " +
                                            "( "  +
                                            "(" + (isSuperadmin == 1) + " AND C.Level >= 2 AND B.OrganizationID = '" + organizationID + "') OR " +
                                            "(" + (isSuperadmin == 0) + " AND B.OrganizationID = '" + organizationID + "' AND A.BranchID = '" + branchID + "') " +
                                            ") ";

                    using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = cmd.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                TreatmentServicesModel sServiceObj = new TreatmentServicesModel();
                                sServiceObj.ID = Convert.ToInt32(sReader["ID"]);
                                sServiceObj.CategoryID = Convert.ToInt32(sReader["CategoryID"]);
                                sServiceObj.Name = sReader["Name"].ToString();
                                sServiceObj.Prices = Convert.ToDecimal(sReader["Prices"]);
                                sServiceObj.Duration = Convert.ToDecimal(sReader["Duration"]);
                                sServiceObj.Status = Convert.ToInt32(sReader["Status"]);
                                sServiceObj.Description = sReader["Description"].ToString();
                                sServiceObj.BranchID = Convert.ToInt32(sReader["BranchID"]);
                                sServiceObj.DoctorInCharge = sReader["DoctorInCharge"].ToString();

                                if (sReader["CreatedDate"] != null && sReader["CreatedDate"].ToString() != "")
                                {
                                    sServiceObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                }
                                sServiceObj.CreatedBy = sReader["CreatedBy"].ToString();

                                if (sReader["UpdatedDate"] != null && sReader["UpdatedDate"].ToString() != "")
                                {
                                    sServiceObj.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }
                                sServiceObj.UpdatedBy = sReader["UpdatedBy"].ToString();

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
                logger.Error("TreatmentServicesRepository >>> GetTreatmentServicesList >>> ", ex);
                return null;
            }
        }

        /// <summary>
        /// Get Doctor available
        /// </summary>
        /// <param name="config"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>3
        public static List<TreatmentServicesDoctorModel> GetServicesDoctorList(IConfiguration config, int serviceID)
        {
            List<TreatmentServicesDoctorModel> sResult = new List<TreatmentServicesDoctorModel>();

            try
            {
                using (var ctx = new TreatmentServicesDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ID AS 'ServiceID', B.ID AS 'DoctorID', B.Name " +
                                            "FROM Mst_service_doctor AS A " +
                                            "INNER JOIN mst_doctor AS B ON B.ID = A.DoctorID " +
                                            "WHERE A.ServiceID = '" + serviceID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                TreatmentServicesDoctorModel sServiceDocObj = new TreatmentServicesDoctorModel();
                                sServiceDocObj.ServiceID = Convert.ToInt32(sReader["ServiceID"]);
                                sServiceDocObj.DoctorID = Convert.ToInt32(sReader["DoctorID"]);
                                sServiceDocObj.DoctorName = sReader["Name"].ToString();

                                sResult.Add(sServiceDocObj);
                            }
                        }
                    }

                    sConn.Close();

                    return sResult.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Treatment Service Info by ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public static TreatmentServicesModel GetServicesInfoByID(IConfiguration config, long serviceID)
        {
            try
            {
                using (var ctx = new TreatmentServicesDBContext(config))
                {
                    return ctx.mst_services.Where(x => x.ID == serviceID && x.Status == 1).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
				logger.Error("TreatmentServicesRepository >>> GetServicesInfoByID >>> ", ex);
				return null;
            }
        }
    }
}
