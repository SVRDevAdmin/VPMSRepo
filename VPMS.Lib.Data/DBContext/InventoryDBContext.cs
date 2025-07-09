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
	public class InventoryDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");
		private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public DbSet<InventoryModel> Mst_Product { get; set; }
		public DbSet<InventoryStatus> Mst_Product_Status { get; set; }
		public DbSet<InventoryCategory> Mst_ProductType { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public ObservableCollection<InventoryInfoList> GetInventoryList(int organizationid, int branchID, int isSuperadmin, int start, int total, out int totalInventory, string search = "")
		{
			ObservableCollection<InventoryInfoList> sList = new ObservableCollection<InventoryInfoList>();
			int No = start + 1;
			totalInventory = 0;
			var limit = "";

			if(total != 0)
			{
                limit = "LIMIT " + start + ", " + total + ";";
            }

			//var filter = "WHERE a.name like '%" + search + "%' OR b.TypeName like '%" + search + "%' OR a.InventoryName like '%" + search + "%' ";
			//var joinQuery = 
			//	"join mst_producttype b on b.ID = a.ProductTypeID " + 
			//	"join mst_product_status c on c.ProductID = a.id " + 
			//	"join mst_branch d on d.ID = a.BranchID " + 
			//	"join mst_organisation e on e.ID = d.OrganizationID ";

			//var totalInventoryQuery = "(select Count(a.Name) from mst_product a " + joinQuery + filter + ")";
			//var completeQuery = "select a.ID, a.InventoryName, b.TypeName, a.Usage, a.name, a.ImageFilePath, a.ImageFileName, a.SKU, c.QtyInStores, a.PricePerQty, e.Name as 'Organisation', d.Name as 'Branch', c.StockStatus, " + totalInventoryQuery + " as 'TotalInventory' from mst_product a " +
			//	joinQuery + filter + "Order by a.ID " + limit + ";";

			String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', " +
									"a.ID, a.InventoryName, b.TypeName, a.Usage, a.name, a.ImageFilePath, a.ImageFileName, " +
									"a.SKU, c.QtyInStores, a.PricePerQty, e.Name as 'Organisation', d.Name as 'Branch', " +
									"c.StockStatus, COUNT(*) OVER() as 'TotalInventory' " +
									"FROM mst_product a " +
									"INNER JOIN mst_producttype b on b.ID = a.ProductTypeID " +
									"INNER JOIN mst_product_status c on c.ProductID = a.id  " +
									"INNER JOIN mst_branch d on d.ID = a.BranchID " +
									"LEFT JOIN mst_organisation e on e.ID = d.OrganizationID " +
									"WHERE " +
									"(" + 
									"(" + (isSuperadmin == 1) + " AND e.Level >= 2 AND d.OrganizationID = '" + organizationid + "') OR " + 
									"(" + (isSuperadmin == 0) + " AND d.OrganizationID = '" + organizationid + "' AND a.BranchID = '" + branchID + "') " +
									") AND " +
									"( " + (search == "") + " OR " +
									"(a.name LIKE '%" + search + "%' OR b.TypeName LIKE '%" + search  + "%' OR a.SKU LIKE '%" + search  + "%'  OR " +
									"d.Name LIKE '%" + search + "%' OR e.Name LIKE '%" + search + "%') " +
									") " +
									"ORDER BY a.ID " + limit + ";";


            try
			{
				using (MySqlConnection conn = new MySqlConnection(connectionString))
				{
					conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sSelectCommand, conn);

                    using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							sList.Add(new InventoryInfoList()
							{

								ID = int.Parse(reader["ID"].ToString()),
								No = Convert.ToInt32(reader["row_num"]),
								InventoryName = reader["InventoryName"].ToString(),
								Category = reader["TypeName"].ToString(), 
								Usage = reader["Usage"].ToString(), 
								ProductName = reader["name"].ToString(), 
								//Image = reader["ImageFilePath"].ToString() + reader["ImageFileName"].ToString(),
								Image = reader["ImageFileName"].ToString(),
								SKU = reader["SKU"].ToString(),
								Quantity = int.Parse(reader["QtyInStores"].ToString()),
								PricePerQty = float.Parse(reader["PricePerQty"].ToString()), 
								Organisation = reader["Organisation"].ToString(),
								Branch = reader["Branch"].ToString(),
								StockStatus = int.Parse(reader["StockStatus"].ToString()),
							});

							totalInventory = Convert.ToInt32(reader["TotalInventory"]);
						}
					}

					conn.Close();
				}
			}
			catch (Exception ex)
			{
				logger.Error("InventoryDBContext >>> GetInventoryList >>> ", ex);
			}

			return sList;
		}

		public ObservableCollection<InventoryInvoice> GetInventoryInvoice(int planID)
		{
			ObservableCollection<InventoryInvoice> sList = new ObservableCollection<InventoryInvoice>();

			var query = "select a.ProductName, a.Discount, a.TotalPrice, b.ExpiryDate from txn_treatmentplan_products a join mst_product_status b on b.ProductID = a.ProductID where a.PlanID = "+ planID + ";";

			try
			{
				using (MySqlConnection conn = new MySqlConnection(connectionString))
				{
					conn.Open();
					MySqlCommand cmd = new MySqlCommand(query, conn);

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							sList.Add(new InventoryInvoice()
							{
								ProductName = reader["ProductName"].ToString(),
								Discount = float.Parse(reader["Discount"].ToString()),
								TotalPrice = float.Parse(reader["TotalPrice"].ToString()),
								ExpiryDate = DateOnly.FromDateTime(DateTime.Parse(reader["ExpiryDate"].ToString()))
							});
						}
					}

					conn.Close();
				}
			}
			catch (Exception ex)
			{
				logger.Error("InventoryDBContext >>> GetInventoryInvoice >>> ", ex);
			}

			return sList;
		}
	}
}
