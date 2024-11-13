using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class RoleDBContext : DbContext
    {
        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        public DbSet<RoleModel> Mst_Roles { get; set; }
        public DbSet<RoleIdentityObject> aspnetroles {  get; set; }
        public DbSet<AccessPermissionObject> mst_accesspermission {  get; set; }
        public DbSet<RolePermissionObject> mst_rolepermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
