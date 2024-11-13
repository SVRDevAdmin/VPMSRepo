using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using ZstdSharp.Unsafe;

namespace VPMS.Lib.Data
{
    public class UserRepository
    {
        /// <summary>
        /// Get User List Order by Surname
        /// </summary>
        /// <returns></returns>
        public static List<UserModel> GetStaffList(String organizationID)
        {
            List<UserModel> sStaffList = new List<UserModel>();

            try
            {
                using (var ctx = new UserDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.UserID, A.Surname, A.LastName, A.StaffID, A.Gender, A.EmailAddress, A.Status, " +
                                            "A.RoleID, A.BranchID, A.LastLoginDate, A.CreatedDate, A.CreatedBy, A.UpdatedDate, A.UpdatedBy " +
                                            "FROM mst_user AS A " +
                                            "INNER JOIN mst_branch AS B ON B.ID = A.BranchID " +
                                            "WHERE (" + (organizationID == null) + " OR B.OrganizationID = '" + organizationID + "') " +
                                            "ORDER BY A.Surname";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                var sStaff = new UserModel();
                                sStaff.UserID = sReader["UserID"].ToString();
                                sStaff.Surname = sReader["Surname"].ToString();
                                sStaff.LastName = sReader["LastName"].ToString();
                                sStaff.StaffID = sReader["StaffID"].ToString();
                                sStaff.Gender = sReader["Gender"].ToString();
                                sStaff.EmailAddress = sReader["EmailAddress"].ToString();
                                sStaff.Status = Convert.ToInt32(sReader["Status"]);
                                sStaff.RoleID = sReader["RoleID"].ToString();
                                sStaff.BranchID = Convert.ToInt32(sReader["BranchID"]);
                                sStaff.LastLoginDate = (!String.IsNullOrEmpty(sReader["LastLoginDate"].ToString())) ? Convert.ToDateTime(sReader["LastLoginDate"]) : null;
                                sStaff.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sStaff.CreatedBy = sReader["CreatedBy"].ToString();
                                sStaff.UpdatedDate = (!String.IsNullOrEmpty(sReader["UpdatedDate"].ToString())) ? Convert.ToDateTime(sReader["UpdatedDate"]) : null;
                                sStaff.UpdatedBy = (sReader["UpdatedBy"] != null) ? sReader["UpdatedBy"].ToString() : null;
     

                                sStaffList.Add(sStaff);
                            }
                        }
                    }
                    sConn.Close();

