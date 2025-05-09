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
        public GetTestListRequestHeader header { get; set;  }
        public GetTestListRequestBody body { get; set; }
    }

    public class GetTestListRequestHeader : RequestHeaderBase
    {

    }

    public class GetTestListRequestBody
    {

    }
}
