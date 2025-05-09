using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using ZstdSharp.Unsafe;

namespace VPMS.Lib.Data
{
    public class RoleRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Roles Listing (Wiht Pagination)
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="totalRecords"></param>
		/// <returns></returns>
		public static List<RoleListingObject> GetRolesListing(int organizationid, int pageSize, int pageIndex, out int totalRecords)
        {
            List<RoleListingObject> sRoleList = new List<RoleListingObject>();
            totalRecords = 0;

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', " +
                                            "A.RoleID, A.RoleName, COUNT(B.UserID) AS 'TotalAssigned', " +
                                            "COUNT(*) OVER() AS 'TotalRows', " +
                                            "GROUP_CONCAT(DISTINCT(C.PermissionKey)) AS 'Permissions' " +
                                            "FROM ( " +

                                            "SELECT A1.* " +
                                            "FROM mst_roles AS A1 " +
                                            "INNER JOIN mst_organisation AS C1 ON C1.ID = A1.OrganizationID " +
                                            "WHERE A1.Status = '1' AND " +
                                            "(" +
                                            "((A1.RoleType = 997 OR A1.RoleType = 998 OR A1.RoleType = 999) AND A1.OrganizationID = '" + organizationid + "') OR " +
                                            "((A1.RoleType <> 997 AND A1.RoleType <> 998 AND A1.RoleType <> 999) AND A1.OrganizationID = '" + organizationid + "') " +
                                            ") " +

                                            ") AS A " +
                                            "LEFT JOIN mst_user AS B ON B.RoleID = A.RoleID AND B.Status = '1' " + 
                                            "LEFT JOIN mst_rolepermissions AS C ON C.RoleID = A.RoleID AND c.IsDeleted = '0' " +
                                            "GROUP BY A.RoleID, A.RoleName " +
                                            "LIMIT " + pageSize + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    //String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', " +
                    //                        "A.RoleID, A.RoleName, COUNT(B.UserID) AS 'TotalAssigned', " +
                    //                        "COUNT(*) OVER() AS 'TotalRows', " +
                    //                        "GROUP_CONCAT(DISTINCT(C.PermissionKey)) AS 'Permissions' " +
                    //                        "FROM mst_roles AS A " +
                    //                        "LEFT JOIN mst_user AS B ON B.RoleID = A.RoleID AND B.Status = '1' " +
                    //                        "LEFT JOIN mst_rolepermissions AS C ON C.RoleID = A.RoleID AND c.IsDeleted = '0' " +
                    //                        "WHERE A.Status = '1' " +
                    //                        "GROUP BY A.RoleID, A.RoleName " +
                    //                        "LIMIT " + pageSize + " " +
                    //                        "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = cmd.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sRoleList.Add(new RoleListingObject
                                {
                                    SeqNo = Convert.ToInt32(sReader["row_num"]),
                                    RoleID = sReader["RoleID"].ToString(),
                                    RoleName = sReader["RoleName"].ToString(),
                                    TotalAssigned = (sReader["TotalAssigned"] != null) ? Convert.ToInt32(sReader["TotalAssigned"]) : 0,
                                    sPermission = (sReader["Permissions"] != null) ? sReader["Permissions"].ToString() : ""
                                });

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                            }
                        }
                    }

                    sConn.Close();

                    return sRoleList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("RoleRepository >>> GetRolesListing >>> ", ex);
                return null;
            }
        }

        /// <summary>
        /// Create Role in aspnetroles table
        /// </summary>
        /// <param name="sRoleObject"></param>
        /// <param name="sRoleID"></param>
        /// <returns></returns>
        public static Boolean CreateIdentityRole(RoleModel sRoleObject, out String sRoleID)
        {
            Boolean isSuccess = false;
            sRoleID = "";

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    RoleIdentityObject sIdentity = new RoleIdentityObject();
                    sIdentity.Id = Guid.NewGuid().ToString();
                    sIdentity.Name = sRoleObject.RoleName;
                    sIdentity.NormalizedName = sRoleObject.RoleName.ToUpper();

                    ctx.aspnetroles.Add(sIdentity);
                    ctx.SaveChanges();

                    sRoleID = sIdentity.Id;
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("RoleRepository >>> CreateIdentityRole >>> ", ex);
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Create role profile
        /// </summary>
        /// <param name="sRoleObj"></param>
        /// <returns></returns>
        public static Boolean CreateRole(RoleModel sRoleObj)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    ctx.Mst_Roles.Add(sRoleObj);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
				logger.Error("RoleRepository >>> CreateRole >>> ", ex);
				isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Add Role's permission
        /// </summary>
        /// <param name="sPermissionLists"></param>
        /// <param name="sRoleID"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static Boolean InsertRolePermission(List<String> sPermissionLists, String sRoleID, String sUserID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    foreach(var s in sPermissionLists)
                    {
                        RolePermissionObject r = new RolePermissionObject();
                        r.RoleID = sRoleID;
                        r.PermissionKey = s;
                        r.IsDeleted = 0;
                        r.CreatedDate = DateTime.Now;
                        r.CreatedBy = sUserID;

                        ctx.mst_rolepermissions.Add(r);
                        ctx.SaveChanges();
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
				logger.Error("RoleRepository >>> InsertRolePermission >>> ", ex);
				isSuccess =  false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update Role Profile
        /// </summary>
        /// <param name="sRoleObject"></param>
        /// <returns></returns>
        public static Boolean UpdateRoleProfile(RoleModel sRoleObject)
        {
            Boolean isSuccess = false;
            Boolean isUpdate = false;

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    var sRoleProfile = ctx.Mst_Roles.Where(x => x.RoleID == sRoleObject.RoleID).FirstOrDefault();
                    if (sRoleProfile != null)
                    {
                        if (sRoleProfile.RoleName != sRoleObject.RoleName)
                        {
                            sRoleObject.RoleName = sRoleObject.RoleName;
                            isUpdate = true;
                        }

                        if (sRoleProfile.Description != sRoleObject.Description)
                        {
                            sRoleProfile.Description = sRoleObject.Description;
                            isUpdate = true;
                        }

                        if (sRoleProfile.IsDoctor != sRoleObject.IsDoctor)
                        {
                            sRoleProfile.IsDoctor = sRoleObject.IsDoctor;
                            isUpdate = true;
                        }

                        if (sRoleProfile.BranchID != sRoleObject.BranchID)
                        {
                            sRoleProfile.BranchID = sRoleObject.BranchID;
                            isUpdate = true;
                        }

                        if (isUpdate)
                        {
                            sRoleProfile.UpdatedDate = DateTime.Now;
                            sRoleProfile.UpdatedBy = sRoleObject.CreatedBy;

                            ctx.SaveChanges();
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
				logger.Error("RoleRepository >>> UpdateRoleProfile >>> ", ex);
				isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update Role's Permissions
        /// </summary>
        /// <param name="sPermissionLists"></param>
        /// <param name="sRoleID"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static Boolean UpdateRolePermission(List<String> sPermissionLists, String sRoleID, String sUserID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    var sRolePerms = ctx.mst_rolepermissions.Where(x => x.RoleID == sRoleID && x.IsDeleted == 0).ToList();
                    if (sRolePerms != null)
                    {
                        ctx.mst_rolepermissions.Where(x => x.RoleID == sRoleID && x.IsDeleted == 0).ToList()
                            .ForEach(x => x.IsDeleted = 1);
                        ctx.SaveChanges();
                    }

                    foreach(var s in sPermissionLists)
                    {
                        RolePermissionObject sUpdateRolePerm = new RolePermissionObject();
                        sUpdateRolePerm.RoleID = sRoleID;
                        sUpdateRolePerm.IsDeleted = 0;
                        sUpdateRolePerm.PermissionKey = s;
                        sUpdateRolePerm.CreatedDate = DateTime.Now;
                        sUpdateRolePerm.CreatedBy = sUserID;

                        ctx.mst_rolepermissions.Add(sUpdateRolePerm);
                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
				logger.Error("RoleRepository >>> UpdateRolePermission >>> ", ex);
				isSuccess =  false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Delete the Role Profile
        /// </summary>
        /// <param name="sRoleID"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static Boolean DeleteRole(String sRoleID, String sUserID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    var sRoleObj = ctx.Mst_Roles.Where(x => x.RoleID == sRoleID).FirstOrDefault();
                    if (sRoleObj != null)
                    {
                        sRoleObj.Status = 0;
                        sRoleObj.UpdatedDate = DateTime.Now;
                        sRoleObj.UpdatedBy = sUserID;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
				logger.Error("RoleRepository >>> DeleteRole >>> ", ex);
				isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Get Permission list available
        /// </summary>
        /// <returns></returns>
        public static List<AccessPermissionObject> GetAccessPermissionList()
        {
            try
            {
                using (var ctx = new RoleDBContext())
                {
                    return ctx.mst_accesspermission.Where(x => x.IsActive == 1 && !x.PermissionKey.Contains("General.")).ToList();
                }
            }
            catch (Exception ex)
            {
				logger.Error("RoleRepository >>> GetAccessPermissionList >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Role's Profile by Role ID
        /// </summary>
        /// <param name="sRoleID"></param>
        /// <returns></returns>
        public static RoleModelExtObject GetRoleProfileByID(String sRoleID)
        {
            RoleModelExtObject sResult = new RoleModelExtObject();

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.RoleID, A.RoleName, A.RoleType, A.`Status`, A.IsAdmin, A.IsDoctor, " +
                                            "A.CreatedDate, A.CreatedBy, A.UpdatedDate, A.UpdatedBy, B.OrganizationID , A.BranchID, A.Description " +
                                            "FROM mst_roles AS A " +
                                            "LEFT JOIN mst_branch AS B ON B.ID = A.BranchID " +
                                            "WHERE A.RoleID = '" + sRoleID + "' AND A.Status = '1'";

                    using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = cmd.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sResult.RoleID = sReader["RoleID"].ToString();
                                sResult.RoleName = sReader["RoleName"].ToString();
                                sResult.RoleType = Convert.ToInt32(sReader["RoleType"]);
                                sResult.Status = Convert.ToInt32(sReader["Status"]);
                                sResult.IsAdmin = Convert.ToInt32(sReader["IsAdmin"]);
                                sResult.IsDoctor = Convert.ToInt32(sReader["IsDoctor"]);
                                sResult.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sResult.CreatedBy = sReader["CreatedBy"].ToString();

                                if (sReader["UpdatedDate"] != null & (!String.IsNullOrEmpty(sReader["UpdatedDate"].ToString())))
                                {
                                    sResult.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }
                                
                                sResult.UpdatedBy = (sReader["UpdatedBy"] != null) ? sReader["UpdatedBy"].ToString() : "";

                                if (sReader["OrganizationID"] != null && !String.IsNullOrEmpty(sReader["OrganizationID"].ToString()))
                                {
                                    sResult.OrganisationID = Convert.ToInt32(sReader["OrganizationID"]);
                                }                                

                                if (sReader["BranchID"] != null && !String.IsNullOrEmpty(sReader["BranchID"].ToString()))
                                {
                                    sResult.BranchID = Convert.ToInt32(sReader["BranchID"]);
                                }
                                
                                sResult.Description = sReader["Description"].ToString();
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
        /// Get Role ID By Name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static String GetRoleIDByRoleName(String roleName)
        {
            try
            {
                using (var ctx = new RoleDBContext())
                {
                    return ctx.Mst_Roles.Where(x => x.RoleName.ToLower() == roleName.ToLower() && 
                                                    x.Status == 1)
                                        .FirstOrDefault()
                                        .RoleID;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Role's permission by Role ID
        /// </summary>
        /// <param name="sRoleID"></param>
        /// <returns></returns>
        public static List<String> GetRolePermissionsByRoleID(String sRoleID)
        {
            try
            {
                using (var ctx = new RoleDBContext())
                {
                    var sResult = ctx.mst_rolepermissions
                                     .Where(x => x.RoleID == sRoleID && x.IsDeleted == 0)
                                     .Select(x => x.PermissionKey)
                                     .ToList();

                    return sResult;
                }
            }
            catch (Exception ex)
            {
				logger.Error("RoleRepository >>> GetRolePermissionsByRoleID >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Roles List
        /// </summary>
        /// <returns></returns>
    //    public static List<RoleDropdownObject> GetRolesList()
    //    {
    //        try
    //        {
    //            using (var ctx = new RoleDBContext())
    //            {
    //                return ctx.Mst_Roles
    //                          .Where(x => x.Status == 1)
    //                          .OrderBy(x => x.RoleName)
    //                          .Select(x => new RoleDropdownObject
    //                          {
    //                              RoleID = x.RoleID,
    //                              RoleName = x.RoleName
    //                          })
    //                          .ToList();
    //            };
    //        }
    //        catch (Exception ex)
    //        {
				//logger.Error("RoleRepository >>> GetRolesList >>> ", ex);
				//return null;
    //        }
    //    }

        public static List<RoleDropdownObject> GetRolesList(int iOrganizationID)
        {
            List<RoleDropdownObject> sResultList = new List<RoleDropdownObject>();

            try
            {
                using (var ctx = new RoleDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.RoleID, A.RoleName " +
                                            "FROM mst_roles AS A " +
                                            "LEFT JOIN mst_branch AS B ON B.ID = A.BranchID " +
                                            "INNER JOIN mst_organisation AS C ON C.ID = B.OrganizationID " +
                                            "WHERE A.`Status`= '1' AND " + 
                                            "(" + (iOrganizationID == -1) + " OR C.ID = '" + iOrganizationID +"') " +
                                            "ORDER BY A.RoleName ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                RoleDropdownObject roleObject = new RoleDropdownObject();
                                roleObject.RoleID = sReader["RoleID"].ToString();
                                roleObject.RoleName = sReader["RoleName"].ToString();

                                sResultList.Add(roleObject);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("RoleRepository >>> GetRolesList >>> ", ex);
                return null;
            }
        }
    }
}
