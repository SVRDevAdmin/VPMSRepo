using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using VPMS;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;
using VPMSWeb.Models;

namespace VPMSWeb.Controllers
{
	public class UserController : Controller
	{
		/// <summary>
		/// Index page
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// User Details page
		/// </summary>
		/// <returns></returns>
		public IActionResult UserDetails()
		{
            ViewData["UserStatusDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Status");
            ViewData["UserGenderDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Gender");
            ViewData["UserOrganizationDropdown"] = OrganizationRepository.GetOrganizationList(2);
			ViewData["UserRoleDropdown"] = RoleRepository.GetRolesList();

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
            ViewData["UserStatusDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Status");
            ViewData["UserGenderDropdown"] = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Gender");
            ViewData["UserOrganizationDropdown"] = OrganizationRepository.GetOrganizationList(2);
            ViewData["UserRoleDropdown"] = RoleRepository.GetRolesList();

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
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <returns></returns>
		public IActionResult GetUserListingView(String UserID, String RoleID, String GenderID, String OrganizationID, String BranchID, 
												String Status, int pageSize, int pageIndex)
		{
			int iTotalRecords;

			var sResult = UserRepository.GetUserListingByFilter(UserID, RoleID, GenderID, OrganizationID, BranchID, Status, pageSize, pageIndex, out iTotalRecords);
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
			sNewUser.CreatedBy = "";

			String sIdentityUserID = "";
			if (!UserRepository.ValidateIdentityUser(sUserModel.loginID))
			{
                if (UserRepository.AddIdentityUser(sNewIdentity, sNewUser.RoleID, out sIdentityUserID))
                {
                    sNewUser.UserID = sIdentityUserID;
                    if (UserRepository.CreateUser(sNewUser))
                    {
                        sResp.StatusCode = (int)StatusCodes.Status200OK;

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
        public IActionResult GetUserListDropdown()
		{
			var sUsersListObj = UserRepository.GetStaffList();
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
	}
}
