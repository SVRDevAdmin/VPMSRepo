namespace VPMSCustomer.Lib.Models
{
    public class LoginResponseObject
    {
        public int? StatusCode { get; set; }
        public String? StatusCodeMessage {  get; set; }
        public Boolean isFirstTimeLogin { get; set; }
        public Boolean isAccountLockedOut { get; set; }
    }
}
