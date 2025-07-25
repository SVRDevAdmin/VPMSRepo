using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly VPMS.Lib.Data.DBContext.ServicesDBContext _servicesDBContext = new VPMS.Lib.Data.DBContext.ServicesDBContext();
        public String sMasterDataGroupName = "Gender";

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Doctor Listing
        /// </summary>
        /// <param name="sKeyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IActionResult GetDoctorViewList(String sKeyword, int organizationID, int branchID, int pageSize, int pageIndex)
        {
            int iTotalRecords;
            int isSuperadmin = 0;
            var organizationObj = OrganizationRepository.GetOrganizationByID(organizationID);
            if (organizationObj != null)
            {
                if (organizationObj.Level == 0 || organizationObj.Level == 1)
                {
                    isSuperadmin = 1;
                }
            }

            var sResult = DoctorRepository.GetDoctorViewList(ConfigSettings.GetConfigurationSettings(), isSuperadmin, sKeyword, organizationID, branchID, pageSize, pageIndex, out iTotalRecords);
            if (sResult != null)
            {
                return Json(new { data = sResult, totalRecord = iTotalRecords });
            }

            return null;
        }

        /// <summary>
        /// Add Doctor
        /// </summary>
        /// <param name="sDoctorModel"></param>
        /// <returns></returns>
        public IActionResult AddDoctor(NewDoctorControllerModel sDoctorModel)
        {
            VPMSWeb.Models.ResponseStatusObject sResp = new VPMSWeb.Models.ResponseStatusObject();
            DateTime sNow = DateTime.Now;

            DoctorModel sNewDoctor = new DoctorModel();
            sNewDoctor.Name = sDoctorModel.doctorName;
            sNewDoctor.Gender = sDoctorModel.doctorGender;
            sNewDoctor.System_ID = sDoctorModel.systemID;
            sNewDoctor.LicenseNo = sDoctorModel.licenseNo;
            sNewDoctor.Designation = sDoctorModel.designation;
            sNewDoctor.Specialty = sDoctorModel.specialty;
            sNewDoctor.IsDeleted = 0;
            sNewDoctor.CreatedDateTimestamp = sNow.ToUniversalTime();
            sNewDoctor.CreatedDate = sNow;
            sNewDoctor.CreatedBy = sDoctorModel.updatedBy;
            sNewDoctor.BranchID = sDoctorModel.branchID;

            if (DoctorRepository.AddDoctor(ConfigSettings.GetConfigurationSettings(), sNewDoctor, sDoctorModel.servicesLst))
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="sDoctorModel"></param>
        /// <returns></returns>
        public IActionResult UpdateDoctor(NewDoctorControllerModel sDoctorModel)
        {
            VPMSWeb.Models.ResponseStatusObject sResp = new VPMSWeb.Models.ResponseStatusObject();

            DoctorModel sUpdateDoctor = new DoctorModel();
            sUpdateDoctor.ID = Convert.ToInt32(sDoctorModel.ID);
            sUpdateDoctor.Name = sDoctorModel.doctorName;
            sUpdateDoctor.Gender = sDoctorModel.doctorGender;
            sUpdateDoctor.System_ID = sDoctorModel.systemID;
            sUpdateDoctor.LicenseNo = sDoctorModel.licenseNo;
            sUpdateDoctor.Designation = sDoctorModel.designation;
            sUpdateDoctor.Specialty = sDoctorModel.specialty;
            sUpdateDoctor.IsDeleted = 0;
            sUpdateDoctor.BranchID = sDoctorModel.branchID;
            sUpdateDoctor.UpdatedBy = sDoctorModel.updatedBy;

            if (DoctorRepository.UpdateDoctor(ConfigSettings.GetConfigurationSettings(), sUpdateDoctor, sDoctorModel.servicesLst))
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Delete Doctor 
        /// </summary>
        /// <param name="iDoctorID"></param>
        /// <returns></returns>
        public IActionResult DeleteDoctor(int iDoctorID)
        {
            VPMSWeb.Models.ResponseStatusObject sResp = new VPMSWeb.Models.ResponseStatusObject();

            if (DoctorRepository.DeleteDoctor(ConfigSettings.GetConfigurationSettings(), iDoctorID))
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Go to new Doctor Profile page
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewDoctorProfile()
        {
            ViewData["DoctorProfile"] = null;

            int iTotal = 0;
            int roleIsAdmin = 0;
            int iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
            int iBranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));
            roleIsAdmin = Convert.ToInt32(HttpContext.Session.GetString("IsAdmin"));

            int isSuperadmin = 0;
            var organizationObj = OrganizationRepository.GetOrganizationByID(iOrganizationID);
            if (organizationObj != null)
            {
                if (organizationObj.Level == 0 || organizationObj.Level == 1 || (organizationObj.Level == 2 && roleIsAdmin == 1))
                {
                    isSuperadmin = 1;
                }
                else
                {
                    isSuperadmin = 0;
                }
            }

            ObservableCollection<ServiceList> sServiceList = new ObservableCollection<ServiceList>();
            sServiceList = _servicesDBContext.GetServiceList(1, 1000, isSuperadmin, iBranchID, iOrganizationID, out iTotal);

            ViewData["ServicesList"] = sServiceList;

            return View("ViewDoctorProfile");
        }

        /// <summary>
        /// Edit / View Doctor Profile page 
        /// </summary>
        /// <param name="doctorid"></param>
        /// <param name="ViewType"></param>
        /// <returns></returns>
        [Route("/Doctor/ViewDoctorProfile/{doctorid}/{ViewType}")]
        public IActionResult ViewDoctorProfile(int? doctorid, String ViewType)
        {
            DoctorDetailModel sDoctorObject = new DoctorDetailModel();
            sDoctorObject = DoctorRepository.GetDoctorByID(ConfigSettings.GetConfigurationSettings(), doctorid.Value);

            List<DoctorServicesExtendedModel> sDoctorServiceList = new List<DoctorServicesExtendedModel>();
            sDoctorServiceList = DoctorRepository.GetDoctorServicesList(ConfigSettings.GetConfigurationSettings(), doctorid.Value);

            int iTotal = 0;
            int roleIsAdmin = 0;
            int iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
            int iBranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));
            roleIsAdmin = Convert.ToInt32(HttpContext.Session.GetString("IsAdmin"));

            int isSuperadmin = 0;
            var organizationObj = OrganizationRepository.GetOrganizationByID(iOrganizationID);
            if (organizationObj != null)
            {
                if (organizationObj.Level == 0 || organizationObj.Level == 1 || (organizationObj.Level == 2 && roleIsAdmin == 1))
                {
                    isSuperadmin = 1;
                }
                else
                {
                    isSuperadmin = 0;
                }
            }

            ObservableCollection<ServiceList> sServiceList = new ObservableCollection<ServiceList>();
            sServiceList = _servicesDBContext.GetServiceList(1, 1000, isSuperadmin, iBranchID, iOrganizationID, out iTotal);

            ViewData["DoctorProfile"] = sDoctorObject;
            ViewData["ViewType"] = ViewType;
            ViewData["ServicesList"] = sServiceList;
            ViewData["DoctorServices"] = sDoctorServiceList;

            return View("ViewDoctorProfile", sDoctorObject);
        }

        /// <summary>
        /// Get Gender master List
        /// </summary>
        /// <returns></returns>
        public IActionResult GetGenderListDropdown()
        {
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sMasterDataGroupName);
            return Json(sMasterCodeObj);
        }
    }
}
