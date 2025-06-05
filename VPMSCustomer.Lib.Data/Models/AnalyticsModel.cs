using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class AnalyticsModel
    {
        public long ID { get; set; }
        public DateTime? TransDate { get; set; }
        public String? TransDateInMonth { get; set; }
        public String? TransDateInYear { get; set; }
        public long? PatientID { get; set; }
        public long? PetID { get; set; }
        public int? ServiceID { get; set; }
        public String? ServiceName { get; set; }
        public Decimal? TotalValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class ExpensesSummaryModel
    {
        public int? ServiceID { get; set;  }
        public String? ServiceName { get; set; }
        public Decimal? Total { get; set; }
    }

    public class ExpensesSummaryBreakdownModel
    {
        public int? PetID { get; set; }
        public String? PetName { get; set; }
        public int? ServiceID { get; set; }
        public String? ServiceName { get; set; }
        public Decimal? PetServicesTotal { get; set; }
        public Decimal? PetTotal { get; set; }
        public Decimal? ServcExpsPercentage { get; set; }
    }

    public class ExpensesSummaryLog
    {
        [Key]
        public int ID { get; set; }
        public String? TransactionType { get; set; }
        public DateTime? TransactionDate {  get; set; }
        public String? ExecutionType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }
}
