using Microsoft.AspNetCore.Mvc;
using VPMS;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
	public class UserController : Controller
	{
		private readonly UserDBContext _userDBContext = new UserDBContext();
		private readonly BranchDBContext _branchDBContext = new BranchDBContext();
		private readonly OrganisationDBContext _organisationDBContext = new OrganisationDBContext();
		private readonly RoleDBContext _roleDBContext = new RoleDBContext();

		public IActionResult Index()
		{
			return RedirectToAction("UserProfile");
		}

		public IActionResult UserProfile()
		{

			var profile = _userDBContext.Mst_User.FirstOrDefault(x => x.UserID == HttpContext.Session.GetString("UserID"));
			var branch = _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == profile.BranchID);
			var organisation = _organisationDBContext.Mst_Organisation.FirstOrDefault(x => x.Id == branch.OrganizationID);
			var role = _roleDBContext.Mst_Roles.FirstOrDefault(x => x.RoleID == profile.RoleID);

			var userProfile = new StaffProfileInfo()
			{
				UserID = profile.UserID,
				Surname = profile.Surname,
				LastName = profile.LastName,
				StaffID = profile.StaffID,
				Gender = profile.Gender,
				Role = role.RoleName,
				Organisation = organisation.Name,
				Email = profile.EmailAddress,
				Branch = branch.Name,
				Status = profile.Status
			};


			ViewBag.PreviousPage = Program.CurrentPage;

			return View(userProfile);
		}
	}
}
