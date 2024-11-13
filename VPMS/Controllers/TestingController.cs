using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
	public class TestingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
