﻿using System;
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
    public class TestResultsDBContext : DbContext
    {
        private readonly IConfiguration config;
        //public DbSet<TestResultModel> Txn_TestResults {  get; set; }
        public DbSet<TestResultsTxnModel> Txn_TestResults { get; set; }

        public TestResultsDBContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
        }
    }
}
