using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Models;
using Microsoft.Extensions.Localization;

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

        //[HttpPost()]
        //public IActionResult ValidateLogin(String loginID, String loginPassword)
        //{
        //    ResponseCodeObject sResp = new ResponseCodeObject();

        //    return Json(sResp);
        //}
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
        //// GET: LoginController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: LoginController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: LoginController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LoginController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LoginController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
