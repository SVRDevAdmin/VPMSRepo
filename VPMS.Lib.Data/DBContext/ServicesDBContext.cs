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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VPMS.Lib.Data.DBContext
{
	public class ServicesDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public DbSet<ServicesModel> Mst_Services { get; set; }
		public DbSet<ServiceCategory> Mst_ServicesCategory { get; set; }
		public DbSet<ServiceDoctor> Mst_Service_Doctor { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public ObservableCollection<ServiceList> GetServiceList(int start, int total, int branch, int organisation, out int totalServices, string search = "")
		{
			ObservableCollection<ServiceList> sList = new ObservableCollection<ServiceList>();
			int No = start + 1;
			totalServices = 0;
            var roleFilter = "";

            if (branch != 0)
            {
                roleFilter += "AND c.ID = " + branch + " ";
            }
            else if (organisation != 0)
            {
                roleFilter += "AND d.ID = " + organisation + " ";
            }

            var filter = "WHERE (a.name like '%" + search + "%' OR b.name like '%" + search + "%' OR a.DoctorInCharge like '%" + search + "%') "+ roleFilter + " ";
			var joinQuery = 
				"join mst_servicescategory b on b.ID = a.CategoryID " +
				"join mst_branch c on c.ID = a.BranchID " +
				"join mst_organisation d on d.ID = c.OrganizationID ";

			var totalServiceQuery = "(select Count(a.Name) from mst_services a "+ joinQuery + filter + ")";
			var completeQuery = "select a.Name, a.ID, b.SubCategoryName as 'Category', a.Prices, a.DoctorInCharge, d.Name as 'Organisation', c.Name as 'Branch', a.Duration, a.Status, "+ totalServiceQuery + " as 'TotalServices' from mst_services a " +
				joinQuery + filter + "Order by a.ID LIMIT " + start + ", " + total + ";";

			try
			{
				using (MySqlConnection conn = new MySqlConnection(connectionString))
				{
					conn.Open();
					MySqlCommand cmd = new MySqlCommand(completeQuery, conn);

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							sList.Add(new ServiceList()
							{

								ID = int.Parse(reader["ID"].ToString()),
								No = No++,
								Name = reader["Name"].ToString(),
								Category = reader["Category"].ToString(),
								Price = float.Parse(reader["Prices"].ToString()),
								DoctorInCharge = reader["DoctorInCharge"].ToString(),
								Organisation = reader["Organisation"].ToString(),
								Branch = reader["Branch"].ToString(),
								Duration = float.Parse(reader["Duration"].ToString()),
								Status = int.Parse(reader["Status"].ToString())
							});

							totalServices = Convert.ToInt32(reader["TotalServices"]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Error("Database Error >> ", ex);
			}

			return sList;
		}
	}
}
