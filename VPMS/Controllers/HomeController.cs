using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;
using VPMS;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMS.Models;
using VPMSWeb.Interface;
using VPMSWeb.Lib;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
	[Authorize]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        String sLangCodeGroup = "LanguageSelection";

		public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			String sessionUserID = "";
			String sessionBranchID = "";

            if (HttpContext.Session.GetString("UserID") != null)
			{
                sessionUserID = HttpContext.Session.GetString("UserID");
            }
			if (HttpContext.Session.GetString("BranchID") != null)
			{
                sessionBranchID = HttpContext.Session.GetString("BranchID");
            }

			var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sLangCodeGroup);
			ViewData["LanguageCodeList"] = sMasterCodeObj;
			Program.LanguageCodeList = sMasterCodeObj;

            var sCountryList = CountryRepository.GetCountryList(ConfigSettings.GetConfigurationSettings());
			ViewData["CountryList"] = sCountryList;
            Program.CountryList = sCountryList;

            var sUserConfigurationSettings = ConfigurationRepository.GetUserConfigurationSettings(ConfigSettings.GetConfigurationSettings(), sessionUserID);
			if (sUserConfigurationSettings != null && sUserConfigurationSettings.Count != 0)
			{
				ViewData["LanguageSelected"] = sUserConfigurationSettings.Where(x => x.ConfigurationKey == "UserSettings_Language").FirstOrDefault();
				ViewData["CountrySelected"] = sUserConfigurationSettings.Where(x => x.ConfigurationKey == "UserSettings_Country").FirstOrDefault();

				Program.LanguageSelected = ViewData["LanguageSelected"] as ConfigurationModel;
				Program.CountrySelected = ViewData["CountrySelected"] as ConfigurationModel;
			}



			try
			{

				if (Program.LanguageSelected != null)
				{
					var sLangSelected = ViewData["LanguageSelected"] as ConfigurationModel;
					ViewData["LanguageFullNameSelected"] = sMasterCodeObj.Where(x => x.CodeID == sLangSelected.ConfigurationValue).FirstOrDefault();
					Program.LanguageFullNameSelected = ViewData["LanguageFullNameSelected"] as MastercodeModel;

					Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Program.LanguageSelected.ConfigurationValue);
					Thread.CurrentThread.CurrentUICulture = new CultureInfo(Program.LanguageSelected.ConfigurationValue);
					Response.Cookies.Append("Language", Program.LanguageSelected.ConfigurationValue);
				}
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
				ViewData["LanguageFullNameSelected"] = new MastercodeModel();
			}	

			Program.CurrentPage = "/Home/Index";
            Program.CurrentPage = "/Dashboard/Dashboard";

            //return View();

            //return RedirectToAction("Index", "Appointment");
            return RedirectToAction("Dashboard", "Dashboard");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		//public IActionResult ChangeLanguage(String lang, String UserID)
		//{
		//	if (!String.IsNullOrEmpty(lang))
		//	{
		//		ConfigurationModel sModel = new ConfigurationModel();
		//		sModel.UserID = UserID;
		//		sModel.ConfigurationKey = "UserSettings_Language";
		//		sModel.ConfigurationValue = lang;
		//		sModel.CreatedDate = DateTime.Now;
		//		sModel.CreatedBy = UserID;

		//		if (ConfigurationRepository.UpdateUserConfigurationSettingsByKey(ConfigSettings.GetConfigurationSettings(), sModel))
		//		{
		//			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
		//			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

		//                  Program.LanguageFullNameSelected = Program.LanguageCodeList.Where(x => x.CodeID == lang).FirstOrDefault();
		//			Program.LanguageSelected = sModel;

		//		}
		//		else
		//		{
		//			//todo:
		//		}
		//	}
		//	else
		//	{
		//		Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
		//		Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
		//		lang = "en";
		//	}

		//	Response.Cookies.Append("Language", lang);
		//	return Redirect(Request.GetTypedHeaders().Referer.ToString());
		//}

		public bool ChangeLanguage(String lang, String UserID)
		{
			try
			{
				if (!String.IsNullOrEmpty(lang))
				{
					ConfigurationModel sModel = new ConfigurationModel();
					sModel.UserID = UserID;
					sModel.ConfigurationKey = "UserSettings_Language";
					sModel.ConfigurationValue = lang;
					sModel.CreatedDate = DateTime.Now;
					sModel.CreatedBy = UserID;


                    if (ConfigurationRepository.UpdateUserConfigurationSettingsByKey(ConfigSettings.GetConfigurationSettings(), sModel))
					{
						Program.LanguageFullNameSelected = Program.LanguageCodeList.Where(x => x.CodeID == lang).FirstOrDefault();
						Program.LanguageSelected = sModel;

                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

                    }
					else
					{
						//todo:
					}
				}
				else
				{
					Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
					lang = "en";
				}
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
				lang = "en";
			}

			Response.Cookies.Append("Language", lang);
			return true;
		}

	}
}
