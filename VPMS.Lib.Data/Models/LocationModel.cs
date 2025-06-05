using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class LocationModel
    {
        [Key]
        public int ID { get; set; }
        public String? System_LocationID { get; set; }
        public String? System_LocationName { get; set; }
        public int? System_Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
