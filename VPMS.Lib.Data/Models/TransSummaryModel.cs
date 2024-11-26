using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class TransSummaryModel
    {
        public long ID { get; set; }
        public String? SummaryType { get; set; }
        public DateTime? SummaryDate { get; set; }
        public int? BranchID { get; set; }
        public String? DateInYear { get; set; }
        public String? DateInMonth { get; set; }
        public int? Week {  get; set; }
        public int? Quarter { get; set; }
        public String? Group {  get; set; }
        public String? SubGroup { get; set; }
        public Decimal? TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class TransSummaryLogModel
    {
        [Key]
        public int ID { get; set; }
        public String? TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }
}
