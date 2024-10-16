using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPMS;
using VPMS.Lib.Data.DBContext;

namespace VPMSWeb.Controllers
{
    [Authorize(Roles = "Superadmin")]
    public class DashboardController : Controller
    {
        private readonly PatientDBContext _patientDBContext = new PatientDBContext();

        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }


        public IActionResult Dashboard()
        {
            ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();

            Program.CurrentPage = "/Dashboard/Dashboard";

            return View();
        }
    }
}
