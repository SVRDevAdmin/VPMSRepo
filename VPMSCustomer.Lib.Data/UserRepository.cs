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
        public static Boolean AddAspNetUser(IdentityUserObject sUserObj, String sRoleID, String sPassword)
        {
            IPasswordHasher<IdentityUser> _passwordHasher = new PasswordHasher<IdentityUser>();
            IdentityUser sUser = new IdentityUser();

            Boolean isSuccess = false;
            //sUserID = "";

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

                    //sUserID = sUserObj.Id;

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

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
                isSuccess = false;
            }

            return isSuccess;
        }

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
                isValid = false;
            }

            return isValid;
        }

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
                return null;
            }
        }
    }
}
