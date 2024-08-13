using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace VPMS.Lib.Data.DBContext
{
	public class MastercodeDBContext : DbContext
	{
		private readonly IConfiguration config;
		public DbSet<MastercodeModel> mst_mastercodedata { get; set; }

		public MastercodeDBContext(IConfiguration config)
		{
			this.config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
		}
	}
}