using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
	public class CurrencyModel : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public string? Country { get; set; }
        public string? CurrencySymbol { get; set; }
        public string? CurrencyCode { get; set; }
        public String? DisplayFormat { get; set; }
        public int? Status { get; set; }
    }
}
