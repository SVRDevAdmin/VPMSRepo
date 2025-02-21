using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMSCustomer.Lib.Data.Models
{
    public class PatientModel
    {
        [Key]
        public long ID {  get; set; }
        public int? BranchID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class PatientLoginModel
    {
        [Key]
        public long ID { get; set; }
        public long? PatientOwnerID { get; set; }
        public int? ProfileActivated { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
        public String? AspnetUserID { get; set; }
    }

    public class PatientOwnerModel
    {
        [Key]
        public long ID { get; set; }
        public long? PatientID { get; set; }
        public String? Name { get; set; }
        public String? Gender { get; set; }
        public String? ContactNo { get; set; }
        public String? EmailAddress { get; set; }
        public String? Address { get; set; }
        public String? PostCode { get; set; }
        public String? City { get; set; }
        public String? State { get; set; }
        public String? Country { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
        public int? IsPrimary { get; set; }
    }

    public class PatientOwnerExtendedModel : PatientOwnerModel
    {
        public String? GenderName { get; set; }
    }
}
