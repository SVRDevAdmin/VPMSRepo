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
    public class AppointmentDBContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<AppointmentModel> mst_appointment {  get; set; }
        public DbSet<AppointmentServiceModel> mst_appointment_services { get; set; }
        public DbSet<PatientOwnerModel> mst_patients_owner { get; set; }
        public DbSet<PetModel> mst_pets { get; set; }

        public AppointmentDBContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
        }
    }
}
