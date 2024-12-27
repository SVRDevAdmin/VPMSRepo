using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using VPMS;
using VPMS.Lib;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

using Spire.Additions.Qt;
using Spire.Pdf.Graphics;
using System.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Html;
using System.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;
using VPMSWeb.Interface;

namespace VPMSWeb.Controllers
{
    public class InvoiceReceiptController : Controller
    {
		private readonly InvoiceReceiptDBContext _invoiceReceiptDBContext = new InvoiceReceiptDBContext();
		private readonly PatientDBContext _patientDBContext = new PatientDBContext();
		private readonly BranchDBContext _branchDBContext = new BranchDBContext();
		private readonly InventoryDBContext _inventoryDBContext = new InventoryDBContext();

        private readonly IStringLocalizer _localizer;

        int totalInvoiceReceipt;
		float totalAll = 0;

        public InvoiceReceiptController(IStringLocalizerFactory factory)
        {
            var type = typeof(LanguageResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("Resource", assemblyName.Name);
        }

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

            //var receiptNoList = _invoiceReceiptDBContext.Mst_InvoiceReceipt.AsNoTracking().Select(x => x.ReceiptNo);
            var OrganisationCode = HttpContext.Session.GetString("OrganisationCode");

			//Random rnd = new Random();
			var invoiceNoTemp = invoice.InvoiceNo;
            string receiptNoString = "";

			if(OrganisationCode == "V")
			{
                receiptNoString = "V" + invoiceNoTemp.Substring(1).Replace("V", "R");
            }
			else
			{
                receiptNoString = OrganisationCode + invoiceNoTemp.Replace(OrganisationCode, "").Replace("V", "R");
            }

            //var existed = true;

            //while (existed)
            //{
            //	int num = rnd.Next(1, 999999);
            //	receiptNoString = "#" + num;
            //	if (!receiptNoList.Contains(receiptNoString))
            //	{
            //		existed = false;
            //	}
            //}

            //var branchCode = HttpContext.Session.GetString("BranchCode");
            //var currentDateString = DateTime.Now.ToString("yyMMdd");
            //var receiptNoList = _invoiceReceiptDBContext.Mst_InvoiceReceipt.Where(x => x.ReceiptNo.StartsWith(branchCode + "-" + currentDateString + "-")).OrderBy(x => x.ReceiptNo).Select(x => x.ReceiptNo).AsNoTracking().ToList();

            //if (receiptNoList.Count == 0)
            //{
            //    receiptNoString = branchCode + "-" + currentDateString + "-1";
            //}
            //else
            //{
            //    var latestNo = receiptNoList.LastOrDefault().Split("-")[2];
            //    receiptNoString = branchCode + "-" + currentDateString + "-" + (int.Parse(latestNo) + 1);
            //}

            invoice.ReceiptNo = receiptNoString;

			_invoiceReceiptDBContext.Update(invoice);
			_invoiceReceiptDBContext.SaveChanges(true);


			return SendInvoiceEmail(id, "Receipt");
		}

		public ViewInvoiceReceipt ViewInvoiceReceipt(int id)
		{
			return GetInvoiceReceiptInfos(id);
		}

