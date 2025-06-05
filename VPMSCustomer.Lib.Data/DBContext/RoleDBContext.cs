using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Microsoft.Extensions.Hosting;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data.DBContext
{
    public class RoleDBContext : DbContext
    {
        public DbSet<RoleModel> mst_roles { get; set; }

        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
