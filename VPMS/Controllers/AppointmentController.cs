using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            AppointmentViewModel sModel = new AppointmentViewModel();

            sModel.TreatmentServicesModel = TreatmentServicesRepository.GetTreatmentServicesList(ConfigSettings.GetConfigurationSettings(), 1);
            sModel.PatientSelectionModel = AppointmentRepository.GetPatientOwnerList(ConfigSettings.GetConfigurationSettings());
            
            ViewData["AppointmentViewModel"] = sModel;

            return View();
        }

        //public IActionResult GetServicesList()
        //{
        //    var sResult = TreatmentServicesRepository.GetTreatmentServicesList(ConfigSettings.GetConfigurationSettings(), 1);
        //    if (sResult != null)
        //    {
        //        ServicesData servicesData = new ServicesData();
        //        servicesData.ServiceList = new SelectList(sResult, "ID", "Name");
        //        return View(servicesData);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        public IActionResult CreateAppointment(AppointmentModel sModel)
        {
            String abc = "abc";
            sModel.CreatedDate = DateTime.Now;
            sModel.CreatedBy = "SYSTEM";
            sModel.Status = 1;
            sModel.UniqueIDKey = VPMS.Lib.Helpers.GenerateRandomKeyString(32);

            List<long> sServices = new List<long>();

            var sResult = AppointmentRepository.CreateAppointment(ConfigSettings.GetConfigurationSettings(), sModel, sServices);
            if (sResult != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult GetServiceDoctorList(int ServicesID)
        {
            var sServicesDoctorList = TreatmentServicesRepository.GetServicesDoctorList(ConfigSettings.GetConfigurationSettings(), ServicesID);
            return Json(sServicesDoctorList);
        }

        public IActionResult GetPetListByPatientID(long PatientID)
        {
            var sPetList = AppointmentRepository.GetPetListByOwnerID(ConfigSettings.GetConfigurationSettings(), PatientID);
            return Json(sPetList);
        }
    }

    public class AppointmentViewModel
    {
        public List<TreatmentServicesModel> TreatmentServicesModel { get; set; }
        public List<PatientSelectionModel> PatientSelectionModel {  get; set; }
    }
}
