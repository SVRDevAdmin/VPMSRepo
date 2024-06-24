using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<IdentityUser>)_userStore;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();
            IdentityRole role = new IdentityRole();


            if (!roles.Where(x => x.Name == "Superadmin").Any())
            {
                role = new IdentityRole("Superadmin");
                await _roleManager.CreateAsync(role);
            }

            return View();
        }

        public async Task<IActionResult> SignIn(LoginModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginInfo.Username);

                if (user == null)
                {
                    user = Activator.CreateInstance<IdentityUser>();

                    await _userStore.SetUserNameAsync(user, loginInfo.Username, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, loginInfo.Email, CancellationToken.None);

                    var result = await _userManager.CreateAsync(user, loginInfo.Password);

                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "superadmin");
                    }

                    return View("Index");
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(loginInfo.Username,
                               loginInfo.Password, false, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View("Index");

        }
    }
}
