using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
	public class CountryDBContext : DbContext
	{
		private readonly IConfiguration config;
		public DbSet<CountryListModel> mst_countrylist { get; set; }
		public DbSet<StateModel> mst_state { get; set; }
		public DbSet<CityModel> mst_city{ get; set; }
		public DbSet<CurrencyModel> mst_currency { get; set; }

		public CountryDBContext(IConfiguration config)
		{
			this.config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
		}
	}
}