using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
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

        [Route("/Account/Profile/{aspNetUserID}")]
        [HttpGet]
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

        //UpdatePatientOwnerProfile
        [Route("/Account/Profile/UpdateProfile")]
        [HttpPost]
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
    }


}
