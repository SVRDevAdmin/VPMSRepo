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
	public class InvoiceReceiptDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public DbSet<InvoiceReceiptModel> Mst_InvoiceReceipt { get; set; }
        public DbSet<InvoiceReceiptNo> Txn_InvoiceReceiptNo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public ObservableCollection<InvoiceReceiptInfo> GetInvoiceReceiptList(int start, int total, int status, int branch, int organisation,  int isSuperadmin, string invoiceReceiptNo, string petName, string ownerName, string doctor, out int totalInvoiceReceipt)
		{
			ObservableCollection<InvoiceReceiptInfo> sList = new ObservableCollection<InvoiceReceiptInfo>();
			int No = start + 1;
			totalInvoiceReceipt = 0;
			var roleFilter = "";
			var statusFilter = "";
			var columnName = "";

			//if (branch != 0)
			//{
			//	roleFilter += "AND b.ID = " + branch + " ";
			//}
			//else if (organisation != 0)
			//{
			//	roleFilter += "AND c.ID = " + organisation + " ";
			//}

			//if(status == 2)
			//{
			//	statusFilter = "a.Status in (2, 4) ";
   //             columnName = "InvoiceNo";
			//}

			//if(status == 3)
   //         {
   //             statusFilter = "a.Status = 3 ";
   //             columnName = "ReceiptNo";
			//}

			//var filter = "WHERE a."+ columnName + " like '%" + invoiceReceiptNo + "%' AND a.PetName like '%" + petName + "%' AND a.OwnerName like '%" + ownerName + "%' AND " + statusFilter + roleFilter + " ";
			//var joinQuery =
			//	"join mst_branch b on b.ID = a.Branch " +
			//	"join mst_organisation c on c.ID = b.OrganizationID ";

			//var totalInvoiceReceiptQuery = "(select Count(a.ID) from mst_invoicereceipt a " + joinQuery + filter + ")";
			//var completeQuery = "select a.ID, a.InvoiceNo, a.ReceiptNo, a.CreatedDate, a.UpdatedDate, a.PetName, a.OwnerName, a.Fee, a.Status, " + totalInvoiceReceiptQuery + " as 'TotalInvoiceReceipt' from mst_invoicereceipt a " +
			//	joinQuery + filter + " LIMIT " + start + ", " + total + ";";

			String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', " +
									"a.ID, a.InvoiceNo, a.ReceiptNo, a.CreatedDate, a.UpdatedDate, a.PetName, " +
									"a.OwnerName, a.Fee, a.Status, COUNT(*) OVER() as 'TotalInvoiceReceipt' " +
									"FROM mst_invoicereceipt a " +
									"INNER JOIN mst_branch b on b.ID = a.Branch " +
									"INNER JOIN mst_organisation c on c.ID = b.OrganizationID " +
									"WHERE ( " +
									"(" + (status == 2) + " AND a.Status in (2, 4)) OR " +
									"(" + (status == 3) + " AND a.Status = '3')" +
									") " +
									"AND " +
									"(" +
									"(" + (isSuperadmin == 1) + " AND c.Level >= 2 AND b.OrganizationID = '" + organisation + "') OR " +
									"(" + (isSuperadmin == 0) + " AND b.OrganizationID = '" + organisation + "' AND a.Branch = '" + branch + "') " +
									") AND " +
									"(" + (invoiceReceiptNo == null) + " OR (" +
									"(" + (status == 2) + " AND a.InvoiceNo LIKE '%" + invoiceReceiptNo  + "%') OR " +
									"(" + (status == 3) + " AND a.ReceiptNo LIKE '%" + invoiceReceiptNo + "%') " +
									")" +
									") AND " +
									"(" + (petName == null) + " OR a.PetName LIKE '%" + petName + "%') AND " +
									"(" + (ownerName == null) + " OR a.OwnerName LIKE '%" + ownerName + "%') " +
									"LIMIT " + start + ", " + total;


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
							sList.Add(new InvoiceReceiptInfo()
							{

								ID = int.Parse(reader["ID"].ToString()),
								No = No++, 
								InvoiceNo = reader["InvoiceNo"].ToString(),
								ReceiptNo = reader["ReceiptNo"].ToString(),
								Date = DateTime.Parse(reader["CreatedDate"].ToString()),
								PetName = reader["PetName"].ToString(),
								OwnerName = reader["OwnerName"].ToString(),
								Fee = float.Parse(reader["Fee"].ToString()),
								UpdatedDate = (reader["UpdatedDate"].ToString() != "") ? DateTime.Parse(reader["UpdatedDate"].ToString()) : DateTime.Now,
								Status = int.Parse(reader["Status"].ToString())
                            });


                            totalInvoiceReceipt = Convert.ToInt32(reader["TotalInvoiceReceipt"]);
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

		/// <summary>
		/// Get all invoices transaction summary by date
		/// </summary>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public List<InvoiceTransSummaryModel> GetInvoiceTransactionSummaryByDate(DateTime dtStart, DateTime dtEnd)
		{
			List<InvoiceTransSummaryModel> sResult = new List<InvoiceTransSummaryModel>();

            try 
			{
				using (var sConn = new MySqlConnection(connectionString))
				{
					sConn.Open();

					String sSelectCommand = "SELECT T1.SummaryDate, T1.Branch, " +
											"IFNULL(T2.TotalAmount, 0) AS 'TotalAmount', IFNULL(T2.TotalDiscount, 0) AS 'TotalDiscount' " +
											"FROM (" +
											"SELECT STR_TO_DATE('" + dtStart.ToString("yyyy-MM-dd") + "', '%Y-%m-%d') AS 'SummaryDate', " +
											"ID AS 'Branch', `Name` AS 'BranchName' " +
											"FROM Mst_Branch WHERE `Status`= 1 " +
											") AS T1 " +
											"LEFT JOIN (" +
                                            "SELECT  STR_TO_DATE(A.CreatedDate, '%Y-%m-%d') AS 'SummaryDate', A.Branch, SUM(A.Fee) AS 'TotalAmount', SUM(A.GrandDiscount) AS 'TotalDiscount' " +
											"FROM mst_invoicereceipt AS A " +
											"INNER JOIN txn_treatmentplan AS B ON B.ID = A.TreatmentPlanID " +
                                            "WHERE A.CreatedDate >= '" + dtStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                                            "A.CreatedDate <= '" + dtEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
											"GROUP BY A.CreatedDate, A.Branch " +
											") AS T2 ON T2.SummaryDate = T1.SummaryDate AND T2.Branch = T1.Branch ";


                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
					{
						using (var sReader = sCommand.ExecuteReader())
						{
							while (sReader.Read())
							{
								sResult.Add(new InvoiceTransSummaryModel
								{
									SummaryDate = Convert.ToDateTime(sReader["SummaryDate"]),
									BranchID = Convert.ToInt32(sReader["Branch"]),
									TotalAmount = Convert.ToDecimal(sReader["TotalAmount"]),
									TotalDiscount = Convert.ToDecimal(sReader["TotalDiscount"])
								});
                            }
						}
					}

					sConn.Close();
				}
			}
			catch (Exception ex)
			{
				return null;
			}

			return sResult;
        }

		/// <summary>
		/// Get Invoice transaction details summary by Date
		/// </summary>
		/// <param name="dtStart"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public List<TransSummaryBreakdownModel> GetTransactionSummaryBreakdownByDate(DateTime dtStart, DateTime dtEnd)
		{
			List<TransSummaryBreakdownModel> sResult = new List<TransSummaryBreakdownModel>();

            try
			{
				using (var sConn = new MySqlConnection(connectionString))
				{
					sConn.Open();

					String sSelectCommand = "SELECT A.CreatedDate AS 'SummaryDate', A.Branch, B.PetID, M.Species, " +
											"B.TreatmentPlanID, B.PlanName, B.TotalCost AS 'TreatmentPlanAmount', " +
											"C.ServiceID, C.ServiceName, C.TotalPrice AS 'ServicePrice' " + 
											"FROM mst_invoicereceipt AS A " + 
											"INNER JOIN txn_treatmentplan AS B ON B.ID = A.TreatmentPlanID " +
											"INNER JOIN txn_treatmentplan_services AS C ON C.PlanID = B.ID " + 
											"INNER JOIN mst_pets AS M ON M.ID = B.PetID " +
											"WHERE A.CreatedDate >= '" + dtStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND " + 
											"A.CreatedDate <= '" + dtEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' ";

					using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
					{
						using (var sReader = sCommand.ExecuteReader())
						{
							while (sReader.Read())
							{
								sResult.Add(new TransSummaryBreakdownModel
								{
									SummaryDate = Convert.ToDateTime(sReader["SummaryDate"]),
									BranchID = Convert.ToInt32(sReader["Branch"]),
									PetID = Convert.ToInt32(sReader["PetID"]),
									Species = sReader["Species"].ToString(),
									TreatmentPlanID = Convert.ToInt32(sReader["TreatmentPlanID"]),
									TreatmentPlanName = sReader["PlanName"].ToString(),
									TreatmentPlanAmount = Convert.ToDecimal(sReader["TreatmentPlanAmount"]),
									ServiceID = Convert.ToInt32(sReader["ServiceID"]),
									ServiceName = sReader["ServiceName"].ToString(),
									ServicePrice = Convert.ToDecimal(sReader["ServicePrice"])
								});
                            }
						}
					}

					sConn.Close();
				}
			}
			catch (Exception ex)
			{
				return null;
			}

			return sResult;
        }
	}
}
