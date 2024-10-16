using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class RoleModel : AuditModel
    {
        [Key]
        public string RoleID { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public int RoleType { get; set; }

        public int Status { get; set; }

		public int IsAdmin { get; set; }

		public int IsDoctor { get; set; }
	}
}
