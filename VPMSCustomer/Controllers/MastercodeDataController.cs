using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data.Models;
using VPMSCustomer.Lib.Data;

namespace VPMSCustomer.Controllers
{
    public class MastercodeDataController : Controller
    {
        [Route("/Masterdata/Gender")]
        [HttpGet()]
        public IActionResult GetGenderMasterdata()
        {
            List<MasterCodeDataModel> sMastercodeObj = new List<MasterCodeDataModel>();
            String sGenderGroup = "Gender";

            try
            {
                sMastercodeObj = MastercodeRepository.GetMastercodeDataByGroup(sGenderGroup);
                if (sMastercodeObj != null)
                {
                    return Json(new { data = sMastercodeObj });
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

        [Route("/Masterdata/CountryList")]
        [HttpGet()]
        public IActionResult GetCountryList()
        {
            List<CountryModel> sCountryObjList = new List<CountryModel>();

            try
            {
                sCountryObjList = MastercodeRepository.GetCountryList();
                if (sCountryObjList != null)
                {
                    return Json(new { data = sCountryObjList });
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

        [Route("/Masterdata/StatesList/{countryid}")]
        [HttpGet()]
        public IActionResult GetStatesList(int countryid)
        {
            //GetStatesList
            List<StateModel> sStatesList = new List<StateModel>();

            try
            {
                sStatesList = MastercodeRepository.GetStatesList(countryid);
                if (sStatesList != null)
                {
                    return Json(new { data = sStatesList });
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

        [Route("/Masterdata/CityList/{stateid}")]
        [HttpGet()]
        public IActionResult GetCityList(int stateid)
        {
            List<CityModel> sCityList = new List<CityModel>();

            try
            {
                sCityList = MastercodeRepository.GetCityList(stateid);
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
