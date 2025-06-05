namespace VPMSCustomer.Lib.Models
{
    public class ResponseCodeObject
    {
        public int? StatusCode { get; set; }
        public Boolean? isRecordExists { get; set; }
        public int? TotalRecords { get; set; }
    }

    public class AppointmentResponseCodeObject  : ResponseCodeObject
    { 
        public Boolean? isOverlap { get; set; }
    }

}
