using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class DoctorModel : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int IsDeleted { get; set; }
    }
}
