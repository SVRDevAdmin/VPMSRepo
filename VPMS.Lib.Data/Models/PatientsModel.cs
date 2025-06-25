using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class PatientsModel : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public int BranchID { get; set; }
    }

    public class Pets : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string Name { get; set; } = null!;
        public string RegistrationNo { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateOnly DOB { get; set; }
        public int Age { get; set; }
        public string Species { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string? Allergies { get; set; }
        public Decimal Weight { get; set; }
        public string WeightUnit { get; set; } = null!;
        public Decimal Height { get; set; }
        public string HeightUnit { get; set; } = null!;
        public int Status { get; set; }
        public int AvatarID { get; set; }
    }

    public class Pet_Growth : AuditPartialModel
	{
		[Key]
		public int ID { get; set; }
		public int PetID { get; set; }
		public int Age { get; set; }
		public Decimal Height { get; set; }
		public Decimal Weight { get; set; }
		public string? Allergies { get; set; }
		public Decimal BMI { get; set; }
	}

    public class Patient_Owner : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
		public string EmailAddress { get; set; } = null!;
		public string Address { get; set; } = null!;
        public string PostCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int Status { get; set; }
        public int IsPrimary { get; set; }
    }

    public class Patient_Owner_Login
    {
        [Key]
        public long ID { get; set; }
        public long? PatientOwnerID { get; set; }
        public int? ProfileActivated { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? CreatedDate {  get; set; }
        public String? CreatedBy {  get; set; }
        public DateTime? UpdatedDate {  get; set; }
        public String? UpdatedBy {  get; set; }
        public String? AspnetUserID { get; set; }
    }

    public class Account_Creation_Logs
    {
        [Key]
        public long ID { get; set; }
        public String? EmailAddress { get; set; }
        public String? InvitationCode { get; set; }
        public DateTime? LinkCreatedDate {  get; set; }
        public DateTime? LinkExpiryDate { get; set; }
        public DateTime? AccountCreationDate { get; set; }
        public int? PatientOwnerID { get; set; }
    }

    public class Pets_Breed : AuditPartialModel
    {
        [Key]
        public int ID { get; set; }
        public string Species { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public int Active { get; set; }
        public int SeqOrder { get; set; }
    }

    public class PatientsInfo
    {
        public List<PetsInfo> petsInfo { get; set; }
        public int totalPatients { get; set; }
	}

    public class PetsInfo
    {
        public int No { get; set; }
        public int PatientID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public string Species { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
	}

    public class PatientSelectionModel
    {
        [Key]
        public long ID { get; set; }
        public long PatientID { get; set; }
        public String? Name { get; set; }
    }

    public class PetsSelectionModel
    {
        [Key]
        public long ID { get; set; }
        public String? Name { get; set; }
    }

    public class PatientOwnerModel
    {
        [Key]
        public long ID { get; set; }
        public long PatientID { get; set; }
        public String? Name { get; set; }
        public String? Gender { get; set; }
        public String? ContactNo { get; set; }
        public String? EmailAddress { get; set; } = null!;
        public String? Address { get; set; }
        public String? PostCode { get; set; }
        public String? City { get; set; }
        public String? State { get; set; }
        public String? Country { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class PetModel
    {
        [Key]
        public long ID { get; set; }
        public long? PatientID { get; set; }
        public String? Name { get; set; }
        public String? RegistrationNo { get; set; }
        public String? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        public String? Species { get; set; }
        public String? Breed { get; set; }
        public String? Color { get; set; }
        public String? Allergies { get; set; }
        public decimal? Weight { get; set; }
        public String? WeightUnit { get; set; }
        public decimal? Height { get; set; }
        public String? HeightUnit { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

	public class PatientInfoProfile
	{
		public List<Patient_Owner> patient_Owners { set; get; }
		public List<Pets> Pets { set; get; }
	}

    public class PatientPetInfo
    {
        public long OwnerID { get; set; }
        public long PatientID { get; set; }
        public String? Name { get; set; }
        public String? Gender { get; set; }
        public String? Address { get; set; }
        public String? PostCode { get; set;  }
        public String? City { get; set; }
        public String? State { get; set; }
        public String? Country { get; set; }
        public long? PetID { get; set; }
        public String? PetName { get; set; }
        public DateTime? PetDOB { get; set; }
    }    

    public class PatientTreatmentPlan : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public int PetID { get; set; }
        public int TreatmentPlanID { get; set; }
        public string PlanName { get; set; } = null!;
        public DateOnly TreatmentStart { get; set; }
        public DateOnly TreatmentEnd { get; set; }
        public float TotalCost { get; set; }
		public String? Remarks { get; set; }
		public int Status { get; set; }
	}

	public class PatientTreatmentPlanServices : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int PlanID { get; set; }
		public int ServiceID { get; set; }
		public string ServiceName { get; set; }
		public float Price { get; set; }
		public float Discount { get; set; }
		public float TotalPrice { get; set; }
		public int IsDeleted { get; set; }
	}

	public class PatientTreatmentPlanProducts : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int PlanID { get; set; }
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public int Units { get; set; }
		public float PricePerQty { get; set; }
		public float TotalPrice { get; set; }
		public float Discount { get; set; }
		public int IsDeleted { get; set; }
	}

    public class PatientVaccinationTreatments
	{
		public DateOnly Date { get; set; }
		public string CategoryName { get; set; } = null!;
		public string ServiceName { get; set; }
	}

	public class PatientHealthCardMedication
	{
		public DateOnly Date { get; set; }
		public string Name { get; set; } = null!;
		public int Status { get; set; }
	}

	public class PatientMedicalRecordService : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int PetID { get; set; }
		public int CategoryID { get; set; }
		public int ServiceID { get; set; }
		public int Type { get; set; }
		public DateOnly DueDate { get; set; }
		public string Description { get; set; } = null!;
		public string Remarks { get; set; } = null!;
		public int IsDeleted { get; set; }
	}

	public class PatientMedicalRecordMedication : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int PetID { get; set; }
		public int CategoryID { get; set; }
		public int ProductID { get; set; }
		public DateOnly ExpiryDate { get; set; }
		public int Status { get; set; }
		public string Description { get; set; } = null!;
	}

    public class PetAvatarObject : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public String? Species { get; set; }
        public String? AvatarImage { get; set; }
        public String? ColorCode { get; set; }
        public int? Status { get; set; }
    }
}
