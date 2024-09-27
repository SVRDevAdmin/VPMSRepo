using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class NotificationRepository
    {
        /// <summary>
        /// Get Notification List By Group & Pagination
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sGroup"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static List<NotificationModel> GetNotificationList(IConfiguration config, String sGroup, int pageSize, int pageIndex, out int totalRecords)
        {
            List<NotificationModel> sNotificationList = new List<NotificationModel>();
            totalRecords = 0;

            try
            {
                using (var ctx = new NotificationDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ID, NotificationType, Title, Content, CreatedDate, CreatedBy, " +
                                            "Count(*) OVER() as 'TotalRows' " +
                                            "FROM Txn_Notifications " +
                                            "WHERE (" +  (sGroup == null) + " OR NotificationGroup ='" + sGroup + "') " +
                                            "ORDER BY CreatedDate DESC " +
                                            "LIMIT " + pageSize  + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sNotificationList.Add(new NotificationModel
                                {
                                    ID = Convert.ToInt64(sReader["ID"]),
                                    NotificationType = sReader["NotificationType"].ToString(),
                                    Title = sReader["Title"].ToString(),
                                    Content = sReader["Content"].ToString(),
                                    CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]),
                                    CreatedBy = sReader["CreatedBy"].ToString()
                                });

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                            }
                        }
                    }

                    sConn.Close();

                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return sNotificationList;
        }
    }
}
