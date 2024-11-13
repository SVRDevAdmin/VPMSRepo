using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VPMS;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using System.Web;
using System.Data;
using Microsoft.AspNetCore.Http;
using VPMS.Lib.Data;
using VPMS.Lib;
using VPMSWeb.Lib.Settings;
using System.Collections.Generic;
using System.Globalization;

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
		private readonly OrganisationDBContext _organizationDBContext = new OrganisationDBContext(); 

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
            var anyPersonExist = false;

			try
			{
				anyPersonExist = _userManager.Users.Any();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

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
			try
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

						CookieOptions cookies = new CookieOptions
						{
							Expires = DateTime.Now.AddHours(1)
						};						

                        Response.Cookies.Append("user", userInfo.Surname +" "+ userInfo.LastName, cookies);
						Response.Cookies.Append("userid", userInfo.UserID, cookies);

						HttpContext.Session.SetString("Username", userInfo.Surname + " " + userInfo.LastName);
						HttpContext.Session.SetString("LoginID", loginInfo.Username);
						HttpContext.Session.SetString("BranchID", userInfo.BranchID.ToString());
						HttpContext.Session.SetString("RoleID", userInfo.RoleID);
						HttpContext.Session.SetString("UserID", userInfo.UserID);
						HttpContext.Session.SetString("Level1ID", userInfo.Level1ID.ToString());

						var userRole = await _userManager.GetRolesAsync(user);
                        HttpContext.Session.SetString("RoleName", userRole.First());

						int iIsAdmin = 0;
						var sRoleContext = _roleDBContext.Mst_Roles.Where(x => x.RoleID == userInfo.RoleID).FirstOrDefault();
						if (sRoleContext != null)
						{
							iIsAdmin = sRoleContext.IsAdmin;
						}
                        HttpContext.Session.SetString("IsAdmin", iIsAdmin.ToString());

                        int iOrgID = -1;
						var sBranchContext = _branchDBContext.Mst_Branch.First(x => x.ID == userInfo.BranchID);
						if (sBranchContext != null)
						{
							iOrgID = sBranchContext.OrganizationID;

                        }
                        HttpContext.Session.SetString("OrganisationID", iOrgID.ToString());
						//HttpContext.Session.SetString("OrganisationID", _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == userInfo.BranchID).OrganizationID.ToString());

						int iLevel = -1;
						var sOrgContext = _organizationDBContext.Mst_Organisation.Where(x => x.Id == iOrgID).FirstOrDefault();
						if (sOrgContext != null)
						{
							iLevel = sOrgContext.Level;
                        }
                        HttpContext.Session.SetString("Level", iLevel.ToString());

						var organisation = _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == userInfo.BranchID);
						var organisationID = organisation == null ? 0 : organisation.OrganizationID;

						HttpContext.Session.SetString("OrganisationID", organisationID.ToString());

                        var randomAlphanumeric = GenerateRandomAlphanumeric(32);
						var sessionCreatedOn = DateTime.Now;
						var sessionExpiredOn = DateTime.Now.AddMinutes(5);

						var prevLoginSessionLog = _loginSessionDBContext.Txn_LoginSession_Log.OrderByDescending(x => x.CreatedDate).FirstOrDefault(x => x.LoginID == loginInfo.Username);
						if(prevLoginSessionLog != null && prevLoginSessionLog.ActionType == "user-login")
						{
							_loginSessionDBContext.Txn_LoginSession_Log.Add(new LoginSessionLogModel() { SessionID = prevLoginSessionLog.SessionID, SessionCreatedOn = prevLoginSessionLog.SessionCreatedOn, SessionExpiredOn = prevLoginSessionLog.SessionExpiredOn, UserID = prevLoginSessionLog.UserID, LoginID = prevLoginSessionLog.LoginID, CreatedDate = DateTime.Now, CreatedBy = prevLoginSessionLog.LoginID, ActionType = "redundant-logout" });
						}

						_loginSessionDBContext.Txn_LoginSession.Add(new LoginSessionModel() { UserID = user.Id, LoginID = user.UserName, SessionCreatedOn = sessionCreatedOn, SessionExpiredOn = sessionExpiredOn, SessionID = randomAlphanumeric });

						_loginSessionDBContext.Txn_LoginSession_Log.Add(new LoginSessionLogModel() { UserID = user.Id, LoginID = user.UserName, SessionCreatedOn = sessionCreatedOn, SessionExpiredOn = sessionExpiredOn, SessionID = randomAlphanumeric, ActionType = "user-login", CreatedDate = DateTime.Now, CreatedBy = user.UserName });

						_loginSessionDBContext.SaveChanges();

						HttpContext.Session.SetString("SessionID", randomAlphanumeric);

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
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				ViewData["alert"] = "Error occur. Please try again later.";

				return View("Login");
			}
        }

        [Authorize(Roles = "Superadmin")]
        public IActionResult FirstRegister()
        {
			try
			{
				var Roles = _roleDBContext.Mst_Roles.ToList();
				var Branches = _branchDBContext.Mst_Branch.ToList();

				return View(new RegisterModel() { Roles = Roles, Branches = Branches });
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
				return View(new RegisterModel());
			}
        }

        [Authorize(Roles = "Superadmin")]
        public async Task<IActionResult> SignUp(RegisterModel registerInfo)
        {
			try
			{
				var user = await _userManager.FindByNameAsync(registerInfo.Username);

				if (user == null)
				{
					user = Activator.CreateInstance<IdentityUser>();

					await _userStore.SetUserNameAsync(user, registerInfo.Username, CancellationToken.None);
					await _emailStore.SetEmailAsync(user, registerInfo.Email, CancellationToken.None);

					var generatedPassword = RandomPasswordGenerator();

					//var result = await _userManager.CreateAsync(user, generatedPassword);
					var result = await _userManager.CreateAsync(user, "Abcd@1234");

                    if (result.Succeeded)
					{
						_userDBContext.Add(new UserModel() { UserID = user.Id, Surname = registerInfo.Surname, LastName = registerInfo.LastName, StaffID = "ABC1234567", Gender = "M", EmailAddress = registerInfo.Email, Status = 1, RoleID = registerInfo.Role, BranchID = registerInfo.Branch, CreatedDate = DateTime.Now, CreatedBy = "System" });

						_userDBContext.SaveChanges();

						//var addclaim = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Fullname", registerInfo.Name));

						var role = await _roleManager.FindByIdAsync(registerInfo.Role);

						var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

						return View("Login");
					}
				}
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return View("FirstRegister");
		}

        public async Task<IActionResult> Logout(bool autoLogout = false)
        {
			try
			{
				var loginSessionLog = _loginSessionDBContext.Txn_LoginSession_Log.OrderByDescending(x => x.CreatedDate).FirstOrDefault(x => x.LoginID == HttpContext.Session.GetString("LoginID"));

				var newLoginSessionLog = new LoginSessionLogModel() { SessionID = loginSessionLog.SessionID, SessionCreatedOn = loginSessionLog.SessionCreatedOn, SessionExpiredOn = loginSessionLog.SessionExpiredOn, UserID = loginSessionLog.UserID, LoginID = loginSessionLog.LoginID, CreatedDate = DateTime.Now, CreatedBy = loginSessionLog.LoginID };

				if (autoLogout) { newLoginSessionLog.ActionType = "idle-logout"; }
				else { newLoginSessionLog.ActionType = "user-logout"; }

				_loginSessionDBContext.Txn_LoginSession_Log.Add(newLoginSessionLog);

				_loginSessionDBContext.SaveChanges();

				await _signInManager.SignOutAsync();

			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

            return RedirectToAction("Login", "Login");
        }

        public IActionResult AccessDenied()
        {
			Program.AccessDenied = "true";

            return RedirectToAction("Index", "Appointment");
        }

		public bool ResetAccessDenied()
		{
            Program.AccessDenied = "false";

			return true;
        }

        public IActionResult PasswordRecovery()
        {
            return View();
        }

        public async Task<IActionResult> RecoverPasswordAsync(RegisterModel recoverInfo)
        {
			try
			{
				var user = await _userManager.FindByNameAsync(recoverInfo.Username);

				if (user != null)
				{
					var userEmail = await _userManager.GetEmailAsync(user);

					if (userEmail != null && _userManager.NormalizeEmail(userEmail) == _userManager.NormalizeEmail(recoverInfo.Email))
					{
						var newPassword = RandomPasswordGenerator();

						var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

						var setNewPassword = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

						if (setNewPassword.Succeeded)
                        {
                            List<String> sRecipientList = new List<String>();
                            sRecipientList.Add(recoverInfo.Email);
                            var staffInfo = _userDBContext.Mst_User.FirstOrDefault(x => x.UserID == user.Id);
							var language = ConfigurationRepository.GetUserConfigurationSettings(ConfigSettings.GetConfigurationSettings(), user.Id).FirstOrDefault(x => x.ConfigurationKey == "UserSettings_Language").ConfigurationValue;

                            var emailTemplate = TemplateRepository.GetTemplateByCodeLang(ConfigSettings.GetConfigurationSettings(), "VPMS_EN002", language);
                            emailTemplate.TemplateContent = emailTemplate.TemplateContent.Replace("###<staff_fullname>###", staffInfo.Surname + " " + staffInfo.LastName)
                                                                                         .Replace("###<password>###", newPassword);


                            SendNotificationEmail(sRecipientList, emailTemplate);

                            return RedirectToAction("Login", "Login");
                        }

                        ViewData["alert"] = "Fail to reset password.";
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
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
				ViewData["alert"] = "Error occur. Please try again later.";
			}

            return View("PasswordRecovery");
        }

        public IActionResult ChangePassword()
        {
            ViewBag.PreviousPage = Program.CurrentPage;

            return View();
        }

        public async Task<IActionResult> ChangingPassword(LoginModel changeInfo)
        {
			try
			{
				if (!ModelState["NewPassword"].Errors.Any())
				{
					if (changeInfo != null)
					{
						string userId = User.Identity.Name;

						var user = await _userManager.FindByNameAsync(User.Identity.Name);

						if (user != null)
						{

							var passwordChanged = await _userManager.ChangePasswordAsync(user, changeInfo.Password, changeInfo.NewPassword);

							if (passwordChanged.Succeeded)
							{
								await _signInManager.SignOutAsync();

								return RedirectToAction("Login", "Login");
							}
						}

					}
				}
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

            ViewBag.PreviousPage = Program.CurrentPage;

            return View("ChangePassword");
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
			try
			{
				var roles = _roleManager.Roles.ToList();
				IdentityRole role = new IdentityRole();


				if (!roles.Where(x => x.Name == "Superadmin").Any())
				{
					role = new IdentityRole("Superadmin");
					var roleCreated = await _roleManager.CreateAsync(role);
					if (roleCreated.Succeeded)
					{
						_roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 999, Status = 1, IsAdmin = 1, IsDoctor = 0, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

						_roleDBContext.SaveChanges();
					}
				}


				if (!roles.Where(x => x.Name == "Doctor").Any())
				{
					role = new IdentityRole("Doctor");
					var roleCreated = await _roleManager.CreateAsync(role);
					if (roleCreated.Succeeded)
					{
						_roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 1, Status = 1, IsAdmin = 0, IsDoctor = 1, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

						_roleDBContext.SaveChanges();
					}
				}


				if (!roles.Where(x => x.Name == "Clinic Admin").Any())
				{
					role = new IdentityRole("Clinic Admin");
					var roleCreated = await _roleManager.CreateAsync(role);
					if (roleCreated.Succeeded)
					{
						_roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 2, Status = 1, IsAdmin = 1, IsDoctor = 0, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

						_roleDBContext.SaveChanges();
					}
				}


				if (!roles.Where(x => x.Name == "User").Any())
				{
					role = new IdentityRole("User");
					var roleCreated = await _roleManager.CreateAsync(role);
					if (roleCreated.Succeeded)
					{
						_roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 3, Status = 1, IsAdmin = 0, IsDoctor = 0, CreatedDate = DateTime.Now, CreatedBy = "Admin" });

						_roleDBContext.SaveChanges();
					}
				}


				if (!roles.Where(x => x.Name == "Superuser").Any())
				{
					role = new IdentityRole("Superuser");
					var roleCreated = await _roleManager.CreateAsync(role);
					if (roleCreated.Succeeded)
					{
						_roleDBContext.Add(new RoleModel { RoleID = role.Id, RoleName = role.Name, RoleType = 998, Status = 1, IsAdmin = 1, IsDoctor = 0, CreatedDate = DateTime.Now, CreatedBy = "System" });

						_roleDBContext.SaveChanges();
					}
				}

				var test = _roleDBContext.Mst_Roles;
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}			
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
			try
			{
				var user = await _userManager.FindByIdAsync(id);

				await _userManager.DeleteAsync(user);

				var userInfo = _userDBContext.Mst_User.FirstOrDefault(x => x.UserID == id);

				_userDBContext.Mst_User.Remove(userInfo);

				_userDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

            return View("Login");
        }

		public void SendNotificationEmail(List<String> sRecipientList, TemplateModel emailTemplate)
		{
			var sEmailConfig = ConfigSettings.GetConfigurationSettings();
			String? sHost = sEmailConfig.GetSection("SMTP:Host").Value;
			int? sPortNo = Convert.ToInt32(sEmailConfig.GetSection("SMTP:Port").Value);
			String? sUsername = sEmailConfig.GetSection("SMTP:Username").Value;
			String? sPassword = sEmailConfig.GetSection("SMTP:Password").Value;
			String? sSender = sEmailConfig.GetSection("SMTP:Sender").Value;

			try
			{
				VPMS.Lib.EmailObject sEmailObj = new VPMS.Lib.EmailObject();
				sEmailObj.SenderEmail = sSender;
				sEmailObj.RecipientEmail = sRecipientList;
				sEmailObj.Subject = (emailTemplate != null) ? emailTemplate.TemplateTitle : "";
				sEmailObj.Body = (emailTemplate != null) ? emailTemplate.TemplateContent : "";
				sEmailObj.SMTPHost = sHost;
				sEmailObj.PortNo = sPortNo.Value;
				sEmailObj.HostUsername = sUsername;
				sEmailObj.HostPassword = sPassword;
				sEmailObj.EnableSsl = true;
				sEmailObj.UseDefaultCredentials = false;
				sEmailObj.IsHtml = true;

				String sErrorMessage = "";
				EmailHelpers.SendEmail(sEmailObj, out sErrorMessage);
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}
		}
	}
}
