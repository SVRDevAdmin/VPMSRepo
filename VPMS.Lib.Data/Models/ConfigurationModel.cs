using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class ConfigurationModel
    {
        [Key]
        public int ID { get; set; }
        public String? UserID { get; set; }
        public String? ConfigurationKey { get; set; }
        public String? ConfigurationValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
