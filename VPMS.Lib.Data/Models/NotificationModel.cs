using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class NotificationModel
    {
        [Key]
        public long ID { get; set; }
        public String? NotificationGroup { get; set; }
        public String? NotificationType { get; set; }
        public String? Title { get; set; }
        public String? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy {  get; set; }
    }

    public class NotificationReceiverModel
    {
        [Key]
        public long ID { get; set; }
        public long? NotificationID { get; set; }
        public String? TargetUser { get; set; }
        public int? Statu {  get; set; }
        public DateTime? MsgReadDateTime { get; set; }
        public DateTime? MsgDeletedDateTime {  get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
