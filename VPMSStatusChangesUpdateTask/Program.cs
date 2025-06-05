using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI;
using System.Net.NetworkInformation;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using log4net.Repository;
using System.Reflection;

namespace VPMSStatusChangesUpdateTask
{
    public class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static String sAppointmentStatusTaskName = "StatusChangesTask";
        static String sLoginSessionAutoTaskName = "SessionAutoLogoutTask";
        static IConfiguration config;

        static void Main(string[] args)
        {
            ILoggerRepository repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));

            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                var sBuilder = new ConfigurationBuilder();
                sBuilder.SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                config = sBuilder.Build();


                AppointmentStatusUpdate();
                LoginSessionStatusUpdate();
            }
        }

        private static void AppointmentStatusUpdate()
        {
            DateTime dtStart = DateTime.Now;
            int iStatusCompleted = 1;

            log.Info("Execute Appointment Status Update BEGIN");

            try
            {
                var sApptList = AppointmentRepository.GetExpiredAppointment(config, dtStart);
                if (sApptList != null && sApptList.Count > 0)
                {
                    log.Info("Total Appointment Records : " + sApptList.Count.ToString());

                    foreach (var appt in sApptList)
                    {
                        if (AppointmentRepository.UpdateAppointmentStatus(config, appt.AppointmentID, iStatusCompleted, sAppointmentStatusTaskName))
                        {
                            log.Info("Status updated complete for Appointment Record [AppointmentID : " + appt.AppointmentID.ToString() + "] ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("VPMSStatusChangesUpdateTask >>> AppointmentStatusUpdate >>> " + ex.ToString());
            }

            log.Info("Execute Appointment Status Update COMPLETED.");
        }

        private static void LoginSessionStatusUpdate()
        {
            DateTime dtNow = DateTime.Now;

            log.Info("Execute Login Session Checking BEGIN.");

            try
            {
                var sExpiredSessionList = SessionRepository.GetExpiredLoginSession();
                if (sExpiredSessionList != null && sExpiredSessionList.Count > 0)
                {
                    log.Info("Total Login Session records : " + sExpiredSessionList.Count.ToString());

                    foreach (var s in sExpiredSessionList)
                    {
                        LoginSessionLogModel sLogObject= new LoginSessionLogModel();
                        sLogObject.ActionType = "auto-logout";
                        sLogObject.SessionID = s.SessionID;
                        sLogObject.SessionCreatedOn = s.SessionCreatedOn;
                        sLogObject.SessionExpiredOn = s.SessionExpiredOn;
                        sLogObject.UserID = s.UserID;
                        sLogObject.LoginID = s.LoginID;
                        sLogObject.CreatedDate = DateTime.Now;
                        sLogObject.CreatedBy = sLoginSessionAutoTaskName;

                        log.Info("Archives Login Session [" + s.SessionID + "] ");
                        if (SessionRepository.InsertLoginSessionLog(sLogObject))
                        {
                            log.Info("Cleared Login Session [" + s.SessionID  + "] from main table.");
                            SessionRepository.DeleteLoginSession(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("VPMSStatusChangesUpdateTask >>> LoginSessionStatusUpdate >>> " + ex.ToString());
            }

            log.Info("Execute Login Session Checking COMPLETED.");
        }
    }
}
