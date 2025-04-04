using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data.Models;
using VPMSCustomer.Lib.Data;

namespace VPMSCustomer.Controllers
{
    public class MastercodeDataController : Controller
    {
        /// <summary>
        /// Get Gender Master List
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get Country Master List
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get State List by Country ID
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        [Route("/Masterdata/StatesList/{countryid}")]
        [HttpGet()]
        public IActionResult GetStatesList(int countryid)
        {
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

        /// <summary>
        /// Get City List by State ID
        /// </summary>
        /// <param name="stateid"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Language Master list
        /// </summary>
        /// <returns></returns>
        [Route("/Masterdata/LanguageList")]
        [HttpGet()]
        public IActionResult GetLanguageList()
        {
            List<MasterCodeDataModel> sLanguageObj = new List<MasterCodeDataModel>();
            String sLanguageGroup = "LanguageSelection";

            try
            {
                sLanguageObj = MastercodeRepository.GetMastercodeDataByGroup(sLanguageGroup);
                if (sLanguageObj != null)
                {
                    return Json(new { data = sLanguageObj });
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

        [Route("/Masterdata/DoctorList/{branchid}")]
        [HttpGet()]
        public IActionResult GetDoctorList(int branchid)
        {
            try
            {
                var sDoctorObj = DoctorRepository.GetDoctorList(branchid);
                if (sDoctorObj != null)
                {
                    return Json(new { data = sDoctorObj });
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
