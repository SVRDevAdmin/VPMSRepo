using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Interface.API.General;

namespace VPMS.Interface.API.VCheck.RequestMessage
{
    public class GetTestListRequest
    {
        public RequestHeaderObject header { get; set;  }
        public RequestBodyObject body { get; set; }
    }

    public class RequestHeaderObject : ReqeustHeaderBase
    {

    }

    public class RequestBodyObject
    {

    }
}
