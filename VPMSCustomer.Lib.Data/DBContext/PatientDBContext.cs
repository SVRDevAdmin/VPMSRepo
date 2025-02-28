using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data.DBContext
{
    public class PatientDBContext : DbContext
    {
        public DbSet<PatientLoginModel> mst_patients_login { get; set; }
        public DbSet<PatientModel> mst_patients { get; set; }
        public DbSet<PatientOwnerModel> mst_patients_Owner { get; set; }
        public DbSet<PatientsConfigurationLogsModel> patients_configuration_logs { get; set; }
        public DbSet<PatientConfigurationModel> mst_patients_configuration {  get; set; }

        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
