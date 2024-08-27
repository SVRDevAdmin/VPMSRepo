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
            sModel.SpeciesModel = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Species");
            
            ViewData["AppointmentViewModel"] = sModel;

            return View();
        }

        [HttpGet]
        public IActionResult GetCalendarAppointmentsMonthView(String sYear, String sMonth, String searchOwner, String searchPet, 
                                                              String searchServices, String searchDoctor)
        {
            var appViewModel = AppointmentRepository.GetCalendarAppointmentMonthView(ConfigSettings.GetConfigurationSettings(), sYear, sMonth, searchOwner, searchPet, searchServices, searchDoctor);
            return Json(appViewModel);
        }

        [HttpGet]
        public IActionResult GetAppointmentByID(String ApptID)
        {
            var apptData = AppointmentRepository.GetAppointmentByID(ConfigSettings.GetConfigurationSettings(), ApptID);
            return Json(apptData);
        }

        public IActionResult UpdateAppointment(DateTime ApptDate, DateTime ApptStartTime, DateTime ApptEndTime, long ApptID)
        {
            ResponseResultCode sResp = new ResponseResultCode();

            if (AppointmentRepository.UpdatedAppointment(ConfigSettings.GetConfigurationSettings(), ApptDate, ApptStartTime, ApptEndTime, ApptID))
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        public IActionResult UpdateAppointmentStatus(long ApptID, int ApptStatus)
        {
            ResponseResultCode sResp = new ResponseResultCode();

            if (AppointmentRepository.UpdateAppointmentStatus(ConfigSettings.GetConfigurationSettings(), ApptID, ApptStatus))
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        public IActionResult CreateAppointment(AppointmentControllerModel sControllerModel)
        {
            ResponseResultCode sResp = new ResponseResultCode();

            AppointmentModel sModel = new AppointmentModel();
            sModel.BranchID = sControllerModel.BranchID;
            sModel.ApptDate = sControllerModel.ApptDate;
            sModel.ApptStartTime = sControllerModel.ApptStartTime;
            sModel.ApptEndTime = sControllerModel.ApptEndTime;
            sModel.OwnerID = sControllerModel.OwnerID;
            sModel.PetID = sControllerModel.PetID;
            sModel.EmailNotify = sControllerModel.EmailNotify;
            sModel.InchargeDoctor = sControllerModel.InchargeDoctor;
            sModel.CreatedDate = DateTime.Now;
            sModel.CreatedBy = "SYSTEM";
            sModel.Status = 0;
            sModel.UniqueIDKey = VPMS.Lib.Helpers.GenerateRandomKeyString(32);

            List<long> sServices = new List<long>();
            if (sControllerModel.ServiceList.Count > 0)
            {
                sServices = sControllerModel.ServiceList;
            }

            if (!AppointmentRepository.ValidateAppointmentByDoctor(ConfigSettings.GetConfigurationSettings(), sModel))
            {
                if (!AppointmentRepository.ValidateAppointmentByPatient(ConfigSettings.GetConfigurationSettings(), sModel))
                {
                    bool sResult = AppointmentRepository.CreateAppointment(ConfigSettings.GetConfigurationSettings(), sModel, sServices);
                    if (sResult)
                    {
                        //todo : send email

                        sResp.StatusCode = (int)StatusCodes.Status200OK;
                        sResp.isDoctApptOverlap = false;
                        sResp.isPatientAppOverlap = false;

                        return Json(sResp);
                    }
                    else
                    {
                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                        sResp.isDoctApptOverlap = false;
                        sResp.isPatientAppOverlap = false;

                        return Json(sResp);
                    }
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.isDoctApptOverlap = false;
                    sResp.isPatientAppOverlap = true;

                    return Json(sResp);
                }
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                sResp.isDoctApptOverlap = true;
                sResp.isPatientAppOverlap = false;

                return Json(sResp);
            }

        }

        public IActionResult CreateNewClientAppointment(AppointmentNewClientControllerModel sControllerModel)
        {
            ResponseResultCode sResp = new ResponseResultCode();
            DateTime sNow = DateTime.Now;

            AppointmentPatientsModel sPatient = new AppointmentPatientsModel();
            sPatient.BranchID = sControllerModel.BranchID;
            sPatient.CreatedDate = sNow;
            sPatient.CreatedBy = "SYSTEM";

            PatientOwnerModel sPatientOwner = new PatientOwnerModel();
            sPatientOwner.Name = sControllerModel.OwnerName;
            sPatientOwner.ContactNo = sControllerModel.ContactNo;
            sPatientOwner.Gender = "";
            sPatientOwner.Address = "";
            sPatientOwner.PostCode = "";
            sPatientOwner.City = "";
            sPatientOwner.State = "";
            sPatientOwner.Country = "";
            sPatientOwner.Status = 1;
            sPatientOwner.CreatedDate = sNow;
            sPatientOwner.CreatedBy = "SYSTEM";

            PetModel sPet = new PetModel();
            sPet.Name = sControllerModel.PetName;
            sPet.RegistrationNo = "";
            sPet.Gender = "";
            sPet.Age = -1;
            sPet.Breed = "";
            sPet.Color = "";
            sPet.Allergies = "";
            sPet.Weight = 0;
            sPet.WeightUnit = "";
            sPet.Height = 0;
            sPet.HeightUnit = "";
            sPet.DOB = sControllerModel.PetDOB;
            sPet.Species = sControllerModel.Species;
            sPet.Status = 1;
            sPet.CreatedDate = sNow;
            sPet.CreatedBy = "SYSTEM";

            AppointmentModel sModel = new AppointmentModel();
            sModel.BranchID = sControllerModel.BranchID;
            sModel.ApptDate = sControllerModel.ApptDate;
            sModel.ApptStartTime = sControllerModel.ApptStartTime;
            sModel.ApptEndTime = sControllerModel.ApptEndTime;
            sModel.EmailNotify = sControllerModel.EmailNotify;
            sModel.InchargeDoctor = sControllerModel.InchargeDoctor;
            sModel.CreatedDate = DateTime.Now;
            sModel.CreatedBy = "SYSTEM";
            sModel.Status = 0;
            sModel.UniqueIDKey = VPMS.Lib.Helpers.GenerateRandomKeyString(32);

            List<long> sServices = new List<long>();
            if (sControllerModel.ServiceList.Count > 0)
            {
                sServices = sControllerModel.ServiceList;
            }


            if (!AppointmentRepository.ValidateAppointmentByDoctor(ConfigSettings.GetConfigurationSettings(), sModel))
            {
                Boolean sResult = AppointmentRepository.CreateNewClientAppointment(ConfigSettings.GetConfigurationSettings(),
                                                                            sPatient, sPatientOwner, sPet, sModel, sServices);
                if (sResult)
                {
                    sResp.StatusCode = (int)StatusCodes.Status200OK;
                    sResp.isDoctApptOverlap = false;
                    sResp.isPatientAppOverlap = false;

                    return Json(sResp);
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.isDoctApptOverlap = false;
                    sResp.isPatientAppOverlap = false;

                    return Json(sResp);
                }
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                sResp.isDoctApptOverlap = true;
                sResp.isPatientAppOverlap = false;

                return Json(sResp);
            }

        }

        public IActionResult GetServiceDoctorList(int ServicesID)
        {
            var sServicesDoctorList = TreatmentServicesRepository.GetServicesDoctorList(ConfigSettings.GetConfigurationSettings(), ServicesID);
            return Json(sServicesDoctorList);
        }

        public IActionResult GetPetListByPatientID(long PatientID)
        {
            List<PetsSelectionModel> sPetList = AppointmentRepository.GetPetListByOwnerID(ConfigSettings.GetConfigurationSettings(), PatientID);
            return Json(sPetList);
        }
    }

    public class AppointmentViewModel
    {
        public List<TreatmentServicesModel> TreatmentServicesModel { get; set; }
        public List<PatientSelectionModel> PatientSelectionModel {  get; set; }
        public List<MastercodeModel> SpeciesModel { get; set; }
    }

    public class ResponseResultCode
    {
        public int? StatusCode { get; set; }
        public Boolean? isDoctApptOverlap { get; set; }
        public Boolean? isPatientAppOverlap { get; set; }
    }
}
