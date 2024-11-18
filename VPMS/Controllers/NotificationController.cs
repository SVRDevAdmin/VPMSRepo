using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class NotificationController : Controller
    {
        /// <summary>
        /// Index Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotificationSettings()
        {
            return View("Settings");
        }

        /// <summary>
        /// Get Notification List by Notification Group
        /// </summary>
        /// <param name="sGroup"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IActionResult GetNotificationList(String sGroup, int pageSize, int pageIndex)
        {
            int iTotalRecords = 0;

            var sResult = NotificationRepository.GetNotificationList(ConfigSettings.GetConfigurationSettings(), sGroup, pageSize, pageIndex, out iTotalRecords);
            if (sResult != null)
            {
                return Json(new { data = sResult, totalRecord = iTotalRecords });
            }

            return null;
        }
    }
}
