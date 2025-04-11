using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class AppointmentModel
    {
        [Key]
        public long AppointmentID { get; set; }
        public String? UniqueIDKey { get; set; }
        public int? BranchID { get; set; }
        public DateTime? ApptDate { get; set; }
        public TimeSpan? ApptStartTime { get; set; }
        public TimeSpan? ApptEndTime { get; set; }
        public long? OwnerID { get; set; }
        public long? PetID { get; set; }
        public int? Status { get; set; }
        public Boolean? EmailNotify { get; set; }
        public String? InchargeDoctor { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class AppointmentServicesModel
    {
        [Key]
        public long ID { get; set; }
        public long? ApptID { get; set;  }
        public long? ServicesID { get; set; }
        public int? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }

    public class AppointmentDetailsModel
    {
        public long AppointmentID { get; set; }
        public String? UniqueIDKey { get; set; }
        public int? BranchID { get; set; }
        public DateTime? ApptDate { get; set; }
        public String? ApptStartTime { get; set; }
        public String? ApptEndTime { get; set; }
        public int? PetID { get; set; }
        public int? Status { get; set; }
        public long? ServicesID { get; set; }
        public String? InchargeDoctor { get; set; }
    }

    public class AppointmentGridDisplayModel
    {
        public long AppointmentID { get; set; }
        public DateTime? ApptDate { get; set; }
        public String? ApptStartTime { get; set; }
        public String? ApptEndTime { get; set; }
        public long? OwnerID { get; set; }
        public long? PetID { get; set; }
        public int? Status { get; set; }
        public String? InchargeDoctor { get; set; }
        public long? ServicesID { get; set; }
        public String? ServicesName { get; set; }
    }

    public class AppointmentGroupingModel
    {
        [Key]
        public int ID { get; set; }
        public String? AppointmentGroup { get; set; }
        public String? AppointmentSubGroup { get; set; }
        public String? AppointmentSubGrpValue { get; set; }
        public int? SeqNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }

    public class AppointmentCreationModel
    {
        public int BranchID { get; set; }
        public DateTime? ApptDate { get; set; }
        public DateTime? ApptStartTime { get; set; }
        public DateTime? ApptEndTime { get; set; }
        public int? OwnerID { get; set; }
        public int? PetID { get; set; }
        public String? InchargeDoctor { get; set; }
        public long? ServiceID { get; set; }
    }
}
