using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using VPMS;
using VPMS.Lib;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;
using VPMSWeb.Models;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
	{
		/// <summary>
		/// Index page
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            bool hasPermission = SetPermission(roles, "StaffListing.View");

            if (!hasPermission)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

            return View();
		}

		/// <summary>
		/// User Details page
		/// </summary>
		/// <returns></returns>
		public IActionResult UserDetails()
		{
            int iOrganizationID = -1;
            if (HttpContext.Session.GetString("Level") == "0")
            {
                iOrganizationID = -1;
            }
            else
            {
                if (!String.IsNullOrEmpty(HttpContext.Session.GetString("OrganisationID")))
                {
                    iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
                }
            }


            ViewData["UserStatusDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Status");
            ViewData["UserGenderDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Gender");
            ViewData["UserOrganizationDropdown"] = OrganizationRepository.GetOrganizationList(2);
			ViewData["UserRoleDropdown"] = RoleRepository.GetRolesList(iOrganizationID);

            return View("UserDetails");
		}

		/// <summary>
		/// User Details page By User ID
		/// </summary>
		/// <param name="UserID"></param>
		/// <param name="ViewType"></param>
		/// <returns></returns>
		[Route("/User/UserProfile/{UserID}/{ViewType}")]
		public IActionResult UserProfile(String UserID, String ViewType)
		{
            int iOrganizationID = -1;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("OrganisationID")))
            {
                iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
            }

            ViewData["UserStatusDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Status");
            ViewData["UserGenderDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Gender");
            ViewData["UserOrganizationDropdown"] = OrganizationRepository.GetOrganizationList(2);
            ViewData["UserRoleDropdown"] = RoleRepository.GetRolesList(iOrganizationID);

            UserProfileExtObj sUserObj = UserRepository.GetUserProfileByUserID(UserID);
			ViewData["UserProfile"] = sUserObj;
			ViewData["ViewType"] = ViewType;

            return View("UserDetails", sUserObj);
		}

        /// <summary>
        /// Get User Listing View by Filter
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="RoleID"></param>
        /// <param name="GenderID"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="BranchID"></param>
        /// <param name="Status"></param>
        /// <param name="loginOrganizationID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
		public IActionResult GetUserListingView(String UserID, String RoleID, String GenderID, String OrganizationID, String BranchID, 
												String Status, int loginOrganizationID, int pageSize, int pageIndex)
		{
			int iTotalRecords;
            int isSuperadmin = 0;
            var organizationObj = OrganizationRepository.GetOrganizationByID(loginOrganizationID);
            if (organizationObj != null)
            {
                if (organizationObj.Level == 0 || organizationObj.Level == 1)
                {
                    isSuperadmin = 1;
                }
            }

            var sResult = UserRepository.GetUserListingByFilter(isSuperadmin, UserID, RoleID, GenderID, OrganizationID, BranchID, Status, loginOrganizationID, pageSize, pageIndex, out iTotalRecords);
			if (sResult != null)
			{
				return Json(new { data = sResult, totalRecord = iTotalRecords });
			}

			return null;
		}

		/// <summary>
		/// Add User Profile
		/// </summary>
		/// <param name="sUserModel"></param>
		/// <returns></returns>
		public IActionResult AddUser(UserInputControllerObj sUserModel)
		{
			Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

			IdentityUserObject sNewIdentity = new IdentityUserObject();
			sNewIdentity.Id = Guid.NewGuid().ToString();
			sNewIdentity.UserName = sUserModel.loginID;
			sNewIdentity.NormalizedUserName = sUserModel.loginID.ToUpper();
			sNewIdentity.Email = sUserModel.emailAddress;
			sNewIdentity.NormalizedEmail = sUserModel.emailAddress.ToUpper();
			sNewIdentity.EmailConfirmed = 0;
			sNewIdentity.SecurityStamp = Guid.NewGuid().ToString();
			sNewIdentity.PhoneNumberConfirmed = 0;
			sNewIdentity.TwoFactorEnabled = 0;
			sNewIdentity.AccessFailedCount = 0;
			sNewIdentity.LockoutEnabled = 1;

			UserModel sNewUser = new UserModel();
			sNewUser.Surname = sUserModel.surName;
			sNewUser.LastName = sUserModel.lastName;
			sNewUser.StaffID = sUserModel.staffID;
			sNewUser.Gender = sUserModel.gender;
			sNewUser.EmailAddress = sUserModel.emailAddress;
			sNewUser.Status = Convert.ToInt32(sUserModel.userStatus);
			sNewUser.RoleID = sUserModel.roleID;
			sNewUser.BranchID = Convert.ToInt32(sUserModel.branchID);
			sNewUser.CreatedDate = DateTime.Now;
			sNewUser.CreatedBy = sUserModel.createdBy;

			String sIdentityUserID = "";
			if (!UserRepository.ValidateIdentityUser(sUserModel.loginID))
			{
				String sTempPass = "Abcd@1234";

                if (UserRepository.AddIdentityUser(sNewIdentity, sNewUser.RoleID, sTempPass, out sIdentityUserID))
                {
                    sNewUser.UserID = sIdentityUserID;
                    if (UserRepository.CreateUser(sNewUser))
                    {
                        sResp.StatusCode = (int)StatusCodes.Status200OK;

						ProcessSendAccountCreationEmail(sUserModel, sTempPass);
                    }
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                }
            }
			else
			{
				sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
				sResp.isRecordExists = true;
            }


			return Json(sResp);
		}

		public void ProcessSendAccountCreationEmail(UserInputControllerObj sUserProfileInput, String sTempPass)

        {
            var sEmailConfig = ConfigSettings.GetConfigurationSettings();
            String? sHost = sEmailConfig.GetSection("SMTP:Host").Value;
            int? sPortNo = Convert.ToInt32(sEmailConfig.GetSection("SMTP:Port").Value);
            String? sUsername = sEmailConfig.GetSection("SMTP:Username").Value;
            String? sPassword = sEmailConfig.GetSection("SMTP:Password").Value;
            String? sSender = sEmailConfig.GetSection("SMTP:Sender").Value;

            try
			{
				var emailTemplate = TemplateRepository.GetTemplateByCodeLang(ConfigSettings.GetConfigurationSettings(), "VPMS_EN010");
				emailTemplate.TemplateContent = emailTemplate.TemplateContent.Replace("###<user_fullname>###", (sUserProfileInput.surName + " " + sUserProfileInput.lastName))
																			 .Replace("###<userlogin_id>###", sUserProfileInput.loginID)
																			 .Replace("###<user_password>###", sTempPass);

				List<String> sRecipient = new List<string>();
				sRecipient.Add(sUserProfileInput.emailAddress);

                VPMS.Lib.EmailObject sEmailObj = new VPMS.Lib.EmailObject();
                sEmailObj.SenderEmail = sSender;
				sEmailObj.RecipientEmail = sRecipient;
                sEmailObj.Subject = (emailTemplate != null) ? emailTemplate.TemplateTitle : "";
                sEmailObj.Body = (emailTemplate != null) ? emailTemplate.TemplateContent : "";
                sEmailObj.SMTPHost = sHost;
                sEmailObj.PortNo = sPortNo.Value;
                sEmailObj.HostUsername = sUsername;
                sEmailObj.HostPassword = sPassword;
                sEmailObj.EnableSsl = true;
                sEmailObj.UseDefaultCredentials = false;
                sEmailObj.IsHtml = true;

                String sErrorMessage = "";
                EmailHelpers.SendEmail(sEmailObj, out sErrorMessage);

            }
			catch (Exception ex)
			{
				String abc = "xxx";
			}
		}

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="sUserModel"></param>
        /// <returns></returns>
        public IActionResult UpdateUser(UserInputControllerObj sUserModel)
		{
			Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

			UserModel sUpdateUser = new UserModel();
			sUpdateUser.UserID = sUserModel.userID;
            sUpdateUser.Surname = sUserModel.surName;
            sUpdateUser.LastName = sUserModel.lastName;
            sUpdateUser.StaffID = sUserModel.staffID;
            sUpdateUser.Gender = sUserModel.gender;
            sUpdateUser.EmailAddress = sUserModel.emailAddress;
            sUpdateUser.Status = Convert.ToInt32(sUserModel.userStatus);
            sUpdateUser.RoleID = sUserModel.roleID;
            sUpdateUser.BranchID = Convert.ToInt32(sUserModel.branchID);
            sUpdateUser.CreatedDate = DateTime.Now;
            sUpdateUser.CreatedBy = "";

			if (UserRepository.UpdateUser(sUpdateUser))
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
		/// Delete User
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="updatedBy"></param>
		/// <returns></returns>
		public IActionResult DeleteUser(String userID, String updatedBy)
		{
			ResponseStatusObject sResp = new ResponseStatusObject();

			if (UserRepository.DeleteUser(userID, updatedBy))
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
		/// Get User List Dropdown
		/// </summary>
		/// <returns></returns>
        public IActionResult GetUserListDropdown(String organizationID)
		{
			var sUsersListObj = UserRepository.GetStaffList(organizationID);
			return Json(sUsersListObj);
		}

		/// <summary>
		/// Get Gender List Dropdown
		/// </summary>
		/// <returns></returns>
		public IActionResult GetGenderListDropdown()
		{
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Gender");
            return Json(sMasterCodeObj);
        }

        //public IActionResult UserProfile()
        //{

        //	var profile = _userDBContext.Mst_User.FirstOrDefault(x => x.UserID == HttpContext.Session.GetString("UserID"));
        //	var branch = _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == profile.BranchID);
        //	var organisation = _organisationDBContext.Mst_Organisation.FirstOrDefault(x => x.Id == branch.OrganizationID);
        //	var role = _roleDBContext.Mst_Roles.FirstOrDefault(x => x.RoleID == profile.RoleID);

        //	var userProfile = new StaffProfileInfo()
        //	{
        //		UserID = profile.UserID,
        //		Surname = profile.Surname,
        //		LastName = profile.LastName,
        //		StaffID = profile.StaffID,
        //		Gender = profile.Gender,
        //		Role = role.RoleName,
        //		Organisation = organisation.Name,
        //		Email = profile.EmailAddress,
        //		Branch = branch.Name,
        //		Status = profile.Status
        //	};


        //	ViewBag.PreviousPage = Program.CurrentPage;

        //	return View(userProfile);
        //}

        public bool SetPermission(List<string> roles, string permissionNeed)
        {
            bool havePermission = false;
            ViewData["CanAdd"] = false;
            ViewData["CanEdit"] = false;
            ViewData["CanView"] = false;
            ViewData["CanDelete"] = false;

            if (roles.Where(x => x.Contains("General.")).Count() > 0 || HttpContext.Session.GetString("IsAdmin") == "1")
            {
                ViewData["CanAdd"] = true;
                ViewData["CanEdit"] = true;
                ViewData["CanView"] = true;
                ViewData["CanDelete"] = true;
                havePermission = true;
            }
            else
            {
                if (roles.Contains("Staff.Add"))
                {
                    ViewData["CanAdd"] = true;
                }

                if (roles.Contains("StaffDetails.View"))
                {
                    ViewData["CanView"] = true;
                }

                if (roles.Contains("StaffDetails.Edit"))
                {
                    ViewData["CanEdit"] = true;
                }

                if (roles.Contains("StaffDetails.Delete"))
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
