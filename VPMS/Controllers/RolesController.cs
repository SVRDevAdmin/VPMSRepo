using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IO.IsolatedStorage;
using System.Linq.Expressions;
using System.Net.Security;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;
using VPMSWeb.Models;
using Org.BouncyCastle.Asn1.Mozilla;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        String sStatusMastercodeName  = "Status";
        String sRoleTypeMastercodeName = "RoleType";

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            bool hasPermission = SetPermission(roles, "RoleListing.View");

            if (!hasPermission)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

            return View();
        }

        /// <summary>
        /// Get Roles Listing (With Pagination)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IActionResult GetRoleListing(int organizationid, int pageSize, int pageIndex)
        {
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            bool hasPermission = SetPermission(roles, "RoleListing.View");

            if (!hasPermission)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

            int iTotalRecords;
            int isSuperadmin = 0;
            var organizationObj = OrganizationRepository.GetOrganizationByID(organizationid);
            if (organizationObj != null)
            {
                if (organizationObj.Level == 0 || organizationObj.Level == 1)
                {
                    isSuperadmin = 1;
                }
            }

            var sResult = RoleRepository.GetRolesListing(organizationid, isSuperadmin, pageSize, pageIndex, out iTotalRecords);
            if (sResult != null)
            {
                return Json(new { data = sResult, totalRecord = iTotalRecords });
            }

            return null;
        }

        /// <summary>
        /// Role Profile
        /// </summary>
        /// <returns></returns>
        public IActionResult RoleProfile()
        {
            ViewData["RoleTypeDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sRoleTypeMastercodeName);
            ViewData["StatusDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sStatusMastercodeName);
            ViewData["OrganizationDropdown"] = OrganizationRepository.GetOrganizationList(2);

            var sPermissionObject = RoleRepository.GetAccessPermissionList();
            if (sPermissionObject != null)
            {
                ViewData["PermissionObject"] = sPermissionObject;
                ViewData["PermissionGroup"] = sPermissionObject.GroupBy(x => x.PermissionGrouping).Select(x => x.Key).ToList();
            }

            return View("RoleProfile");
        }

        /// <summary>
        /// Add New Role
        /// </summary>
        /// <param name="sRoleModel"></param>
        /// <returns></returns>
        public IActionResult AddRole(NewRoleControllerModel sRoleModel)
        {
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();
            DateTime sNow = DateTime.Now;

            RoleModel sNewRole = new RoleModel();
            sNewRole.RoleName = sRoleModel.roleName;
            sNewRole.RoleType = sRoleModel.roleType.Value;
            sNewRole.Status = (sRoleModel.status != null) ? sRoleModel.status.Value : 0;
            sNewRole.IsDoctor = (sRoleModel.isDoctor != null) ? sRoleModel.isDoctor.Value : 0;
            sNewRole.Description = sRoleModel.roleDescription;
            sNewRole.BranchID = (sRoleModel.branchID != null) ? sRoleModel.branchID.Value : 0;
            sNewRole.OrganizationID = (sRoleModel.organizationID != null) ? sRoleModel.organizationID.Value : 0;
            sNewRole.CreatedDate = sNow;
            sNewRole.CreatedBy = sRoleModel.userID;

            String sIdentityRoleID = "";
            if (RoleRepository.CreateIdentityRole(sNewRole, out sIdentityRoleID))
            {
                sNewRole.RoleID = sIdentityRoleID;

                if (RoleRepository.CreateRole(sNewRole))
                {
                    RoleRepository.InsertRolePermission(sRoleModel.permissionsList, sNewRole.RoleID, sNewRole.CreatedBy);

                    sResp.StatusCode = (int)StatusCodes.Status200OK;
                }
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IActionResult DeleteRole(String roleID, String userID)
        {
            ResponseStatusObject sResp = new ResponseStatusObject();
            if (RoleRepository.DeleteRole(roleID, userID))
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Update Role Profile
        /// </summary>
        /// <param name="sUpdateRoleModel"></param>
        /// <returns></returns>
        public IActionResult UpdateRole(NewRoleControllerModel sUpdateRoleModel)
        {
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();
            DateTime sNow = DateTime.Now;

            RoleModel sUpdateRole = new RoleModel();
            sUpdateRole.RoleID = sUpdateRoleModel.roleID;
            sUpdateRole.RoleName = sUpdateRoleModel.roleName;
            sUpdateRole.Status = (sUpdateRoleModel.status != null) ? sUpdateRoleModel.status.Value : 0;
            sUpdateRole.IsDoctor = (sUpdateRoleModel.isDoctor != null) ? sUpdateRoleModel.isDoctor.Value : 0;
            sUpdateRole.Description = sUpdateRoleModel.roleDescription;
            sUpdateRole.BranchID = (sUpdateRoleModel.branchID != null) ? sUpdateRoleModel.branchID.Value : 0;
            sUpdateRole.CreatedDate = sNow;
            sUpdateRole.CreatedBy = sUpdateRoleModel.userID;

            if (RoleRepository.UpdateRoleProfile(sUpdateRole))
            {
                RoleRepository.UpdateRolePermission(sUpdateRoleModel.permissionsList, sUpdateRole.RoleID, sUpdateRole.CreatedBy);

                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// View / Edit Role Profile
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="ViewType"></param>
        /// <returns></returns>
        [Route("/Roles/ViewRoleProfile/{roleid}/{ViewType}")]
        public IActionResult ViewRoleProfile(String roleid, String ViewType)
        {
            RoleModel sRoleObject = RoleRepository.GetRoleProfileByID(roleid);
            List<String> sRolePermissionList = RoleRepository.GetRolePermissionsByRoleID(roleid);

            ViewData["RoleProfile"] = sRoleObject;
            ViewData["RolePermission"] = sRolePermissionList;
            ViewData["ViewType"] = ViewType;

            var sPermissionObject = RoleRepository.GetAccessPermissionList();
            if (sPermissionObject != null)
            {
                ViewData["PermissionObject"] = sPermissionObject;
                ViewData["PermissionGroup"] = sPermissionObject.GroupBy(x => x.PermissionGrouping).Select(x => x.Key).ToList();
            }

            ViewData["RoleTypeDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sRoleTypeMastercodeName);
            ViewData["StatusDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sStatusMastercodeName);
            ViewData["OrganizationDropdown"] = OrganizationRepository.GetOrganizationList(2);

            return View("RoleProfile", sRoleObject);
        }

        /// <summary>
        /// Get Permission List by Role ID
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public IActionResult GetRolePermissionsByRoleID(String roleID)
        {
            var sPermList = RoleRepository.GetRolePermissionsByRoleID(roleID);
            return Json(sPermList);
        }

        /// <summary>
        /// Get Status List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetStatusListDropdown()
        {
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sStatusMastercodeName);
            return Json(sMasterCodeObj);
        }

        /// <summary>
        /// Get Organization List
        /// </summary>
        /// <param name="iLevel"></param>
        /// <returns></returns>
        public IActionResult GetOrganizationList(int iLevel)
        {
            var sOrganizationList = OrganizationRepository.GetOrganizationList(iLevel);
            return Json(sOrganizationList);
        }

        /// <summary>
        /// Get Branch List
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public IActionResult GetBranchList(int organizationID)
        {
            var sBranchList = BranchRepository.GetBranchListByOrgID(organizationID);
            return Json(sBranchList);
        }

        /// <summary>
        /// Get Roles List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRolesList()
        {
            var sRoleList = RoleRepository.GetRolesList(-1);
            return Json(sRoleList);
        }

        public bool SetPermission(List<string> roles, string permissionNeed)
        {
            bool havePermission = false;
            ViewData["CanAdd"] = false;
            ViewData["CanEdit"] = false;
            ViewData["CanView"] = false;
            ViewData["CanDelete"] = false;

            if (roles.Where(x => x.Contains("General.")).Count() > 0)
            {
                ViewData["CanAdd"] = true;
                ViewData["CanEdit"] = true;
                ViewData["CanView"] = true;
                ViewData["CanDelete"] = true;
                havePermission = true;
            }
            else
            {
                if (roles.Contains("Role.Add"))
                {
                    ViewData["CanAdd"] = true;
                }

                if (roles.Contains("RoleDetails.View"))
                {
                    ViewData["CanView"] = true;
                }

                if (roles.Contains("RoleDetails.Edit"))
                {
                    ViewData["CanEdit"] = true;
                }

                if (roles.Contains("Role.Delete"))
                {
                    ViewData["CanDelete"] = true;
                }

                if (roles.Contains(permissionNeed))
                {
                    havePermission = true;
                }
            }

            return havePermission;
        }
    }
}
