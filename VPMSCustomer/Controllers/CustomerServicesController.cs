using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Controllers
{
    public class CustomerServicesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Clinic List
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [Route("/CustomerServices/GetClinicList")]
        public IActionResult GetAllClinicList(String state)
        {
            List<BranchModel> sBranchList = new List<BranchModel>();

            try
            {
                sBranchList = MastercodeRepository.GetBranchViewListing(state);
                if (sBranchList != null)
                {
                    return Json(new { data = sBranchList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
