using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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

		public List<TreatmentPlanExtendedModel> GetTreatmentPlanList(int start, int total, int isSuperadmin, int branch, int organisation, out int totalTreatmentPlan, string search = "")
		{
            //List<TreatmentPlanModel> sResult = new List<TreatmentPlanModel>();
            List<TreatmentPlanExtendedModel> sResultList = new List<TreatmentPlanExtendedModel>();
            totalTreatmentPlan = 0;

            try
			{
				//if(branch != 0)
				//{
				//                sResult = Mst_TreatmentPlan.Where(x => (x.Name.Contains(search) || x.Remarks.Contains(search) || x.CreatedBy.Contains(search)) && x.BranchID == branch).ToList();
				//            }
				//else if(organisation != 0)
				//{
				//                List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
				//                sResult = Mst_TreatmentPlan.Where(x => (x.Name.Contains(search) || x.Remarks.Contains(search) || x.CreatedBy.Contains(search)) && branchList.Contains(x.BranchID)).ToList();
				//            }
				//else
				//{
				//                sResult = Mst_TreatmentPlan.Where(x => x.Name.Contains(search) || x.Remarks.Contains(search) || x.CreatedBy.Contains(search)).ToList();
				//            }

				//totalTreatmentPlan = sResult.Count();

				//sResult = sResult.Skip(start).Take(total).ToList();
				MySqlConnection sConn = new MySqlConnection(connectionString);
				sConn.Open();

				String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', " +
										"A.ID, A.Name, A.BranchID, A.Remarks, A.TotalPrice, A.`Status`, A.IsDeleted, " + 
										"A.CreatedDate, A.CreatedBy, B.Name AS 'BranchName', B.OrganizationID, C.Name AS 'OrganizationName', " +
										"COUNT(*) OVER() AS 'TotalRows' " +
										"FROM mst_treatmentplan AS A " +
										"INNER JOIN mst_branch AS B ON B.id = A.BranchID " +
										"INNER JOIN mst_organisation AS C ON C.ID = B.OrganizationID " +
										"WHERE " +
										"(" +
										"(" + (isSuperadmin == 1) + " AND C.Level >=2 AND B.OrganizationID = '" + organisation + "') OR " +
										"(" + (isSuperadmin == 0) + " AND B.OrganizationID = '" + organisation + "' AND A.BranchID = '" + branch + "')" +
										") AND " +
										"(" + (search == "") + " OR " +
										"(A.Name LIKE '%" + search + "%' OR A.Remarks LIKE '%" + search + "%' OR A.CreatedBy LIKE '%" + search + "%') " +
										") " + 
										"LIMIT " + total + " " +
										"OFFSET " + start;

                using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
                {
                    using (var sReader = cmd.ExecuteReader())
                    {
                        while (sReader.Read())
                        {
                            TreatmentPlanExtendedModel sPlanObj = new TreatmentPlanExtendedModel();
							sPlanObj.SeqNo = Convert.ToInt32(sReader["row_num"]);
                            sPlanObj.ID = Convert.ToInt32(sReader["ID"]);
                            sPlanObj.Name = sReader["Name"].ToString();
                            sPlanObj.Remarks = sReader["Remarks"].ToString();
                            sPlanObj.TotalPrice = float.Parse(sReader["TotalPrice"].ToString());
                            sPlanObj.Status = Convert.ToInt32(sReader["Status"]);
                            sPlanObj.IsDeleted = Convert.ToInt32(sReader["IsDeleted"]);
                            sPlanObj.BranchID = Convert.ToInt32(sReader["BranchID"]);
                            sPlanObj.BranchName = sReader["BranchName"].ToString();
                            sPlanObj.OrganizationID = Convert.ToInt32(sReader["OrganizationID"]);
                            sPlanObj.OrganizationName = sReader["OrganizationName"].ToString();

                            if (sReader["CreatedDate"] != null && sReader["CreatedDate"].ToString() != "")
                            {
                                sPlanObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                            }

                            sPlanObj.CreatedBy = sReader["CreatedBy"].ToString();

                            sResultList.Add(sPlanObj);

							totalTreatmentPlan = Convert.ToInt32(sReader["TotalRows"]);
                        }
                    }
                }


                sConn.Close();

				return sResultList;
			}
			catch (Exception ex)
			{
				logger.Error("Database Error >> ", ex);
				return null;
			}

			//return sResult;
		}

		public TreatmentPlanExtendedModel GetTreatmentPlanByPlanID(int planID)
		{
			List<TreatmentPlanExtendedModel> sResultList = new List<TreatmentPlanExtendedModel>();

			try
			{
				MySqlConnection sConn = new MySqlConnection(connectionString);
				sConn.Open();

				String sSelectCommand = "SELECT A.ID, A.Name, A.BranchID, A.Remarks, A.TotalPrice, A.Status, A.IsDeleted, " +
										"A.CreatedDate, A.CreatedBy, B.Name AS 'BranchName', B.OrganizationID, " +
										"C.Name AS 'OrganizationName' " +
										"FROM mst_treatmentplan AS A " +
										"INNER JOIN mst_branch AS B ON B.id = A.BranchID " +
										"INNER JOIN mst_organisation AS C ON C.ID = B.OrganizationID " +
										"WHERE A.ID = '" + planID + "' ";

				using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
				{
					using (var sReader = cmd.ExecuteReader())
					{
						while (sReader.Read())
						{
							TreatmentPlanExtendedModel sPlanObj = new TreatmentPlanExtendedModel();
							sPlanObj.ID = Convert.ToInt32(sReader["ID"]);
							sPlanObj.Name = sReader["Name"].ToString();
							sPlanObj.Remarks = sReader["Remarks"].ToString();
							sPlanObj.TotalPrice = float.Parse(sReader["TotalPrice"].ToString());
							sPlanObj.Status = Convert.ToInt32(sReader["Status"]);
							sPlanObj.IsDeleted = Convert.ToInt32(sReader["IsDeleted"]);

							if (sReader["CreatedDate"] != null && sReader["CreatedDate"].ToString() != "")
							{
								sPlanObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
							}

							sPlanObj.CreatedBy = sReader["CreatedBy"].ToString();
							sPlanObj.OrganizationID = Convert.ToInt32(sReader["OrganizationID"]);
							sPlanObj.OrganizationName = sReader["OrganizationName"].ToString();
							sPlanObj.BranchID = Convert.ToInt32(sReader["BranchID"]);
							sPlanObj.BranchName = sReader["BranchName"].ToString();

							sResultList.Add(sPlanObj);
                        }
					}
				}

                sConn.Close();

				return sResultList.FirstOrDefault();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public List<TreatmentPlanExtendedModel> GetTreatmentPlanListByOrganizationBranch(int organizationID, int branchID)
		{
			List<TreatmentPlanExtendedModel> sResultList = new List<TreatmentPlanExtendedModel>();

			try
			{
				MySqlConnection sConn = new MySqlConnection(connectionString);
				sConn.Open();

				String sSelectCommand = "SELECT A.ID, A.Name, A.BranchID, A.Remarks, A.TotalPrice, A.Status, " +
										"A.IsDeleted, A.CreatedDate, A.CreatedBy, B.Name AS 'BranchName', " +
										"B.OrganizationID, C.Name AS 'OrganizationName' " +
										"FROM mst_treatmentplan AS A " +
										"INNER JOIN mst_branch AS B ON B.id = A.BranchID " +
										"INNER JOIN mst_organisation AS C ON C.ID = B.OrganizationID " +
										"WHERE B.OrganizationID = '" + organizationID + "' AND " +
										"(" + (branchID == 0) + " OR A.BranchID = '" + branchID + "') ";

				using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
				{
					using (var sReader = cmd.ExecuteReader())
					{
						while (sReader.Read())
						{
							TreatmentPlanExtendedModel sPlanObj = new TreatmentPlanExtendedModel();
							sPlanObj.ID = Convert.ToInt32(sReader["ID"]);
							sPlanObj.Name = sReader["Name"].ToString();
							sPlanObj.Remarks = sReader["Remarks"].ToString();
							sPlanObj.TotalPrice = float.Parse(sReader["TotalPrice"].ToString());
							sPlanObj.Status = Convert.ToInt32(sReader["Status"]);
							sPlanObj.IsDeleted = Convert.ToInt32(sReader["IsDeleted"]);
							sPlanObj.BranchID = Convert.ToInt32(sReader["BranchID"]);
							sPlanObj.BranchName = sReader["BranchName"].ToString();
							sPlanObj.OrganizationID = Convert.ToInt32(sReader["OrganizationID"]);
							sPlanObj.OrganizationName = sReader["OrganizationName"].ToString();

							if (sReader["CreatedDate"] != null && sReader["CreatedDate"].ToString() != "")
							{
								sPlanObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
							}

							sPlanObj.CreatedBy = sReader["CreatedBy"].ToString();


							sResultList.Add(sPlanObj);
                        }
					}
				}

                sConn.Close();

				return sResultList;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
