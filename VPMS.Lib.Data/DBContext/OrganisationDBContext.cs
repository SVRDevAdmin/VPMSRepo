﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
	public class OrganisationDBContext : DbContext
	{
		private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

		public DbSet<OrganisationModel> Mst_Organisation { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
	}
}
