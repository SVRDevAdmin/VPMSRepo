using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class TreatmentPlanDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

		public DbSet<TreatmentPlanModel> Mst_TreatmentPlan { get; set; }
		public DbSet<TreatmentPlanService> Mst_TreatmentPlan_Services { get; set; }
		public DbSet<TreatmentPlanProduct> Mst_TreatmentPlan_Products { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public List<TreatmentPlanModel> GetTreatmentPlanList(int start, int total, out int totalTreatmentPlan, string search = "")
		{
			var sResult = Mst_TreatmentPlan.Where(x => x.Name.Contains(search) || x.Remarks.Contains(search) || x.CreatedBy.Contains(search)).ToList();
			totalTreatmentPlan = sResult.Count();
			return sResult.Skip(start).Take(total).ToList();
		}
	}
}
