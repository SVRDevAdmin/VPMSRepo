using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using VPMSCustomer.Lib.Data;

namespace VPMSCustomer.Controllers
{
    public class NotificationsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("Notifications/GetNotificationViewListing")]
        [HttpGet()]
        public IActionResult GetNotificationViewListing(string userID, int pageSize, int pageIndex)
        {
            int iTotalRecords = 0;

            try
            {
                var sResult = NotificationRepository.GetNotificationViewListing(userID, pageSize, pageIndex, out iTotalRecords);
                if (sResult != null)
                {
                    return Json(new { data = sResult, totalRecords = iTotalRecords });
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

        [Route("Notification/UpdateMessageReadStatus")]
        [HttpPost()]
        public IActionResult UpdateNotificationReadStatus(long msgReceiverID, String userID)
        {
            VPMSCustomer.Lib.Models.ResponseCodeObject sResp = new Lib.Models.ResponseCodeObject();

            try
            {
                var sResult = NotificationRepository.UpdateNotificationReadStatus(msgReceiverID, userID);
                if (sResult)
                {
                    sResp.StatusCode = (int)StatusCodes.Status200OK;
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                }
            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }
    }
}
