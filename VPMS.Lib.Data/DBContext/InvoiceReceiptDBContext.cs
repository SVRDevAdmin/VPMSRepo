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
	public class InvoiceReceiptDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public DbSet<InvoiceReceiptModel> Mst_InvoiceReceipt { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public ObservableCollection<InvoiceReceiptInfo> GetInvoiceReceiptList(int start, int total, int status, int branch, int organisation, string invoiceReceiptNo, string petName, string ownerName, string doctor, out int totalInvoiceReceipt)
		{
			ObservableCollection<InvoiceReceiptInfo> sList = new ObservableCollection<InvoiceReceiptInfo>();
			int No = start + 1;
			totalInvoiceReceipt = 0;
			var roleFilter = "";
			var statusFilter = "";

			if (branch != 0)
			{
				roleFilter += "AND b.ID = " + branch + " ";
			}
			else if (organisation != 0)
			{
				roleFilter += "AND c.ID = " + organisation + " ";
			}

			if(status == 2)
			{
				statusFilter = "InvoiceNo";
			}

			if(status == 3)
			{
				statusFilter = "ReceiptNo";
			}

			var filter = "WHERE a."+ statusFilter + " like '%" + invoiceReceiptNo + "%' AND a.PetName like '%" + petName + "%' AND a.Doctor like '%" + doctor + "%' AND a.OwnerName like '%" + ownerName + "%' AND a.Status = " + status + " " + roleFilter + " ";
			var joinQuery =
				"join mst_branch b on b.ID = a.Branch " +
				"join mst_organisation c on c.ID = b.OrganizationID ";

			var totalInvoiceReceiptQuery = "(select Count(a.ID) from mst_invoicereceipt a " + joinQuery + filter + ")";
			var completeQuery = "select a.ID, a.InvoiceNo, a.ReceiptNo, a.CreatedDate, a.PetName, a.Doctor, a.OwnerName, a.Fee, " + totalInvoiceReceiptQuery + " as 'TotalInvoiceReceipt' from mst_invoicereceipt a " +
				joinQuery + filter + " LIMIT " + start + ", " + total + ";";

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
							sList.Add(new InvoiceReceiptInfo()
							{

								ID = int.Parse(reader["ID"].ToString()),
								No = No++, 
								InvoiceNo = reader["InvoiceNo"].ToString(),
								ReceiptNo = reader["ReceiptNo"].ToString(),
								Date = DateTime.Parse(reader["CreatedDate"].ToString()),
								PetName = reader["PetName"].ToString(),
								Doctor = reader["Doctor"].ToString(),
								OwnerName = reader["OwnerName"].ToString(),
								Fee = float.Parse(reader["Fee"].ToString())
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
	}
}
