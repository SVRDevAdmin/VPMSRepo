using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using VPMS.Models;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Localization;
using VPMS.Lib.Data;
using VPMSWeb.Lib.Settings;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
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
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sLangCodeGroup);
            ViewData["LanguageCodeList"] = sMasterCodeObj;

            var sCountryList = CountryRepository.GetCountryList(ConfigSettings.GetConfigurationSettings());
            ViewData["CountryList"] = sCountryList;

            var sUserConfigurationSettings = ConfigurationRepository.GetUserConfigurationSettings(ConfigSettings.GetConfigurationSettings(), "User A");
            if (sUserConfigurationSettings != null)
            {
                ViewData["LanguageSelected"] = sUserConfigurationSettings.Where(x => x.ConfigurationKey == "UserSettings_Language").FirstOrDefault();
                ViewData["CountrySelected"] = sUserConfigurationSettings.Where(x => x.ConfigurationKey == "UserSettings_Country").FirstOrDefault();
            }

            if (ViewData["LanguageSelected"] != null)
            {
                var sLangSelected = ViewData["LanguageSelected"] as ConfigurationModel;
                ViewData["LanguageFullNameSelected"] = sMasterCodeObj.Where(x => x.CodeID == sLangSelected.ConfigurationValue).FirstOrDefault();
            }
            
            

            return View();
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

        public IActionResult ChangeLanguage(String lang, String UserID)
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

            Response.Cookies.Append("Language", lang);
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }
    }
}
