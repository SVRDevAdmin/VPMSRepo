using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class NotificationModel
    {
        [Key]
        public long ID {  get; set; }
        public String? NotificationGroup { get; set; }
        public String? NotificationType { get; set; }
        public String? Title { get; set; }
        public String? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }

    public class NotificationReceiver()
    {
        [Key]
        public long ID { get; set; }
        public long? NotificationID { get; set; }
        public String? UserID { get; set; }
        public int? Status { get; set; }
        public DateTime? MsgReadDateTime { get; set; }
        public DateTime? MsgDeletedDateTime { get; set; }
        public DateTime? UpdatedDate {  get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class NotificationViewModel : NotificationModel
    {
        public int SeqNo { get; set; }
        public long NotificationReceiverID { get; set; }
    }

    public class NotificationAdminModel
    {
        [Key]
        public long ID { get; set; }
        public int? BranchID { get; set; }
        public String? NotificationGroup { get; set; }
        public String? NotificationType { get; set; }
        public String? Title { get; set; }
        public String? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }

    public class NotificationAdminReceiverModel
    {
        [Key]
        public long? ID { get; set; }
        public long? NotificationID { get; set; }
        public String? TargetUser { get; set; }
        public int? Status { get; set; }
        public DateTime? MsgReadDateTime { get; set; }
        public DateTime? MsgDeletedDateTime { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
