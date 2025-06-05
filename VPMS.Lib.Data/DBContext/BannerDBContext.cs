using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class BannerDBContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<BannerModel> mst_banners { get; set; }
        public DbSet<BlogModel> mst_blogs { get; set; }

        public BannerDBContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
        }
    }
}
