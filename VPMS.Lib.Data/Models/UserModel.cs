using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class UserModel : AuditModel
    {
        [Key]
        public string UserID { get; set; } = null!;

        public string Surname { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string StaffID { get; set; } = null!;

		public string Gender { get; set; } = null!;

		public string EmailAddress { get; set; } = null!;

        public int Status { get; set; }

        public string RoleID { get; set; } = null!;

        public int BranchID { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }

    public class StaffProfileInfo
	{
		public string UserID { get; set; } = null!;
		public string Surname { get; set; }
        public string LastName {  get; set; }
        public string StaffID { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string Organisation {  get; set; }
		public string Email { get; set; }
        public string Branch {  get; set; }
        public int Status { get; set; }
	}
}
