using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;


namespace VPMSWeb.Controllers
{
    public class MastercodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Mastercode/GetStatesListByCountry")]
        [HttpGet()]
        public IActionResult GetStatesListByCountry(int countryID)
        {
            try
            {
                var sStateList = CountryRepository.GetStatesListByCountry(ConfigSettings.GetConfigurationSettings(), countryID);
                if (sStateList != null)
                {
                    return Json(new { data = sStateList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Mastercode/GetCityListByState")]
        [HttpGet()]
        public IActionResult GetCityListByState(int stateID)
        {
            try
            {
                var sCityList = CountryRepository.GetCityListByState(ConfigSettings.GetConfigurationSettings(), stateID);
                if (sCityList != null)
                {
                    return Json(new { data = sCityList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
