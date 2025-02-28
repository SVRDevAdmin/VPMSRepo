using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VPMSCustomer.Lib.Data.Models
{
    public class CustomerLoginSession
    {
        [Key]
        public long ID { get; set; }
        public String? SessionID { get; set; }
        public DateTime? SessionCreatedOn { get; set; }
        public DateTime? SessionExpiredOn { get; set; }
        public String? UserID { get; set; }
        public String? LoginID { get; set; }
    }

    public class CustomerLoginSessionLog
    {
        [Key]
        public long ID { get; set;  }
        public String? ActionType { get; set; }
        public String? SessionID { get; set; }
        public DateTime? SessionCreatedOn { get; set; }
        public DateTime? SessionExpiredOn { get; set; }
        public String? LoginID { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
