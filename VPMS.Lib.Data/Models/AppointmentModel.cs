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

    public class AppointmentListModel : AppointmentModel
    {
        public String? Services { get; set;  }
        public String? ServiceName { get; set; }
        public String? PetName { get; set; }
        public String? DoctorName { get; set;  }
        public String? OwnerName { get; set; }
        public long? PatientID { get; set; }
    }

    public class UpcomingAppointment
    {
        public DateOnly? ApptDate { get; set; }
        public TimeOnly ApptStartTime { get; set; }
		public TimeOnly ApptEndTime { get; set; }
		public string PetName { get; set; }
        public string Service { get; set; }
        public string Doctor { get; set; }
    }
}
