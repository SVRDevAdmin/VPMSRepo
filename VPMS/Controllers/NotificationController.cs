using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
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

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <returns></returns>
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
        public IActionResult GetNotificationList(String sUserID, int sBranchID, String sGroup, int pageSize, int pageIndex)
        {
            int iTotalRecords = 0;
            int iTotalNew = 0;

            var sResult = NotificationRepository.GetNotificationList(ConfigSettings.GetConfigurationSettings(), sUserID, sBranchID, sGroup, pageSize, pageIndex, out iTotalRecords, out iTotalNew);
            if (sResult != null)
            {
                return Json(new { data = sResult, totalRecord = iTotalRecords, totalNew = iTotalNew });
            }

            return null;
        }

        /// <summary>
        /// Get Notification Settings By User
        /// </summary>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public IActionResult GetNotificationSettings(String sUserID)
        {
            var sResult = NotificationRepository.GetNotificationSettings(ConfigSettings.GetConfigurationSettings(), sUserID);
            if (sResult != null)
            {
                return Json(new { data = sResult });
            }

            return null;
        }

        /// <summary>
        /// Update Notification Settings
        /// </summary>
        /// <param name="config"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateNotificationSettings(List<VPMS.Lib.Data.Models.NotificationConfigModel> config, String userID)
        {
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

            var sResult = NotificationRepository.UpdateNotificationSettings(ConfigSettings.GetConfigurationSettings(),
                                                                            userID, config);

            if (sResult)
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Update Message Read info
        /// </summary>
        /// <param name="iMessageReceiverID"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateMessageReadStatus(long iMessageReceiverID, String sUserID)
        {
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

            var sResult = NotificationRepository.UpdateMessageReadStatus(ConfigSettings.GetConfigurationSettings(), iMessageReceiverID, sUserID);
            if (sResult)
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        [HttpPost]
        public IActionResult TestNotification(NotificationModel sNotification)
        {
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();
            List<String> sUser = NotificationRepository.GetReceiverListByNotificationGroup(ConfigSettings.GetConfigurationSettings(), sNotification.NotificationGroup);

            var sResult = NotificationRepository.InserNotification(ConfigSettings.GetConfigurationSettings(), sNotification, sUser);
            if (sResult)
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }
    }
}
