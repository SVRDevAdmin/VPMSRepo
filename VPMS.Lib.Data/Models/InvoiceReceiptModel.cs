using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
	public class InvoiceReceiptModel : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public int TreatmentPlanID { get; set; }
		public int Branch { get; set; }
		public string InvoiceNo { get; set; }
		public String? ReceiptNo { get; set; }
		public string PetName { get; set; }
		public string OwnerName { get; set; }
		public float Fee { get; set; }
		public float Tax { get; set; }
		public float GrandDiscount { get; set; }
		public int Status { get; set; }
	}

    public class InvoiceReceiptNo
    {
        [Key]
        public int ID { get; set; }
        public string OrganisationAbbr { get; set; }
        public int? BranchID { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public int? RunningNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class InvoiceReceiptInfo
	{
		public int No { get; set; }
		public int ID { get; set; }
		public string InvoiceNo { get; set; }
		public string ReceiptNo { get; set; }
		public DateTime Date { get; set; }
		public int PetID { get; set; }
		public string PetName { get; set; }
		public string OwnerName { get; set; }
		public float Fee { get; set; }
		public string Remarks { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
    }

	public class InvoiceReceiptInfos
	{
		public List<InvoiceReceiptInfo> invoiceReceiptList { get; set; }
		public int TotalInvoiceReceipt { get; set; }
	}

	public class ViewInvoiceReceipt
	{
		public InvoiceReceiptModel InvoiceReceipt {  get; set; }
		public PatientTreatmentPlan TreatmentPlan { get; set; }
		public List<PatientTreatmentPlanServices> Services { get; set; }
		public List<InventoryInvoice> Products { get; set; }
		public Patient_Owner Owner { get; set; }
		public Pets Pet { get; set; }
		public UpcomingAppointment UpcomingAppointment { get; set; }
    }

	public class InvoiceTransSummaryModel
	{
		public DateTime? SummaryDate { get; set; }
		public int? BranchID { get; set;  }
		public Decimal? TotalAmount { get; set;  }
		public Decimal? TotalDiscount { get; set;  }
	}

	public class TransSummaryBreakdownModel
	{
		public DateTime? SummaryDate { get; set; }
		public int? BranchID { get; set; }
		public int? PetID {  get; set; }
		public String? Species {  get; set; }
		public int? TreatmentPlanID {  get; set; }
		public String? TreatmentPlanName { get; set; }
		public Decimal? TreatmentPlanAmount { get; set; }
		public int? ServiceID {  get; set; }
		public String? ServiceName { get; set; }
		public Decimal? ServicePrice { get; set; }
    }
}
