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
}