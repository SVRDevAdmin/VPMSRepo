using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class TestsListModel
    {
        [Key]
        public int ID { get; set; }
        public String? System_TestID { get; set; }
        public String? System_TestName { get; set; }
        public String? System_Description { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedDate {  get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set;  }
    }

    public class scheduledTestsSubmission
    {
        [Key]
        public long ID { get; set; }
        public long? PatientID { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public String? TestID { get; set; }
        public String? TestName { get; set; }
        public String? LocationID { get; set; }
        public String? LocationName { get; set; }
        public int? Status { get; set; }
        public DateTime? SubmissionSent { get; set; }
        public String? ResponseStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
