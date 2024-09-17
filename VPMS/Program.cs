using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMSWeb.Middleware;

namespace VPMS
{
    public class Program
    {
        public static ConfigurationModel LanguageSelected { get; set; }
        public static ConfigurationModel CountrySelected { get; set; }
        public static List<MastercodeModel> LanguageCodeList { get; set; }
        public static List<CountryListModel> CountryList { get; set; }
        public static MastercodeModel LanguageFullNameSelected { get; set; }
        public static VPMSWeb.Interface.IResourcesLocalizer _LangResources { get; set; }

        public static string CurrentPage { get; set; }

		public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

			builder.Services.AddLocalization(options =>
			{
				options.ResourcesPath = "Resources";
			});
			builder.Services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new[]
				 {
					new CultureInfo("en-US"),
					new CultureInfo("zh-Hans")
				};

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            builder.Services.AddSingleton<VPMSWeb.Interface.IResourcesLocalizer, VPMSWeb.Lib.ResourcesLocalizer>();

			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<MembershipDBContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MembershipDBContext>();

            var identitySettings = builder.Configuration.GetSection("IdentitySettings");

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireNonAlphanumeric = identitySettings.GetValue<bool>("RequireNonAlphanumeric"); ;
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

                options.EventsType = typeof(CustomCookieAuthenticationEvents);
            });

            builder.Services.AddTransient<CustomCookieAuthenticationEvents>();

            //Added for session state
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            var app = builder.Build();

			app.UseRequestLocalization();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
