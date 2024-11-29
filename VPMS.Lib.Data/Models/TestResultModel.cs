using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class TestResultModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime? ResultDateTime { get; set; }
        public String? ResultType {  get; set; }
        public String? ResultStatus { get; set; }
        public String? ResultValue { get; set; }
        public String? ResultParameter { get; set; }
        public String? ReferenceRange {  get; set; }
        public String? PatientID { get; set; }
        //public String? OwnerID { get; set; }
        public String? PetID { get; set; }
        public String? PetName { get; set; }
        public String? OperatorID { get; set; }
        public String? InchargeDoctor { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class TestResultsTxnModel
    {
        [Key]
        public int ID { get; set; }
        public int? BranchID { get; set; }
        public DateTime? ResultDateTime { get; set; }
        public String? ResultCategories {  get; set; }
        public String? ResultType { get; set;  }
        public String? PatientID {  get; set; }
        public String? PetID { get; set; }
        public String? OperatorID { get; set; }
        public String? InchargeDoctor { get; set; }
        public String? DeviceName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class TestResultViewModel()
    {
        public int SeqNo { get; set; }
        public int? ResultID { get; set; }
        public DateTime? ResultDateTime { get; set; }
        public String? ResultCategories { get; set; }
        public String? ResultType { get; set; }
        public String? ResultStatus { get; set; }
        public String? ResultValue { get; set; }
        public String? ResultUnit { get; set; }
        public String? ReferenceRange { get; set; }
        public String? InchargeDoctor { get; set; }
        public String? OperatorID { get; set; }
        public String? DeviceName { get; set; }
    }
}
