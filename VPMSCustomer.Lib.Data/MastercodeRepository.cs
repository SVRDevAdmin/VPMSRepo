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
        #region "MasterCode Data"
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
                return null;
            }
        }
        #endregion

        #region "Country & State & City"
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
                return null;
            }
        }

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
                return null;
            }
        }

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
                return null;
            }
        }
        #endregion

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
                return null;
            }
        }

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
                return null;
            }
        }
    }
}
