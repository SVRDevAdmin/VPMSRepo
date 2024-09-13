using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class DoctorController : Controller
    {
        public String sMasterDataGroupName = "Gender";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDoctorViewList()
        {
            var sResult = DoctorRepository.GetDoctorViewList(ConfigSettings.GetConfigurationSettings(), "");
            if (sResult != null)
            {
                return Json(sResult);
            }

            return null;
        }

        public IActionResult AddDoctor(NewDoctorControllerModel sDoctorModel)
        {
            ResponseResultCode sResp = new ResponseResultCode();

            DoctorModel sNewDoctor = new DoctorModel();
            sNewDoctor.Name = sDoctorModel.doctorName;
            sNewDoctor.Gender = sDoctorModel.doctorGender;
            sNewDoctor.System_ID = sDoctorModel.systemID;
            sNewDoctor.LicenseNo = sDoctorModel.licenseNo;
            sNewDoctor.Designation = sDoctorModel.designation;
            sNewDoctor.Specialty = sDoctorModel.specialty;
            sNewDoctor.IsDeleted = 0;
            sNewDoctor.CreatedDate = DateTime.Now;
            sNewDoctor.CreatedBy = "SYSTEM";

            if (DoctorRepository.AddDoctor(ConfigSettings.GetConfigurationSettings(), sNewDoctor))
            {
                sResp.statusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.statusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        public IActionResult ViewDoctorProfile()
        {
            return View("ViewDoctorProfile");
        }

        public IActionResult GetGenderListDropdown()
        {
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sMasterDataGroupName);
            return Json(sMasterCodeObj);
        }
    }

    public class ResponseResultCode
    {
        public int? statusCode { get; set; }
    }
}
