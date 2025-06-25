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
    public class NotificationDBContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<NotificationModel> Txn_Notifications { get; set; }
        public DbSet<NotificationReceiverModel> Txn_Notification_Receiver { get; set; }
        public DbSet<NotificationConfiguration> Mst_Users_Configuration { get; set; }
        public DbSet<NotificationCustomerModel> txn_customer_notifications { get; set; }
        public DbSet<NotificationCustomerReceiverModel> txn_customer_notification_receiver {  get; set; }

        public NotificationDBContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")));
        }
    }
}
