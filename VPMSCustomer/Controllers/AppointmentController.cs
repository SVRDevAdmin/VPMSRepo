using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.Models;
using VPMSCustomer.Lib.Models;

namespace VPMSCustomer.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: AppointmentController
        public ActionResult Index()
        {
            return View();
        }

        [Route("/Appointment/Details/{appointmentID}")]
        public IActionResult Details(long appointmentID)
        {
            ViewData["appointmentSelected"] = appointmentID;

            return View("Details");
        }

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

        [Route("/Appointment/SubmitAppointment")]
        [HttpPost()]
        public IActionResult SubmitAppointment(int branchID, string appointmentDate, string appointmentStartTime, string appointmentEndTime,
                                               int ownerID, int petID, int doctorID, String inchargeDoctor, long serviceID, string submittedBy)
        {
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
                sNewAppointment.Status = 0;
                sNewAppointment.EmailNotify = false;
                sNewAppointment.InchargeDoctor = inchargeDoctor;
                sNewAppointment.CreatedDate = DateTime.Now;
                sNewAppointment.CreatedBy = submittedBy;

                if (AppointmentRepository.CreateAppointment(sNewAppointment, serviceID, submittedBy))
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
    }
}
