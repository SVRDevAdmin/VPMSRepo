namespace VPMSWeb.Lib.API.UpdateTestResults
{
    public class ResponseMessage
    {
        public General.ResponseHeaderBase header { get; set; }
        public ResponseMessageBody body { get; set; }
    }

    public class ResponseMessageBody
    {
        public String responsecode { get; set; }
        public String responsestatus { get; set; }
        public String responsemessage { get; set; }
    }
}
