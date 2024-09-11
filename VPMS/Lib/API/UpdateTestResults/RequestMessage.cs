namespace VPMSWeb.Lib.API.UpdateTestResults
{
    public class RequestMessage
    {
        public General.RequestHeaderBase? header { get; set; }
        public RequestMessageBody body { get; set; }
    }

    public class RequestMessageBody
    {
        public List<TestResultObject> results { get; set; }

        public Boolean ValidateRequiredField()
        {
            Boolean isValid = true;

            if (results == null || results.Count == 0)
            {
                isValid = false;
            }

            return isValid;
        }
    }

    public class TestResultObject
    {
        public String resulttype {  get; set; }
        public String resultdatetime { get; set; }
        public String operatorid { get; set; }
        public String patientid { get; set; }
        public String ownerid { get; set; }
        public String petid { get; set; }
        public String petname { get; set; }
        public String inchargedoctor { get; set; }
        public String resultstatus { get; set; }
        public String resultvalue { get; set; }
        public String resultparameter { get; set; }
        public String referencerange { get; set; }
    }
}
