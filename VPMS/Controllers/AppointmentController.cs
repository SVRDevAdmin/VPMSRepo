using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Runtime.CompilerServices;
using VPMS.Lib;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class AppointmentController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            int cookieBranchID = 0;
            if (Request.Cookies["BranchID"] != null)
            {
                cookieBranchID = Convert.ToInt32(Request.Cookies["BranchID"]);
            }

            AppointmentViewModel sModel = new AppointmentViewModel();

            //sModel.TreatmentServicesModel = TreatmentServicesRepository.GetTreatmentServicesList(ConfigSettings.GetConfigurationSettings(), 1);
            sModel.TreatmentServicesModel = TreatmentServicesRepository.GetTreatmentServicesList(ConfigSettings.GetConfigurationSettings(), cookieBranchID);
            sModel.PatientSelectionModel = PatientRepository.GetPatientOwnerList(ConfigSettings.GetConfigurationSettings());
            sModel.SpeciesModel = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), "Species");
            
            ViewData["AppointmentViewModel"] = sModel;

            return View();
        }

        /// <summary>
        /// Get Appointment list by Calendar View
        /// </summary>
        /// <param name="sYear"></param>
        /// <param name="sMonth"></param>
        /// <param name="searchOwner"></param>
        /// <param name="searchPet"></param>
        /// <param name="searchServices"></param>
        /// <param name="searchDoctor"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCalendarAppointmentsMonthView(String sYear, String sMonth, String searchOwner, String searchPet, 
                                                              String searchServices, String searchDoctor)
        {
            var appViewModel = AppointmentRepository.GetCalendarAppointmentMonthView(ConfigSettings.GetConfigurationSettings(), sYear, sMonth, searchOwner, searchPet, searchServices, searchDoctor);
            return Json(appViewModel);
        }

        /// <summary>
        /// Get Appointment Record by AppointmentID
        /// </summary>
        /// <param name="ApptID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAppointmentByID(String ApptID)
        {
            var apptData = AppointmentRepository.GetAppointmentByID(ConfigSettings.GetConfigurationSettings(), ApptID);
            return Json(apptData);
        }

        /// <summary>
        /// Update Appointment Information
        /// </summary>
        /// <param name="ApptDate"></param>
        /// <param name="ApptStartTime"></param>
        /// <param name="ApptEndTime"></param>
        /// <param name="ApptID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update Appointment Status
        /// </summary>
        /// <param name="ApptID"></param>
        /// <param name="ApptStatus"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create Appointment
        /// </summary>
        /// <param name="sControllerModel"></param>
        /// <returns></returns>
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
                        if (sModel.EmailNotify == true)
                        {
                            List<String> sRecipientList = new List<string>();
                            sRecipientList.Add("kenny@svrtech.com.my");

                            string sApptOwnerName = "";
                            String sApptPetName = "";
                            PatientPetInfo sPatientOwner = PatientRepository.GetPatientPetProfileByOwnerPetID(ConfigSettings.GetConfigurationSettings(), sModel.OwnerID.Value, sModel.PetID.Value);
                            if (sPatientOwner != null)
                            {
                                sApptOwnerName = (sPatientOwner != null) ? sPatientOwner.Name : "";
                                sApptPetName = (sPatientOwner != null) ? sPatientOwner.PetName : "";
                                //sRecipietnList.Add(sPatientOwner.Email);
                            }

                            SendNotificationEmail(sApptOwnerName, sApptPetName, sModel.InchargeDoctor, sModel.ApptDate, sModel.ApptStartTime,
                                                  sModel.ApptEndTime, sRecipientList, sServices);
                        }

                        
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

        /// <summary>
        /// Create new client Appointment
        /// </summary>
        /// <param name="sControllerModel"></param>
        /// <returns></returns>
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
                    if (sModel.EmailNotify == true)
                    {
                        List<String> sRecipientList = new List<string>();
                        if (sControllerModel.EmailAddress.Length > 0)
                        {
                            sRecipientList.Add(sControllerModel.EmailAddress);
                        }

                        SendNotificationEmail(sPatientOwner.Name, sPet.Name, sModel.InchargeDoctor, sModel.ApptDate, sModel.ApptStartTime,
                                              sModel.ApptEndTime, sRecipientList, sServices);
                    }

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

        /// <summary>
        /// Get Doctor list by services selected
        /// </summary>
        /// <param name="ServicesID"></param>
        /// <returns></returns>
        public IActionResult GetServiceDoctorList(int ServicesID)
        {
            var sServicesDoctorList = TreatmentServicesRepository.GetServicesDoctorList(ConfigSettings.GetConfigurationSettings(), ServicesID);
            return Json(sServicesDoctorList);
        }
        
        /// <summary>
        /// Get Pet List By Owner ID
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public IActionResult GetPetListByPatientID(long PatientID)
        {
            List<PetsSelectionModel> sPetList = PatientRepository.GetPetListByOwnerID(ConfigSettings.GetConfigurationSettings(), PatientID);
            return Json(sPetList);
        }

        /// <summary>
        /// Populate email content
        /// </summary>
        /// <param name="OwnerName"></param>
        /// <param name="PetName"></param>
        /// <param name="DoctorName"></param>
        /// <param name="ApptDate"></param>
        /// <param name="ApptStartTime"></param>
        /// <param name="ApptEndTime"></param>
        /// <param name="sRecipientList"></param>
        /// <param name="sServices"></param>
        public void SendNotificationEmail(String OwnerName, String PetName, String DoctorName, DateTime? ApptDate, 
                                          DateTime? ApptStartTime, DateTime? ApptEndTime, List<String> sRecipientList,
                                          List<long> sServices)
        {
            var sEmailConfig = ConfigSettings.GetConfigurationSettings();
            String? sHost = sEmailConfig.GetSection("SMTP:Host").Value;
            int? sPortNo = Convert.ToInt32(sEmailConfig.GetSection("SMTP:Port").Value);
            String? sUsername = sEmailConfig.GetSection("SMTP:Username").Value;
            String? sPassword = sEmailConfig.GetSection("SMTP:Password").Value;
            String? sSender = sEmailConfig.GetSection("SMTP:Sender").Value;

            String sServicesFullName = "";
            if (sServices.Count > 0)
            {
                foreach (var s in sServices)
                {
                    var sServiceObj = TreatmentServicesRepository.GetServicesInfoByID(ConfigSettings.GetConfigurationSettings(), s);
                    if (sServiceObj != null)
                    {
                        if (sServicesFullName.Length > 0)
                        {
                            sServicesFullName += "<br/>";
                        }
                        sServicesFullName += sServiceObj.Name;
                    }
                }
            }

            var emailTemplate = TemplateRepository.GetTemplateByCodeLang(ConfigSettings.GetConfigurationSettings(), "VPMS_EN001", "en");
            emailTemplate.TemplateContent = emailTemplate.TemplateContent.Replace("###<appointmetdate>###", ApptDate.Value.ToString("dd/MM/yyyy"))
                                                                         .Replace("###<appointmenttime>###", ApptStartTime.Value.ToString("HH:mm") +
                                                                                                            " ~ " + 
                                                                                                            ApptEndTime.Value.ToString("HH:mm"))
                                                                         .Replace("###<petname>###", PetName)
                                                                         .Replace("###<services>###", sServicesFullName)
                                                                         .Replace("###<doctorname>###", DoctorName)
                                                                         .Replace("###<customer>###", OwnerName);

            try
            {
                VPMS.Lib.EmailObject sEmailObj = new VPMS.Lib.EmailObject();
                sEmailObj.SenderEmail = sSender;
                sEmailObj.RecipientEmail = sRecipientList;
                sEmailObj.Subject = (emailTemplate != null) ? emailTemplate.TemplateTitle : "";
                sEmailObj.Body = (emailTemplate != null) ? emailTemplate.TemplateContent : "";
                sEmailObj.SMTPHost = sHost;
                sEmailObj.PortNo = sPortNo.Value;
                sEmailObj.HostUsername = sUsername;
                sEmailObj.HostPassword = sPassword;
                sEmailObj.EnableSsl = true;
                sEmailObj.UseDefaultCredentials = false;
                sEmailObj.IsHtml = true;

                String sErrorMessage = "";
                EmailHelpers.SendEmail(sEmailObj, out sErrorMessage);
            }
            catch (Exception ex)
            {
                //todo:
            }
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
