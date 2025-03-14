using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class PetDataModel
    {
        [Key]
        public long ID { get; set; }
        public long PatientID { get; set; }
        public String? Name { get; set; }
        public String? RegistrationNo { get; set; }
        public String? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        public String? Species { get; set; }
        public String? Breed { get; set; }
        public String? Color { get; set; }
        public String? Allergies { get; set; }
        public Decimal? Weight { get; set; }
        public String? WeightUnit { get; set; }
        public Decimal? Height { get; set; }
        public String? HeightUnit { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy {  get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
        public int? AvatarID { get; set; }
    }

    public class PetDataExtendedModel : PetDataModel
    {
        public String? AvatarImage { get; set; }
        public String? ColorCode { get; set; }
    }

    public class PetTreatmentServiceModel
    {
        public int ServiceID { get; set; }
        public String? ServiceName { get; set; }
        public String? CategoryName { get; set; }
        public String? SubCategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class PetMedicationModel
    {
        public String? PlanName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? Usage {  get; set; }
        public String? Description { get; set; }
    }

    public class PetTreatmentPlanModel
    {
        public int TreatmentPlanID { get; set; }
        public String? PlanName { get; set; }
        public DateTime? TreatmentStart { get; set; }
        public DateTime? TreatmentEnd { get; set; }
        public String? Remarks { get; set; }
    }

    public class PetTestResultsModel
    {
        public int SeqNo { get; set; }
        public DateTime? ResultDateTime { get; set; }
        public String? ResultType { get; set;  }
        public String? OverallStatus { get; set; }
        public String? PatientID { get; set; }
        public String? OperatorID { get; set; }
        public String? DeviceName { get; set; }
        public int? TotalRows { get; set; }
    }
}
