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
    public class MastercodeRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region "MasterCode Data"
        /// <summary>
        /// Get Mastercode Data By Group Name
        /// </summary>
        /// <param name="sGroup"></param>
        /// <returns></returns>
        public static List<MasterCodeDataModel> GetMastercodeDataByGroup(String sGroup)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_mastercodedata.Where(x => x.CodeGroup == sGroup && x.IsActive == 1).OrderBy(x => x.SeqOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetMastercodeDataByGroup >>> " + ex.ToString());
                return null;
            }
        }
        #endregion

        #region "Country & State & City"
        /// <summary>
        /// Get coyntry List
        /// </summary>
        /// <returns></returns>
        public static List<CountryModel> GetCountryList()
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_countrylist.Where(x => x.IsActive == 1).OrderBy(x => x.CountryName).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetCountryList >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Country Master record By Name
        /// </summary>
        /// <param name="sCountryName"></param>
        /// <returns></returns>
        public static CountryModel GetCountryInfoByName(String sCountryName)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_countrylist.Where(x => x.CountryName == sCountryName && x.IsActive == 1).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetCountryInfoByName >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get State list by Country ID
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<StateModel> GetStatesList(int CountryID)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_state.Where(x => x.CountryID == CountryID).OrderBy(x => x.State).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetStatesList >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get City List by State ID
        /// </summary>
        /// <param name="StateID"></param>
        /// <returns></returns>
        public static List<CityModel> GetCityList(int StateID)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_city.Where(x => x.StateID == StateID).OrderBy(x => x.City).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetCityList >>> " + ex.ToString());
                return null;
            }
        }
        #endregion

        /// <summary>
        /// Get Clinic Branch List
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public static List<BranchModel> GetBranchList(int organizationID)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_branch.Where(x => organizationID == -1 || x.OrganizationID == organizationID).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetBranchList >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Branch List in View Listing
        /// </summary>
        /// <param name="sState"></param>
        /// <returns></returns>
        public static List<BranchModel> GetBranchViewListing(String sState)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_branch.Where(x => sState == "All" || x.State == sState).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetBranchViewListing >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Doctor incharge for specific services
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public static List<DoctorModel> GetDoctorListByServices(int serviceID)
        {
            List<DoctorModel> sResultList = new List<DoctorModel>();

            try
            {
                using (var ctx = new DoctorDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT B.ID, B.`Name`, B.Gender, B.Designation, B.System_ID " +
                                            "FROM mst_service_doctor AS A " +
                                            "INNER JOIN mst_doctor AS B ON b.ID = A.DoctorID " +
                                            "WHERE A.ServiceID = '" + serviceID + "' AND " +
                                            "A.IsDeleted = '0' AND " +
                                            "B.IsDeleted = '0' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                DoctorModel sDoctorObj = new DoctorModel();
                                sDoctorObj.ID = Convert.ToInt32(sReader["ID"]);
                                sDoctorObj.Name = sReader["Name"].ToString();
                                sDoctorObj.Gender = sReader["Gender"].ToString();
                                sDoctorObj.Designation = sReader["Designation"].ToString();
                                sDoctorObj.System_ID = sReader["System_ID"].ToString();

                                sResultList.Add(sDoctorObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("MastercodeRepository >>> GetDoctorListByServices >>> " + ex.ToString());
                return null;
            }
        }
    }
}
