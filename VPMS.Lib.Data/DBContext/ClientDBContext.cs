using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class ClientDBContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<ClientModel> mst_client { get; set; }
        public DbSet<ClientAuthModel> mst_client_auth { get; set; }

        public ClientDBContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
        }
    }
}
