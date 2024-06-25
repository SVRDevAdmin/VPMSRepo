using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> SignIn(LoginModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginInfo.Username);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginInfo.Username,
                               loginInfo.Password, false, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View("Login");

        }

        public IActionResult FirstRegister()
        {
            var anyPersonExist = _userManager.Users.Any();

            if (anyPersonExist)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> SignUp(RegisterModel registerInfo)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(registerInfo.Username);

                if (user == null)
                {
                    user = Activator.CreateInstance<IdentityUser>();

                    await _userStore.SetUserNameAsync(user, registerInfo.Username, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, registerInfo.Email, CancellationToken.None);

                    var result = await _userManager.CreateAsync(user, registerInfo.Password);

                    if (result.Succeeded)
                    {
                        var roles = _roleManager.Roles.ToList();
                        IdentityRole role = new IdentityRole();


                        if (!roles.Where(x => x.Name == "Superadmin").Any())
                        {
                            role = new IdentityRole("Superadmin");
                            await _roleManager.CreateAsync(role);
                        }

                        var roleResult = await _userManager.AddToRoleAsync(user, "Superadmin");

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return View("Login");
                    }
                }
            }

            return View("Register");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult PasswordRecovery()
        {
            return View();
        }

        public async Task<IActionResult> RecoverPasswordAsync(RegisterModel recoverInfo)
        {
            var user = await _userManager.FindByNameAsync(recoverInfo.Username);

            if (user != null)
            {
                var userEmail = await _userManager.GetEmailAsync(user);

                if(userEmail != null && _userManager.NormalizeEmail(userEmail) == _userManager.NormalizeEmail(recoverInfo.Email))
                {
                    var newPassword = RandomPasswordGenerator();

                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var setNewPassword = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                    return RedirectToAction("Login", "Login");
                }
            }

            return View("PasswordRecovery");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public async Task<IActionResult> ChangingPassword(LoginModel changeInfo)
        {
            if (ModelState.IsValid)
            {

                if (changeInfo != null)
                {
                    var user = await _userManager.FindByNameAsync(changeInfo.Username);

                    if (user != null)
                    {
                        var passwordChanged = await _userManager.ChangePasswordAsync(user, changeInfo.Password, changeInfo.NewPassword);

                        return RedirectToAction("Login", "Login");
                    }

                }

            }

            return View();

        }

        private string RandomPasswordGenerator()
        {
            Random res = new Random();

            // String that contain alphabets, numbers and special character
            String lowerCase = "abcdefghijklmnopqrstuvwxyz";
            String upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            String number = "0123456789";
            String specialChar = "!@#$%^&*()_+-={}|[];,./:<>?";

            // Initializing the empty string 
            String randomstring = "";

            randomstring += lowerCase[res.Next(lowerCase.Length)];
            randomstring += upperCase[res.Next(upperCase.Length)];
            randomstring += number[res.Next(number.Length)];
            randomstring += specialChar[res.Next(specialChar.Length)];
            randomstring += lowerCase[res.Next(lowerCase.Length)];
            randomstring += upperCase[res.Next(upperCase.Length)];
            randomstring += number[res.Next(number.Length)];
            randomstring += specialChar[res.Next(specialChar.Length)];

            char[] chars = randomstring.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                int randomIndex = res.Next(0, chars.Length);
                char temp = chars[randomIndex];
                chars[randomIndex] = chars[i];
                chars[i] = temp;
            }

            randomstring = string.Join("", chars);

            return randomstring;
        }
    }
}
