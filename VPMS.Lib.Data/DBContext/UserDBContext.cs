using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class UserDBContext : DbContext
    {
        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        public DbSet<UserModel> Mst_User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));



		public StaffProfileInfo GetCurrentStaffProfile(string id)
		{
			StaffProfileInfo staffProfileInfo = new StaffProfileInfo()
			{
				Surname = "Kim",
				LastName = "Ji-won",
				StaffID = "ABC1234567",
				Gender = "Female",
				Role = "Senior Veterinarian",
				Organisation = "Itaewon Animal Hospital",
				Email = "Kim@gmail.com",
				Branch = "HQ",
				Status = 1
			};

			return staffProfileInfo;
		}
	}
}
