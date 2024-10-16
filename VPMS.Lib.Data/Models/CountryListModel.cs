using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
	public class CountryListModel
	{
		[Key]
		public int ID { get; set; }
		public String? CountryCode { get; set; }
		public String? CountryName { get; set; }
		public Boolean IsActive { get; set; }
	}

	public class StateModel : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int CountryID { get; set; }
		public String? State { get; set; }
	}

	public class CityModel : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int StateID { get; set; }
		public String? City { get; set; }
	}
}