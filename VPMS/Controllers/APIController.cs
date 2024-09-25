using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VPMS.Lib.Data;
using VPMSWeb.Lib.API;
using VPMSWeb.Lib.API.General;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class APIController : Controller
    {
        public Dictionary<String, String> sRespCodeDictionary = ResponseCodeRepo.GetResponseCodeDictionary();

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Appointment List By Date Range
        /// </summary>
        /// <param name="sRequest"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("/api/appointment/GetAppointmentByDateRange")]
        public IActionResult GetAppointmentByDate([FromBody()] VPMSWeb.Lib.API.AppointmentByDate.RequestMessage sRequest)
        {
            var sResp = new VPMSWeb.Lib.API.AppointmentByDate.ResponseMessage();
            var sRespHeader = new VPMSWeb.Lib.API.General.ResponseHeaderBase();
            var sRespBody = new VPMSWeb.Lib.API.AppointmentByDate.ResponseMessageBody();

            String sRespCode = "";
            String sRespStatus = "";
            String sRespMessage = "";

            try
            {
                if (ValidateMessageTimestamp(sRequest.header.realTimeStamp))
                {
                    if (ValidateAuthToken(sRequest.header.authtoken, out sRespCode, out sRespStatus))
                    {
                        if (sRequest.body.ValidateRequiredField())
                        {
                            Boolean isNewType = false;
                            if (sRequest.body.transtype.ToLower() == "new")
                            {
                                isNewType = true;
                            }

                            DateTime dtStart = DateTime.ParseExact(sRequest.body.startdate, "yyyyMMddHHmmss",
                                                                   System.Globalization.CultureInfo.InvariantCulture);
                            DateTime dtEnd = DateTime.ParseExact(sRequest.body.enddate, "yyyyMMddHHmmss",
                                                                System.Globalization.CultureInfo.InvariantCulture);
                            //dtEnd = dtEnd.AddDays(1).AddMinutes(-1);


                            var appointList = new List<VPMSWeb.Lib.API.General.AppointmentResultObject>();
                            var sResult = AppointmentRepository.GetAppointmentByDateRange(ConfigSettings.GetConfigurationSettings(), isNewType, dtStart, dtEnd);
                            if (sResult != null && sResult.Count > 0)
                            {
                                foreach (var sResultRow in sResult)
                                {
                                    appointList.Add(new AppointmentResultObject
                                    {
                                        uniqueid = sResultRow.UniqueIDKey,
                                        branchid = sResultRow.BranchID.Value,
                                        appointmentdate = sResultRow.ApptDate.Value.ToString("yyyyMMdd"),
                                        starttime = sResultRow.ApptStartTime.Value.ToString("HHmmss"),
                                        endtime = sResultRow.ApptEndTime.Value.ToString("HHmmss"),
                                        patientid = sResultRow.PatientID.Value,
                                        ownerid = sResultRow.OwnerID.Value,
                                        ownername = sResultRow.OwnerName,
                                        doctor = sResultRow.DoctorName,
                                        services = sResultRow.ServiceName,
                                        petid = sResultRow.PetID.Value,
                                        petname = sResultRow.PetName,
                                        createddate = (sResultRow.CreatedDate != null) ? sResultRow.CreatedDate.Value.ToString("yyyyMMddHHmmss") : null,
                                        createdby = sResultRow.CreatedBy,
                                        updateddate = (sResultRow.UpdatedDate != null) ? sResultRow.UpdatedDate.Value.ToString("yyyyMMddHHmmss") : null,
                                        updatedby = sResultRow.UpdatedBy
                                    });
                                }

                                sRespBody.results = appointList;
                            }

                            sRespCode = "VPMS.0001";
                            sRespStatus = sRespCodeDictionary["VPMS.0001"];
                            sRespMessage = "";
                        }
                        else
                        {
                            sRespCode = "VPMS.0012";
                            sRespStatus = sRespCodeDictionary["VPMS.0012"];
                        }

                    }
                }
                else
                {
                    sRespCode = "VPMS.0013";
                    sRespStatus = sRespCodeDictionary["VPMS.0013"];
                }
            }
            catch (Exception ex)
            {
                sRespCode = "VPMS.9999";
                sRespStatus = sRespCodeDictionary["VPMS.9999"];
            }

            sRespBody.responsecode = sRespCode;
            sRespBody.responsestatus = sRespStatus;
            sRespBody.responsemessage = sRespMessage;

            sRespHeader.realTimeStamp = DateTime.Now.ToUniversalTime();
            sRespHeader.authtoken = sRequest.header.authtoken;

            sResp.body = sRespBody;
            sResp.header = sRespHeader;

            return Json(sResp);
        }

        /// <summary>
        /// Get Appointment record by Appointment Unique ID
        /// </summary>
        /// <param name="sRequest"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("/api/Appointment/GetAppointmentByID")]
        public IActionResult GetAppointmentByUniqueID([FromBody()] VPMSWeb.Lib.API.AppointmentByID.RequestMessage sRequest)
        {
            var sResp = new VPMSWeb.Lib.API.AppointmentByID.ResponseMessage();
            var sRespHeader = new VPMSWeb.Lib.API.General.ResponseHeaderBase();
            var sRespBody = new VPMSWeb.Lib.API.AppointmentByID.ResponseMessageBody();

            String sRespCode = "";
            String sRespStatus = "";
            String sRespMessage = "";

            try
            {
                if (ValidateMessageTimestamp(sRequest.header.realTimeStamp))
                {
                    if (ValidateAuthToken(sRequest.header.authtoken, out sRespCode, out sRespStatus))
                    {
                        if (sRequest.body.ValidateRequiredField())
                        {
                            var sResult = AppointmentRepository.GetAppointmentByUniqueID(ConfigSettings.GetConfigurationSettings(), sRequest.body.appointmentuniqueid);
                            if (sResult != null)
                            {
                                var sApptObj = new VPMSWeb.Lib.API.General.AppointmentResultObject();
                                sApptObj.uniqueid = sResult.UniqueIDKey;
                                sApptObj.branchid = sResult.BranchID.Value;
                                sApptObj.appointmentdate = sResult.ApptDate.Value.ToString("yyyyMMdd");
                                sApptObj.starttime = sResult.ApptStartTime.Value.ToString("HHmmss");
                                sApptObj.endtime = sResult.ApptEndTime.Value.ToString("HHmmss");
                                sApptObj.patientid = sResult.PatientID.Value;
                                sApptObj.ownerid = sResult.OwnerID.Value;
                                sApptObj.ownername = sResult.OwnerName;
                                sApptObj.doctor = sResult.DoctorName;
                                sApptObj.services = sResult.ServiceName;
                                sApptObj.petid = sResult.PetID.Value;
                                sApptObj.petname = sResult.PetName;

                                if (sResult.CreatedDate != null)
                                {
                                    sApptObj.createddate = sResult.CreatedDate.Value.ToString("yyyyMMddHHmmss");
                                }
                                sApptObj.createdby = sResult.CreatedBy;

                                if (sResult.UpdatedDate != null)
                                {
                                    sApptObj.updateddate = sResult.UpdatedDate.Value.ToString("yyyyMMddHHmmss");
                                }
                                sApptObj.updatedby = sResult.UpdatedBy;

                                sRespBody.results = sApptObj;

                                sRespCode = "VPMS.0001";
                                sRespStatus = sRespCodeDictionary["VPMS.0001"];
                                sRespMessage = "";

                            }
                        }
                        else
                        {
                            sRespCode = "VPMS.0012";
                            sRespStatus = sRespCodeDictionary["VPMS.0012"];
                        }
                    }
                }
                else
                {
                    sRespCode = "VPMS.0013";
                    sRespStatus = sRespCodeDictionary["VPMS.0013"];
                } 
            }
            catch (Exception ex)
            {
                sRespCode = "VPMS.9999";
                sRespStatus = sRespCodeDictionary["VPMS.9999"];
            }

            sRespBody.responsecode = sRespCode;
            sRespBody.responsestatus = sRespStatus;
            sRespBody.responsemessage = "";

            sRespHeader.realTimeStamp = DateTime.Now.ToUniversalTime();
            sRespHeader.authtoken = sRequest.header.authtoken;

            sResp.header = sRespHeader;
            sResp.body = sRespBody;

            return Json(sResp);
        }

        /// <summary>
        /// Allow third party vendor update test result 
        /// </summary>
        /// <param name="sRequest"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("/api/TestResult/UpdateTestResult")]
        public IActionResult UpdateTestResult([FromBody()] VPMSWeb.Lib.API.UpdateTestResults.RequestMessage sRequest)
        {
            var sResp = new VPMSWeb.Lib.API.UpdateTestResults.ResponseMessage();
            var sRespHeader = new VPMSWeb.Lib.API.General.ResponseHeaderBase();
            var sRespBody = new VPMSWeb.Lib.API.UpdateTestResults.ResponseMessageBody();

            String sRespCode = "";
            String sRespStatus = "";
            String sRespMessage = "";

            try
            {
                if (ValidateMessageTimestamp(sRequest.header.realTimeStamp))
                {
                    if (ValidateAuthToken(sRequest.header.authtoken, out sRespCode, out sRespStatus))
                    {
                        if (sRequest.body.ValidateRequiredField())
                        {
                            var sTestResultList = new List<VPMS.Lib.Data.Models.TestResultModel>();

                            foreach(var r in sRequest.body.results)
                            {
                                DateTime dtTestResult = DateTime.ParseExact(r.resultdatetime, "yyyyMMddHHmmss",
                                                        System.Globalization.CultureInfo.InvariantCulture);

                                sTestResultList.Add(new VPMS.Lib.Data.Models.TestResultModel
                                {
                                    ResultType = r.resulttype,
                                    ResultDateTime = dtTestResult,
                                    ResultStatus = r.resultstatus,
                                    ResultValue = r.resultvalue,
                                    ResultParameter = r.resultparameter,
                                    ReferenceRange = r.referencerange,
                                    PatientID = r.patientid,
                                    //OwnerID = r.ownerid,
                                    PetID = r.petid,
                                    PetName = r.petname,
                                    OperatorID = r.operatorid,
                                    InchargeDoctor = r.inchargedoctor,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = "SYSTEM"
                                });
                            }

                            if (TestResultRepository.InsertTestResults(ConfigSettings.GetConfigurationSettings(), sTestResultList))
                            {
                                sRespCode = "VPMS.0001";
                                sRespStatus = sRespCodeDictionary["VPMS.0001"];
                                sRespMessage = "";
                            }
                            else
                            {
                                sRespCode = "VPMS.0014";
                                sRespStatus = sRespCodeDictionary["VPMS.0014"];
                            }
                        }
                        else
                        {
                            sRespCode = "VPMS.0012";
                            sRespStatus = sRespCodeDictionary["VPMS.0012"];
                        }
                    }
                }
                else
                {
                    sRespCode = "VPMS.0013";
                    sRespStatus = sRespCodeDictionary["VPMS.0013"];
                }
            }
            catch (Exception ex)
            {
                sRespCode = "VPMS.9999";
                sRespStatus = sRespCodeDictionary["VPMS.9999"];
            }

            sRespBody.responsecode = sRespCode;
            sRespBody.responsestatus = sRespStatus;
            sRespBody.responsemessage = "";

            sRespHeader.realTimeStamp = DateTime.Now.ToUniversalTime();
            sRespHeader.authtoken = sRequest.header.authtoken;

            sResp.header = sRespHeader;
            sResp.body = sRespBody;

            return Json(sResp);
        }

        /// <summary>
        /// Validate Auth Token
        /// </summary>
        /// <param name="sAuthToken"></param>
        /// <param name="sErrorCode"></param>
        /// <param name="sErrorStatus"></param>
        /// <returns></returns>
        public Boolean ValidateAuthToken(String sAuthToken, out String sErrorCode, out String sErrorStatus)
        {
            Boolean isValid = false;
            sErrorCode = "";
            sErrorStatus = "";

            var sAuthObj = ClientRepository.GetClientAuth(ConfigSettings.GetConfigurationSettings(), sAuthToken);
            if (sAuthObj != null)
            {
                DateTime dtEnd = sAuthObj.EndDate.Value.AddDays(1).AddSeconds(-1);

                if (sAuthObj.StartDate <= DateTime.Now && dtEnd >= DateTime.Now)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    sErrorCode = "VPMS.0011";
                    sErrorCode = sRespCodeDictionary["VPMS.0011"];
                }
            }
            else
            {
                isValid = false;
                sErrorCode = "VPMS.0010";
                sErrorCode = sRespCodeDictionary["VPMS.0010"];
            }

            return isValid;
        }

        /// <summary>
        /// Validate Message Timestamp
        /// </summary>
        /// <param name="sMessageTimeStamp"></param>
        /// <returns></returns>
        public Boolean ValidateMessageTimestamp(DateTime sMessageTimeStamp)
        {
            Boolean isValid = true;
            int timeoutPlusMins = 15;
            int timeoutMinusMins = -15;

            //TimeSpan ts = DateTime.UtcNow.Subtract(sMessageTimeStamp);
            TimeSpan ts = DateTime.Now.Subtract(sMessageTimeStamp);
            if (ts.TotalMinutes < timeoutMinusMins || ts.TotalMinutes > timeoutPlusMins)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
