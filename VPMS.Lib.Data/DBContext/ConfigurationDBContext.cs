using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
	public class ConfigurationDBContext : DbContext
	{
		private readonly IConfiguration config;
		public DbSet<ConfigurationModel> mst_users_configuration { get; set; }

		public ConfigurationDBContext(IConfiguration config)
		{
			this.config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
		}
	}
}