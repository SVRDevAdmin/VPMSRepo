using System.ComponentModel.DataAnnotations;

namespace VPMS.Lib.Data.Models
{
    public class UserModel : AuditModel
    {
        [Key]
        public string UserID { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public int Status { get; set; }

        public string RoleID { get; set; } = null!;

        public int BranchID { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}
