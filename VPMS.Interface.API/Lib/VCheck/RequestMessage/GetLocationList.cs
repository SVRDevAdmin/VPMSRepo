using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Interface.API.General;

namespace VPMS.Interface.API.VCheck.RequestMessage
{
    public class GetLocationListRequest
    {
        public GetLocationListReqHeader header { get; set; }
        public GetLocationListReqBody body { get; set; }
    }

    public class GetLocationListReqHeader : RequestHeaderBase
    {

    }

    public class GetLocationListReqBody
    {

    }
}
