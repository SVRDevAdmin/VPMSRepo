using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class TaxModel
    {
        [Key]
        public int ID { get; set; } 
        public String? TaxType { get; set; }
        public String? Description { get; set; }
        public Decimal? ChargesRate { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class TaxConfigurationListModel : TaxModel
    {
        public int SeqNo { get; set; }
        public String? TaxName { get; set; }
        public String? StatusName { get; set; }
        public String? CreatedDateInString { get; set; }
    }
}
