using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.Mozilla;

namespace VPMS.Lib.Data.Models
{
    public class NotificationModel
    {
        [Key]
        public long ID { get; set; }
        public int? BranchID { get; set; }
        public String? NotificationGroup { get; set; }
        public String? NotificationType { get; set; }
        public String? Title { get; set; }
        public String? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy {  get; set; }
    }

    public class NotificationExtendedModel : NotificationModel
    {
        public long? NotifMsgID { get; set; }
    }

    public class NotificationReceiverModel
    {
        [Key]
        public long ID { get; set; }
        public long? NotificationID { get; set; }
        public String? TargetUser { get; set; }
        public int? Status {  get; set; }
        public DateTime? MsgReadDateTime { get; set; }
        public DateTime? MsgDeletedDateTime {  get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class NotificationSettingsModel
    {
        public String? CodeGroup { get; set; }
        public String? CodeID { get; set; }
        public String? CodeName { get; set; }
        public String? Description {  get; set; }
        public int? SeqOrder {  get; set; }
        public int? ConfigurationID {  get; set; }
        public String? UserID {  get; set; }
        public String? ConfigurationKey { get; set; }
        public String? ConfigurationValue { get; set; }
    }

    public class NotificationConfigModel
    {
        public String? Key { get; set; }
        public String? Value { get; set; } 
    }

    public class NotificationConfiguration : ConfigurationModel
    {

    }
}
