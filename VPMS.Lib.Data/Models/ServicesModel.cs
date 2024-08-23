using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
	public class ServicesModel : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int CategoryID { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public float Duration { get; set; }
		public int Status { get; set; }
		public string Description { get; set; }
		public int BranchID { get; set; }
	}

	public class ServiceCategory : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public string Name { get; set; }
		public int Status { get; set; }
	}
}
