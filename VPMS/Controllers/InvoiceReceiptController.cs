using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Cryptography.X509Certificates;
using VPMS;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class InvoiceReceiptController : Controller
    {
		private readonly InvoiceReceiptDBContext _invoiceReceiptDBContext = new InvoiceReceiptDBContext();
		private readonly PatientDBContext _patientDBContext = new PatientDBContext();
		private readonly BranchDBContext _branchDBContext = new BranchDBContext();
		private readonly InventoryDBContext _inventoryDBContext = new InventoryDBContext();

        int totalInvoiceReceipt;

		public IActionResult Index()
        {
            return View();
        }

        public IActionResult InvoiceReceiptListing()
        {
            return View();
        }

		public IActionResult CreateInvoice()
		{
			ViewData["Customer"] = _patientDBContext.Mst_Patients_Owner.ToList();
			ViewData["Items"] = _inventoryDBContext.Mst_Product.ToList();

			var branch = _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == int.Parse(HttpContext.Session.GetString("BranchID")));
			ViewData["BranchName"] = branch.Name;
			ViewData["BranchContactNo"] = branch.ContactNo;
			ViewData["BranchAddress"] = branch.Address;

			return View();
		}

		public InvoiceReceiptInfos GetInvoiceList(int rowLimit, int page, string invoiceNo, string petName, string ownerName, string doctor)
        {
			var invoiceReceiptInfos = new InvoiceReceiptInfos()
			{
				invoiceReceiptList = new List<InvoiceReceiptInfo>(),
				TotalInvoiceReceipt = 0
			};

			int start = (page - 1) * rowLimit;

			var role = HttpContext.Session.GetString("RoleName");
			var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
			var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

			if (role != "User")
			{
				try
				{
					var invoiceList = _invoiceReceiptDBContext.GetInvoiceReceiptList(start, rowLimit, 2, branch, organisation, invoiceNo, petName, ownerName, doctor, out totalInvoiceReceipt).ToList();

					//if(invoiceList.Count == 0 && start != 0)
					//{
					//	invoiceList = _invoiceReceiptDBContext.GetInvoiceReceiptList(start-1, rowLimit, 2, branch, organisation, invoiceNo, petName, ownerName, doctor, out totalInvoiceReceipt).ToList();
					//}

					invoiceReceiptInfos = new InvoiceReceiptInfos() { invoiceReceiptList = invoiceList, TotalInvoiceReceipt = totalInvoiceReceipt };

				}
				catch (Exception ex)
				{
					Program.logger.Error("Controller Error >> ", ex);
				}
			}

			return invoiceReceiptInfos;

		}

		public InvoiceReceiptInfos GetInvoiceReceiptList(int rowLimit, int page, int status, string invoiceReceiptNo, string petName, string ownerName, string doctor)
		{
			var invoiceReceiptInfos = new InvoiceReceiptInfos()
			{
				invoiceReceiptList = new List<InvoiceReceiptInfo>(),
				TotalInvoiceReceipt = 0
			};

			int start = (page - 1) * rowLimit;

			var role = HttpContext.Session.GetString("RoleName");
			var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
			var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

			if (role != "User")
			{
				try
				{
					var invoiceList = _invoiceReceiptDBContext.GetInvoiceReceiptList(start, rowLimit, status, branch, organisation, invoiceReceiptNo, petName, ownerName, doctor, out totalInvoiceReceipt).ToList();

					invoiceReceiptInfos = new InvoiceReceiptInfos() { invoiceReceiptList = invoiceList, TotalInvoiceReceipt = totalInvoiceReceipt };

				}
				catch (Exception ex)
				{
					Program.logger.Error("Controller Error >> ", ex);
				}
			}

			return invoiceReceiptInfos;
		}

		public List<InvoiceReceiptModel> GetInvoiceListByPetID(int petID)
		{
			var treatmentPlanIDList = _patientDBContext.Txn_TreatmentPlan.Where(x => x.PetID == petID).Select(y => y.ID).ToList();
			return _invoiceReceiptDBContext.Mst_InvoiceReceipt.Where(x => treatmentPlanIDList.Contains(x.TreatmentPlanID)).ToList();
		}

		public InvoiceReceiptModel GetInvoiceByID(int id)
		{
			return _invoiceReceiptDBContext.Mst_InvoiceReceipt.FirstOrDefault(x => x.ID == id);
		}

		public List<PatientTreatmentPlanServices> GetServicesByPlanID(int planID)
		{
			return _patientDBContext.Txn_TreatmentPlan_Services.Where(x => x.PlanID == planID).ToList();
		}

		public List<PatientTreatmentPlanProducts> GetProductsByPlanID(int planID)
		{
			return _patientDBContext.Txn_TreatmentPlan_Products.Where(x => x.PlanID == planID).ToList();
		}

		public int CreateQuickInvoice([FromBody] InvoiceReceiptInfo invoiceReceiptInfo)
		{
			var treatmentPlan = new PatientTreatmentPlan() { TreatmentPlanID = 0, PetID = invoiceReceiptInfo.PetID, PlanName = "Quick Invoice", TotalCost = invoiceReceiptInfo.Fee, Status = 2, Remarks = invoiceReceiptInfo.Remarks, CreatedDate = DateTime.Now, CreatedBy = HttpContext.Session.GetString("Username") };
			_patientDBContext.Txn_TreatmentPlan.Add(treatmentPlan);
			_patientDBContext.SaveChanges();

			var invoice = new InvoiceReceiptModel();
			invoice.TreatmentPlanID = treatmentPlan.ID;
			invoice.Branch = int.Parse(HttpContext.Session.GetString("BranchID"));
			invoice.InvoiceNo = invoiceReceiptInfo.InvoiceNo;
			invoice.OwnerName = invoiceReceiptInfo.OwnerName;
			invoice.PetName = invoiceReceiptInfo.PetName;
			invoice.Fee = invoiceReceiptInfo.Fee;
			invoice.Tax = 6;
			invoice.GrandDiscount = 0;
			invoice.Status = 2;
			invoice.CreatedBy = treatmentPlan.CreatedBy;
			invoice.CreatedDate = treatmentPlan.CreatedDate;

			_invoiceReceiptDBContext.Add(invoice);
			_invoiceReceiptDBContext.SaveChanges();

			return treatmentPlan.ID;
		}

		public bool UpdateInvoice([FromBody] InvoiceReceiptModel invoiceReceiptModel)
		{
			var invoice = _invoiceReceiptDBContext.Mst_InvoiceReceipt.FirstOrDefault(x => x.ID == invoiceReceiptModel.ID);
			invoice.Fee = invoiceReceiptModel.Fee;
			invoice.GrandDiscount = invoiceReceiptModel.GrandDiscount;
			invoice.Status = invoiceReceiptModel.Status;
			invoice.CreatedDate = DateTime.Now;
			invoice.CreatedBy = HttpContext.Session.GetString("Username");

			_invoiceReceiptDBContext.Update(invoice);
			_invoiceReceiptDBContext.SaveChanges();

			var treatmentPlan = _patientDBContext.Txn_TreatmentPlan.FirstOrDefault(x => x.ID == invoice.TreatmentPlanID);
			treatmentPlan.TotalCost = invoiceReceiptModel.Fee;
			treatmentPlan.Status = invoiceReceiptModel.Status;
			treatmentPlan.UpdatedDate = DateTime.Now;
			treatmentPlan.UpdatedBy = HttpContext.Session.GetString("Username");

			_patientDBContext.Update(treatmentPlan);
			_patientDBContext.SaveChanges();

			return true;
		}

		public bool UpdateInvoiceService([FromBody] PatientTreatmentPlanServices patientTreatmentPlanServices)
		{
			var service = _patientDBContext.Txn_TreatmentPlan_Services.FirstOrDefault(x => x.ID == patientTreatmentPlanServices.ID);
			service.Discount = patientTreatmentPlanServices.Discount;
			service.TotalPrice = patientTreatmentPlanServices.TotalPrice;
			service.UpdatedDate = patientTreatmentPlanServices.UpdatedDate;
			service.UpdatedBy = HttpContext.Session.GetString("Username");

			_patientDBContext.Update(service);
			_patientDBContext.SaveChanges();

			return true;
		}

		public bool UpdateInvoiceProduct([FromBody] PatientTreatmentPlanProducts patientTreatmentPlanProducts)
		{
			var product = _patientDBContext.Txn_TreatmentPlan_Products.FirstOrDefault(x => x.ID == patientTreatmentPlanProducts.ID);
			product.Discount = patientTreatmentPlanProducts.Discount;
			product.TotalPrice = patientTreatmentPlanProducts.TotalPrice;
			product.UpdatedDate = patientTreatmentPlanProducts.UpdatedDate;
			product.UpdatedBy = HttpContext.Session.GetString("Username");

			_patientDBContext.Update(product);
			_patientDBContext.SaveChanges();

			return true;
		}

		public bool MarkAsPaid(int id)
		{
			var invoice = _invoiceReceiptDBContext.Mst_InvoiceReceipt.FirstOrDefault(x => x.ID == id);
			invoice.Status = 3;
			invoice.UpdatedDate = DateTime.Now;
			invoice.UpdatedBy = HttpContext.Session.GetString("Username");

			var receiptNoList = _invoiceReceiptDBContext.Mst_InvoiceReceipt.AsNoTracking().Select(x => x.ReceiptNo);

			Random rnd = new Random();
			string receiptNoString = "";
			var existed = true;

			while (existed)
			{
				int num = rnd.Next(1, 999999);
				receiptNoString = "#" + num;
				if (!receiptNoList.Contains(receiptNoString))
				{
					existed = false;
				}
			}

			invoice.ReceiptNo = receiptNoString;

			_invoiceReceiptDBContext.Update(invoice);
			_invoiceReceiptDBContext.SaveChanges(true);

			return true;
		}

		public ViewInvoiceReceipt ViewInvoiceReceipt(int id)
		{
			var invoiceReceipt = _invoiceReceiptDBContext.Mst_InvoiceReceipt.FirstOrDefault(x => x.ID == id);
			var treatmentPlan = _patientDBContext.Txn_TreatmentPlan.FirstOrDefault(x => x.ID == invoiceReceipt.TreatmentPlanID);
			var services = _patientDBContext.Txn_TreatmentPlan_Services.Where(x => x.PlanID == invoiceReceipt.TreatmentPlanID).ToList();
			var products = _inventoryDBContext.GetInventoryInvoice(invoiceReceipt.TreatmentPlanID).ToList();
			var pet = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.ID == treatmentPlan.PetID);
			var owner = _patientDBContext.Mst_Patients_Owner.FirstOrDefault(x => x.PatientID == pet.PatientID && invoiceReceipt.OwnerName == x.Name);

			UpcomingAppointment upcomingAppointment = AppointmentRepository.GetUpcomingAppointment(ConfigSettings.GetConfigurationSettings(), owner.ID, pet.ID);

            ViewInvoiceReceipt viewInvoiceReceipt = new ViewInvoiceReceipt()
			{
				InvoiceReceipt = invoiceReceipt, TreatmentPlan = treatmentPlan, Services = services, Products = products, Owner = owner, Pet = pet, UpcomingAppointment = upcomingAppointment
            };

			return viewInvoiceReceipt;
		}
	}
}
