using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using VPMS.Lib.Data.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace VPMS.Lib.Data.DBContext
{
    public class LocationDBContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<LocationModel> mst_locationlist { get; set; }

        public LocationDBContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
        }
    }
}
