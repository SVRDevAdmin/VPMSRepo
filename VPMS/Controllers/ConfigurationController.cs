using Microsoft.AspNetCore.Mvc;
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
            ConfigurationModel sModel = new ConfigurationModel();
            sModel.UserID = UserID;
            sModel.ConfigurationKey = ConfigKey;
            sModel.ConfigurationValue = ConfigVal;
            sModel.CreatedDate = DateTime.Now;
            sModel.CreatedBy = UserID;

            if (ConfigurationRepository.UpdateUserConfigurationSettingsByKey(ConfigSettings.GetConfigurationSettings(), sModel))
            {
				Program.LanguageFullNameSelected = Program.LanguageCodeList.Where(x => x.CodeID == ConfigVal).FirstOrDefault();

				return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
