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

	public class PatientInfoProfile
	{
		public List<Patient_Owner> patient_Owners { set; get; }
		public List<Pets> Pets { set; get; }
	}

    public class PatientTreatmentPlan : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public int PetID { get; set; }
        public int PlanID { get; set; }
        public string PlanName { get; set; } = null!;
        public DateOnly TreatmentStart { get; set; }
        public DateOnly TreatmentEnd { get; set; }
        public float TotalCost { get; set; }
		public int Status { get; set; }
	}
}
