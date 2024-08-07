using Microsoft.AspNetCore.Mvc;
using System.Data;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class ThemeController : Controller
    {
        public String sThemeConfigurationKey = "UserSettings_Themes";
        public String sDefaultTheme = "light";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetThemes(String data, String userid)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);

            ConfigurationModel sModel = new ConfigurationModel();
            sModel.UserID = userid;
            sModel.ConfigurationKey = sThemeConfigurationKey;
            sModel.ConfigurationValue = data;
            sModel.CreatedDate = DateTime.Now;
            sModel.CreatedBy = userid;

            if (ConfigurationRepository.UpdateUserConfigurationSettingsByKey(ConfigSettings.GetConfigurationSettings(), sModel))
            {
                Response.Cookies.Append("theme", data, cookies);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult GetThemes(String userid)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);

            try
            {
                var sResult = ConfigurationRepository.GetUserConfigurationSettingsByKey(ConfigSettings.GetConfigurationSettings(), userid, sThemeConfigurationKey);
                if (sResult != null)
                {
                    Response.Cookies.Append("theme", sResult.ConfigurationValue, cookies);
                    return Ok();
                }
                else
                {
                    Response.Cookies.Append("theme", sDefaultTheme, cookies);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        public void abc()
        {

        }
    }
}
