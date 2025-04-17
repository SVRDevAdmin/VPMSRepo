using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.Models;
using VPMSCustomer.Lib.Models;
using VPMSCustomer.Lib;

namespace VPMSCustomer.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: AppointmentController
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Appointment Details page
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        [Route("/Appointment/Details/{appointmentID}")]
        public IActionResult Details(long appointmentID)
        {
            ViewData["appointmentSelected"] = appointmentID;

            return View("Details");
        }

        /// <summary>
        /// Get Customer's appointment records
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        [Route("/Appointment/GetViewListing/{patientID}")]
        [HttpGet()]
        public IActionResult GetAppointmentViewListing(long patientID)
        {
            try
            {
                var sResult = AppointmentRepository.GetAppointmentViewListingByPatientID(patientID);
                if (sResult != null)
                {
                    return Json(new { data = sResult });
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
        /// Confirmed the new appointment date reverted
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        [Route("/Appointment/ConfirmedAppointment")]
        [HttpPost()]
        public IActionResult ConfirmedAppointment(long appointmentID, String sUpdatedBy)
        {
            int confirmStatus = 0;

            try
            {
                var sResult = AppointmentRepository.UpdateAppointmentStatus(appointmentID, confirmStatus, sUpdatedBy);
                if (sResult)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        /// <summary>
        /// Update Appointment Status Changes
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        [Route("/Appointment/UpdateAppointmentStatus")]
        [HttpPost()]
        public IActionResult UpdateAppointmentStatus(long appointmentID, String sUpdatedBy)
        {
            int cancelStatus = 2;

            try
            {
                var sResult = AppointmentRepository.UpdateAppointmentStatus(appointmentID, cancelStatus, sUpdatedBy);
                if (sResult)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        /// <summary>
        /// Get Appointment Views Grouping
        /// </summary>
        /// <param name="appointmentGroup"></param>
        /// <returns></returns>
        [Route("/Appointment/GetAppointmentGrouping")]
        [HttpGet()]
        public IActionResult GetAppointmentGroup(String appointmentGroup)
        {
            try
            {
                var sResult = AppointmentRepository.GetAppointmentGrouping(appointmentGroup);
                if (sResult != null)
                {
                    return Json(new { data = sResult });
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
        /// Get Appointment details
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        [Route("/Appointment/GetAppointmentDetail/{appointmentID}")]
        [HttpGet()]
        public IActionResult GetAppointmentDetailsByID(long appointmentID)
        {
            try
            {
                var sResult = AppointmentRepository.GetAppointmentDetailsByID(appointmentID);
                if (sResult != null)
                {
                    return Json(new { data = sResult });
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
        /// Update Appointment details
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <param name="branchID"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="servicesID"></param>
        /// <param name="inchargeDoctor"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [Route("/Appointment/UpdateAppointmentDetails")]
        [HttpPost()]
        public IActionResult UpdateAppointmentDetails(long appointmentID, int branchID, string appointmentDate, string startTime, 
                                                      string endTime, int servicesID, String inchargeDoctor, string updatedBy)
        {
            AppointmentResponseCodeObject sRespObj = new AppointmentResponseCodeObject();

            try
            {
                DateTime dtAppt = DateTime.ParseExact(appointmentDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtStart = DateTime.ParseExact(startTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = DateTime.ParseExact(endTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                if (!AppointmentRepository.ValidateAppointmentRequested(appointmentID, branchID, dtAppt, dtStart, dtEnd))
                {
                    var sResult = AppointmentRepository.UpdateAppointmentDetails(appointmentID, branchID, dtAppt, dtStart, dtEnd, servicesID, inchargeDoctor, updatedBy);
                    if (sResult)
                    {
                        sRespObj.StatusCode = (int)StatusCodes.Status200OK;
                        sRespObj.isOverlap = false;
                        sRespObj.isRecordExists = false;
                        
                        return Json(sRespObj);
                    }
                    else
                    {
                        sRespObj.StatusCode = (int)StatusCodes.Status400BadRequest;
                        sRespObj.isOverlap = false;
                        sRespObj.isRecordExists = false;

                        return Json(sRespObj);
                    }
                }
                else
                {
                    sRespObj.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sRespObj.isOverlap = true;
                    sRespObj.isRecordExists = false;

                    return Json(sRespObj);
                }

            }
            catch (Exception ex)
            {
                sRespObj.StatusCode = (int)StatusCodes.Status400BadRequest;
                sRespObj.isOverlap = false;
                sRespObj.isRecordExists = false;

                return Json(sRespObj);
            }
        }

        /// <summary>
        /// Create Appointment
        /// </summary>
        /// <param name="branchID"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="appointmentStartTime"></param>
        /// <param name="appointmentEndTime"></param>
        /// <param name="ownerID"></param>
        /// <param name="petID"></param>
        /// <param name="doctorID"></param>
        /// <param name="inchargeDoctor"></param>
        /// <param name="serviceID"></param>
        /// <param name="submittedBy"></param>
        /// <returns></returns>
        [Route("/Appointment/SubmitAppointment")]
        [HttpPost()]
        public IActionResult SubmitAppointment(int branchID, string appointmentDate, string appointmentStartTime, string appointmentEndTime,
                                               int ownerID, int petID, int doctorID, String inchargeDoctor, long serviceID, string submittedBy)
        {
            AppointmentResponseCodeObject sRespObj = new AppointmentResponseCodeObject();

            try
            {
                AppointmentModel sNewAppointment = new AppointmentModel();
                sNewAppointment.UniqueIDKey = VPMSCustomer.Lib.Helper.GenerateRandomKeyString(32);
                sNewAppointment.BranchID = branchID;
                sNewAppointment.ApptDate = DateTime.ParseExact(appointmentDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                DateTime timespanStart =  DateTime.ParseExact(appointmentStartTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                sNewAppointment.ApptStartTime = timespanStart.TimeOfDay;

                DateTime timespanEnd = DateTime.ParseExact(appointmentEndTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                sNewAppointment.ApptEndTime = timespanEnd.TimeOfDay;

                sNewAppointment.OwnerID = ownerID;
                sNewAppointment.PetID = petID;
                sNewAppointment.Status = 3;
                sNewAppointment.EmailNotify = false;
                sNewAppointment.InchargeDoctor = inchargeDoctor;
                sNewAppointment.CreatedDate = DateTime.Now;
                sNewAppointment.CreatedBy = submittedBy;

                if (!AppointmentRepository.ValidateAppointmentCreation(sNewAppointment))
                {
                    if (AppointmentRepository.CreateAppointment(sNewAppointment, serviceID, submittedBy))
                    {
                        SendNotificationAppointmentSubmitEmail(sNewAppointment, serviceID);

                        sRespObj.StatusCode = (int)StatusCodes.Status200OK;
                        sRespObj.isOverlap = false;
                        sRespObj.isRecordExists = false;
                    }
                    else
                    {
                        sRespObj.StatusCode = (int)StatusCodes.Status400BadRequest;
                        sRespObj.isOverlap = false;
                        sRespObj.isRecordExists = false;
                    }
                }
                else
                {
                    sRespObj.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sRespObj.isOverlap = true;
                    sRespObj.isRecordExists = false;
                }
            }
            catch (Exception ex)
            {
                sRespObj.StatusCode = (int)StatusCodes.Status400BadRequest;
                sRespObj.isOverlap = false;
                sRespObj.isRecordExists = false;
            }

            return Json(sRespObj);
        }

        public void SendNotificationAppointmentSubmitEmail(AppointmentModel sNewAppointment, long sServiceID)
        {
            var sEmailConfig = VPMSCustomer.Lib.Shared.ConfigSettings.GetConfigurationSettings();
            String? sHost = sEmailConfig.GetSection("SMTP:Host").Value;
            int? sPortNo = Convert.ToInt32(sEmailConfig.GetSection("SMTP:Port").Value);
            String? sUsername = sEmailConfig.GetSection("SMTP:Username").Value;
            String? sPassword = sEmailConfig.GetSection("SMTP:Password").Value;
            String? sSender = sEmailConfig.GetSection("SMTP:Sender").Value;

            String sServiceName = "";
            var sServiceObj = ServicesRepository.GetServiceDetailsByID(sServiceID);
            if (sServiceObj != null)
            {
                sServiceName = sServiceObj.Name;
            }

            String sPetName = "";
            var sPetObj = PetRepository.GetPetProfileByID(Convert.ToInt32(sNewAppointment.PetID));
            if (sPetObj != null)
            {
                sPetName = sPetObj.Name;
            }

            String sOwnerName = "";
            List<String> lstRecipientEmail = new List<string>();
            var sPatientObj = PatientRepository.GetPatientOwnerByID(sNewAppointment.OwnerID.Value);
            if (sPatientObj != null)
            {
                sOwnerName = sPatientObj.Name;

                lstRecipientEmail.Add(sPatientObj.EmailAddress);
            }

            var emailTemplate = TemplateRepository.GetTemplateByCodeLang(VPMSCustomer.Lib.Shared.ConfigSettings.GetConfigurationSettings(), "CPMS_EN002", "en");
            emailTemplate.TemplateContent = emailTemplate.TemplateContent.Replace("###<customer>###", sOwnerName)
                                                                         .Replace("###<services>###", sServiceName)
                                                                         .Replace("###<appointmetdate>###", sNewAppointment.ApptDate.Value.ToString("dd/MM/yyyy"))
                                                                         .Replace("###<appointmenttime>###", sNewAppointment.ApptStartTime.Value.ToString())
                                                                         .Replace("###<petname>###", sPetName)
                                                                         .Replace("###<doctorname>###", sNewAppointment.InchargeDoctor);

            emailTemplate.TemplateTitle = emailTemplate.TemplateTitle.Replace("###<services>###", sServiceName);


            try
            {
                VPMSCustomer.Lib.EmailObject sEmailObj = new VPMSCustomer.Lib.EmailObject();
                sEmailObj.SenderEmail = sSender;
                sEmailObj.RecipientEmail = lstRecipientEmail;
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
                VPMSCustomer.Lib.EmailHelpers.SendEmail(sEmailObj, out sErrorMessage);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
