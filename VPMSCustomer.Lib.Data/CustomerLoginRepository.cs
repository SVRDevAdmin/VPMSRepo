using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class CustomerLoginRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Insert Session record
        /// </summary>
        /// <param name="sSession"></param>
        /// <param name="sAction"></param>
        /// <returns></returns>
        public static Boolean InsertSession(CustomerLoginSession sSession, String sAction)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new CustomerLoginDBContext())
                {
                    ctx.txn_customer_loginsession.Add(sSession);
                    ctx.SaveChanges();

                    CustomerLoginSessionLog sLog = new CustomerLoginSessionLog();
                    sLog.ActionType = sAction;
                    sLog.SessionID = sSession.SessionID;
                    sLog.SessionCreatedOn = sSession.SessionCreatedOn;
                    sLog.SessionExpiredOn = sSession.SessionExpiredOn; 
                    sLog.LoginID = sSession.LoginID;
                    sLog.CreatedDate = DateTime.Now;

                    ctx.txn_customer_loginsession_log.Add(sLog);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("CustomerLoginRepository >>> InsertSession >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Remove Session Record
        /// </summary>
        /// <param name="sSessionID"></param>
        /// <returns></returns>
        public static Boolean DeleteSession(String sSessionID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new CustomerLoginDBContext())
                {
                    var sResult = ctx.txn_customer_loginsession.Where(x => x.SessionID == sSessionID).FirstOrDefault();
                    if (sResult != null)
                    {
                        ctx.txn_customer_loginsession.Remove(sResult);
                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("CustomerLoginRepository >>> DeleteSession >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Insert Summary Log
        /// </summary>
        /// <param name="sSessionID"></param>
        /// <param name="sAction"></param>
        /// <returns></returns>
        public static Boolean InsertSessionLog(String sSessionID, String sAction)
        {
            Boolean isSuccess = false;
           
            try
            {
                using (var ctx = new CustomerLoginDBContext())
                {
                    var sSessionLog = ctx.txn_customer_loginsession.Where(x => x.SessionID == sSessionID).FirstOrDefault();
                    if (sSessionLog != null)
                    {
                        CustomerLoginSessionLog sLog = new CustomerLoginSessionLog();
                        sLog.ActionType = sAction;
                        sLog.SessionID = sSessionLog.SessionID;
                        sLog.SessionCreatedOn = sSessionLog.SessionCreatedOn;
                        sLog.SessionExpiredOn = sSessionLog.SessionExpiredOn;
                        sLog.LoginID = sSessionLog.LoginID;
                        sLog.CreatedDate = DateTime.Now;

                        ctx.txn_customer_loginsession_log.Add(sLog);
                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("CustomerLoginRepository >>> InsertSessionLog >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Validate Session ID validity
        /// </summary>
        /// <param name="sSessionID"></param>
        /// <returns></returns>
        public static Boolean ValidateSession(String sSessionID)
        {
            Boolean isValid = false;
            DateTime sNow = DateTime.Now;

            try
            {
                using (var ctx = new CustomerLoginDBContext())
                {
                    var sResult = ctx.txn_customer_loginsession.Where(x => x.SessionID == sSessionID).FirstOrDefault();
                    if (sResult != null)
                    {
                        if (sResult.SessionExpiredOn < sNow)
                        {
                            isValid = false;
                        }
                        else
                        {
                            isValid = true;
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("CustomerLoginRepository >>> ValidateSession >>> " + ex.ToString());
                isValid = false;
            }

            return isValid;
        }
    }
}
