using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Middleware
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private const string TicketIssuedTicks = nameof(TicketIssuedTicks);
        private readonly LoginSessionDBContext _loginSessionDBContext = new LoginSessionDBContext();

        public override async Task SigningIn(CookieSigningInContext context)
        {
            context.Properties.SetString(
                TicketIssuedTicks,
                DateTimeOffset.UtcNow.Ticks.ToString());

            await base.SigningIn(context);
        }

        public override async Task ValidatePrincipal(
            CookieValidatePrincipalContext context)
        {
            var ticketIssuedTicksValue = context
                .Properties.GetString(TicketIssuedTicks);
            var idleSessionCheck = context.Properties.IssuedUtc;

            if (ticketIssuedTicksValue is null ||
                !long.TryParse(ticketIssuedTicksValue, out var ticketIssuedTicks))
            {
                await RejectPrincipalAsync(context);
                return;
            }

            var ticketIssuedUtc =
                new DateTimeOffset(ticketIssuedTicks, TimeSpan.FromHours(0));

            if ((DateTimeOffset.UtcNow - ticketIssuedUtc > TimeSpan.FromHours(1)) /*|| (DateTimeOffset.UtcNow - idleSessionCheck > TimeSpan.FromMinutes(5))*/)
            {
                //var loginSessionLog = _loginSessionDBContext.Txn_LoginSession_Log.OrderByDescending(x => x.CreatedDate).FirstOrDefault(x => x.LoginID == context.Principal.Identity.Name);

                //var newLoginSessionLog = new LoginSessionLogModel() { ActionType = "expired-logout", SessionID = loginSessionLog.SessionID, SessionCreatedOn = loginSessionLog.SessionCreatedOn, SessionExpiredOn = loginSessionLog.SessionExpiredOn, UserID = loginSessionLog.UserID, LoginID = loginSessionLog.LoginID, CreatedDate = DateTime.Now, CreatedBy = loginSessionLog.LoginID };

                //_loginSessionDBContext.Txn_LoginSession_Log.Add(newLoginSessionLog);

                //_loginSessionDBContext.SaveChanges();


                await RejectPrincipalAsync(context);
                return;
            }

            await base.ValidatePrincipal(context);
        }

        private static async Task RejectPrincipalAsync(
            CookieValidatePrincipalContext context)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync();
        }
    }
}
