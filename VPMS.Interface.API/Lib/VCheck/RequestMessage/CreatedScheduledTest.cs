using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Interface.API.VCheck.RequestMessage
{
    public class CreateScheduledTestRequest
    {
        public CreateScheduledTestHeader header { get; set; }
        public CreateScheduledTestBody body { get; set; }
    }

    public class CreateScheduledTestHeader : RequestHeaderObject
    {

    }

    public class CreateScheduledTestBody
    {
        public String? locationid { get; set; }
        public String? scheduledtestname { get; set; }
        public String? scheduleddatetime {  get; set; }
        public String? testuniqueid { get; set; }
        public String? scheduledby {  get; set; }
        public String? personincharges { get; set; }
        public String? patientid { get; set; }
        public String? patientname { get; set; }
        public String? gender { get; set; }
        public String? species { get; set; }
        public String? ownername { get; set; }
        public String? scheduledcreateddate { get; set; }
    }
}
