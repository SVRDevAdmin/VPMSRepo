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
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Xml;

namespace VPMS.Lib.Data
{
    public class AppointmentRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
				logger.Error("AppointmentRepository >>> ValidateAppointmentByDoctor >>> ", ex);
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
				logger.Error("AppointmentRepository >>> ValidateAppointmentByPatient >>> ", ex);
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
				logger.Error("AppointmentRepository >>> CreateAppointment >>> ", ex);
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
				logger.Error("AppointmentRepository >>> CreateNewClientAppointment >>> ", ex);
			}

            return isSuccess;
        }

        /// <summary>
        ///  Update Appointment Info by Appointment ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sApptDate"></param>
        /// <param name="sApptStartTime"></param>
        /// <param name="sApptEndTime"></param>
        /// <param name="iApptID"></param>
        /// <param name="RespReschedule"></param>
        /// <returns></returns>
        public static Boolean UpdatedAppointment(IConfiguration config, DateTime sApptDate, DateTime sApptStartTime, DateTime sApptEndTime, long iApptID, Boolean RespReschedule)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sStatusString = "";
                    if (RespReschedule)
                    {
                        sStatusString = ", Status = '4' ";
                    }

                    String sUpdateCommand = "UPDATE Mst_Appointment " + 
                                            "Set ApptDate = '" + sApptDate.ToString("yyyy-MM-dd") + "', " +
                                            " ApptStartTime = '" + sApptStartTime.ToString("HH:mm:ss") + "', " +
                                            " ApptEndTime = '" + sApptEndTime.ToString("HH:mm:ss") + "', " +
                                            " UpdatedDate = NOW(), " +
                                            " UpdatedBy = 'SYSTEM' " +
                                            sStatusString + 
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
				logger.Error("AppointmentRepository >>> UpdatedAppointment >>> ", ex);
			}

            return isSuccess;
        }

        /// <summary>
        /// Update Appointment Status
        /// </summary>
        /// <param name="config"></param>
        /// <param name="iApptID"></param>
        /// <param name="iStatus"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean UpdateAppointmentStatus(IConfiguration config, long iApptID, int iStatus, String sUpdatedBy = "SYSTEM")
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
                                            " UpdatedBy = '" + sUpdatedBy + "' " +
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
				logger.Error("AppointmentRepository >>> UpdateAppointmentStatus >>> ", ex);
			}

            return isSuccess;
        }

        /// <summary>
        /// Get list of appointment records for Calendar vieww display
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sYear"></param>
        /// <param name="sMonth"></param>
        /// <param name="searchOwner"></param>
        /// <param name="searchPet"></param>
        /// <param name="searchServices"></param>
        /// <param name="searchDoctor"></param>
        /// <returns></returns>
        public static List<AppointmentMonthViewModel> GetCalendarAppointmentMonthView(IConfiguration config, String sYear, String sMonth, 
                                                                int isSuperadmin, int branchID, int organizationID, String searchOwner, String searchPet, String searchServices, 
                                                                String searchDoctor)
        {
            List<AppointmentMonthViewModel> sResult = new List<AppointmentMonthViewModel>();

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.AppointmentID, A.ApptDate, A.ApptStartTime, A.ApptEndTime, C.`Name`, P.Name AS 'PetName', " + 
                                            "PA.Name AS 'OwnerName', A.InchargeDoctor AS 'Doctor', A.BranchID, A.Status, " +
                                            "BB.`Name` AS 'BranchName', BB.OrganizationID " +
                                            "FROM mst_appointment AS A " +
                                            "INNER JOIN mst_appointment_services AS b ON b.ApptID = A.AppointmentID " +
                                            "LEFT JOIN mst_services AS C ON C.ID = b.ServicesID " +
                                            "LEFT JOIN mst_pets AS P ON P.ID = A.PetID " + 
                                            "LEFT JOIN mst_patients_owner AS PA ON PA.ID = A.OwnerID " +
                                            "INNER JOIN mst_branch AS BB ON BB.ID = A.BranchID " +
                                            "LEFT JOIN mst_organisation AS O ON O.ID = BB.OrganizationID " +
                                            "WHERE " +
                                            "(" +
                                            "(" + (isSuperadmin == 1) + " AND O.Level >= 2 AND BB.OrganizationID = '" + organizationID + "' ) OR " +
                                            "(" + (isSuperadmin == 0) + " AND BB.OrganizationID = '" + organizationID + "' AND A.BranchID = '" + branchID + "') " +
                                            ") " +
                                            "AND " +
                                            "(YEAR(A.ApptDate) = '" + Convert.ToInt32(sYear) + "' AND MONTH(A.ApptDate) = '" + Convert.ToInt32(sMonth) + "') AND " +
                                            "B.IsDeleted = 0 AND " +
                                            "(A.Status = 0 OR A.Status = 3) AND " +
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
                                    OwnerName = sReader["OwnerName"].ToString(),
                                    BranchID = Convert.ToInt32(sReader["BranchID"]),
                                    Status = Convert.ToInt32(sReader["Status"])
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
				logger.Error("AppointmentRepository >>> GetCalendarAppointmentMonthView >>> ", ex);
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
                                           "PA.Name AS 'OwnerName', A.InchargeDoctor AS 'Doctor', A.Status, PA.EmailAddress " + 
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
                                    sResult.Status = Convert.ToInt32(sReader["Status"]);
                                    sResult.EmailAddress = sReader["EmailAddress"].ToString();
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
				logger.Error("AppointmentRepository >>> GetAppointmentByID >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Appointment Details by UniqueIDKey
        /// </summary>
        /// <param name="config"></param>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public static AppointmentListModel GetAppointmentByUniqueID(IConfiguration config, String uniqueID)
        {
            AppointmentListModel AppointmentObj = new AppointmentListModel();

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.*, P.Name AS 'PetName', PA.Name AS 'OwnerName', b.ServicesID, C.Name AS 'ServiceName', PP.ID AS 'PatientID' " +
                                            "FROM mst_appointment AS A INNER JOIN mst_appointment_services AS b ON b.ApptID = A.AppointmentID " +
                                            "LEFT JOIN mst_services AS C ON C.ID = b.ServicesID " +
                                            "LEFT JOIN mst_pets AS P ON P.ID = A.PetID " +
                                            "LEFT JOIN mst_patients_owner AS PA ON PA.ID = A.OwnerID " +
                                            "LEFT JOIN mst_patients AS PP ON PP.ID = PA.PatientID " +
                                            "WHERE A.UniqueIDKey = '" + uniqueID  + "' and B.IsDeleted = 0";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            if (sReader.HasRows)
                            {
                                while (sReader.Read())
                                {
                                    AppointmentObj.UniqueIDKey = sReader["UniqueIDKey"].ToString();
                                    AppointmentObj.AppointmentID = Convert.ToInt64(sReader["AppointmentID"]);
                                    AppointmentObj.ApptDate = Convert.ToDateTime(sReader["ApptDate"]);

                                    TimeSpan tmStart = (TimeSpan)sReader["ApptStartTime"];
                                    AppointmentObj.ApptStartTime = DateTime.Now.Add(tmStart);

                                    TimeSpan tmEnd = (TimeSpan)sReader["ApptEndTime"];
                                    AppointmentObj.ApptEndTime = DateTime.Now.Add(tmEnd);

                                    AppointmentObj.ServiceName = sReader["ServiceName"].ToString();
                                    AppointmentObj.PatientID = Convert.ToInt64(sReader["PatientID"]);
                                    AppointmentObj.PetID = Convert.ToInt64(sReader["PetID"]);
                                    AppointmentObj.PetName = sReader["PetName"].ToString();
                                    AppointmentObj.DoctorName = sReader["InchargeDoctor"].ToString();
                                    AppointmentObj.OwnerID = Convert.ToInt64(sReader["OwnerID"]);
                                    AppointmentObj.OwnerName = sReader["OwnerName"].ToString();
                                    AppointmentObj.BranchID = Convert.ToInt32(sReader["BranchID"]);

                                    if (sReader["CreatedDate"] != null && !String.IsNullOrEmpty(sReader["CreatedDate"].ToString()))
                                    {
                                        AppointmentObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                    }
                                    AppointmentObj.CreatedBy = sReader["CreatedBy"].ToString();

                                    if (sReader["UpdatedDate"] != null && !String.IsNullOrEmpty(sReader["UpdatedDate"].ToString()))
                                    {
                                        AppointmentObj.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                    }
                                    AppointmentObj.UpdatedBy = sReader["UpdatedBy"].ToString();
                                }
                            }
                        }
                    }
                    sConn.Close();

                }

                return AppointmentObj;
            }
            catch (Exception ex)
            {
				logger.Error("AppointmentRepository >>> GetAppointmentByUniqueID >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Appointment list by Created Date or Updated Date
        /// </summary>
        /// <param name="config"></param>
        /// <param name="isNewAppt"></param>
        /// <param name="sStart"></param>
        /// <param name="sEnd"></param>
        /// <returns></returns>
        public static List<AppointmentListModel> GetAppointmentByDateRange(IConfiguration config, Boolean isNewAppt, DateTime sStart, DateTime sEnd)
        {
            List<AppointmentListModel> AppointmentList = new List<AppointmentListModel>();

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String selectCommand = "SELECT A.*, P.Name AS 'PetName', PA.Name AS 'OwnerName', b.ServicesID, C.Name AS 'ServiceName', PP.ID AS 'PatientID' " +
                                           "FROM mst_appointment AS A " +
                                           "INNER JOIN mst_appointment_services AS b ON b.ApptID = A.AppointmentID " +
                                           "LEFT JOIN mst_services AS C ON C.ID = b.ServicesID " +
                                           "LEFT JOIN mst_pets AS P ON P.ID = A.PetID " +
                                           "LEFT JOIN mst_patients_owner AS PA ON PA.ID = A.OwnerID " +
                                           "LEFT JOIN mst_patients AS PP ON PP.ID = PA.PatientID " +
                                           "WHERE (" + (isNewAppt == true) + " AND A.CreatedDate >= '" + sStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND A.CreatedDate <= '" + sEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' AND A.UpdatedDate Is Null) OR " +
                                           "      (" + (isNewAppt == false) + " AND A.UpdatedDate >= '" + sStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND A.UpdatedDate <= '" + sEnd.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    using (MySqlCommand sCommand = new MySqlCommand(selectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            if (sReader.HasRows)
                            {
                                while (sReader.Read())
                                {
                                    TimeSpan tmStart = (TimeSpan)sReader["ApptStartTime"];
                                    TimeSpan tmEnd = (TimeSpan)sReader["ApptEndTime"];

                                    DateTime sCreatedDate = DateTime.MinValue;
                                    if (sReader["CreatedDate"] != null && !String.IsNullOrEmpty(sReader["CreatedDate"].ToString()))
                                    {
                                        sCreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                    }

                                    DateTime sUpdatedDate = DateTime.MinValue;
                                    if (sReader["UpdatedDate"] != null && !String.IsNullOrEmpty(sReader["UpdatedDate"].ToString()))
                                    {
                                        sUpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                    }

                                    AppointmentList.Add(new AppointmentListModel
                                    {
                                        UniqueIDKey = sReader["UniqueIDKey"].ToString(),
                                        AppointmentID = Convert.ToInt64(sReader["AppointmentID"]),
                                        ApptDate = Convert.ToDateTime(sReader["ApptDate"]),
                                        ApptStartTime = DateTime.Now.Add(tmStart),
                                        ApptEndTime = DateTime.Now.Add(tmEnd),
                                        ServiceName = sReader["ServiceName"].ToString(),
                                        PatientID = Convert.ToInt64(sReader["PatientID"]),
                                        PetID = Convert.ToInt64(sReader["PetID"]),
                                        PetName = sReader["PetName"].ToString(),
                                        DoctorName = sReader["InchargeDoctor"].ToString(),
                                        OwnerID = Convert.ToInt64(sReader["OwnerID"]),
                                        OwnerName = sReader["OwnerName"].ToString(),
                                        BranchID = Convert.ToInt32(sReader["BranchID"]),
                                        CreatedDate = (sCreatedDate != DateTime.MinValue) ? 
                                                        sCreatedDate : 
                                                        null,
                                        CreatedBy = sReader["CreatedBy"].ToString(),
                                        UpdatedDate = (sUpdatedDate != DateTime.MinValue) ? 
                                                        sUpdatedDate : 
                                                        null,
                                        UpdatedBy = sReader["UpdatedBy"].ToString()
                                    });
                                }
                            }
                        }
                    }

                    sConn.Close();

                    return AppointmentList;
                }
            }
            catch (Exception ex)
            {
				logger.Error("AppointmentRepository >>> GetAppointmentByDateRange >>> ", ex);
				return null;
            }
        }

		/// <summary>
		/// Get Upcoming Appointment by Owner ID or Pet ID
		/// </summary>
		/// <param name="config"></param>
		/// <param name="ownerID"></param>
		/// <param name="petID"></param>
		/// <returns></returns>
		public static UpcomingAppointment GetUpcomingAppointment(IConfiguration config, int ownerID, int petID)
        {
            UpcomingAppointment upcomingAppointment = new UpcomingAppointment();

            string queryFilter;

            if (ownerID != 0)
            {
                queryFilter = "AND a.OwnerID = " + ownerID + " ";
            }
            else
            {
                queryFilter = "AND a.PetID = " + petID + " ";
            }

            var selectCommand =
				"select a.ApptDate as 'Date', a.ApptStartTime as 'StartTime', a.ApptEndTime as 'EndTime', c.Name as 'PetName', d.name as 'ServiceName', a.InchargeDoctor from mst_appointment a " +
                "inner join mst_appointment_services b on b.ApptID = a.AppointmentID " +
                "inner join mst_pets c on a.PetID = c.ID " +
                "inner join mst_services d on d.ID = b.ServicesID " +
				"where (a.ApptDate > current_date() OR (a.ApptDate = current_date() AND a.ApptStartTime > current_time())) " + queryFilter +
                "Order By a.ApptDate, a.ApptStartTime " +
                "LIMIT 0,1;";

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    using (MySqlCommand sCommand = new MySqlCommand(selectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            if (sReader.HasRows)
                            {
                                while (sReader.Read())
                                {
                                    upcomingAppointment.ApptDate = DateOnly.FromDateTime(DateTime.Parse(sReader["Date"].ToString()));
                                    upcomingAppointment.ApptStartTime = TimeOnly.FromDateTime(DateTime.Parse(sReader["StartTime"].ToString()));
									upcomingAppointment.ApptEndTime = TimeOnly.FromDateTime(DateTime.Parse(sReader["EndTime"].ToString()));
									upcomingAppointment.PetName = sReader["PetName"].ToString();
                                    upcomingAppointment.Service = sReader["ServiceName"].ToString();
                                    upcomingAppointment.Doctor = sReader["InchargeDoctor"].ToString();
                                }
                            }
                        }
                    }

                    sConn.Close();
                }
            }
            catch (Exception ex)
            {
				logger.Error("AppointmentRepository >>> GetUpcomingAppointment >>> ", ex);
			}

            return upcomingAppointment;
        }

        /// <summary>
        /// Get Upcoming Appointments
        /// </summary>
        /// <param name="config"></param>
        /// <param name="doctor"></param>
        /// <param name="organisationID"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public static List<UpcomingAppointment> GetTodayUpcomingAppointmentList(IConfiguration config, string doctor, int organisationID, int branchID)
        {
            List<UpcomingAppointment> upcomingAppointmentList = new List<UpcomingAppointment>();
            string tomorow = DateTime.Now.AddDays(1).Date.ToString("yyyy-MM-dd HH:mm:ss");
            string doctorFilter = "";
            string roleFilter = "";

            if(doctor != null && doctor != "")
            {
                doctorFilter = "AND a.InchargeDoctor = '"+ doctor + "'";
            }

            if (organisationID != 0)
            {
                roleFilter = "AND f.OrganizationID = " + organisationID + " ";
            }
            else if (branchID != 0)
            {
                roleFilter = "AND f.ID = " + branchID + " ";
            }

            var selectCommand =
                "select a.ApptDate as 'Date', a.ApptStartTime as 'StartTime', a.ApptEndTime as 'EndTime', c.Name as 'PetName', d.name as 'ServiceName', a.InchargeDoctor, e.Gender from mst_appointment a " +
                "inner join mst_appointment_services b on b.ApptID = a.AppointmentID " +
                "inner join mst_pets c on a.PetID = c.ID " +
                "inner join mst_services d on d.ID = b.ServicesID " +
                "inner join mst_doctor e on e.Name = REPLACE(a.InchargeDoctor, \"Dr. \", \"\") " +
                "inner join mst_branch f on f.ID = e.BranchID " + roleFilter +
                "where a.ApptDate = current_date() AND a.ApptStartTime > current_time() AND a.ApptDate < '" + tomorow + "' " + doctorFilter +
                "Order By a.ApptDate, a.ApptStartTime " +
                "LIMIT 0,15;";

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    using (MySqlCommand sCommand = new MySqlCommand(selectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            if (sReader.HasRows)
                            {
                                while (sReader.Read())
                                {
                                    upcomingAppointmentList.Add(new UpcomingAppointment()
                                    {
                                        ApptDate = DateOnly.FromDateTime(DateTime.Parse(sReader["Date"].ToString())),
                                        ApptStartTime = TimeOnly.FromDateTime(DateTime.Parse(sReader["StartTime"].ToString())),
                                        ApptEndTime = TimeOnly.FromDateTime(DateTime.Parse(sReader["EndTime"].ToString())),
                                        PetName = sReader["PetName"].ToString(),
                                        Service = sReader["ServiceName"].ToString(),
                                        Doctor = sReader["InchargeDoctor"].ToString(),
                                        Gender = sReader["Gender"].ToString()
                                    });
                                }
                            }
                        }
                    }

                    sConn.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("AppointmentRepository >>> GetTodayUpcomingAppointmentList >>> ", ex);
            }

            return upcomingAppointmentList;
        }

        /// <summary>
        /// Get expired Appointment
        /// </summary>
        /// <param name="config"></param>
        /// <param name="dtTarget"></param>
        /// <returns></returns>
        public static List<AppointmentModel> GetExpiredAppointment(IConfiguration config, DateTime dtTarget)
        {
            List<AppointmentModel> sResultList = new List<AppointmentModel>();

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.AppointmentID, A.UniqueIDKey, A.BranchID, A.ApptDate, ApptStartTime, " +
                                            "A.ApptEndTime, A.OwnerID, A.PetID, A.Status, A.EmailNotify, A.InchargeDoctor, " +
                                            "A.CreatedDate, A.CreatedBy, A.UpdatedDate, A.UpdatedBy " +
                                            "FROM mst_appointment as A " +
                                            "WHERE A.ApptDate < '" + dtTarget.ToString("yyyy-MM-dd") + "' AND " +
                                            "A.STATUS IN (0) ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            if (sReader.HasRows)
                            {
                                while (sReader.Read())
                                {
                                    AppointmentModel AppointmentObj = new AppointmentModel();
                                    AppointmentObj.UniqueIDKey = sReader["UniqueIDKey"].ToString();
                                    AppointmentObj.AppointmentID = Convert.ToInt64(sReader["AppointmentID"]);
                                    AppointmentObj.ApptDate = Convert.ToDateTime(sReader["ApptDate"]);

                                    TimeSpan tmStart = (TimeSpan)sReader["ApptStartTime"];
                                    AppointmentObj.ApptStartTime = DateTime.Now.Add(tmStart);

                                    TimeSpan tmEnd = (TimeSpan)sReader["ApptEndTime"];
                                    AppointmentObj.ApptEndTime = DateTime.Now.Add(tmEnd);
                                    AppointmentObj.PetID = Convert.ToInt64(sReader["PetID"]);
                                    AppointmentObj.OwnerID = Convert.ToInt64(sReader["OwnerID"]);
                                    AppointmentObj.BranchID = Convert.ToInt32(sReader["BranchID"]);

                                    if (sReader["CreatedDate"] != null && !String.IsNullOrEmpty(sReader["CreatedDate"].ToString()))
                                    {
                                        AppointmentObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                    }
                                    AppointmentObj.CreatedBy = sReader["CreatedBy"].ToString();

                                    if (sReader["UpdatedDate"] != null && !String.IsNullOrEmpty(sReader["UpdatedDate"].ToString()))
                                    {
                                        AppointmentObj.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                    }
                                    AppointmentObj.UpdatedBy = sReader["UpdatedBy"].ToString();


                                    sResultList.Add(AppointmentObj);
                                }
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("AppointmentRepository >>> GetExpiredAppointment >>> " + ex.ToString());
                return null;
            }
        }
    }

}
