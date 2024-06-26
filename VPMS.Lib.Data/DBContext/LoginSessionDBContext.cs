using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class LoginSessionDBContext : DbContext
    {
        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        public DbSet<LoginSessionModel> Txn_LoginSession { get; set; }
        public DbSet<LoginSessionLogModel> Txn_LoginSession_Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
