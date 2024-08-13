using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class MastercodeModel
    {
        [Key]
        public int ID { get; set; }
        public String? CodeGroup {  get; set; }
        public String? CodeID {  get; set; }
        public String? CodeName {  get; set; }
        public String? Description {  get; set; }
        public Boolean? IsActive { get; set; }
        public int? SeqOrder {  get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy {  get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
