using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace VPMSCustomer.Lib.Data.DBContext
{
    public class CustomerLoginDBContext : DbContext
    {
        public DbSet<CustomerLoginSession> txn_customer_loginsession { get; set; }
        public DbSet<CustomerLoginSessionLog> txn_customer_loginsession_log { get; set; }

        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
