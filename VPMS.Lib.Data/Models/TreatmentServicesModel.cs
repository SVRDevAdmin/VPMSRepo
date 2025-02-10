using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class TreatmentServicesModel
    {
        [Key]
        public int ID { get; set; }
        public int? CategoryID { get; set; }
        public String? Name { get; set; }
        public decimal? Prices { get; set; }    
        public decimal? Duration { get; set; }
        public int? Status { get; set; }
        public String? Description { get; set; }
        public int? BranchID { get; set; }
        public String? DoctorInCharge { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate {  get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class TreatmentServicesDoctorModel
    {
        public int ServiceID { get; set; }
        public int DoctorID { get; set; }
        public String DoctorName { get; set; }
    }
}
