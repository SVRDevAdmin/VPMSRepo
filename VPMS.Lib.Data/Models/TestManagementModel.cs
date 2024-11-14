using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
	public class TestManagementModel
	{
	}

	public class TestResults : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int BranchID { get; set; }
		public DateTime ResultDateTime { get; set; }
		public string ResultCategories { get; set; } = null!;
		public string ResultType { get; set; } = null!;
		public string PatientID { get; set; }
		public string PetID { get; set; } = null!;
		public string OperatorID { get; set; } = null!;
		public string InchargeDoctor { get; set; } = null!;
		public string DeviceName { get; set; } = null!;
	}

	public class TestResultDetails : AuditPartialModel
	{
		[Key]
		public int ID { get; set; }
		public int ResultID { get; set; }
		public string ResultParameter { get; set; } = null!;
		public int ResultSeqID { get; set; }
		public string ResultStatus { get; set; } = null!;
		public string ResultValue { get; set; } = null!;
		public string? ResultUnit { get; set; }
		public string? ReferenceRange { get; set; }
	}
}