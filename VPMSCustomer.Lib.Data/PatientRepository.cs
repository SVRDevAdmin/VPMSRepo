using Microsoft.AspNetCore.Identity;
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
    public class PatientRepository
    {
        /// <summary>
        /// Create Patient Login Profile
        /// </summary>
        /// <param name="PatientOwnerID"></param>
        /// <param name="sAspNetUserID"></param>
        /// <param name="dtActivation"></param>
        /// <returns></returns>
        public static Boolean AddPatientLogin(long PatientOwnerID, String sAspNetUserID, DateTime? dtActivation)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new PatientDBContext())
                {
                    PatientLoginModel sPatientLogin = new PatientLoginModel();
                    sPatientLogin.PatientOwnerID = PatientOwnerID;
                    sPatientLogin.AspnetUserID = sAspNetUserID;
                    sPatientLogin.ActivationDate = dtActivation;
                    sPatientLogin.ProfileActivated = 1;
                    sPatientLogin.CreatedDate = DateTime.Now;
                    sPatientLogin.CreatedBy = "";

                    ctx.mst_patients_login.Add(sPatientLogin);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
        
        /// <summary>
        /// Get Patient's Owner profile by GUID
        /// </summary>
        /// <param name="sAspNetUserID"></param>
        /// <returns></returns>
        public static PatientOwnerModel GetPatientOwnerByIdentityUserID(String sAspNetUserID)
        {
            List<PatientOwnerModel> sResult = new List<PatientOwnerModel>();

            try
            {
                using (var ctx = new PatientDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT B.* " + 
                                            "FROM mst_patients_login AS A " +
                                            "INNER JOIN mst_patients_owner AS B ON B.Id = A.PatientOwnerID " +
                                            "WHERE A.AspnetUserID = '" + sAspNetUserID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PatientOwnerModel sOwnerObj = new PatientOwnerModel();
                                sOwnerObj.ID = Convert.ToInt64(sReader["ID"]);
                                sOwnerObj.PatientID = Convert.ToInt64(sReader["PatientID"]);
                                sOwnerObj.Name = sReader["Name"].ToString();
                                sOwnerObj.Gender = sReader["Gender"].ToString();
                                sOwnerObj.ContactNo = sReader["ContactNo"].ToString();
                                sOwnerObj.EmailAddress = sReader["EmailAddress"].ToString();
                                sOwnerObj.Address = sReader["Address"].ToString();
                                sOwnerObj.PostCode = sReader["PostCode"].ToString();
                                sOwnerObj.City = sReader["City"].ToString();
                                sOwnerObj.State = sReader["State"].ToString();
                                sOwnerObj.Country = sReader["Country"].ToString();
                                sOwnerObj.Status = Convert.ToInt32(sReader["Status"]);

                                if (!String.IsNullOrEmpty(sReader["CreatedDate"].ToString()))
                                {
                                    sOwnerObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                }
                                
                                if (sReader["CreatedBy"] != null)
                                {
                                    sOwnerObj.CreatedBy = sReader["CreatedBy"].ToString();
                                }
                                
                                if (!String.IsNullOrEmpty(sReader["UpdatedDate"].ToString()))
                                {
                                    sOwnerObj.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }

                                if (sReader["UpdatedBy"] != null)
                                {
                                    sOwnerObj.UpdatedBy = sReader["UpdatedBy"].ToString();
                                }
                                
                                sResult.Add(sOwnerObj);
                            }
                        }
                    }

                    sConn.Close();

                    return sResult.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Patient's Owner Profile by Patient ID
        /// </summary>
        /// <param name="iPatientID"></param>
        /// <returns></returns>
        public static List<PatientOwnerExtendedModel> GetPatientOwnersByPatientID(long iPatientID)
        {
            List<PatientOwnerExtendedModel> sResult = new List<PatientOwnerExtendedModel>();

            try
            {
                using (var ctx = new PatientDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.*, B.CodeName AS 'GenderName' " +
                                            "FROM mst_patients_owner AS A " +
                                            "LEFT JOIN (" +
                                                "SELECT * FROM mst_mastercodedata WHERE CodeGroup='Gender' AND IsActive=1" +
                                            ") AS B ON B.CodeID COLLATE UTF8MB4_GENERAL_CI = A.Gender " +
                                            "WHERE A.PatientID = '" + iPatientID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PatientOwnerExtendedModel sOwnerObj = new PatientOwnerExtendedModel();
                                sOwnerObj.ID = Convert.ToInt64(sReader["ID"]);
                                sOwnerObj.PatientID = Convert.ToInt64(sReader["PatientID"]);
                                sOwnerObj.Name = sReader["Name"].ToString();
                                sOwnerObj.Gender = sReader["Gender"].ToString();
                                sOwnerObj.GenderName = sReader["GenderName"].ToString();
                                sOwnerObj.ContactNo = sReader["ContactNo"].ToString();
                                sOwnerObj.EmailAddress = sReader["EmailAddress"].ToString();
                                sOwnerObj.Address = sReader["Address"].ToString();
                                sOwnerObj.PostCode = sReader["PostCode"].ToString();
                                sOwnerObj.City = sReader["City"].ToString();
                                sOwnerObj.State = sReader["State"].ToString();
                                sOwnerObj.Country = sReader["Country"].ToString();
                                sOwnerObj.Status = Convert.ToInt32(sReader["Status"]);
                                sOwnerObj.IsPrimary = Convert.ToInt32(sReader["IsPrimary"]);

                                if (!String.IsNullOrEmpty(sReader["CreatedDate"].ToString()))
                                {
                                    sOwnerObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                }

                                if (sReader["CreatedBy"] != null)
                                {
                                    sOwnerObj.CreatedBy = sReader["CreatedBy"].ToString();
                                }

                                if (!String.IsNullOrEmpty(sReader["UpdatedDate"].ToString()))
                                {
                                    sOwnerObj.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }

                                if (sReader["UpdatedBy"] != null)
                                {
                                    sOwnerObj.UpdatedBy = sReader["UpdatedBy"].ToString();
                                }

                                sResult.Add(sOwnerObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update Patient's Owner Profile
        /// </summary>
        /// <param name="sOwnerProfiles"></param>
        /// <returns></returns>
        public static Boolean UpdatePatientOwnerProfile(List<PatientOwnerExtendedModel> sOwnerProfiles)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new PatientDBContext())
                {
                    foreach(var owner in sOwnerProfiles)
                    {
                        var sOwnerObj = ctx.mst_patients_Owner.Where(x => x.ID == owner.ID).FirstOrDefault();
                        if (sOwnerObj != null)
                        {
                            sOwnerObj.Name = owner.Name;
                            sOwnerObj.Gender = owner.Gender;
                            sOwnerObj.ContactNo = owner.ContactNo;
                            sOwnerObj.EmailAddress = owner.EmailAddress;
                            sOwnerObj.Address = owner.Address;
                            sOwnerObj.PostCode = owner.PostCode;
                            sOwnerObj.City = owner.City;
                            sOwnerObj.State = owner.State;
                            sOwnerObj.Country = owner.Country;
                            sOwnerObj.UpdatedDate = DateTime.Now;
                            sOwnerObj.UpdatedBy = owner.UpdatedBy;

                            ctx.SaveChanges();

                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update Patients Configuration
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sConfigKey"></param>
        /// <param name="sConfigValue"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean UpdatePatientConfiguration(String sUserID, String sConfigKey, String sConfigValue, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new PatientDBContext())
                {
                    var sConfigurationObj = ctx.mst_patients_configuration
                                                .Where(x => x.UserID == sUserID && x.ConfigurationKey == sConfigKey)
                                                .FirstOrDefault();

                    if (sConfigurationObj != null)
                    {
                        sConfigurationObj.ConfigurationValue = sConfigValue;
                        sConfigurationObj.UpdatedBy = sUpdatedBy;
                        sConfigurationObj.UpdatedDate = DateTime.Now;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                    else
                    {
                        PatientConfigurationModel sNewConfiguration = new PatientConfigurationModel();
                        sNewConfiguration.UserID = sUserID;
                        sNewConfiguration.ConfigurationKey = sConfigKey;
                        sNewConfiguration.ConfigurationValue = sConfigValue;
                        sNewConfiguration.CreatedDate = DateTime.Now;
                        sNewConfiguration.CreatedBy = sUpdatedBy;

                        ctx.mst_patients_configuration.Add(sNewConfiguration);
                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Get Patient's Configuration Settings
        /// </summary>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static List<PatientConfigurationModel> GetPatientConfiguration(String sUserID)
        {
            try
            {
                using (var ctx = new PatientDBContext())
                {
                    return ctx.mst_patients_configuration.Where(x => x.UserID == sUserID).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Patient's Owner By OwnerID
        /// </summary>
        /// <param name="iOwnerID"></param>
        /// <returns></returns>
        public static PatientOwnerModel GetPatientOwnerByID(long iOwnerID)
        {
            try
            {
                using (var ctx = new PatientDBContext())
                {
                    return ctx.mst_patients_Owner.Where(x => x.ID == iOwnerID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
