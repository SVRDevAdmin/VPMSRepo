using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class TestingModel
    {
        [Key]
        public int Id { get; set; }
        public String ColumnA { get; set; }
        public String ColumnB { get; set; }
    }

}
