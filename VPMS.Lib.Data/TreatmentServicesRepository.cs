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
        public static List<TreatmentServicesModel> GetTreatmentServicesList(IConfiguration config, int branchID)
        {
            try
            {
                using (var ctx = new TreatmentServicesDBContext(config))
                {
                    return ctx.mst_services.Where(x => x.BranchID == branchID && x.Status == 1).ToList();
                }
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
        /// <returns></returns>
    //    public static List<TreatmentServicesModel> GetServicesDoctorList(IConfiguration config, int serviceID)
    //    {
    //        try
    //        {
    //            using (var ctx = new TreatmentServicesDBContext(config))
    //            {
    //                return ctx.mst_services.Where(x => x.ID == serviceID && x.Status == 1).ToList();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
				//logger.Error("TreatmentServicesRepository >>> GetServicesDoctorList >>> ", ex);
				//return null;
    //        }
    //    }

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
