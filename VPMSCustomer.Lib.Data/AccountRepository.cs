using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class AccountRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Account Creation Log record by Code
        /// </summary>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public static AccountCreationModel GetInvitationLinkDetails(String invitationCode)
        {
            try
            {
                using (var ctx = new AccountDBContext())
                {
                    return ctx.mst_account_creation_logs.Where(x => x.InvitationCode == invitationCode).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("AccountRepository >>> GetInvitationLinkDetails >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Update Account Creation Status
        /// </summary>
        /// <param name="invitationCode"></param>
        /// <param name="dtActivation"></param>
        /// <returns></returns>
        public static Boolean UpdateInvitationLinkStatus(String invitationCode, DateTime dtActivation)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AccountDBContext())
                {
                    var sCreationLog = ctx.mst_account_creation_logs.Where(x => x.InvitationCode == invitationCode).FirstOrDefault();
                    if (sCreationLog != null)
                    {
                        sCreationLog.AccountCreationDate = dtActivation;
                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("AccountRepository >>> UpdateInvitationLinkStatus >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
