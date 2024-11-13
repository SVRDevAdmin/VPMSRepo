﻿using Microsoft.EntityFrameworkCore;
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
	public class OrganisationDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public DbSet<OrganisationModel> Mst_Organisation { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public ObservableCollection<OrganisationList> GetOrganisationList(int start, int total, int organisation, out int totalOrganisations, string search = "")
		{
			ObservableCollection<OrganisationList> sList = new ObservableCollection<OrganisationList>();
			int No = start + 1;
			totalOrganisations = 0;
			var roleFilter = "";

			if (organisation != 0)
			{
				roleFilter += "AND a.ID = " + organisation + " ";
			}

			var filter = "WHERE (a.name like '%" + search + "%') AND a.Level = 2 AND a.Status = 1 " + roleFilter + " ";
			var joinQuery =	"join mst_branch b on b.OrganizationID = a.ID AND b.Status = 1 ";

			var totalServiceQuery = "(Select Count(a.id) from mst_organisation a " + filter + ")";
			var completeQuery = "select a.ID, a.Name, (Select Count(Surname) from mst_user where BranchID in (Select ID FROM mst_branch where OrganizationID = a.ID)) as 'TotalStaff', group_concat(concat(b.Name) SEPARATOR ', ') as 'Branches', " + totalServiceQuery + " as 'TotalOrganisations' from mst_organisation a " +
				joinQuery + filter + "GROUP BY a.ID, a.Name, a.TotalStaff Order by a.ID LIMIT " + start + ", " + total + ";";

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
							sList.Add(new OrganisationList()
							{

								ID = int.Parse(reader["ID"].ToString()),
								No = No++,
								Name = reader["Name"].ToString(),
								Branches = reader["Branches"].ToString(),
								TotalStaff = int.Parse(reader["TotalStaff"].ToString())
							});

							totalOrganisations = Convert.ToInt32(reader["TotalOrganisations"]);
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
