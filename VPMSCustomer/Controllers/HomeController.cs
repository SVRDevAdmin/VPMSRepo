using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data;
//using VPMSCustomer.Models;

namespace VPMSCustomer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Banners List
        /// </summary>
        /// <returns></returns>
        [Route("/Dashboard/GetBannersList")]
        [HttpGet()]
        public IActionResult GetBannersList()
        {
            try
            {
                var sBannerObj = BannerRepository.GetDashboardBannersList();
                if (sBannerObj != null)
                {
                    return Json(new { data = sBannerObj });
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

        /// <summary>
        /// Get Blogs List
        /// </summary>
        /// <returns></returns>
        [Route("/Dashboard/GetBlogsList")]
        [HttpGet()]
        public IActionResult GetBlogsList()
        {
            try
            {
                var sBlogObj = BannerRepository.GetBlogsList();
                if (sBlogObj != null)
                {
                    return Json(new { data = sBlogObj });
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
