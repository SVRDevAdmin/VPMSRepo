namespace VPMSWeb.Lib.API.General
{
    public class General
    {
    }

    public class AppointmentResultObject
    {
        public String? uniqueid { get; set; }
        public int? branchid { get; set; }
        public String? appointmentdate { get; set; }
        public String? starttime { get; set; }
        public String? endtime { get; set; }
        public long? patientid { get; set; }
        public long? petid { get; set; }
        public String? petname { get; set; }
        public long? ownerid { get; set; }
        public String? ownername { get; set; }
        public String? doctor { get; set; }
        public String? services { get; set; }
        public String? createddate { get; set; }
        public String? createdby { get; set; }
        public String? updateddate { get; set; }
        public String? updatedby { get; set; }
    }
}
