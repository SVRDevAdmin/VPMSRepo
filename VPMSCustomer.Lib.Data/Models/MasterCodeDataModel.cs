using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMSCustomer.Lib.Data.Models
{
    public class MasterCodeDataModel
    {
        [Key]
        public int ID { get; set; }
        public String? CodeGroup { get; set; }
        public String? CodeID {  get; set; }
        public String? CodeName {  get; set; }
        public String? Description {  get; set; }
        public int? IsActive {  get; set; }
        public int? SeqOrder { get; set;}
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