                    return sStaffList;
                    //return ctx.Mst_User.OrderBy(x => x.Surname).ToList();
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get User Listing View by filter
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="RoleID"></param>
        /// <param name="GenderID"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="BranchID"></param>
        /// <param name="Status"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static List<UserListingViewObject> GetUserListingByFilter(String UserID, String RoleID, String GenderID, 
                                                String OrganizationID, String BranchID, String Status, int pageSize, 
                                                int pageIndex, out int totalRecords)
        {
            List<UserListingViewObject> sResult = new List<UserListingViewObject>();
            totalRecords = 0;

            try
            {
                using (var ctx = new UserDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', " +
                                            "A.UserID, CONCAT(A.Surname, ' ', A.LastName) AS 'StaffName', A.StaffID, " +
                                            "B.RoleName, A.Gender, E.CodeName as 'GenderName', A.EmailAddress, A.Status, " + 
                                            "C.CodeName AS 'StatusName', A.BranchID, D.Name AS 'BranchName', D.OrganizationID, " + 
                                            "O.Name as 'Organization', " +
                                            "COUNT(*) OVER() AS 'TotalRows' " +
                                            "FROM mst_user AS A " +
                                            "LEFT JOIN mst_roles AS B ON B.RoleID = A.RoleID " +
                                            "LEFT JOIN mst_branch AS D ON D.ID = A.BranchID " +
                                            "LEFT JOIN mst_organisation AS O ON O.ID = D.OrganizationID " +
                                            "LEFT JOIN (" +
                                                "SELECT * FROM mst_mastercodedata WHERE codeGroup='Status' AND IsActive=1" +
                                            ") AS C ON C.CodeID = A.Status " +
                                            "LEFT JOIN (" +
                                                "SELECT * FROM mst_mastercodedata WHERE codeGroup='Gender' AND IsActive=1" +
                                            ") AS E on E.CodeID = A.Gender " +
                                            "WHERE (A.Status <> '2') AND " +
                                            "(" + (UserID == null) + " OR A.UserID = '" + UserID + "') AND " +
                                            "(" + (RoleID == null) + " OR A.RoleID = '" + RoleID + "') AND " +
                                            "(" + (GenderID == null) + " OR A.Gender = '" + GenderID + "') AND " +
                                            "(" + (OrganizationID == null) + " OR D.OrganizationID = '" + OrganizationID + "') AND " +
                                            "(" + (BranchID == null) + " OR A.BranchID = '" + BranchID + "') AND " +
                                            "(" + (Status == null) + " OR A.Status = '" + Status + "') " +
                                            "ORDER BY A.Surname " +
                                            "LIMIT " + pageSize + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sResult.Add(new UserListingViewObject
                                {
                                    SeqNo = Convert.ToInt32(sReader["row_num"]),
                                    UserID = sReader["UserID"].ToString(),
                                    StaffName = sReader["StaffName"].ToString(),
                                    StaffID = sReader["StaffID"].ToString(),
                                    RoleName = sReader["RoleName"].ToString(),
                                    Gender = sReader["GenderName"].ToString(),
                                    EmailAddress = sReader["EmailAddress"].ToString(),
                                    Status = sReader["Status"].ToString(),
                                    StatusName = sReader["StatusName"].ToString(),
                                    BranchID = sReader["BranchID"].ToString(),
                                    BranchName = sReader["BranchName"].ToString(),
                                    OrganizationID = sReader["OrganizationID"].ToString(),
                                    Organization = sReader["Organization"].ToString()
                                });

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                            }
                        }
                    }
                    sConn.Close();

