using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.NtruPrime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class NotificationRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Notification View Listing
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static List<NotificationViewModel> GetNotificationViewListing(String sUserID, int pageSize, int pageIndex, out int totalRecords)
        {
            List<NotificationViewModel> sResultList = new List<NotificationViewModel>();
            totalRecords = 0;

            try
            {
                using (var ctx = new NotificationDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER (ORDER BY B.CreatedDate DESC) AS 'SeqNo', " +
                                            "B.ID, B.NotificationGroup, B.NotificationType, B.Title, B.Content, " + 
                                            "B.CreatedDate, B.CreatedBy, A.ID AS 'NotificationReceiverID', " +
                                            "COUNT(*) OVER() AS 'TotalRows' " + 
                                            "FROM (" + 
                                            "SELECT * FROM txn_customer_notification_receiver " + 
                                            "WHERE UserID='" + sUserID + "' " +
                                            ") AS A " +
                                            "INNER JOIN txn_customer_notifications AS B ON B.ID = A.NotificationID " +
                                            "ORDER BY B.CreatedDate DESC " +
                                            "LIMIT " + pageSize + " " + 
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                NotificationViewModel sNotificationObj = new NotificationViewModel();
                                sNotificationObj.SeqNo = Convert.ToInt32(sReader["SeqNo"]);
                                sNotificationObj.ID = Convert.ToInt64(sReader["ID"]);
                                sNotificationObj.NotificationGroup = sReader["NotificationGroup"].ToString();
                                sNotificationObj.NotificationType = sReader["NotificationType"].ToString();
                                sNotificationObj.Title = sReader["Title"].ToString();
                                sNotificationObj.Content = sReader["Content"].ToString();
                                sNotificationObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sNotificationObj.CreatedBy = sReader["CreatedBy"].ToString();
                                sNotificationObj.NotificationReceiverID = Convert.ToInt64(sReader["NotificationReceiverID"]);

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);

                                sResultList.Add(sNotificationObj);
                            }
                        }
                    }
                    
                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                logger.Error("NotificationRepository >>> GetNotificationViewListing >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Update Notification Read Status
        /// </summary>
        /// <param name="notificationReceiverID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Boolean UpdateNotificationReadStatus(long notificationReceiverID, string userID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new NotificationDBContext())
                {
                    var sResult = ctx.txn_customer_notification_receiver.Where(x => x.ID == notificationReceiverID).FirstOrDefault();
                    if (sResult != null)
                    {
                        sResult.MsgReadDateTime = DateTime.Now;
                        sResult.Status = 1;
                        sResult.UpdatedDate = DateTime.Now;
                        sResult.UpdatedBy = userID;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error("NotificationRepository >>> UpdateNotificationReadStatus >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Insert Notification Record
        /// </summary>
        /// <param name="sNotificationObj"></param>
        /// <param name="sUserID"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean InsertNotification(NotificationModel sNotificationObj, List<String> sUserID, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new NotificationDBContext())
                {
                    ctx.txn_customer_notifications.Add(sNotificationObj);
                    ctx.SaveChanges();

                    if (sUserID.Count > 0)
                    {
                        foreach (var s in sUserID)
                        {
                            NotificationReceiver sReceiver = new NotificationReceiver();
                            sReceiver.NotificationID = sNotificationObj.ID;
                            sReceiver.UserID = s;
                            sReceiver.UpdatedDate = DateTime.Now;
                            sReceiver.UpdatedBy = sUpdatedBy;

                            ctx.txn_customer_notification_receiver.Add(sReceiver);
                            ctx.SaveChanges();
                        }
                        
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("NotificationRepository >>> InsertNotification >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Insert Notification to Admin
        /// </summary>
        /// <param name="sNotificationObj"></param>
        /// <param name="sUserID"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean InsertAdminNotification(NotificationAdminModel sNotificationObj, List<String> sUserID, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new NotificationDBContext())
                {
                    ctx.txn_notifications.Add(sNotificationObj);
                    ctx.SaveChanges();

                    if (sUserID.Count > 0)
                    {
                        foreach (var  s in sUserID)
                        {
                            NotificationAdminReceiverModel sReceiver = new NotificationAdminReceiverModel();
                            sReceiver.NotificationID = sNotificationObj.ID;
                            sReceiver.TargetUser = s;
                            sReceiver.Status = 0;
                            sReceiver.UpdatedDate = DateTime.Now;
                            sReceiver.UpdatedBy = sUpdatedBy;

                            ctx.txn_notification_receiver.Add(sReceiver);
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
    }
}
