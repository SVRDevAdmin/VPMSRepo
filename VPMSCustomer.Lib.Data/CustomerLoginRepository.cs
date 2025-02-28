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
                isSuccess = false;
            }

            return isSuccess;
        }

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
                isSuccess = false;
            }

            return isSuccess;
        }

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
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
