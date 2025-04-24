using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Globalization;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.Models;
using VPMSCustomer.Lib.Models;

namespace VPMSCustomer.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View("ResetPassword");
        }

        public ActionResult Profile()
        {
            return View("Profile");
        }

        public ActionResult AccountSettings()
        {
            return View("AccountSettings");
        }

        /// <summary>
        /// Get create account invitation link details
        /// </summary>
        /// <param name="invitationcode"></param>
        /// <returns></returns>
        [Route("/Account/AccountCreation/{invitationcode}")]
        public IActionResult AccountCreation(String invitationcode)
        {
            AccountCreationExtendedObj sResp = new AccountCreationExtendedObj();

            var sInvitationLinkObject = AccountRepository.GetInvitationLinkDetails(invitationcode);
            if (sInvitationLinkObject != null)
            {
                if (sInvitationLinkObject.AccountCreationDate == null)
                {
                    DateTime sNow = DateTime.Now;

                    if (sInvitationLinkObject.LinkCreatedDate <= sNow && sInvitationLinkObject.LinkExpiryDate >= sNow)
                    {
                        sResp.isLinkExists = true;
                        sResp.isLinkExpired = false;
                        sResp.isActivated = false;
                        sResp.EmailAddress = sInvitationLinkObject.EmailAddress;
                        sResp.PatientOwnerID = sInvitationLinkObject.PatientOwnerID;
                        sResp.InvitationCode = sInvitationLinkObject.InvitationCode;
                    }
                    else
                    {
                        sResp.InvitationCode = sInvitationLinkObject.InvitationCode;
                        sResp.isLinkExists = true;
                        sResp.isLinkExpired = true;
                        sResp.isActivated = false;
                    }
                }
                else
                {
                    sResp.isActivated = true;
                }
            }
            else
            {
                sResp.isLinkExists = false;
                sResp.isLinkExpired = false;
                sResp.isActivated = false;
            }

            return View("Index", sResp);
        }

        /// <summary>
        /// Register customer login
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sEmail"></param>
        /// <param name="sPassword"></param>
        /// <param name="iPatientOwnerID"></param>
        /// <param name="sInvitationCode"></param>
        /// <returns></returns>
        [Route("/Account/RegisterCustomer")]
        [HttpPost()]
        public IActionResult RegisterCustomerAccount(String sUserName, String sEmail, String sPassword, long iPatientOwnerID, String sInvitationCode)
        {
            ResponseCodeObject sResp = new ResponseCodeObject();

            try
            {
                IdentityUserObject sNewAspnetCustomer = new IdentityUserObject();
                sNewAspnetCustomer.Id = Guid.NewGuid().ToString();
                sNewAspnetCustomer.UserName = sUserName;
                sNewAspnetCustomer.NormalizedUserName = sUserName.ToUpper();
                sNewAspnetCustomer.Email = sEmail;
                sNewAspnetCustomer.NormalizedEmail = sEmail.ToUpper();
                sNewAspnetCustomer.EmailConfirmed = 0;
                sNewAspnetCustomer.SecurityStamp = Guid.NewGuid().ToString();
                sNewAspnetCustomer.PhoneNumberConfirmed = 0;
                sNewAspnetCustomer.TwoFactorEnabled = 0;
                sNewAspnetCustomer.AccessFailedCount = 0;
                sNewAspnetCustomer.LockoutEnabled = 0;

                if (UserRepository.ValidateAspNetUserUserName(sUserName))
                {
                    String sRoleID = RolesRepository.GetRoleIDByName("Customer");

                    if (UserRepository.AddAspNetUser(sNewAspnetCustomer, sRoleID, sPassword))
                    {
                        PatientRepository.AddPatientLogin(iPatientOwnerID, sNewAspnetCustomer.Id, DateTime.Now);
                        AccountRepository.UpdateInvitationLinkStatus(sInvitationCode, DateTime.Now);

                        sResp.StatusCode = (int)StatusCodes.Status200OK;
                    }
                    else
                    {
                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.isRecordExists = true;
                }

            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Get Customer profile by User ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [Route("/Account/GetCustomerAccount/{userID}")]
        [HttpGet]
        public IActionResult GetCustomerAccount(String userID)
        {
            try
            {
                var sUserObj = UserRepository.GetAspNetUserByUserID(userID);
                if (sUserObj != null)
                {
                    return Json(new { data = sUserObj });
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Customer Profile by Identity ID
        /// </summary>
        /// <param name="aspNetUserID"></param>
        /// <returns></returns>
        [Route("/Account/Profile/{aspNetUserID}")]
        [HttpGet()]
        public IActionResult GetProfileAccountByIdentityUserID(String aspNetUserID)
        {
            List<PatientOwnerExtendedModel> sPatientOwnerObj = new List<PatientOwnerExtendedModel>();

            try
            {
                var sOwnerProfile = PatientRepository.GetPatientOwnerByIdentityUserID(aspNetUserID);
                if (sOwnerProfile != null)
                {
                    var sPatientID = sOwnerProfile.PatientID;
                    if (sPatientID != null)
                    {
                        sPatientOwnerObj = PatientRepository.GetPatientOwnersByPatientID(sPatientID.Value);
                    }
                     
                }

                return Json(new { data = sPatientOwnerObj });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update Customer Profile Info
        /// </summary>
        /// <param name="ownerlist"></param>
        /// <returns></returns>
        [Route("/Account/Profile/UpdateProfile")]
        [HttpPost()]
        public IActionResult UpdateProfileAccounts(List<PatientOwnerExtendedModel> ownerlist)
        {
            ResponseCodeObject sResp = new ResponseCodeObject();

            try
            {
                if (PatientRepository.UpdatePatientOwnerProfile(ownerlist))
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

        /// <summary>
        /// Update Customer's account Settings
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="configKey"></param>
        /// <param name="configValue"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [Route("/Account/Settings/Update")]
        [HttpPost()]
        public IActionResult UpdateConfigurationSettings(String userID, String configKey, String configValue, String updatedBy)
        {
            ResponseCodeObject sResp = new ResponseCodeObject();
            String sNotificationContent = "";

            try
            {
                var sResult = PatientRepository.UpdatePatientConfiguration(userID, configKey, configValue, updatedBy);
                if (sResult)
                {
                    HttpContext.Session.SetString(configKey, configValue);

                    if (configKey == "CustomerSettings_Language")
                    {
                        sNotificationContent = "Language Settings updated to " + configValue + ".";

                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(configValue);
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(configValue);

                        Response.Cookies.Append("Language", configValue);
                    }

                    if (configKey == "CustomerSettings_Themes")
                    {
                        sNotificationContent = "Themes Settings updated to " + configValue + ".";

                        CookieOptions cookies = new CookieOptions();
                        cookies.Expires = DateTime.Now.AddDays(1);

                        Response.Cookies.Append("CustomerTheme", configValue, cookies);
                    }

                    if (configKey == "CustomerSettings_Country")
                    {
                        sNotificationContent = "Country Settings updated to " + configValue + ".";
                    }

                    NotificationModel sNotificationObj = new NotificationModel();
                    sNotificationObj.NotificationGroup = "Message";
                    sNotificationObj.Title = "Account Settings Changes";
                    sNotificationObj.Content = sNotificationContent;
                    sNotificationObj.CreatedDate = DateTime.Now;
                    sNotificationObj.CreatedBy = updatedBy;

                    List<String> sUserIDLst = new List<String>();
                    if (!String.IsNullOrEmpty(userID))
                    {
                        sUserIDLst.Add(userID);
                    }

                    NotificationRepository.InsertNotification(sNotificationObj, sUserIDLst, updatedBy);
                    

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
