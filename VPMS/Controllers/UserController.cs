using Microsoft.AspNetCore.Mvc;
using VPMS;
using VPMS.Lib.Data.DBContext;

namespace VPMSWeb.Controllers
{
	public class UserController : Controller
	{
		private readonly UserDBContext _userDBContext = new UserDBContext();

		public IActionResult Index()
		{
			return RedirectToAction("UserProfile");
		}

		public IActionResult UserProfile()
		{
			var userProfile = _userDBContext.GetCurrentStaffProfile("test");

			ViewBag.PreviousPage = Program.CurrentPage;

			return View(userProfile);
		}
	}
}
