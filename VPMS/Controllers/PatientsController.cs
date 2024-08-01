using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
