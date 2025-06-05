using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class ServiceModel
    {
        [Key]
        public int ID { get; set; }
        public int? CategoryID { get; set; }
        public String? Name { get; set; }
        public Decimal? Prices { get; set; }
        public Decimal? Duration { get; set; }
        public int? Status { get; set; }
        public String? Description { get; set; }
        public String Precaution { get; set; }
        public int? BranchID { get; set; }
        public String? DoctorInCharge { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set;  }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
