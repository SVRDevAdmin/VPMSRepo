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
    public class DoctorDBContext : DbContext
    {
        public DbSet<DoctorModel> Mst_Doctor { get; set; }
        public DbSet<DoctorServicesModel> Mst_Doctor_Services { get; set; }

        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
