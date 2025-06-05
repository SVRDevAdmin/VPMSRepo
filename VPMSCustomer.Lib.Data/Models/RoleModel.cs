using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class RoleModel
    {
        [Key]
        public String RoleID {  get; set; }
        public String? RoleName {  get; set; }
        public int? RoleType { get; set; }
        public int? BranchID { get; set; }
        public int? Status { get; set; }
        public int? IsAdmin { get; set; }
        public int? IsDoctor { get; set; }
        public String? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy {  get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy {  get; set; }
    }
}
