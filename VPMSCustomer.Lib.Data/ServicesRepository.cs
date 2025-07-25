using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class ServicesRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Services List By Branch ID
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public static List<ServiceModel> GetServicesListByBranchID(int branchID)
        {
            try
            {
                using (var ctx = new ServiceDBContext())
                {
                    return ctx.mst_services.Where(x => x.BranchID == branchID && x.Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ServicesRepository >>> GetServicesListByBranchID >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Doctor's Service List
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns></returns>
        public static List<ServiceModel> GetServicesListByDoctorID(int doctorID)
        {
            List<ServiceModel> sResultList = new List<ServiceModel>();

            try
            {
                using (var ctx = new ServiceDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT B.ID, B.Name, B.Description, B.Precaution, B.Prices, B.Duration " +
                                            "FROM mst_doctor_services As A " +
                                            "INNER JOIN mst_services AS B ON B.ID = A.ServicesID " +
                                            "WHERE A.DoctorID = '" + doctorID + "' AND " +
                                            "A.Status = '1' AND " +
                                            "B.Status = '1' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                ServiceModel sServiceObj = new ServiceModel();
                                sServiceObj.ID = Convert.ToInt32(sReader["ID"]);
                                sServiceObj.Name = sReader["Name"].ToString();
                                sServiceObj.Description = sReader["Description"].ToString();
                                sServiceObj.Precaution = sReader["Precaution"].ToString();
                                sServiceObj.Prices = Convert.ToDecimal(sReader["Prices"]);
                                sServiceObj.Duration = Convert.ToDecimal(sReader["Duration"]);

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
                logger.Error("ServicesRepository >>> GetServicesListByDoctorID >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Service Details by Service ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public static ServiceModel GetServiceDetailsByID(long serviceID)
        {
            try
            {
                using (var ctx = new ServiceDBContext())
                {
                    return ctx.mst_services.Where(x => x.ID == serviceID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ServicesRepository >>> GetServiceDetailsByID >>> " + ex.ToString());
                return null;
            }
        }
    }
}
