using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Models;
using Microsoft.Extensions.Localization;
using ZstdSharp.Unsafe;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IStringLocalizer _localizer;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager
            , IUserStore<IdentityUser> userStore, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<IdentityUser>)_userStore;
            _roleManager = roleManager;
        }

        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [Route("/Login/Logout")]
        [HttpPost()]
        public async Task<IActionResult> Logout(String sessionID, String actionType)
        {
            LoginResponseObject sResp = new LoginResponseObject();

            try
            {
                if (CustomerLoginRepository.InsertSessionLog(sessionID, actionType))
                {
                    CustomerLoginRepository.DeleteSession(sessionID);

                    await _signInManager.SignOutAsync();

                    HttpContext.Session.Remove("UserID");
                    HttpContext.Session.Remove("UserName");
                    HttpContext.Session.Remove("CustomerSettings_Language");
                    HttpContext.Session.Remove("CustomerSettings_Country");
                    HttpContext.Session.Remove("CustomerSettings_Themes");
                    HttpContext.Session.Remove("LoginSession");

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
        /// Validate Login
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> ValidateLogin(String sUserName, String sPassword)
        {
            LoginResponseObject sResp = new LoginResponseObject();

            try
            {
                var user = await _userManager.FindByNameAsync(sUserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(sUserName, sPassword, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("UserID", user.Id);
                        HttpContext.Session.SetString("UserName", user.UserName);

                        long? iPatientID = 0;
                        long? iPatientOwnerID = 0;
                        String? PatientGender = "";
                        var sPatientOwner = PatientRepository.GetPatientOwnerByIdentityUserID(user.Id);
                        if (sPatientOwner != null)
                        {
                            iPatientOwnerID = sPatientOwner.ID;
                            iPatientID = sPatientOwner.PatientID;
                            PatientGender = sPatientOwner.Gender;
                        }

                        String sCountrySettings = "";
                        String sThemesSettings = "";
                        String sLanguageSettings = "";
                        var sAccountConfiguration = PatientRepository.GetPatientConfiguration(user.Id);
                        if (sAccountConfiguration != null)
                        {
                            // ---- Language Settings ----- //
                            var sLanguageConfig = sAccountConfiguration.Where(x => x.ConfigurationKey == "CustomerSettings_Language")
                                                                       .FirstOrDefault();
                            if (sLanguageConfig != null)
                            {
                                sLanguageSettings = (sLanguageConfig.ConfigurationValue != null) ? sLanguageConfig.ConfigurationValue : "";
                            }

                            // ----- Country Settings ------ //
                            var sCountryConfig = sAccountConfiguration.Where(x => x.ConfigurationKey == "CustomerSettings_Country")
                                                                      .FirstOrDefault();
                            if (sCountryConfig != null)
                            {
                                sCountrySettings = (sCountryConfig.ConfigurationValue != null) ? sCountryConfig.ConfigurationValue : "";
                            }

                            // ------ Themes Settings ------ //
                            var sThemesConfig = sAccountConfiguration.Where(x => x.ConfigurationKey == "CustomerSettings_Themes")
                                                                     .FirstOrDefault();
                            if (sThemesConfig != null)
                            {
                                sThemesSettings = (sThemesConfig.ConfigurationValue != null) ? sThemesConfig.ConfigurationValue : "";
                            }
                        }

                        HttpContext.Session.SetString("CustomerSettings_Language", sLanguageSettings);
                        HttpContext.Session.SetString("CustomerSettings_Country", sCountrySettings);
                        HttpContext.Session.SetString("CustomerSettings_Themes", sThemesSettings);

                        HttpContext.Session.SetString("PatientID", iPatientID.ToString());
                        HttpContext.Session.SetString("Gender", PatientGender);
                        HttpContext.Session.SetString("PatientOwnerID", iPatientOwnerID.ToString());

                        // ---------- Create Login Session ------------ //
                        DateTime dtSession = DateTime.Now;

                        CustomerLoginSession sNewSession = new CustomerLoginSession();
                        sNewSession.SessionID = VPMSCustomer.Lib.Helper.GenerateRandomKeyString(32);
                        sNewSession.SessionCreatedOn = dtSession;
                        sNewSession.SessionExpiredOn = dtSession.AddMinutes(60);
                        sNewSession.UserID = user.Id;
                        sNewSession.LoginID = user.UserName;

                        CustomerLoginRepository.InsertSession(sNewSession, "Login");
                        HttpContext.Session.SetString("LoginSession", sNewSession.SessionID);


                        Response.Cookies.Append("Language", sLanguageSettings);
                        Response.Cookies.Append("CustomerTheme", sThemesSettings);

                        sResp.StatusCode = (int)StatusCodes.Status200OK;
                    }
                    else
                    {
                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                        sResp.StatusCodeMessage = "Invalid Login ID or Password.";
                    }
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.StatusCodeMessage = "Invalid Login ID.";
                }
            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                sResp.StatusCodeMessage = "Exception. Please contact administrator.";
            }

            return Json(sResp);
        }

        /// <summary>
        /// Reset user's Password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [Route("/Login/ResetPassword/")]
        [HttpPost()]
        public async Task<IActionResult> ResetPasswordAsync(string userID, string newPassword)
        {
            IPasswordHasher<IdentityUser> _passwordHasher = new PasswordHasher<IdentityUser>();
            LoginResponseObject sResp = new LoginResponseObject();

            try
            {
                var user = await _userManager.FindByIdAsync(userID);

                if (user != null)
                {
                    var sResult = UserRepository.ResetUserPassword(userID, newPassword);
                    if (sResult)
                    {
                        sResp.StatusCode = (int)StatusCodes.Status200OK;
                    }
                    else
                    {
                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    }
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
