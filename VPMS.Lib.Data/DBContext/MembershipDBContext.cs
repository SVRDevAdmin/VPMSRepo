using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;

namespace VPMS.Lib.Data.DBContext
{
    public class MembershipDBContext : IdentityDbContext
    {
        private string connectionString = "";

        public MembershipDBContext(DbContextOptions<MembershipDBContext> options) : base(options)
        {
            connectionString = options.FindExtension<MySqlOptionsExtension>().ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
