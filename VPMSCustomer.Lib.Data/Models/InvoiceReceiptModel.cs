using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Pkcs;

namespace VPMSCustomer.Lib.Data.Models
{
    public class InvoiceReceiptModel
    {
        public int SeqNo { get; set; }
        public int InvReceiptID { get; set; }
        public int? TreatmentPlanID { get; set; }
        public int? Branch {  get; set; }
        public String? InvoiceNo {  get; set; }
        public String? ReceiptNo {  get; set; }
        public Decimal? Fee { get; set; }
        public Decimal? Tax {  get; set; }
        public Decimal? GrandDiscount { get; set; }
        public int? PetID { get; set; }
        public String? PetName { get; set; }
        public String? DoctorName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class InvoiceReceiptDetailsObj
    {
        public int ID { get; set; }
        public String? InvoiceNo { get; set; }
        public String? ReceiptNo { get; set; }
        public String? OwnerName { get; set; }
        public String? Address { get; set; }
        public String? ContactNo { get; set; }
        public String? PetName { get; set; }
        public String? RegistrationNo { get; set; }
        public String? Species { get; set; }
        public String? PlanName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Decimal? Tax { get; set; }
        public Decimal? Fee { get; set;}
    }

    public class InvoiceReceiptServicesObj
    {
        public int ID { get; set; }
        public int? ServiceID { get; set; }
        public String? ServiceName { get; set; }
        public Decimal? Price { get; set; }
        public Decimal? Discount { get; set; }
        public Decimal? TotalPrice { get; set; }
    }

    public class InvoiceReceiptProductsObj
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public int? Units { get; set; }
        public Decimal? PricePerQty { get; set; }
        public Decimal? Discount { get; set; }
        public Decimal? TotalPrice { get; set; }
    }

    public class InvoiceReceiptTotalObj
    {
        public String? ItemName { get; set; }
        public Decimal? ItemTotalValue { get; set; }
    }

    public class DailyExpenseSummObject 
    { 
        public DateTime InvoiceDate { get; set; }
        public long? PatientID { get; set; }
        public long? PetID { get; set; }
        public int? ServiceID { get; set; }
        public String? ServiceName { get; set; }
        public Decimal? ServicePrice { get; set; }
        public String? EntityName { get; set; }
    }
}
