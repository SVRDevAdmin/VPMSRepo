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

        //public static List<PatientOwnerModel> GetPatientOwnersByPatientID(long iPatientID)
        //{
        //    try
        //    {
        //        using (var ctx = new PatientDBContext())
        //        {
        //            return ctx.mst_patients_Owner.Where(x => x.PatientID == iPatientID).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

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
    }
}
