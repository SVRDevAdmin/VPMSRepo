using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class LoginSessionModel
    {
        [Key]
        public int ID { get; set; }
        public string SessionID { get; set; } = null!;
        public DateTime SessionCreatedOn { get; set; }
        public DateTime SessionExpiredOn { get; set; }
        public string LoginID { get; set; } = null!;
    }

    public class LoginSessionLogModel : AuditPartialModel
    {
        [Key]
        public int ID { get; set; }
        public string ActionType { get; set; } = null!;
        public string SessionID { get; set; } = null!;
        public DateTime SessionCreatedOn { get; set; }
        public DateTime SessionExpiredOn { get; set; }
        public string LoginID { get; set; } = null!;
    }
}
