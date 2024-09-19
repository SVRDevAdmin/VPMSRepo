namespace VPMSWeb.Models
{
    public class ResponseStatusObject
    {
        public int? StatusCode { get; set; }
        public Boolean? isDoctApptOverlap { get; set; }
        public Boolean? isPatientAppOverlap { get; set; }
        public int? TotalRecords { get; set; }
    }
}
