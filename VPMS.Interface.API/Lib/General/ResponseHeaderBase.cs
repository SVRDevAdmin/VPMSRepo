using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Interface.API.General
{
    public class ResponseHeaderBase
    {
        public String? timestamp {  get; set; }
        public String? clientkey { get; set; }
    }
}
