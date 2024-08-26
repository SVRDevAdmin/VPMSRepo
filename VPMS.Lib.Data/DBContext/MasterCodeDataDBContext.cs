using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
	public class MasterCodeDataDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

		public DbSet<MasterCodeDataModel> Mst_MasterCodeData { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

	}
}
