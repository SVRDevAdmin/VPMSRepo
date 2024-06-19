using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
