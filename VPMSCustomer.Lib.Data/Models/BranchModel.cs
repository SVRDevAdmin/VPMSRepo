using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class BranchModel
    {
        [Key]
        public int ID { get; set; }
        public int? OrganizationID { get; set; }
        public String? Name { get; set; }
        public String? ContactNo {  get; set; }
        public String? Address { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
