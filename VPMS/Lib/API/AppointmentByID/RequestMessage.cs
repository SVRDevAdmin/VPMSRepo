namespace VPMSWeb.Lib.API.AppointmentByID
{
    public class RequestMessage
    {
        public General.RequestHeaderBase? header { get; set;  }
        public RequestMessageBody? body { get; set; }
    }

    public class RequestMessageBody
    {
        public String? appointmentuniqueid { get; set; }

        public Boolean ValidateRequiredField()
        {
            Boolean isValid = true;

            if (String.IsNullOrEmpty(appointmentuniqueid))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
