using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;
using VPMS.Lib.Data.DBContext;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Org.BouncyCastle.Asn1.X509;

namespace VPMS.Lib.Data
{
    public class AppointmentRepository
    {
        /// <summary>
        /// Verify new appointment crash with any doctor's existing appointment
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public static Boolean ValidateAppointmentByDoctor(IConfiguration config, AppointmentModel sModel)
        {
            Boolean isOverlap = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    var isExists = ctx.mst_appointment
                                       .Where(x => x.Status == 0 &&
                                                   x.BranchID == sModel.BranchID &&
                                                   x.InchargeDoctor == sModel.InchargeDoctor &&
                                                   x.ApptDate == sModel.ApptDate &&
                                                   ((x.ApptStartTime <= sModel.ApptStartTime && x.ApptEndTime >= sModel.ApptStartTime) ||
                                                    (x.ApptStartTime <= sModel.ApptEndTime && x.ApptEndTime >= sModel.ApptEndTime))
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

        /// <summary>
        /// Verifiy New appointment crash with Client's existing appointment in system
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public static Boolean ValidateAppointmentByPatient(IConfiguration config, AppointmentModel sModel)
        {
            Boolean isOverlap = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    var isExists = ctx.mst_appointment
                                      .Where(x => x.Status == 0 &&
                                                  x.BranchID == sModel.BranchID &&
                                                  x.OwnerID == sModel.OwnerID &&
                                                  x.PetID == sModel.PetID &&
                                                  x.ApptDate == sModel.ApptDate &&
                                                  ((x.ApptStartTime <= sModel.ApptStartTime && x.ApptEndTime >= sModel.ApptStartTime) ||
                                                  (x.ApptStartTime <= sModel.ApptEndTime && x.ApptEndTime >= sModel.ApptEndTime))
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

        /// <summary>
        /// Create appointment
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sModel"></param>
        /// <param name="ServiceID"></param>
        /// <returns></returns>
        public static Boolean CreateAppointment(IConfiguration config, AppointmentModel sModel, List<long> ServiceID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    ctx.mst_appointment.Add(sModel);
                    ctx.SaveChanges();


                    foreach (var s in ServiceID)
                    {
                        AppointmentServiceModel sServiceModel = new AppointmentServiceModel();
                        sServiceModel.ApptID = sModel.AppointmentID;
                        sServiceModel.ServicesID = s;
                        sServiceModel.IsDeleted = false;
                        sServiceModel.CreatedDate = DateTime.Now;
                        sServiceModel.CreatedBy = sModel.CreatedBy;

                        ctx.mst_appointment_services.Add(sServiceModel);
                        ctx.SaveChanges();
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Create New appointment With new client information
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sPatient"></param>
        /// <param name="sPatientOwner"></param>
        /// <param name="sPet"></param>
        /// <param name="sModel"></param>
        /// <param name="ServiceID"></param>
        /// <returns></returns>
        public static Boolean CreateNewClientAppointment(IConfiguration config, AppointmentPatientsModel sPatient, 
                                                        PatientOwnerModel sPatientOwner, PetModel sPet,
                                                        AppointmentModel sModel, List<long> ServiceID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    ctx.mst_patients.Add(sPatient);
                    ctx.SaveChanges();

                    sPatientOwner.PatientID = sPatient.ID;
                    ctx.mst_patients_owner.Add(sPatientOwner);
                    ctx.SaveChanges();

                    sPet.PatientID = sPatient.ID;
                    ctx.mst_pets.Add(sPet);
                    ctx.SaveChanges();

                    sModel.PetID = sPet.ID;
                    sModel.OwnerID = sPatientOwner.ID;
                    ctx.mst_appointment.Add(sModel);
                    ctx.SaveChanges();

                    foreach(var s in ServiceID)
                    {
                        AppointmentServiceModel sServiceModel = new AppointmentServiceModel();
                        sServiceModel.ApptID = sModel.AppointmentID;
                        sServiceModel.ServicesID = s;
                        sServiceModel.IsDeleted = false;
                        sServiceModel.CreatedDate = DateTime.Now;
                        sServiceModel.CreatedBy = sModel.CreatedBy;

                        ctx.mst_appointment_services.Add(sServiceModel);
                        ctx.SaveChanges();
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update Appointment Info by Appointment ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sApptDate"></param>
        /// <param name="sApptStartTime"></param>
        /// <param name="sApptEndTime"></param>
        /// <param name="iApptID"></param>
        /// <returns></returns>
        public static Boolean UpdatedAppointment(IConfiguration config, DateTime sApptDate, DateTime sApptStartTime, DateTime sApptEndTime, long iApptID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sUpdateCommand = "UPDATE Mst_Appointment " + 
                                            "Set ApptDate = '" + sApptDate.ToString("yyyy-MM-dd") + "', " +
                                            " ApptStartTime = '" + sApptStartTime.ToString("HH:mm:ss") + "', " +
                                            " ApptEndTime = '" + sApptEndTime.ToString("HH:mm:ss") + "', " +
                                            " UpdatedDate = NOW(), " +
                                            " UpdatedBy = 'SYSTEM' " +
                                            "WHERE AppointmentID = '" + iApptID + "'";

                    using (MySqlCommand cmd = sConn.CreateCommand())
                    {
                        cmd.CommandText = sUpdateCommand;

                        cmd.ExecuteNonQuery();

                        isSuccess = true;
                    }
                    
                    sConn.Close();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static Boolean UpdateAppointmentStatus(IConfiguration config, long iApptID, int iStatus)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sUpdateCommand = "UPDATE Mst_Appointment " +
                                            "Set Status = '" + iStatus + "', " +
                                            " UpdatedDate = NOW(), " +
                                            " UpdatedBy = 'SYSTEM' " +
                                            "WHERE AppointmentID = '" + iApptID + "'";

                    using (MySqlCommand cmd = sConn.CreateCommand())
                    {
                        cmd.CommandText = sUpdateCommand;

                        cmd.ExecuteNonQuery();

                        isSuccess = true;
                    }

                    sConn.Close();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Get list of appointment records for Calendar vieww display
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sYear"></param>
        /// <param name="sMonth"></param>
        /// <returns></returns>
        public static List<AppointmentMonthViewModel> GetCalendarAppointmentMonthView(IConfiguration config, String sYear, String sMonth, 
                                                                String searchOwner, String searchPet, String searchServices, String searchDoctor)
        {
            List<AppointmentMonthViewModel> sResult = new List<AppointmentMonthViewModel>();

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.AppointmentID, A.ApptDate, A.ApptStartTime, A.ApptEndTime, C.`Name`, P.Name AS 'PetName', " + 
                                            "PA.Name AS 'OwnerName', A.InchargeDoctor AS 'Doctor' " +
                                            "FROM mst_appointment AS A " +
                                            "INNER JOIN mst_appointment_services AS b ON b.ApptID = A.AppointmentID " +
                                            "LEFT JOIN mst_services AS C ON C.ID = b.ServicesID " +
                                            "LEFT JOIN mst_pets AS P ON P.ID = A.PetID " + 
                                            "LEFT JOIN mst_patients_owner AS PA ON PA.ID = A.OwnerID " +
                                            "WHERE (YEAR(A.ApptDate) = '" + Convert.ToInt32(sYear) + "' AND MONTH(A.ApptDate) = '" + Convert.ToInt32(sMonth) + "') AND " +
                                            "B.IsDeleted = 0 AND " +
                                            "A.Status = 0 AND " +
                                            "(" + (searchOwner == null) + " OR A.OwnerID = '" + searchOwner + "') AND " + 
                                            "(" + (searchPet == null) + " OR A.PetID = '" + searchPet + "') AND " +
                                            "(" + (searchServices == null) + " OR b.ServicesID = '" + searchServices + "') AND " +
                                            "(" + (searchDoctor == null) + " OR A.InchargeDoctor = '" + searchDoctor + "') " +
                                            "ORDER BY A.ApptStartTime";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sResult.Add(new AppointmentMonthViewModel
                                {
                                    AppointmentID = Convert.ToInt64(sReader["AppointmentID"]),
                                    ApptDate = Convert.ToDateTime(sReader["ApptDate"]),
                                    ApptStartTimeString = sReader["ApptStartTime"].ToString(),
                                    ApptEndTimeString = sReader["ApptEndTime"].ToString(),
                                    ServiceName = sReader["Name"].ToString(),
                                    PetName = sReader["PetName"].ToString(),
                                    DoctorName = sReader["Doctor"].ToString(),
                                    OwnerName = sReader["OwnerName"].ToString()
                                });
                            }
                        }
                    }

                    sConn.Close();

                    return sResult;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Appointment Details by Appointment ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sID"></param>
        /// <returns></returns>
        public static AppointmentMonthViewModel GetAppointmentByID(IConfiguration config, String sID)
        {
            AppointmentMonthViewModel sResult = new AppointmentMonthViewModel();
            long iID = Convert.ToInt64(sID);

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String selectCommand = "SELECT A.AppointmentID, A.ApptDate, A.ApptStartTime, A.ApptEndTime, C.`Name`, P.Name AS 'PetName', " + 
                                           "PA.Name AS 'OwnerName', A.InchargeDoctor AS 'Doctor' " + 
                                           "FROM mst_appointment AS A " +
                                           "INNER JOIN mst_appointment_services AS b ON b.ApptID = A.AppointmentID " +
                                           "LEFT JOIN mst_services AS C ON C.ID = b.ServicesID " + 
                                           "LEFT JOIN mst_pets AS P ON P.ID = A.PetID " + 
                                           "LEFT JOIN mst_patients_owner AS PA ON PA.ID = A.OwnerID " +
                                           "WHERE A.AppointmentID = '" + iID + "' and B.IsDeleted = 0 ";

                    using (MySqlCommand sCommand = new MySqlCommand(selectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            if (sReader.HasRows)
                            {
                                while (sReader.Read())
                                {
                                    sResult.AppointmentID = Convert.ToInt64(sReader["AppointmentID"]);
                                    sResult.ApptDate = Convert.ToDateTime(sReader["ApptDate"]);
                                    sResult.ApptStartTimeString = sReader["ApptStartTime"].ToString();
                                    sResult.ApptEndTimeString = sReader["ApptEndTime"].ToString();
                                    sResult.ServiceName = sReader["Name"].ToString();
                                    sResult.PetName = sReader["PetName"].ToString();
                                    sResult.DoctorName = sReader["Doctor"].ToString();
                                    sResult.OwnerName = sReader["OwnerName"].ToString();
                                }
                            }
                        }
                    }

                    sConn.Close();

                    return sResult;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // --- Temporary sit here --- //
        public static List<PatientSelectionModel> GetPatientOwnerList(IConfiguration config)
        {
            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    return ctx.mst_patients_owner
                              .Where(x => x.Status == 1)
                              .OrderBy(x => x.Name)
                              .Select(x => new PatientSelectionModel
                              {
                                  ID = x.ID,
                                  Name = x.Name,
                                  PatientID = x.PatientID

                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<PetsSelectionModel> GetPetListByOwnerID(IConfiguration config, long patientID)
        {
            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    return ctx.mst_pets
                              .Where(x => x.PatientID == patientID && x.Status == 1)
                              .OrderBy(x => x.Name)
                              .Select(x => new PetsSelectionModel
                              {
                                  ID = x.ID,
                                  Name = x.Name
                              })
                              .ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
