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

        /// <summary>
        /// Get Doctor List By Branch ID
        /// </summary>
        /// <param name="branchid"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Branch List By Organization ID
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        [Route("/Masterdata/BranchList/{organizationID}")]
        [HttpGet()]
        public IActionResult GetBranchList(int organizationID)
        {
            try
            {
                var sBranchObj = MastercodeRepository.GetBranchList(organizationID);
                if (sBranchObj != null)
                {
                    return Json(new { data = sBranchObj });
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
        /// Get Services List By Branch ID
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns></returns>
        [Route("/Masterdata/ServiceList/{branchID}")]
        [HttpGet()]
        public IActionResult GetServicesList(int branchID)
        {
            try
            {
                var sServiceObj = ServicesRepository.GetServicesListByBranchID(branchID);
                if (sServiceObj != null)
                {
                    return Json(new { data = sServiceObj });
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
        /// Get Doctor's Service List
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns></returns>
        [Route("/Masterdata/ServiceListByDoctor/{doctorid}")]
        [HttpGet()]
        public IActionResult ServiceListByDoctor(int doctorid)
        {
            try
            {
                var sServiceObj = ServicesRepository.GetServicesListByDoctorID(doctorid);
                if (sServiceObj != null)
                {
                    return Json(new { data = sServiceObj });
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
        /// Get Doctor List By Services ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        [Route("/Masterdata/DoctorListByService/{serviceID}")]
        [HttpGet()]
        public IActionResult GetDoctorListByServices(int serviceID)
        {
            try
            {
                var sDoctorObj = MastercodeRepository.GetDoctorListByServices(serviceID);
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

        /// <summary>
        /// Get Doctor List by Branch ID
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns></returns>
        [Route("/Masterdata/DoctorListByBranch/{branchID}")]
        [HttpGet()]
        public IActionResult GetDoctorListByBranchID(int branchID)
        {
            try
            {
                var sDoctorObj = MastercodeRepository.GetDoctorListByBranchID(branchID);
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

        /// <summary>
        /// Get Profile Avatar List By Grouping
        /// </summary>
        /// <param name="group"></param>
        /// <param name="subGroup"></param>
        /// <returns></returns>
        [Route("/Masterdata/GetProfileAvatars")]
        [HttpGet()]
        public IActionResult GetProfileAvatarList(String group, String subGroup)
        {
            try
            {
                var sAvatarList = AvatarRepository.GetProfileAvatarList(group, subGroup);
                if (sAvatarList != null)
                {
                    return Json(new { data = sAvatarList });
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
