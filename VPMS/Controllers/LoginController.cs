using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
    public class LoginController : Controller
    {
        private int maxLoginAttempt = 5;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly RoleDBContext _roleDBContext = new RoleDBContext();
        private readonly BranchDBContext _branchDBContext = new BranchDBContext();
        private readonly UserDBContext _userDBContext = new UserDBContext();
        private readonly LoginSessionDBContext _loginSessionDBContext = new LoginSessionDBContext();

        private const string Charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager
            , IUserStore<IdentityUser> userStore, RoleManager<IdentityRole> roleManager)
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
            var test = HttpContext.Session.GetString("SessionID");

            var anyPersonExist = _userManager.Users.Any();

            if (anyPersonExist)
            {
                return View();
            }
            else
            {
                return RedirectToAction("FirstRegister", "Login");
            }
        }

		public IActionResult LoginOld()
		{
			return View();
		}

		public IActionResult LoginNew()
		{
			return View();
		}

		public async Task<IActionResult> SignIn(LoginModel loginInfo)
        {
            var user = await _userManager.FindByNameAsync(loginInfo.Username);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginInfo.Username,
                            loginInfo.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var userInfo = _userDBContext.Mst_User.FirstOrDefault(x => x.UserID == user.Id);

                    if (userInfo != null) { userInfo.LastLoginDate = DateTime.Now; }

                    _userDBContext.SaveChanges();

                    //var randomAlphanumeric = GenerateRandomAlphanumeric(32);
                    //var sessionCreatedOn = DateTime.Now;
                    //var sessionExpiredOn = DateTime.Now.AddMinutes(5);

                    //_loginSessionDBContext.Txn_LoginSession.Add(new LoginSessionModel() { LoginID = user.UserName, SessionCreatedOn = sessionCreatedOn, SessionExpiredOn = sessionExpiredOn, SessionID = randomAlphanumeric });

                    //_loginSessionDBContext.Txn_LoginSession_Log.Add(new LoginSessionLogModel() { LoginID = user.UserName, SessionCreatedOn = sessionCreatedOn, SessionExpiredOn = sessionExpiredOn, SessionID = randomAlphanumeric , ActionType = "user-login", CreatedDate = DateTime.Now, CreatedBy = user.UserName });

                    //_loginSessionDBContext.SaveChanges();

                    //HttpContext.Session.SetString("SessionID", randomAlphanumeric);

                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    ViewData["alert"] = "The account are locked. Please contact administrator.";

                    return View("Login");
                }
                else if (result.IsNotAllowed)
                {
                    ViewData["alert"] = "The account are have not confirmed yet. Please confirm the account through email and try again.";

                    return View("Login");
                }
                else
                {
                    var attempLeft = maxLoginAttempt - user.AccessFailedCount;

                    //ViewData["alert"] = "Wrong password. " + attempLeft + " attemp[s] left before the account is locked.";
                    ViewData["alert"] = "Wrong password or username.";

                    return View("Login");
                }
            }
            else
            {
                ViewData["alert"] = "Account not found based on the username. Please check and try again.";

                return View("Login");
            }

        }

        public IActionResult FirstRegister()
        {
            var Roles = _roleDBContext.Mst_Roles.ToList();
            var Branches = _branchDBContext.Mst_Branch.ToList();

            return View(new RegisterModel() { Roles = Roles, Branches = Branches });
        }

        public async Task<IActionResult> SignUp(RegisterModel registerInfo)
        {
            var user = await _userManager.FindByNameAsync(registerInfo.Username);

            if (user == null)
            {
                user = Activator.CreateInstance<IdentityUser>();

                await _userStore.SetUserNameAsync(user, registerInfo.Username, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, registerInfo.Email, CancellationToken.None);

                var generatedPassword = RandomPasswordGenerator();

                var result = await _userManager.CreateAsync(user, "Abcd@1234");

                if (result.Succeeded)
                {
                    _userDBContext.Add(new UserModel() { UserID = user.Id, Name = registerInfo.Name, EmailAddress = registerInfo.Email, Status = 1, RoleID = registerInfo.Role, BranchID = registerInfo.Branch, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

                    _userDBContext.SaveChanges();

                    var addclaim = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Fullname", registerInfo.Name));

                    var role = await _roleManager.FindByIdAsync(registerInfo.Role);

                    var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

                    return View("Login");
                }
            }

            return View("FirstRegister");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            //var loginSessionLog = _loginSessionDBContext.Txn_LoginSession_Log.OrderByDescending(x => x.CreatedDate).FirstOrDefault(x => x.LoginID == User.Identity.Name);

            //var newLoginSessionLog = new LoginSessionLogModel() { ActionType = "user-logout", SessionID = loginSessionLog.SessionID, SessionCreatedOn = loginSessionLog.SessionCreatedOn, SessionExpiredOn = loginSessionLog.SessionExpiredOn, LoginID = loginSessionLog.LoginID, CreatedDate = DateTime.Now, CreatedBy = loginSessionLog.LoginID };

            //_loginSessionDBContext.Txn_LoginSession_Log.Add(newLoginSessionLog);

            //_loginSessionDBContext.SaveChanges();

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
                else
                {
                    ViewData["alert"] = "Wrong email. Please check and try again.";
                }
            }
            else
            {
                ViewData["alert"] = "Account not found based on the username. Please check and try again.";
            }

            return View("PasswordRecovery");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public async Task<IActionResult> ChangingPassword(LoginModel changeInfo)
        {

            if (changeInfo != null)
            {
                string userId = User.Identity.Name;

                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user != null)
                {

                    var passwordChanged = await _userManager.ChangePasswordAsync(user, changeInfo.Password, changeInfo.NewPassword);

                    return RedirectToAction("Login", "Login");
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

        public static string GenerateRandomAlphanumeric(int length)
        {
            return string.Create<object?>(length, null,
                static (chars, _) => Random.Shared.GetItems(Charset, chars));
        }

        public async Task RolesAsync()
        {
            var roles = _roleManager.Roles.ToList();
            IdentityRole role = new IdentityRole();


            if (!roles.Where(x => x.Name == "Superadmin").Any())
            {
                role = new IdentityRole("Superadmin");
                var roleCreated = await _roleManager.CreateAsync(role);
                if (roleCreated.Succeeded)
                {
                    _roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 999, Status = 1, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

                    _roleDBContext.SaveChanges();
                }
            }


            if (!roles.Where(x => x.Name == "Doctor").Any())
            {
                role = new IdentityRole("Doctor");
                var roleCreated = await _roleManager.CreateAsync(role);
                if (roleCreated.Succeeded)
                {
                    _roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 1, Status = 1, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

                    _roleDBContext.SaveChanges();
                }
            }


            if (!roles.Where(x => x.Name == "Clinic Admin").Any())
            {
                role = new IdentityRole("Clinic Admin");
                var roleCreated = await _roleManager.CreateAsync(role);
                if (roleCreated.Succeeded)
                {
                    _roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 2, Status = 1, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

                    _roleDBContext.SaveChanges();
                }
            }


            if (!roles.Where(x => x.Name == "User").Any())
            {
                role = new IdentityRole("User");
                var roleCreated = await _roleManager.CreateAsync(role);
                if (roleCreated.Succeeded)
                {
                    _roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 3, Status = 1, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

                    _roleDBContext.SaveChanges();
                }
            }

            var test = _roleDBContext.Mst_Roles;
        }
    }
}
