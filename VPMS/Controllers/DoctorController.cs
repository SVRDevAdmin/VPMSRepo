using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
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

            if (DoctorRepository.AddDoctor(ConfigSettings.GetConfigurationSettings(), sNewDoctor))
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

            if (DoctorRepository.UpdateDoctor(ConfigSettings.GetConfigurationSettings(), sUpdateDoctor))
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
            ViewData["DoctorProfile"] = sDoctorObject;
            ViewData["ViewType"] = ViewType;

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
