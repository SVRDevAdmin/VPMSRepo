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
        public String? Description { get; set; }
        public int? BranchID { get; set; }
        public int? OrganizationID { get; set; }
	}

    public class RoleModelExtObject : RoleModel
    {
        public int? OrganisationID { get; set; }
    }

    public class RoleIdentityObject
    {
        [Key]
        public String Id { get; set; }
        public String? Name { get; set; }
        public String? NormalizedName {  get; set; }
        public String? ConcurrencyStamp { get; set; }
    }

    public class RoleListingObject
    {
        public int SeqNo {  get; set; } 
        public String RoleID { get; set; }
        public String RoleName { get; set; }
        public int TotalAssigned {  get; set; }
        public String sPermission {  get; set; }
        public int TotalPermissions { get; set; }
    }

    public class RoleDropdownObject
    {
        public String RoleID { get; set; }
        public String RoleName { get; set; }
    }

    public class RolePermissionObject
    {
        [Key]
        public int ID { get; set; }
        public String? RoleID { get; set; }
        public String? PermissionKey { get; set; }
        public int? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
    }

    public class NewRoleControllerModel
    {
        public String? roleID { get; set; }
        public String? roleName { get; set; }
        public String? roleDescription { get; set; }
        public int? status {  get; set; }
        public int? branchID {  get; set; }
        public int? organizationID { get; set; }
        public int? isDoctor { get; set; }
        public int? roleType { get; set; }
        public String? userID { get; set; }
        public List<String>? permissionsList { get; set; }
    }

    public class AccessPermissionObject
    {
        [Key]
        public int ID { get; set; }
        public String? PermissionGrouping { get; set; }
        public String? PermissionKey { get; set; }
        public String? PermissionName { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy {  get; set; }
    }
}
