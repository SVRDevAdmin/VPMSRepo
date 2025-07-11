using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class CurrencyModel
    {
        [Key]
        public int ID { get; set; }
        public String? Country { get; set; }
        public String? CurrencyCode { get; set; }
        public String? CurrencySymbol { get; set; }
        public String? DisplayFormat { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
