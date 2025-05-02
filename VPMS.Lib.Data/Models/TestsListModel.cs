using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class TestsListModel
    {
        [Key]
        public int ID { get; set; }
        public String? System_TestID { get; set; }
        public String? System_TestName { get; set; }
        public String? System_Description { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedDate {  get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set;  }
    }
}
