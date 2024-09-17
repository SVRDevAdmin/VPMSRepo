namespace VPMSWeb.Lib.API.General
{
    public class ResponseHeaderBase
    {
        private DateTime _timeStamp;

        public String timestamp 
        {
            get
            {
                return _timeStamp.ToString("o");
            }
            set
            {
                _timeStamp = DateTime.Parse(value, System.Globalization.CultureInfo.InvariantCulture, 
                                            System.Globalization.DateTimeStyles.RoundtripKind);
            }
        }

        public String authtoken { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime realTimeStamp
        {
            get
            {
                return _timeStamp;
            }
            set
            {
                _timeStamp = value;
            }
        }
    }
}
