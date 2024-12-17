using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
	public class TestingController : Controller
	{
		public IActionResult Index()
        {
            var invoiceNoTemp = "V001V202412160001";
            var test = "V" + invoiceNoTemp.Substring(1).Replace("V", "R").ToString();

            return View();
		}
	}
}
