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
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly BranchDBContext _branchDBContext = new BranchDBContext();

        public DbSet<TreatmentPlanModel> Mst_TreatmentPlan { get; set; }
		public DbSet<TreatmentPlanService> Mst_TreatmentPlan_Services { get; set; }
		public DbSet<TreatmentPlanProduct> Mst_TreatmentPlan_Products { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public List<TreatmentPlanModel> GetTreatmentPlanList(int start, int total, int branch, int organisation, out int totalTreatmentPlan, string search = "")
		{
			List<TreatmentPlanModel> sResult = new List<TreatmentPlanModel>();
			totalTreatmentPlan = 0;

            try
			{
				if(branch != 0)
				{
                    sResult = Mst_TreatmentPlan.Where(x => (x.Name.Contains(search) || x.Remarks.Contains(search) || x.CreatedBy.Contains(search)) && x.BranchID == branch).ToList();
                }
				else if(organisation != 0)
				{
                    List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
                    sResult = Mst_TreatmentPlan.Where(x => (x.Name.Contains(search) || x.Remarks.Contains(search) || x.CreatedBy.Contains(search)) && branchList.Contains(x.BranchID)).ToList();
                }
				else
				{
                    sResult = Mst_TreatmentPlan.Where(x => x.Name.Contains(search) || x.Remarks.Contains(search) || x.CreatedBy.Contains(search)).ToList();
                }
				
				totalTreatmentPlan = sResult.Count();

				sResult = sResult.Skip(start).Take(total).ToList();
			}
			catch (Exception ex)
			{
				logger.Error("Database Error >> ", ex);
			}

			return sResult;
		}
	}
}
