using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class AuditModel : AuditPartialModel
    {
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class AuditPartialModel
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;

	}

	public class AuditPartialModelUpdated
	{
		public DateTime? UpdatedDate { get; set; }
		public string? UpdatedBy { get; set; }

	}
}
