using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
	public class MasterCodeDataModel : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public string CodeGroup { get; set; } = null!;
		public string CodeID { get; set; } = null!;
		public string CodeName { get; set; } = null!;
		public string CodeDescription { get; set; } = null!;

	}
}
