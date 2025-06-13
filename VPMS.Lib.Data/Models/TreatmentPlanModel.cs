using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class TreatmentPlanModel : AuditModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int BranchID { get; set; }
        public string Remarks { get; set; }
        public float TotalPrice { get; set; }
        public int Status { get; set; }
		public int IsDeleted { get; set; }
	}

    public class TreatmentPlanService : AuditPartialModel
    {
        [Key]
        public int ID { get; set; }
        public int PlanID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public float Price { get; set; }
        public int IsDeleted { get; set; }
	}

	public class TreatmentPlanProduct : AuditPartialModel
	{
		[Key]
		public int ID { get; set; }
		public int PlanID { get; set; }
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public int Units { get; set; }
		public float PricePerQty { get; set; }
		public float TotalPrice { get; set; }
		public int IsDeleted { get; set; }
	}

    public class TreatmentPlanInfos
    {
        public List<TreatmentPlanExtendedModel> treatmentPlans { get; set; }
        public int totalTreatmentPlan {  get; set; }
	}

    public class TreatmentPlanExtendedModel : TreatmentPlanModel
    {
        public int? SeqNo { get; set; }
        public int? OrganizationID { get; set; }
        public String? BranchName { get; set; }
        public String? OrganizationName { get; set; }
    }
}
