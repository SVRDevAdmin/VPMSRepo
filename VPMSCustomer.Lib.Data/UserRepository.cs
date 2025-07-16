using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VPMSCustomer.Lib.Data.Models;
using VPMSCustomer.Lib.Data.DBContext;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace VPMSCustomer.Lib.Data
{
    public class UserRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Create record to ASPNet User
        /// </summary>
        /// <param name="sUserObj"></param>
        /// <param name="sRoleID"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public static Boolean AddAspNetUser(IdentityUserObject sUserObj, String sRoleID, String sPassword)
        {
            IPasswordHasher<IdentityUser> _passwordHasher = new PasswordHasher<IdentityUser>();
            IdentityUser sUser = new IdentityUser();

            Boolean isSuccess = false;

            try
            {
                using (var ctx = new UserDBContext())
                {
                    sUserObj.PasswordHash = _passwordHasher.HashPassword(sUser, sPassword);

                    ctx.aspnetusers.Add(sUserObj);
                    ctx.SaveChanges();

                    var sUserRoleObj = new IdentityUserRoleObject();
                    sUserRoleObj.UserId = sUserObj.Id;
                    sUserRoleObj.RoleId = sRoleID;

                    ctx.aspnetuserroles.Add(sUserRoleObj);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserRepository >>> AddAspNetUser >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Reset customer login Password
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sNewPassword"></param>
        /// <returns></returns>
        public static Boolean ResetUserPassword(String sUserID, String sNewPassword)
        {
            IPasswordHasher<IdentityUser> _passwordHasher = new PasswordHasher<IdentityUser>();
            IdentityUser sUser = new IdentityUser();

            Boolean isSuccess = false;

            try
            {
                using (var ctx = new UserDBContext())
                {
                    var sUserObj = ctx.aspnetusers.Where(x => x.Id == sUserID).FirstOrDefault();
                    if (sUserObj != null)
                    {
                        sUserObj.PasswordHash = _passwordHasher.HashPassword(sUser, sNewPassword);

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserRepository >>> ResetUserPassword >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update User Name
        /// </summary>
        /// <param name="sUsername"></param>
        /// <returns></returns>
        public static Boolean ValidateAspNetUserUserName(String sUsername)
        {
            Boolean isValid = false;

            try
            {
                using (var ctx = new UserDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.* " +
                                            "FROM aspnetusers AS A " +
                                            "INNER JOIN mst_patients_login AS B " +
                                            "ON B.AspnetUserID COLLATE utf8mb4_general_ci = A.Id " +
                                            "WHERE A.UserName='" + sUsername + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            if (!sReader.HasRows)
                            {
                                isValid = true;
                            }
                            else
                            {
                                isValid = false;
                            }
                        }
                    }

                    sConn.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserRepository >>> ValidateAspNetUserUserName >>> " + ex.ToString());
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Get ASPNet User Profile by UserID
        /// </summary>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static IdentityUserObject GetAspNetUserByUserID(String sUserID)
        {
            try
            {
                using (var ctx = new UserDBContext())
                {
                    return ctx.aspnetusers.Where(x => x.Id == sUserID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserRepository >>> GetAspNetUserByUserID >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Users list by Branch ID
        /// </summary>
        /// <param name="iBranchID"></param>
        /// <returns></returns>
        public static List<UserModel> GetUsersListByBranchID(int iBranchID)
        {
            List<UserModel> sUserList = new List<UserModel>();

            try
            {
                using (var ctx = new UserDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.* " +
                                            "FROM mst_user AS A " +
                                            "INNER JOIN mst_roles AS B ON B.RoleID = A.RoleID " +
                                            "WHERE A.Status = 1 AND " +
                                            "A.BranchID = '" + iBranchID + "' ";

                    using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = cmd.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                UserModel sUserObj = new UserModel();
                                sUserObj.UserID = sReader["UserID"].ToString();
                                sUserObj.Surname = sReader["Surname"].ToString();
                                sUserObj.LastName = sReader["LastName"].ToString();
                                sUserObj.StaffID = sReader["StaffID"].ToString();
                                sUserObj.Gender = sReader["Gender"].ToString();
                                sUserObj.EmailAddress = sReader["EmailAddress"].ToString();
                                sUserObj.Status = Convert.ToInt32(sReader["Status"]);
                                sUserObj.RoleID = sReader["RoleID"].ToString();
                                sUserObj.BranchID = Convert.ToInt32(sReader["BranchID"]);
                                sUserObj.OrganizationID = Convert.ToInt32(sReader["OrganizationID"]);
                                sUserObj.Level1ID = Convert.ToInt32(sReader["Level1ID"]);

                                if (sReader["LastLoginDate"] != null && sReader["LastLoginDate"].ToString() != "")
                                {
                                    sUserObj.LastLoginDate = Convert.ToDateTime(sReader["LastLoginDate"]);
                                }

                                if (sReader["CreatedDate"] != null && sReader["CreatedDate"].ToString() != "")
                                {
                                    sUserObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                }

                                sUserObj.CreatedBy = sReader["CreatedBy"].ToString();

                                if (sReader["UpdatedDate"] != null && sReader["UpdatedDate"].ToString() != "")
                                {
                                    sUserObj.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }

                                sUserObj.UpdatedBy = sReader["UpdatedBy"].ToString();


                                sUserList.Add(sUserObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sUserList;
            }
            catch (Exception ex)
            {
                logger.Error("UserRepository >>> GetUsersListByBranchID >>> " + ex.ToString());
                return null;
            }
        }
    }
}
