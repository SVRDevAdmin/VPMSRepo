using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public class UserProfileExtObj : UserModel
    {
        public int OrganizationID { get; set; }
        public String? LoginID {  get; set; }
    }

    public class UserListingViewObject
    {
        public int SeqNo { get; set; }
        public String UserID { get; set; }
        public String StaffName { get; set; }
        public String StaffID { get; set; }
        public String RoleID {  get; set; }
        public String RoleName { get; set; }
        public String Gender {  get; set; }
        public String EmailAddress { get; set; }
        public String Status { get; set;  }
        public String StatusName { get; set; }
        public String BranchID { get; set; }
        public String BranchName { get; set;  }
        public String OrganizationID { get; set; }
        public String Organization { get; set; }
        public int TotalRows { get; set; }
    }

    public class UserInputControllerObj
    {
        public String userID { get; set; }
        public string surName { get; set; }
        public String lastName { get; set; }
        public String staffID { get; set; }
        public String gender { get; set; }
        public String emailAddress { get; set; }
        public String roleID { get; set; }
        public String organizationID { get; set; }
        public String branchID { get; set; }
        public String userStatus { get; set; }
        public String loginID { get; set; }
    }

    public class IdentityUserObject
    {
        public String Id { get; set; }
        public String? UserName { get; set; }
        public String? NormalizedUserName { get; set; }
        public String? Email { get; set; }
        public String? NormalizedEmail { get; set; }
        public int? EmailConfirmed { get; set; }
        public String? PasswordHash { get; set; }
        public String? SecurityStamp { get; set; }
        public String? ConcurrencyStamp { get; set; }
        public String? PhoneNumber { get; set; }
        public int? PhoneNumberConfirmed { get; set; }
        public int? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public int? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
    }

    [PrimaryKey("UserId", "RoleId")]
    public class IdentityUserRoleObject
    {
        public String UserId { get; set; }
        public String RoleId { get; set; }
    }
}
