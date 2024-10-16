using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
	public class CurrencyModel
	{
		[Key]
		public int ID { get; set; }
		public string Country { get; set; } = null!;
		public string CurrencySymbol { get; set; } = null!;

	}
}
