using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data.DBContext
{
    public class AppointmentDBContext : DbContext
    {
        public DbSet<AppointmentModel> mst_appointment { get; set; }
        public DbSet<AppointmentGroupingModel> mst_appointment_grouping { get; set; }
        public DbSet<AppointmentServicesModel> mst_appointment_services { get; set; }

        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
