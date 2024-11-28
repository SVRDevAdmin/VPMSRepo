using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
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
        public static List<NotificationExtendedModel> GetNotificationList(IConfiguration config, String sUserID, int sBranchID, String sGroup, int pageSize, int pageIndex, out int totalRecords, out int totalNew)
        {
            List<NotificationExtendedModel> sNotificationList = new List<NotificationExtendedModel>();
            totalRecords = 0;
            totalNew = 0;

            try
            {
                using (var ctx = new NotificationDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ID, A.NotificationType, A.Title, A.Content, A.CreatedDate, A.CreatedBy, B.ID AS 'NotifMsgID', " +
                                            "Count(*) OVER() as 'TotalRows', " +
                                            "SUM(CASE WHEN B.Status = 0 THEN 1 ELSE 0 END) OVER() AS 'TotalNew' " +
                                            "FROM Txn_Notifications AS A " +
                                            "LEFT JOIN Txn_Notification_Receiver AS B on B.NotificationID = A.ID " +
                                            "WHERE (" +  (sGroup == null) + " OR A.NotificationGroup ='" + sGroup + "') AND " +
                                            "(B.TargetUser ='" + sUserID + "') AND " + 
                                            "A.BranchID = '" + sBranchID + "' " +
                                            "ORDER BY A.CreatedDate DESC " +
                                            "LIMIT " + pageSize  + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sNotificationList.Add(new NotificationExtendedModel
                                {
                                    ID = Convert.ToInt64(sReader["ID"]),
                                    NotificationType = sReader["NotificationType"].ToString(),
                                    Title = sReader["Title"].ToString(),
                                    Content = sReader["Content"].ToString(),
                                    CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]),
                                    CreatedBy = sReader["CreatedBy"].ToString(),
                                    NotifMsgID = Convert.ToInt64(sReader["NotifMsgID"])
                                });

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                                totalNew = Convert.ToInt32(sReader["TotalNew"]);
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

        /// <summary>
        /// Get list of notification config settings by User ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static List<NotificationSettingsModel> GetNotificationSettings(IConfiguration config, String sUserID)
        {
            List<NotificationSettingsModel> sResult = new List<NotificationSettingsModel>();

            try
            {
                using (var ctx = new NotificationDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.CodeGroup, A.CodeID, A.CodeName, A.Description, A.SeqOrder,  " + 
                                            "B.ID AS 'ConfigurationID', B.UserID, B.ConfigurationKey, B.ConfigurationValue " +
                                            "FROM mst_mastercodedata AS A " + 
                                            "LEFT JOIN mst_users_configuration AS B ON B.ConfigurationKey = Concat('UserSettings_', A.CodeID)  AND " +
                                            "B.UserID = '" + sUserID + "' " +
                                            "WHERE A.CodeGroup = 'NotificationConfig' AND A.IsActive=1 " +
                                            "ORDER BY A.SeqOrder";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                NotificationSettingsModel sConfigObj = new NotificationSettingsModel();
                                sConfigObj.CodeGroup = sReader["CodeGroup"].ToString();
                                sConfigObj.CodeID = sReader["CodeID"].ToString();
                                sConfigObj.CodeName = sReader["CodeName"].ToString();
                                sConfigObj.Description = sReader["Description"].ToString();
                                sConfigObj.SeqOrder = Convert.ToInt32(sReader["SeqOrder"]);

                                if (!String.IsNullOrEmpty(sReader["ConfigurationID"].ToString()))
                                {
                                    sConfigObj.ConfigurationID = Convert.ToInt32(sReader["ConfigurationID"]);
                                }
                                
                                if (!String.IsNullOrEmpty(sReader["UserID"].ToString()))
                                {
                                    sConfigObj.UserID = sReader["UserID"].ToString();
                                }
                                
                                if (!String.IsNullOrEmpty(sReader["ConfigurationKey"].ToString()))
                                {
                                    sConfigObj.ConfigurationKey = sReader["ConfigurationKey"].ToString();
                                }

                                if (!String.IsNullOrEmpty(sReader["ConfigurationValue"].ToString()))
                                {
                                    sConfigObj.ConfigurationValue = sReader["ConfigurationValue"].ToString();
                                }

                                sResult.Add(sConfigObj);
                            }
                        }
                    }

                    sConn.Close();

                    return sResult;
                } 
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update Configuration Settings
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sUserID"></param>
        /// <param name="sConfigList"></param>
        /// <returns></returns>
        public static Boolean UpdateNotificationSettings(IConfiguration config, String sUserID, List<NotificationConfigModel> sConfigList)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new NotificationDBContext(config))
                {
                    foreach (var c in sConfigList)
                    {
                        var sUserConfig = ctx.Mst_Users_Configuration
                                            .Where(x => x.UserID == sUserID && x.ConfigurationKey == c.Key)
                                            .FirstOrDefault();

                        if (sUserConfig != null)
                        {
                            sUserConfig.ConfigurationValue = c.Value;
                            sUserConfig.UpdatedDate = DateTime.Now;
                            sUserConfig.UpdatedBy = sUserID;
                            ctx.SaveChanges();
                        }
                        else
                        {
                            NotificationConfiguration sNewConfig = new  NotificationConfiguration();
                            sNewConfig.UserID = sUserID;
                            sNewConfig.ConfigurationKey = c.Key;
                            sNewConfig.ConfigurationValue = c.Value;
                            sNewConfig.CreatedDate = DateTime.Now;
                            sNewConfig.CreatedBy = sUserID;

                            ctx.Mst_Users_Configuration.Add(sNewConfig);
                            ctx.SaveChanges();
                        }
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update Message read Status
        /// </summary>
        /// <param name="config"></param>
        /// <param name="iMessageReceiverID"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static Boolean UpdateMessageReadStatus(IConfiguration config, long iMessageReceiverID, String sUserID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new NotificationDBContext(config))
                {
                    var sResult = ctx.Txn_Notification_Receiver.Where(x => x.ID ==  iMessageReceiverID).FirstOrDefault();
                    if (sResult != null)
                    {
                        if (sResult.Status == 0)
                        {
                            sResult.Status = 1;
                            sResult.MsgReadDateTime = DateTime.Now;
                            sResult.UpdatedDate = DateTime.Now;
                            sResult.UpdatedBy = sUserID;

                            ctx.SaveChanges();
                        }

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Get list of receiver by notification group
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sNotificationGroup"></param>
        /// <returns></returns>
        public static List<String> GetReceiverListByNotificationGroup(IConfiguration config, String sNotificationGroup)
        {
            List<String> sResult = new List<string>();

            try
            {
                using (var ctx = new NotificationDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT B.UserID, B.Surname, B.LastName " +
                                            "FROM mst_notification_receiver_config AS A " + 
                                            "INNER JOIN mst_user AS B ON B.RoleID = A.RoleID AND B.`Status` = 1 " +
                                            "WHERE A.NotificationGroup='" + sNotificationGroup + "'";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sResult.Add(sReader["UserID"].ToString());
                            }
                        }
                    }

                    sConn.Close();

                    return sResult;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Insert Notification
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sNotification"></param>
        /// <param name="sRecipients"></param>
        /// <returns></returns>
        public static Boolean InsertNotification(IConfiguration config, NotificationModel sNotification, List<String> sRecipients)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new NotificationDBContext(config))
                {
                    sNotification.CreatedDate = DateTime.Now;

                    ctx.Txn_Notifications.Add(sNotification);
                    ctx.SaveChanges();

                    
                    foreach(String s in sRecipients)
                    {
                        NotificationReceiverModel sReceiver = new NotificationReceiverModel();
                        sReceiver.NotificationID = sNotification.ID;
                        sReceiver.TargetUser = s;
                        sReceiver.Status = 0;
                        sReceiver.UpdatedDate = sNotification.CreatedDate;
                        sReceiver.UpdatedBy = sNotification.CreatedBy;

                        ctx.Txn_Notification_Receiver.Add(sReceiver);
                        ctx.SaveChanges();
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
