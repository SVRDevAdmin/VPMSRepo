using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.Models;
using Microsoft.Extensions.Configuration;

namespace VPMSCustomer.Lib.Data.DBContext
{
    public class MasterCodeDBContext : DbContext
    {
        public DbSet<MasterCodeDataModel> mst_mastercodedata { get; set; }
        public DbSet<CountryModel> mst_countrylist { get; set; }
        public DbSet<StateModel> mst_state { get; set; }
        public DbSet<CityModel> mst_city { get; set; }

        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
