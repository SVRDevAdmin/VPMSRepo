using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using ZstdSharp.Unsafe;

namespace VPMS.Lib.Data
{
    public class SessionRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Expired Login Session List
        /// </summary>
        /// <returns></returns>
        public static List<LoginSessionModel> GetExpiredLoginSession()
        {
            List<LoginSessionModel> sResultList = new List<LoginSessionModel>();
            DateTime dtNow = DateTime.Now;

            try
            {
                using (var ctx = new LoginSessionDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT T.ID, T.SessionID, T.SessionCreatedOn, T.SessionExpiredOn, " +
                                            "T.UserID, T.LoginID " +
                                            "FROM txn_loginsession AS T " +
                                            "WHERE T.SessionExpiredOn < '" + dtNow.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

                    using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = cmd.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                LoginSessionModel sSessionObj = new LoginSessionModel();
                                sSessionObj.ID = Convert.ToInt32(sReader["ID"]);
                                sSessionObj.SessionID = sReader["SessionID"].ToString();
                                sSessionObj.SessionCreatedOn = Convert.ToDateTime(sReader["SessionCreatedOn"]);
                                sSessionObj.SessionExpiredOn = Convert.ToDateTime(sReader["SessionExpiredOn"]);
                                sSessionObj.UserID = sReader["UserID"].ToString();
                                sSessionObj.LoginID = sReader["LoginID"].ToString();

                                sResultList.Add(sSessionObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("SessionRepository >>> GetExpiredLoginSession >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Remove Login Session record
        /// </summary>
        /// <param name="sSessionObj"></param>
        /// <returns></returns>
        public static Boolean DeleteLoginSession(LoginSessionModel sSessionObj)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new LoginSessionDBContext())
                {
                    ctx.Txn_LoginSession.Remove(sSessionObj);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("SessionRepository >>> DeleteLoginSession >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Insert to Session Log
        /// </summary>
        /// <param name="sLogObject"></param>
        /// <returns></returns>
        public static Boolean InsertLoginSessionLog(LoginSessionLogModel sLogObject)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new LoginSessionDBContext())
                {
                    ctx.Txn_LoginSession_Log.Add(sLogObject);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("SessionRepository >>> InsertLoginSessionLog >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
