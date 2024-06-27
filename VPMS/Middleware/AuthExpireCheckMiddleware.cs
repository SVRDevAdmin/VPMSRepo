using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;

namespace VPMSWeb.Middleware
{
    public class AuthExpireCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CookieAuthenticationOptions _options;

        // obtain CookieAuthenticationOptions object by DI  
        public AuthExpireCheckMiddleware(
            RequestDelegate next,
            IOptions<CookieAuthenticationOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);

            // check only if requested page is "Login"  
            // "/Identity/Account/Login" is default setting by Visual Studio  
            if (context.Request.Path.ToString().
                                     Contains("/Identity/Account/Login"))
            {
                // check only if auth cookie has been recieved  
                // cookiew name ".AspNetCore.Identity.Application"is default  
                // setting by Visual Studio  
                string cookie
                    = context.Request.Cookies[".AspNetCore.Identity.Application"];
                if (!string.IsNullOrEmpty(cookie))
                {
                    IDataProtectionProvider provider =
                        _options.DataProtectionProvider;
                    IDataProtector protector = provider.CreateProtector(
                        "Microsoft.AspNetCore.Authentication.Cookies." +
                        "CookieAuthenticationMiddleware",
                        "Identity.Application",
                        "v2");

                    // decriypt auth ticket in auth cookie  
                    TicketDataFormat format = new TicketDataFormat(protector);
                    AuthenticationTicket authTicket = format.Unprotect(cookie);

                    // get user name  
                    ClaimsPrincipal principal = authTicket.Principal;
                    IIdentity identity = principal.Identity;
                    string userName = identity.Name;

                    // get expiration datetime of auth ticket  
                    AuthenticationProperties property = authTicket.Properties;
                    DateTimeOffset? expiersUrc = property.ExpiresUtc;

                    // check if auth ticket has been expired  
                    if (expiersUrc.Value < DateTimeOffset.UtcNow)
                    {
                        // do something to notify that auth ticket has been expired  
                    }
                }
            }
        }
    }
}
