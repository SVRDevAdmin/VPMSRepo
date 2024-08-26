using System.ComponentModel.DataAnnotations;
namespace VPMS.Lib.Data.Models
{
    public class BranchModel : AuditModel
    {
        [Key]
        public int ID { get; set; }

        public int OrganizationID { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int Status { get; set; }
    }
}
