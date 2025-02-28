using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Globalization;
using VPMSCustomer.Lib.Data.DBContext;

namespace VPMSCustomer
{
    public class Program
    {
        public static VPMSCustomer.Interface.IResourcesLocalizer _LangResources { get; set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // --- Resources ----//
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            String? strConnection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<MembershipDBContext>(options => options.UseMySql(strConnection, ServerVersion.AutoDetect(strConnection)));
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<MembershipDBContext>();
            builder.Services.AddSingleton<VPMSCustomer.Interface.IResourcesLocalizer, VPMSCustomer.Shared.ResourcesLocalizer>();

            var identitySettings = builder.Configuration.GetSection("IdentitySettings");

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireNonAlphanumeric = identitySettings.GetValue<bool>("RequireNonAlphanumeric");
                options.Password.RequiredLength = identitySettings.GetValue<int>("RequireLength");

                //Lockedout setting
                options.SignIn.RequireConfirmedAccount = identitySettings.GetValue<bool>("RequireConfirmedAccount");
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.GetValue<int>("DefaultLockedOutTimeSpan"));
                options.Lockout.MaxFailedAccessAttempts = identitySettings.GetValue<int>("MaxFailedAccessAttempts");
            });

            var cookieSettings = builder.Configuration.GetSection("CookieSetting");
            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieSettings.GetValue<int>("ExpireTimeSpan"));
                options.ExpireTimeSpan = TimeSpan.FromHours(2); //default are 4 hours

                options.LoginPath = "/Login/Login";
                options.AccessDeniedPath = "/Login/AccessDenied";
                //options.SlidingExpiration = true; //default is false
                //options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

                //-----options.EventsType = typeof(CustomCookieAuthenticationEvents);
            });

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                string cookie = String.Empty;
                if (context.Request.Cookies.TryGetValue("Language", out cookie))
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie);
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                }
                await next.Invoke();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}");
                //pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
