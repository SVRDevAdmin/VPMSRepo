using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class UserModel
    {
        [Key]
        public string UserID { get; set; } 
        public string Surname { get; set; } 
        public string LastName { get; set; } 
        public string StaffID { get; set; } 
        public string Gender { get; set; } 
        public string EmailAddress { get; set; } 
        public int Status { get; set; }
        public string RoleID { get; set; } 
        public int BranchID { get; set; }
        public int Level1ID { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy {  get; set; }
        public DateTime? UpdatedDate {  get; set; }
        public String? UpdatedBy { get; set; }
        public int? OrganizationID { get; set; }
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
