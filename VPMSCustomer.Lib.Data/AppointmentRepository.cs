using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class AppointmentRepository
    {
        public static List<AppointmentGridDisplayModel> GetAppointmentViewListingByPatientID(long patientID)
        {
            List<AppointmentGridDisplayModel> sResultList = new List<AppointmentGridDisplayModel>();

            try
            {
                using (var ctx = new AppointmentDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    string sSelectCommand = "SELECT A.AppointmentID, A.ApptDate, A.ApptStartTime,  A.ApptEndTime, A.OwnerID, A.PetID, " +
                                            "A.Status, A.InchargeDoctor, B.ServicesID, C.Name AS 'ServicesName' " +
                                            "FROM ( " +
                                            "SELECT A1.*, A2.PatientID " +
                                            "FROM mst_appointment AS A1 " +
                                            "INNER JOIN mst_patients_owner AS A2 ON A2.ID = A1.OwnerID " +
                                            "WHERE A2.PatientID = '" + patientID + "' " +
                                            ") AS A " +
                                            "INNER JOIN mst_appointment_services AS B ON B.ApptID = A.AppointmentID AND B.IsDeleted = 0 " +
                                            "INNER JOIN mst_services AS C ON C.ID = B.ServicesID ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                AppointmentGridDisplayModel sAppointmentObj = new AppointmentGridDisplayModel();
                                sAppointmentObj.AppointmentID = Convert.ToInt64(sReader["AppointmentID"]);
                                sAppointmentObj.ApptDate = Convert.ToDateTime(sReader["ApptDate"]);
                                sAppointmentObj.ApptStartTime = sReader["ApptStartTime"].ToString();
                                sAppointmentObj.ApptEndTime = sReader["ApptEndTime"].ToString();
                                sAppointmentObj.OwnerID = Convert.ToInt64(sReader["OwnerID"]);
                                sAppointmentObj.PetID = Convert.ToInt64(sReader["PetID"]);
                                sAppointmentObj.Status = Convert.ToInt32(sReader["Status"]);
                                sAppointmentObj.InchargeDoctor = sReader["InchargeDoctor"].ToString();
                                sAppointmentObj.ServicesID = Convert.ToInt64(sReader["ServicesID"]);
                                sAppointmentObj.ServicesName = sReader["ServicesName"].ToString();

                                sResultList.Add(sAppointmentObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<AppointmentDetailsModel> GetAppointmentDetailsByID(long appointmentID)
        {
            List<AppointmentDetailsModel> sResultList = new List<AppointmentDetailsModel>();

            try
            {
                using (var ctx = new AppointmentDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.AppointmentID, A.UniqueIDKey, A.BranchID, A.ApptDate, A.ApptStartTime, " +
                                            "A.ApptEndTime, A.PetID, A.Status, B.ServicesID , A.InchargeDoctor " +
                                            "FROM mst_appointment AS A " +
                                            "INNER JOIN mst_appointment_services AS B ON B.ApptID = A.AppointmentID AND B.IsDeleted = 0 " +
                                            "WHERE A.AppointmentID = '" + appointmentID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                AppointmentDetailsModel sApptObj = new AppointmentDetailsModel();
                                sApptObj.AppointmentID = Convert.ToInt64(sReader["AppointmentID"]);
                                sApptObj.UniqueIDKey = sReader["UniqueIDKey"].ToString();
                                sApptObj.BranchID = Convert.ToInt32(sReader["BranchID"]);
                                sApptObj.ApptDate = Convert.ToDateTime(sReader["ApptDate"]);
                                sApptObj.ApptStartTime = sReader["ApptStartTime"].ToString();
                                sApptObj.ApptEndTime = sReader["ApptEndTime"].ToString();
                                sApptObj.PetID = Convert.ToInt32(sReader["PetID"]);
                                sApptObj.Status = Convert.ToInt32(sReader["Status"]);
                                sApptObj.ServicesID = Convert.ToInt64(sReader["ServicesID"]);
                                sApptObj.InchargeDoctor = sReader["InchargeDoctor"].ToString();

                                sResultList.Add(sApptObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean UpdateAppointmentStatus(long appointmentID, int iStatus, String? sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext())
                {
                    var sApptRecord = ctx.mst_appointment.Where(x => x.AppointmentID == appointmentID).FirstOrDefault();
                    if (sApptRecord != null)
                    {
                        sApptRecord.Status = iStatus;
                        sApptRecord.UpdatedDate = DateTime.Now;
                        sApptRecord.UpdatedBy = sUpdatedBy;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<AppointmentGroupingModel> GetAppointmentGrouping(String sAppointmentGroup)
        {
            try
            {
                using (var ctx = new AppointmentDBContext())
                {
                    return ctx.mst_appointment_grouping.Where(x => x.AppointmentGroup == sAppointmentGroup.ToString())
                                                       .OrderBy(x => x.SeqNo)
                                                       .ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean UpdateAppointmentDetails(long appointmentID, int branchID, DateTime dtAppointment, DateTime dtStart, DateTime dtEnd, long servicesID, String inchargeDoctor, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext())
                {
                    var sApptObj = ctx.mst_appointment.Where(x => x.AppointmentID == appointmentID).FirstOrDefault();
                    if (sApptObj != null)
                    {
                        Boolean isChanges = false;

                        if (sApptObj.BranchID != branchID)
                        {
                            sApptObj.BranchID = branchID;

                            isChanges = true;
                        }

                        if (sApptObj.ApptDate != dtAppointment)
                        {
                            sApptObj.ApptDate = dtAppointment;

                            isChanges = true;
                        }

                        if (sApptObj.ApptStartTime != dtStart.TimeOfDay)
                        {
                            sApptObj.ApptStartTime = dtStart.TimeOfDay;
                        }

                        if (sApptObj.ApptEndTime != dtEnd.TimeOfDay)
                        {
                            sApptObj.ApptEndTime = dtEnd.TimeOfDay;
                        }

                        if (sApptObj.InchargeDoctor != inchargeDoctor)
                        {
                            sApptObj.InchargeDoctor = inchargeDoctor;
                        }

                        if (isChanges)
                        {
                            sApptObj.UpdatedBy = sUpdatedBy;
                            sApptObj.UpdatedDate = DateTime.Now;
                        }

                        ctx.SaveChanges();

                        isSuccess = true;
                    }

                    var sServicesObj = ctx.mst_appointment_services.Where(x => x.ApptID == appointmentID && x.IsDeleted == 0).ToList();
                    if (sServicesObj != null && sServicesObj.Count > 0)
                    {
                        foreach(var s in sServicesObj)
                        {
                            s.IsDeleted = 1;

                            ctx.SaveChanges();

                            isSuccess = true;
                        }
                    }

                    AppointmentServicesModel sNewService = new AppointmentServicesModel();
                    sNewService.ApptID = appointmentID;
                    sNewService.ServicesID = servicesID;
                    sNewService.IsDeleted = 0;
                    sNewService.CreatedDate = DateTime.Now;
                    sNewService.CreatedBy = sUpdatedBy;

                    ctx.mst_appointment_services.Add(sNewService);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static Boolean ValidateAppointmentRequested(long appointmentID, long branchID, DateTime dtAppt, DateTime dtStart, DateTime dtEnd)
        {
            Boolean isOverlap = false;

            try
            {
                using (var ctx = new AppointmentDBContext())
                {
                    var isExists = ctx.mst_appointment.Where(x => x.AppointmentID != appointmentID &&
                                                                  x.Status == 0 &&
                                                                  x.BranchID == branchID &&
                                                                  x.ApptDate == dtAppt &&
                                                                  ((x.ApptStartTime <= dtStart.TimeOfDay && x.ApptEndTime >= dtStart.TimeOfDay) ||
                                                                  (x.ApptStartTime <= dtEnd.TimeOfDay && x.ApptEndTime >= dtEnd.TimeOfDay))
                                                      ).Count();

                    if (isExists > 0)
                    {
                        isOverlap = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isOverlap = false;
            }

            return isOverlap;
        }

        public static Boolean CreateAppointment(AppointmentModel appointmentObject, long serviceID, String submittedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext())
                {
                    ctx.mst_appointment.Add(appointmentObject);
                    ctx.SaveChanges();

                    AppointmentServicesModel sAppointmentServObject = new AppointmentServicesModel();
                    sAppointmentServObject.ApptID = appointmentObject.AppointmentID;
                    sAppointmentServObject.ServicesID = serviceID;
                    sAppointmentServObject.IsDeleted = 0;
                    sAppointmentServObject.CreatedDate = DateTime.Now;
                    sAppointmentServObject.CreatedBy = submittedBy;

                    ctx.mst_appointment_services.Add(sAppointmentServObject);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
