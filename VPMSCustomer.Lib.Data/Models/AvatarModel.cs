using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class AvatarModel
    {
        [Key]
        public int ID { get; set; }
        public String? EntityGroup { get; set; }
        public String? EntitySubGroup { get; set; }
        public String? AvatarFileName { get; set; }
        public String? AvatarFilePath { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get;set; }
    }
}
