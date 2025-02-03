using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class ClientModel
    {
        [Key]
        public int ID { get; set; }
        public String? Name { get; set; }
        public String? Description { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
        public int? BranchID { get; set; }
    }

    public class ClientAuthModel
    {
        [Key]
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public String? ClientKey { get; set; }
        public DateTime? StartDate {  get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
