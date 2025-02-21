using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Models;
using Microsoft.Extensions.Localization;
using ZstdSharp.Unsafe;
using VPMSCustomer.Lib.Data;

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
