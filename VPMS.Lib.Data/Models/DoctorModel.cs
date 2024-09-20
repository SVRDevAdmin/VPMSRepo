using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class DoctorModel
    {
        [Key]
        public int ID { get; set; }
        public String? Name { get; set; }
        public String? Gender { get; set; }
        public String? LicenseNo { get; set; }
        public String? Designation { get; set; }
        public String? Specialty { get; set; }
        public String? System_ID { get; set; }
        public int? IsDeleted {  get; set; }
        public DateTime? CreatedDateTimestamp { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDateTimestamp { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
        public int? BranchID { get; set; }
    }

    public class DoctorExtendedModel  : DoctorModel
    {
        public String? GenderName {  get; set; }
    }

    public class NewDoctorControllerModel
    {
        public int? ID { get; set; }
        public String? doctorName {  get; set; }
        public String? doctorGender { get; set; }
        public String? licenseNo { get; set; }
        public String? systemID { get; set; }
        public String? designation { get; set; }
        public String? specialty { get; set; }
        public int? branchID { get; set; }
    }
}
