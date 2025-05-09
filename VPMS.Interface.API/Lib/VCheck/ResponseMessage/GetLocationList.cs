using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Interface.API.General;

namespace VPMS.Interface.API.VCheck.ResponseMessage
{
    public class GetLocationListResponse
    {
        public GetLocationListRespHeader header { get; set; }
        public GetLocationListRespBody body { get; set; }
    }

    public class GetLocationListRespHeader : ResponseHeaderBase
    {
    }
    
    public class GetLocationListRespBody
    {
        public String? responseCode { get; set; }
        public String? responseStatus { get; set; }
        public String? responseMessage { get; set; }
        public List<LocationListResultObject> results { get; set; }
    }

    public class LocationListResultObject
    {
        public String? locationid { get; set; }
        public String? name { get; set;  }
        public String? status { get; set; }
        public String? createddate { get; set; }
        public String? modifieddate { get; set; }
    }
}
