using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using VPMS;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateUserConfiguration(String ConfigKey, String ConfigVal, String UserID)
        {
            try
            {
                ConfigurationModel sModel = new ConfigurationModel();
                sModel.UserID = UserID;
                sModel.ConfigurationKey = ConfigKey;
                sModel.ConfigurationValue = ConfigVal;
                sModel.CreatedDate = DateTime.Now;
                sModel.CreatedBy = UserID;

                if (ConfigurationRepository.UpdateUserConfigurationSettingsByKey(ConfigSettings.GetConfigurationSettings(), sModel))
                {
                    if (ConfigKey == "UserSettings_Country")
                    {
                        Program.CountrySelected = sModel;
                    }
                    else if (ConfigKey == "UserSettings_Language")
                    {
                        Program.LanguageFullNameSelected = Program.LanguageCodeList.Where(x => x.CodeID == ConfigVal).FirstOrDefault();
                        Program.LanguageSelected = sModel;

                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ConfigVal);
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(ConfigVal);

                        Response.Cookies.Append("Language", ConfigVal);
                    }

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                Program.logger.Error("Controller Error >> ", ex);

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                Response.Cookies.Append("Language", "en");

                return BadRequest();
            }
        }
    }
}
