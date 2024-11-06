using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
	public class TestManagementController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
