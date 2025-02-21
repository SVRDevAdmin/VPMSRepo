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
                //todo
                return null;
            }
        }

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
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
