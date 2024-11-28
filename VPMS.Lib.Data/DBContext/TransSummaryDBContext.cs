using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class TransSummaryDBContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<TransSummaryModel> txn_transactionsummary {  get; set; }
        public DbSet<TransSummaryLogModel> txn_transactionsummarylog { get; set; }

        public TransSummaryDBContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
        }
    }
}
