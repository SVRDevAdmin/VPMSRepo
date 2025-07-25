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
		public float Prices { get; set; }
		public float Duration { get; set; }
		public int Status { get; set; }
		public string Description { get; set; }
		public string Precaution { get; set; }
		public int BranchID { get; set; }
		//public string DoctorInCharge { get; set; }
	}

	public class ServiceCategory : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public string Name { get; set; }
		public string SubCategoryName { get; set; }
		public int Status { get; set; }
	}

	public class ServiceDoctor : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int ServiceID { get; set; }
		public int DoctorID { get; set; }
		public int IsDeleted { get; set; }
	}

	public class ServiceList
	{
		public int No { get; set; }
		public int ID { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public float Price { get; set; }
		public string DoctorInCharge { get; set; }
		public string Organisation { get; set; }
		public string Branch { get; set; }
		public float Duration { get; set; }
		public int Status { get; set; }
	}

	public class ServicesInfo
	{
		public List<ServiceList> ServiceList { get; set; }
		public int totalServices { get; set; }
	}

}
