using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Interface.API.General;

namespace VPMS.Interface.API.VCheck.ResponseMessage
{
    public class GetTestListResponse 
    {
        public GetTestListResponseHeader header { get; set; }
        public GetTestListResponseBody body { get; set; }
    }

    public class GetTestListResponseHeader : ResponseHeaderBase
    {

    }

    public class GetTestListResponseBody
    {
        public String? responseCode { get; set; }
        public String? responseStatus { get; set; }
        public String? responseMessage { get; set; }
        public List<TestListObject> results { get; set; }
    }

    public class TestListObject 
    { 
        public String? testid { get; set; }
        public String? testname { get; set; }
        public String? testdescription { get; set; }
    }

}
