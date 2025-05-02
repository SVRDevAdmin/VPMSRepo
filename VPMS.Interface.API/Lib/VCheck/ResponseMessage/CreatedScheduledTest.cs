using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Interface.API.General;

namespace VPMS.Interface.API.VCheck.ResponseMessage
{
    public class CreateScheduledTestResponse
    {
        public CreateScheduledTestResponse header { get; set; }
        public CreateScheduledTestResponseBody body { get; set; }
    }

    public class CreateScheduledTestResponseHeader : ResponseHeaderBase
    {

    }

    public class CreateScheduledTestResponseBody
    {
        public String? responseCode { get; set; }
        public String? responseStatus { get; set; }
        public String? responseMessage { get; set; }
    }
}
