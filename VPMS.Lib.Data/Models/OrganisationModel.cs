using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
	public class OrganisationModel : AuditModel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public int TotalStaff { get; set; }
		public int Status { get; set; }
	}
}
