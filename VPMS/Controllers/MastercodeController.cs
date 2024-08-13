using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;


namespace VPMSWeb.Controllers
{
    public class MastercodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
