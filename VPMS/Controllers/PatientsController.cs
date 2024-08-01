using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
