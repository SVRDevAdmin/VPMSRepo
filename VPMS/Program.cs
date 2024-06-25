using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VPMS.Lib.Data.DBContext;

namespace VPMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Login/Login";
                options.AccessDeniedPath = "/Login/AccessDenied";
                options.SlidingExpiration = true;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
