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
    }
}