                    return sResult;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Create user in Identity User
        /// </summary>
        /// <param name="sUserObj"></param>
        /// <param name="sRoleID"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static Boolean AddIdentityUser(IdentityUserObject sUserObj, String sRoleID,  out String sUserID)
        {
            IPasswordHasher<IdentityUser> _passwordHasher = new PasswordHasher<IdentityUser>();
            IdentityUser sUser = new IdentityUser();

            Boolean isSuccess = false;
            sUserID = "";

            try
            {
                using (var ctx = new UserDBContext())
                {
                    sUserObj.PasswordHash = _passwordHasher.HashPassword(sUser, "Abcd@1234");

                    ctx.aspnetusers.Add(sUserObj);
                    ctx.SaveChanges();

                    var sUserRoleObj = new IdentityUserRoleObject();
                    sUserRoleObj.UserId = sUserObj.Id;
                    sUserRoleObj.RoleId = sRoleID;

                    ctx.aspnetuserroles.Add(sUserRoleObj);
                    ctx.SaveChanges();

                    sUserID = sUserObj.Id;

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
        /// Validate User profile in Identity user
        /// </summary>
        /// <param name="sLoginID"></param>
        /// <returns></returns>
        public static Boolean ValidateIdentityUser(String sLoginID)
        {
            try
            {
                using (var ctx = new UserDBContext())
                {
                    return ctx.aspnetusers.Where(x => x.UserName == sLoginID).Any();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Add User Profile
        /// </summary>
        /// <param name="sUserProfile"></param>
        /// <returns></returns>
        public static Boolean CreateUser(UserModel sUserProfile)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new UserDBContext())
                {
                    ctx.Mst_User.Add(sUserProfile);
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
        /// Update User Profile
        /// </summary>
        /// <param name="sUserProfile"></param>
        /// <returns></returns>
        public static Boolean UpdateUser(UserModel sUserProfile)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new UserDBContext())
                {
                    var sUserObj = ctx.Mst_User.Where(x => x.UserID == sUserProfile.UserID).FirstOrDefault();
                    if (sUserObj != null)
                    {
                        Boolean isChanges = false;

                        if (sUserObj.Surname != sUserProfile.Surname)
                        {
                            sUserObj.Surname = sUserProfile.Surname;
                            isChanges = true;
                        }

                        if (sUserObj.LastName != sUserProfile.LastName)
                        {
                            sUserObj.LastName = sUserProfile.LastName;
                            isChanges = true;
                        }

                        if (sUserObj.StaffID != sUserProfile.StaffID)
                        {
                            sUserObj.StaffID = sUserObj.StaffID;
                            isChanges = true;
                        }

                        if (sUserObj.Gender != sUserProfile.Gender)
                        {
                            sUserObj.Gender = sUserObj.Gender;
                            isChanges = true;
                        }

                        if (sUserObj.EmailAddress != sUserProfile.EmailAddress)
                        {
                            sUserObj.EmailAddress = sUserProfile.EmailAddress;
                            isChanges = true;
                        }

                        if (sUserObj.Status != sUserProfile.Status)
                        {
                            sUserObj.Status = sUserProfile.Status;
                            isChanges = true;
                        }

                        if (sUserObj.RoleID != sUserProfile.RoleID)
                        {
                            sUserObj.RoleID = sUserProfile.RoleID;
                            isChanges = true;
                        }

                        if (sUserObj.BranchID != sUserProfile.BranchID)
                        {
                            sUserObj.BranchID = sUserProfile.BranchID;
                            isChanges = true;
                        }

                        if (isChanges)
                        {
                            sUserProfile.UpdatedDate = sUserProfile.CreatedDate;
                            sUserProfile.UpdatedBy = sUserProfile.CreatedBy;

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
        /// Delete User Profile
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean DeleteUser(String sUserID, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new UserDBContext())
                {
                    var sUserProfile = ctx.Mst_User.Where(x => x.UserID == sUserID).FirstOrDefault();
                    if (sUserProfile != null)
                    {
                        sUserProfile.Status = 2;
                        sUserProfile.UpdatedDate = DateTime.Now;
                        sUserProfile.UpdatedBy = sUpdatedBy;

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
        /// Get User Profile by User ID
        /// </summary>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static UserProfileExtObj GetUserProfileByUserID(String sUserID)
        {
            UserProfileExtObj sUserObj = new UserProfileExtObj();

            try
            {
                using (var ctx = new UserDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.UserID, A.Surname, A.LastName, A.StaffID, A.Gender, A.EmailAddress, " +
                                            "A.Status, A.RoleID, C.OrganizationID, A.BranchID, A.LastLoginDate, " +
                                            "B.UserName AS 'LoginID' " +
                                            "FROM mst_user AS A " +
                                            "INNER JOIN aspnetusers AS B ON  B.Id = A.UserID COLLATE 'utf8mb4_general_ci' " +
                                            "INNER JOIN mst_branch AS C ON C.ID = A.BranchID AND C.`Status` = '1' " +
                                            "WHERE A.UserID = '" + sUserID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sUserObj.UserID = sReader["UserID"].ToString();
                                sUserObj.Surname = sReader["Surname"].ToString();
                                sUserObj.LastName = sReader["LastName"].ToString();
                                sUserObj.StaffID = sReader["StaffID"].ToString();
                                sUserObj.Gender = sReader["Gender"].ToString();
                                sUserObj.EmailAddress = sReader["EmailAddress"].ToString();
                                sUserObj.Status = Convert.ToInt32(sReader["Status"]);
                                sUserObj.RoleID = sReader["RoleID"].ToString();
                                sUserObj.OrganizationID = Convert.ToInt32(sReader["OrganizationID"]);
                                sUserObj.BranchID = Convert.ToInt32(sReader["BranchID"]);
                                sUserObj.LoginID = sReader["LoginID"].ToString();
                            }
                        }
                    }

                    sConn.Close();

                    return sUserObj;
                    //var sResult = ctx.Mst_User
                    //                 .Where(x => x.UserID == sUserID)
                    //                 .Select(x => new UserProfileExtObj
                    //                 {
                    //                     UserID = x.UserID,
                    //                     Surname = x.Surname,
                    //                     LastName = x.LastName,
                    //                     StaffID = x.StaffID,
                    //                     Gender = x.Gender,
                    //                     EmailAddress = x.EmailAddress,

                    //                 })
                    //                 .FirstOrDefault();

                    //return ctx.Mst_User.Where(x => x.UserID == sUserID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
