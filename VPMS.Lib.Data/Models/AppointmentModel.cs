using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Mozilla;

namespace VPMS.Lib.Data.Models
{
    public class AppointmentModel
    {
        [Key]
        public long AppointmentID {  get; set; }
        public String? UniqueIDKey {  get; set; }
        public int? BranchID { get; set; }
        public DateTime? ApptDate { get; set; }
        public DateTime? ApptStartTime { get; set; }
        public DateTime? ApptEndTime { get; set; }
        public long? OwnerID {  get; set; }
        public long? PetID { get; set; }
        public int? Status { get; set; }
        public Boolean? EmailNotify {  get; set; }
        public String? InchargeDoctor { get; set; }
        public DateTime? CreatedDate {  get; set; } 
        public String? CreatedBy {  get; set; } 
        public DateTime? UpdatedDate {  get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class AppointmentNewClientModel
    {
        public DateTime? ApptDate { get; set; }
        public DateTime? ApptStartTime { get; set; }
        public DateTime? ApptEndTime { get; set; }
        public DateTime? PetDOB {  get; set; }
        public int? BranchID {  get; set; }
        public String? OwnerName { get; set; }
        public String? ContactNo { get; set; }
        public String? PetName {  get; set; }
        public String? Species { get; set; }
        public String? InchargeDoctor { get; set; }
        public String? EmailAddress {  get; set; }
        public Boolean? EmailNotify { get; set; }
    }

    public class AppointmentControllerModel : AppointmentModel
    {
        public List<long> ServiceList {  get; set; }
    }

    public class AppointmentNewClientControllerModel : AppointmentNewClientModel
    {
        public List<long> ServiceList { get; set; }
    }

    public class AppointmentServiceModel
    {
        [Key]
        public long ID { get; set; }
        public long? ApptID { get; set; }
        public long? ServicesID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }

    public class AppointmentPatientsModel
    {
        [Key]
        public long ID { get; set; }
        public int? BranchID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }
    //public class AppointmentPatientOwnerModel 
    //{
    //    [Key]
    //    public long ID { get; set; }
    //    public long? PatientID {  get; set; }
    //    public String? Name { get; set; }
    //    public String? ContactNo { get; set; }
    //    public DateTime? CreatedDate { get; set; }
    //    public String? CreatedBy { get; set; }
    //}

    //public class AppointmentPetModel
    //{
    //    [Key]
    //    public long ID { get; set; }
    //    public long? PatientID { get; set; }
    //    public String? Name { get; set; }
    //    public DateTime? DOB { get; set; }
    //    public String? Species { get; set; }
    //    public int? Status { get; set; }
    //    public DateTime? CreatedDate { get; set; }
    //    public String? CreatedBy { get; set; }
    //}


    public class AppointmentMonthViewModel
    {
        public DateTime? ApptDate { get; set; }
        public String? ApptStartTimeString { get; set; }
        public String? ApptEndTimeString { get; set; }
        public String? ServiceName { get; set; }
        public String? PetName { get; set; }
        public String? OwnerName {  get; set; }
        public String? DoctorName { get; set; }
        public long? AppointmentID { get; set; }
    }

    // temporary
    public class PatientSelectionModel 
    {
        [Key]
        public long ID { get; set;  }
        public long PatientID { get; set; }
        public String? Name { get; set; }
    }

    public  class PetsSelectionModel
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
}
