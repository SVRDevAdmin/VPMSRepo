namespace VPMSWeb.Lib.API.AppointmentByDate
{
    public class RequestMessage
    {
        public General.RequestHeaderBase header { get; set; }
        public RequestMessageBody body { get; set; }
    }

    public class RequestMessageBody
    {
        public String transtype { get; set; }
        public String startdate { get; set; }
        public String enddate { get; set; }

        public Boolean ValidateRequiredField()
        {
            Boolean isValid = true;

            if (String.IsNullOrEmpty(transtype))
            {
                isValid = false;
            }

            if (String.IsNullOrEmpty(startdate))
            {
                isValid = false;
            }

            if (String.IsNullOrEmpty(enddate))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