        public bool SendInvoiceEmail(int id, string type)
        {
            var invoiceReceiptInfos = GetInvoiceReceiptInfos(id);
            CreateInvoicePDF(invoiceReceiptInfos, type);

            List<String> sRecipientList = new List<String>();
            sRecipientList.Add(invoiceReceiptInfos.Owner.EmailAddress);

            var sEmailConfig = ConfigSettings.GetConfigurationSettings();
            String? sHost = sEmailConfig.GetSection("SMTP:Host").Value;
            int? sPortNo = Convert.ToInt32(sEmailConfig.GetSection("SMTP:Port").Value);
            String? sUsername = sEmailConfig.GetSection("SMTP:Username").Value;
            String? sPassword = sEmailConfig.GetSection("SMTP:Password").Value;
            String? sSender = sEmailConfig.GetSection("SMTP:Sender").Value;

            var subject = "";
            var body = "";

            if (type == "Invoice")
            {
                subject = _localizer["InvoiceReceipt_Label_InvoiceCreation"];
                body = _localizer["InvoiceReceipt_Message_InvoiceCreation"];
            }
            else
            {
                subject = _localizer["InvoiceReceipt_Label_ReceiptCreation"];
                body = _localizer["InvoiceReceipt_Message_ReceiptCreation"];
            }

            try
            {
                VPMS.Lib.EmailObject sEmailObj = new VPMS.Lib.EmailObject();
                sEmailObj.SenderEmail = sSender;
                sEmailObj.RecipientEmail = sRecipientList;
                sEmailObj.Subject = subject;
                sEmailObj.Body = body;
                sEmailObj.SMTPHost = sHost;
                sEmailObj.PortNo = sPortNo.Value;
                sEmailObj.HostUsername = sUsername;
                sEmailObj.HostPassword = sPassword;
                sEmailObj.EnableSsl = true;
                sEmailObj.UseDefaultCredentials = false;
                sEmailObj.IsHtml = true;

                var filename = type + ".pdf";
                Attachment attachment = new Attachment(ConfigSettings.GetConfigurationSettings().GetValue<string>("InvoiceReceiptFileTemp"));
                attachment.Name = filename;
                List<Attachment> attachments = new List<Attachment>() { attachment };
                sEmailObj.EmailAttachement = attachments;

                String sErrorMessage = "";
                EmailHelpers.SendEmail(sEmailObj, out sErrorMessage);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public void CreateInvoicePDF(ViewInvoiceReceipt invoiceReceiptInfos, string type)
		{
            var emailTemplate = TemplateRepository.GetTemplateByCodeLang(ConfigSettings.GetConfigurationSettings(), "VPMS_EN003", Program.LanguageSelected.ConfigurationValue);

            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<serviceList>###", GetServiceInfos(invoiceReceiptInfos.Services));
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<productList>###", GetProductInfos(invoiceReceiptInfos.Products));

			if (type == "Invoice")
            {
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<invoiceReceiptNoLabel>###", _localizer["InvoiceReceipt_Label_InvoiceNo"]);
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<invoiceReceiptNo>###", invoiceReceiptInfos.InvoiceReceipt.InvoiceNo);
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<serviceDate>###", invoiceReceiptInfos.InvoiceReceipt.CreatedDate.ToString("dd/MM/yyyy"));
            }
			else
            {
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<invoiceReceiptNoLabel>###", _localizer["InvoiceReceipt_Label_ReceiptNo"]);
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<invoiceReceiptNo>###", invoiceReceiptInfos.InvoiceReceipt.ReceiptNo);
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<serviceDate>###", invoiceReceiptInfos.InvoiceReceipt.UpdatedDate.Value.ToString("dd/MM/yyyy"));
            }
            
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<ownerName>###", invoiceReceiptInfos.Owner.Name);
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<contactNo>###", invoiceReceiptInfos.Owner.ContactNo);
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<address>###", invoiceReceiptInfos.Owner.Address);
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<petName>###", invoiceReceiptInfos.Pet.Name);
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<registrationNo>###", invoiceReceiptInfos.Pet.RegistrationNo);
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<species>###", invoiceReceiptInfos.Pet.Species);
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<serviceName>###", invoiceReceiptInfos.TreatmentPlan.PlanName);
            

            if (invoiceReceiptInfos.UpcomingAppointment.ApptDate != null)
            {
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<nextApptDate>###", invoiceReceiptInfos.UpcomingAppointment.ApptDate.Value.ToString("dd/MM/yyyy") + " " + invoiceReceiptInfos.UpcomingAppointment.ApptStartTime);
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<nextApptName>###", invoiceReceiptInfos.UpcomingAppointment.Service);
            }
            else
            {
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<nextApptDate>###", "N/A");
                emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<nextApptName>###", _localizer["Appointment_Label_NoFutureAppointment"]);
            }

            var tax = (totalAll * 0.06);
            var grandaDiscount = (totalAll + tax) * (invoiceReceiptInfos.InvoiceReceipt.GrandDiscount / 100);
            var grandTotal = totalAll + tax - grandaDiscount;

            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<total>###", totalAll.ToString());
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<tax>###", string.Format("{0:#,##0.##}", tax));
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<grandDiscount>###", invoiceReceiptInfos.InvoiceReceipt.GrandDiscount.ToString());
            emailTemplate.TemplateContent = emailTemplate.TemplateContent?.Replace("###<grandTotal>###", string.Format("{0:#,##0.##}", grandTotal));

            //Specify the output file path
            string fileName = "invoiceReceipt-temp.pdf";

            //Set the plugin path
            HtmlConverter.PluginPath = ConfigSettings.GetConfigurationSettings().GetValue<string>("PluginPath");

            //Convert HTML string to PDF
            HtmlConverter.Convert(emailTemplate.TemplateContent, fileName, true, 100000, new Size(1080, 1000), new PdfMargins(0), LoadHtmlType.SourceCode);
        }

		public ViewInvoiceReceipt GetInvoiceReceiptInfos(int id)
		{
            var invoiceReceipt = _invoiceReceiptDBContext.Mst_InvoiceReceipt.FirstOrDefault(x => x.ID == id);
            var treatmentPlan = _patientDBContext.Txn_TreatmentPlan.FirstOrDefault(x => x.ID == invoiceReceipt.TreatmentPlanID);
            var services = _patientDBContext.Txn_TreatmentPlan_Services.Where(x => x.PlanID == invoiceReceipt.TreatmentPlanID).ToList();
            var products = _inventoryDBContext.GetInventoryInvoice(invoiceReceipt.TreatmentPlanID).ToList();
            var pet = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.ID == treatmentPlan.PetID);
            var owner = _patientDBContext.Mst_Patients_Owner.FirstOrDefault(x => x.PatientID == pet.PatientID && invoiceReceipt.OwnerName == x.Name);

            UpcomingAppointment upcomingAppointment = AppointmentRepository.GetUpcomingAppointment(ConfigSettings.GetConfigurationSettings(), owner.ID, pet.ID);

            ViewInvoiceReceipt invoiceReceiptInfos = new ViewInvoiceReceipt()
            {
                InvoiceReceipt = invoiceReceipt,
                TreatmentPlan = treatmentPlan,
                Services = services,
                Products = products,
                Owner = owner,
                Pet = pet,
                UpcomingAppointment = upcomingAppointment
            };

            return invoiceReceiptInfos;
        }

		public string GetServiceInfos(List<PatientTreatmentPlanServices> services)
		{
			var fullString = "";
			float total = 0;

			foreach (var service in services)
			{
                var temp =
						"<tbody>" +
                            "<tr>" +
                                "<td style=\"padding: 0 1vw;\" class=\"firstColumn\">" + service.ServiceName + "</td>" +
                                "<td style=\"padding: 0 1vw;\" class=\"secondColumn\">" + service.Discount + "</td>" +
                                "<td style=\"padding: 0 1vw;\" class=\"thirdColumn totalServices\">" + service.TotalPrice + "</td>" +
                            "</tr>" +
                        "</tbody>";

				total += service.TotalPrice;

                fullString += temp;
            }

            fullString +=
					"<tbody>" +
                            "<tr>" +
                                "<td style=\"padding: 1vw; padding-bottom: 0; font-weight: bold;\" class=\"firstColumn\">"+ _localizer["Patient_Label_TotalCost"] + "</td>" +
                                "<td style=\"padding: 1vw; padding-bottom: 0;\" class=\"secondColumn\"></td>" +
                                "<td style=\"padding: 1vw; padding-bottom: 0; font-weight: bold;\" class=\"thirdColumn totalAll\">" + total + "</td>" +
                            "</tr>" +
                        "</tbody>";

			totalAll += total;

            return fullString;
		}

        public string GetProductInfos(List<InventoryInvoice> products)
        {
            string fullString = "";
            float total = 0;

            foreach (var product in products)
            {
                string temp =
						"<tbody>" +
                            "<tr>" +
                                "<td style=\"padding: 0 1vw;\" class=\"firstColumn\">" + product.ProductName + " <br> <span style=\"color: red;\">"+ _localizer["Patient_Label_Expiry"] + " : " + product.ExpiryDate.ToString("dd/MM/yyyy") + "</span></td>" +
                                "<td style=\"padding: 0 1vw;\" class=\"secondColumn\">" + product.Discount + "</td>" +
                                "<td style=\"padding: 0 1vw;\" class=\"thirdColumn totalProducts\">" + product.TotalPrice + "</td>" +
                            "</tr>" +
                        "</tbody>";

                total += product.TotalPrice;

                fullString += temp;
            }

            fullString +=
					"<tbody>" +
                            "<tr>" +
                                "<td style=\"padding: 1vw; padding-bottom: 0; font-weight: bold;\" class=\"firstColumn\">"+ _localizer["Patient_Label_TotalCost"] + "</td>" +
                                "<td style=\"padding: 1vw; padding-bottom: 0;\" class=\"secondColumn\"></td>" +
                                "<td style=\"padding: 1vw; padding-bottom: 0; font-weight: bold;\" class=\"thirdColumn totalAll\">" + total + "</td>" +
                            "</tr>" +
                        "</tbody>";

            totalAll += total;

            return fullString;
        }
    }
}

