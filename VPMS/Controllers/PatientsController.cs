using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using VPMS;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
	[Authorize]
	public class PatientsController : Controller
    {
        private readonly PatientDBContext _patientDBContext = new PatientDBContext();
		private readonly MasterCodeDataDBContext _masterCodeDataDBContext = new MasterCodeDataDBContext();
		private readonly ServicesDBContext _servicesDBContext = new ServicesDBContext();
		private readonly TreatmentPlanDBContext _treatmentPlanDBContext = new TreatmentPlanDBContext();
		private readonly InventoryDBContext _inventoryDBContext = new InventoryDBContext();
		private readonly CountryDBContext _countryDBContext = new CountryDBContext(ConfigSettings.GetConfigurationSettings());
        private readonly BranchDBContext _branchDBContext = new BranchDBContext();
		private readonly InvoiceReceiptDBContext _invoiceReceiptDBContext = new InvoiceReceiptDBContext();
		private readonly TestManagementDBContext _testManagementDBContext = new TestManagementDBContext();

		int totalPets;

		public IActionResult Index()
        {
			return RedirectToAction("PatientsList");
		}

		public IActionResult PatientsList()
		{
			try
			{
                var branch = 0;
                var organisation = 0;
                var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
                bool havePermission = hasPermission(roles, "PatientListing.View", out branch, out organisation);

				if (!havePermission)
				{
                    return RedirectToAction("AccessDenied", "Login");
                }

				SetPermission(roles);

                ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
			} 
			catch (Exception ex) {
				Program.logger.Error("Controller Error >> ", ex);
			}

			Program.CurrentPage = "/Patients/PatientsList";

			return View();
		}

		[Route("/Patients/PatientProfile/{type}/{patientid}")]
		public IActionResult ViewPatientProfile(string type, int patientid)
		{
			List<int> patientList = new List<int>();

            try
			{
                //            var role = HttpContext.Session.GetString("RoleName");
                //            var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
                //            var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

                //if (role == "Superadmin")
                //{
                //                patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
                //            }
                //else if(organisation != 0)
                //{
                //	List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
                //                patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

                //            }
                //else if(branch != 0)
                //{
                //                patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
                //            }

                //if (!patientList.Contains(patientid))
                //{
                //                return RedirectToAction("PatientsList", "Patients");
                //            }

                var branch = 0;
                var organisation = 0;
                var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
                bool havePermission = hasPermission(roles, "PatientDetails.View", out branch, out organisation);

                if (!havePermission)
                {
                    return RedirectToAction("AccessDenied", "Login");
                }


                SetPermission(roles);

                ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
				ViewData["Color"] = _masterCodeDataDBContext.Mst_MasterCodeData.Where(x => x.CodeGroup == "Color").Select(y => y.CodeName).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			ViewBag.PatientID = patientid;
			ViewBag.type = type;

			Program.CurrentPage = "/Patients/PatientProfile/" + type + "/" + patientid;

			return View();
		}

		[Route("/Patients/PetProfile/{type}/{patientid}/{petname}")]
		public IActionResult PetProfile(string type,int patientid, string petname)
		{
			Pets petProfile = new Pets();
            List<int> patientList = new List<int>();

            try
			{
                //var role = HttpContext.Session.GetString("RoleName");
                //var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
                //var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

                //if (role == "Superadmin")
                //{
                //    patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
                //}
                //else if (organisation != 0)
                //{
                //    List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
                //    patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

                //}
                //else if (branch != 0)
                //{
                //    patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
                //}

                //if (!patientList.Contains(patientid))
                //{
                //    return RedirectToAction("PatientsList", "Patients");
                //}

                var branch = 0;
                var organisation = 0;
                var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
                bool havePermission = hasPermission(roles, "PatientDetails.View", out branch, out organisation);

                if (!havePermission)
                {
                    return RedirectToAction("AccessDenied", "Login");
                }

                ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
				ViewData["Color"] = _masterCodeDataDBContext.Mst_MasterCodeData.Where(x => x.CodeGroup == "Color").Select(y => y.CodeName).ToList();
				ViewData["OtherPets"] = _patientDBContext.Mst_Pets.Where(x => x.PatientID == patientid && x.Name != petname).Select(y => y.Name).ToList();
				ViewData["VaccinationList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Vaccination").ToList();
				ViewData["SurgeryList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Surgery").ToList();
				ViewData["MedicationList"] = _inventoryDBContext.Mst_ProductType.ToList();

				petProfile = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;
			ViewBag.type = type;
			Program.CurrentPage = "/Patients/PetProfile/" + type + "/" + patientid + "/" + petname;

			return View(petProfile);
		}

		[Route("/Patients/InvoiceBilling/{patientid}/{petname}")]
		public IActionResult InvoiceBilling(int patientid, string petname)
		{
			Pets petInfo = new Pets();

			try
			{
                if (!AllowedPatientList(patientid))
                {
                    return RedirectToAction("PatientsList", "Patients");
                }

                petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;
			Program.CurrentPage = "/Patients/InvoiceBilling/" + patientid + "/" + petname;

			return View(petInfo);
		}

		[Route("/Patients/TreatmentPlan/{patientid}/{petname}")]
		public IActionResult TreatmentPlan(int patientid, string petname)
		{
			Pets petInfo = new Pets();
            List<int> patientList = new List<int>();

            try
			{
                //var role = HttpContext.Session.GetString("RoleName");
                //var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
                //var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

                //if (role == "Superadmin")
                //{
                //    patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
                //}
                //else if (organisation != 0)
                //{
                //    List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
                //    patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

                //}
                //else if (branch != 0)
                //{
                //    patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
                //}

                //if (!patientList.Contains(patientid))
                //{
                //    return RedirectToAction("PatientsList", "Patients");
                //}

                if (!AllowedPatientList(patientid))
                {
                    return RedirectToAction("PatientsList", "Patients");
                }

                petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;

			ViewData["TreatmentPlans"] = _treatmentPlanDBContext.Mst_TreatmentPlan.ToList();

			ViewData["Services"] = _servicesDBContext.Mst_Services.ToList();
			ViewData["Inventories"] = _inventoryDBContext.Mst_Product.ToList();

			Program.CurrentPage = "/Patients/TreatmentPlan/" + patientid + "/" + petname;

            return View(petInfo);
		}

		[Route("/Patients/TestManagement/{patientid}/{petname}")]
		public IActionResult TestManagement(int patientid, string petname)
		{
			//List<int> patientList = new List<int>();

			try
			{
				//var branch = 0;
				//var organisation = 0;

				//var role = HttpContext.Session.GetString("RoleName");
				//var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
				//var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

				//if (role == "Superadmin")
				//{
				//	patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
				//}
				//else if (organisation != 0)
				//{
				//	List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
				//	patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

				//}
				//else if (branch != 0)
				//{
				//	patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
				//}

				//if (!patientList.Contains(patientid))
				//{
				//	return RedirectToAction("PatientsList", "Patients");
				//}

				if (!AllowedPatientList(patientid))
				{
					return RedirectToAction("PatientsList", "Patients");
				}

			}
            catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;

			Program.CurrentPage = "/Patients/TestManagement/" + patientid + "/" + petname;

			return View();
		}

		//[Route("/Patients/BloodTest/{patientid}/{petname}")]
		//public IActionResult BloodTest(int patientid, string petname)
		//{
		//	List<int> patientList = new List<int>();

		//	try
		//	{
		//		var role = HttpContext.Session.GetString("RoleName");
		//		var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
		//		var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

		//		if (role == "Superadmin")
		//		{
		//			patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
		//		}
		//		else if (organisation != 0)
		//		{
		//			List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
		//			patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

		//		}
		//		else if (branch != 0)
		//		{
		//			patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
		//		}

		//		if (!patientList.Contains(patientid))
		//		{
		//			return RedirectToAction("PatientsList", "Patients");
		//		}

		//	}
		//	catch (Exception ex)
		//	{
		//		Program.logger.Error("Controller Error >> ", ex);
		//	}

		//	ViewData["PetName"] = petname;
		//	ViewBag.PatientID = patientid;

		//	Program.CurrentPage = "/Patients/BloodTest/" + patientid + "/" + petname;

		//	return View();
		//}

		//[Route("/Patients/VCheck/{patientid}/{petname}")]
		//public IActionResult VCheck(int patientid, string petname)
		//{
		//	List<int> patientList = new List<int>();

		//	try
		//	{
		//		var role = HttpContext.Session.GetString("RoleName");
		//		var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
		//		var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

		//		if (role == "Superadmin")
		//		{
		//			patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
		//		}
		//		else if (organisation != 0)
		//		{
		//			List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
		//			patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

		//		}
		//		else if (branch != 0)
		//		{
		//			patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
		//		}

		//		if (!patientList.Contains(patientid))
		//		{
		//			return RedirectToAction("PatientsList", "Patients");
		//		}

		//	}
		//	catch (Exception ex)
		//	{
		//		Program.logger.Error("Controller Error >> ", ex);
		//	}

		//	ViewData["PetName"] = petname;
		//	ViewBag.PatientID = patientid;

		//	Program.CurrentPage = "/Patients/VCheck/" + patientid + "/" + petname;

		//	return View();
		//}

		[Route("/Patients/TestManagement/{category}/{patientid}/{petname}")]
		public IActionResult TestResults(int category, int patientid, string petname)
		{
			List<int> patientList = new List<int>();
			string categoryName = "";
			var petID = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname).ID.ToString();
			List<TestResultDetails> resultDetails = new List<TestResultDetails>();

			try
			{
                //var role = HttpContext.Session.GetString("RoleName");
                //var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
                //var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

                //if (role == "Superadmin")
                //{
                //	patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
                //}
                //else if (organisation != 0)
                //{
                //	List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
                //	patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

                //}
                //else if (branch != 0)
                //{
                //	patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
                //}

                //if (!patientList.Contains(patientid))
                //{
                //	return RedirectToAction("PatientsList", "Patients");
                //}

                if (!AllowedPatientList(patientid))
                {
                    return RedirectToAction("PatientsList", "Patients");
                }

            }
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;

			if(category == 1)
			{
				categoryName = "BLOOD TEST";
			}
			else if(category == 3)
			{
				categoryName = "VCHECK";
			}


			var testResult = _testManagementDBContext.Txn_TestResults.OrderByDescending(x => x.CreatedDate).FirstOrDefault(y => y.PetID == petID && y.ResultCategories == categoryName);
			if(testResult != null)
			{
				resultDetails = _testManagementDBContext.Txn_TestResults_Details.Where(x => x.ResultID == testResult.ID).ToList();
			}
			ViewData["Results"] = resultDetails;

			ViewData["Category"] = category;

			Program.CurrentPage = "/Patients/TestManagement/" + category + "/" + patientid + "/" + petname;

			return View();
		}

		public PatientTreatmentPlan GetUpcomingTreatmentPlan(int petID) 
		{
			PatientTreatmentPlan upcomingTreatmentPlan = new PatientTreatmentPlan();

			try
			{
				upcomingTreatmentPlan = _patientDBContext.Txn_TreatmentPlan.Where(x => x.PetID == petID && x.TreatmentStart > DateOnly.FromDateTime(DateTime.Now) && x.PlanName != "Quick Invoice").OrderBy(x => x.TreatmentStart).FirstOrDefault();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			//if (upcomingTreatmentPlan == null) 
			//{
			//	upcomingTreatmentPlan = new PatientTreatmentPlan();
			//}

            upcomingTreatmentPlan = (upcomingTreatmentPlan == null) ? new PatientTreatmentPlan() : upcomingTreatmentPlan;


            return upcomingTreatmentPlan;
		}

		//public PatientTreatmentPlan GetOngoingTreatmentPlan(int petID)
		//{
		//	PatientTreatmentPlan upcomingTreatmentPlan = new PatientTreatmentPlan();

		//	try
		//	{
		//		upcomingTreatmentPlan = _patientDBContext.Txn_TreatmentPlan.Where(x => x.PetID == petID && x.TreatmentStart <= DateOnly.FromDateTime(DateTime.Now) && x.TreatmentEnd >= DateOnly.FromDateTime(DateTime.Now)).OrderBy(x => x.TreatmentStart).FirstOrDefault();
		//	}
		//	catch (Exception ex)
		//	{
		//		Program.logger.Error("Controller Error >> ", ex);
		//	}

		//	if (upcomingTreatmentPlan == null)
		//	{
		//		upcomingTreatmentPlan = new PatientTreatmentPlan();
		//	}

		//	return upcomingTreatmentPlan;
		//}

		public List<PatientTreatmentPlan> GetOngoingTreatmentPlan(int petID)
		{
			List<PatientTreatmentPlan> upcomingTreatmentPlan = new List<PatientTreatmentPlan>();

			try
			{
				upcomingTreatmentPlan = _patientDBContext.Txn_TreatmentPlan.Where(x => x.PetID == petID && x.TreatmentStart <= DateOnly.FromDateTime(DateTime.Now) && x.TreatmentEnd >= DateOnly.FromDateTime(DateTime.Now) && x.PlanName != "Quick Invoice").OrderBy(x => x.TreatmentStart).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			if (upcomingTreatmentPlan == null)
			{
				upcomingTreatmentPlan = new List<PatientTreatmentPlan>();
			}

			return upcomingTreatmentPlan;
		}

		public List<PatientTreatmentPlan> GetPastTreatmentPlan(int petID)
		{
			List<PatientTreatmentPlan> upcomingTreatmentPlan = new List<PatientTreatmentPlan>();

			try
			{
				upcomingTreatmentPlan = _patientDBContext.Txn_TreatmentPlan.Where(x => x.PetID == petID && x.TreatmentEnd < DateOnly.FromDateTime(DateTime.Now) && x.PlanName != "Quick Invoice").OrderBy(x => x.TreatmentStart).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return upcomingTreatmentPlan;
		}

		public List<PatientTreatmentPlanServices> getUpcomingTreatmentPlanServices(int planID)
		{
			List<PatientTreatmentPlanServices> patientTreatmentPlanServices = new List<PatientTreatmentPlanServices>();

			try
			{
				patientTreatmentPlanServices = _patientDBContext.Txn_TreatmentPlan_Services.Where(x => x.PlanID == planID).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return patientTreatmentPlanServices;
		}

		public List<PatientTreatmentPlanProducts> getUpcomingTreatmentPlanProducts(int planID)
		{
			List<PatientTreatmentPlanProducts> patientTreatmentPlanProducts = new List<PatientTreatmentPlanProducts>();

			try
			{
				patientTreatmentPlanProducts = _patientDBContext.Txn_TreatmentPlan_Products.Where(x => x.PlanID == planID).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return patientTreatmentPlanProducts;
		}

		public List<PatientVaccinationTreatments> GetVaccinationTreatmentList(int planID, bool upcoming, int petID)
		{
			return _patientDBContext.GetVaccinationTreatmentList(planID, upcoming, petID).ToList();
		}

		public List<PatientHealthCardMedication> GetHealthCardMedicationList(int petID)
		{
			return _patientDBContext.GetHealthCardMedicationList(petID).ToList();
		}

		public int InsertPatientTreatmentPlan([FromBody] PatientTreatmentPlan patientTreatmentPlan)
		{
			try
			{
				patientTreatmentPlan.CreatedDate = DateTime.Now;
				patientTreatmentPlan.CreatedBy = HttpContext.Session.GetString("Username");
				_patientDBContext.Txn_TreatmentPlan.Add(patientTreatmentPlan);
				_patientDBContext.SaveChanges();

				InvoiceReceiptNo invoiceReceiptNo = new InvoiceReceiptNo();
				//var branchCode = HttpContext.Session.GetString("BranchCode");
                var OrganisationCode = HttpContext.Session.GetString("OrganisationCode");
                var BranchID = HttpContext.Session.GetString("BranchID");
                //var currentDateString = DateTime.Now.ToString("yyMMdd");
				var currentDate = DateTime.Now;
                var petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.ID == patientTreatmentPlan.PetID);
				var patientInfo = _patientDBContext.Mst_Patients.FirstOrDefault(x => x.ID == petInfo.PatientID);
				var owner = _patientDBContext.Mst_Patients_Owner.FirstOrDefault(x => x.PatientID == petInfo.PatientID);
                //var invoiceNoList = _invoiceReceiptDBContext.Mst_InvoiceReceipt.AsNoTracking().Select(x => x.InvoiceNo);
                invoiceReceiptNo = _invoiceReceiptDBContext.Txn_InvoiceReceiptNo.FirstOrDefault(x => x.OrganisationAbbr == OrganisationCode && x.BranchID.ToString() == BranchID && x.Year == currentDate.Year.ToString() && x.Month == currentDate.Month.ToString() && x.Day == currentDate.Day.ToString());
				
				if(invoiceReceiptNo == null)
				{
					invoiceReceiptNo = new InvoiceReceiptNo() { OrganisationAbbr = OrganisationCode, BranchID = int.Parse(BranchID), Year = currentDate.Year.ToString(), Month = currentDate.Month.ToString(), Day = currentDate.Day.ToString(), RunningNo = 1, UpdatedDate = DateTime.Now, UpdatedBy = HttpContext.Session.GetString("Username") };

					_invoiceReceiptDBContext.Txn_InvoiceReceiptNo.Add(invoiceReceiptNo);
                }
				else
				{
					invoiceReceiptNo.RunningNo += 1;
                }

                //Random rnd = new Random();
                string invoiceNoString = invoiceReceiptNo.OrganisationAbbr + invoiceReceiptNo.BranchID.Value.ToString("D3") + "V" + invoiceReceiptNo.Year + int.Parse(invoiceReceiptNo.Month).ToString("D2") + int.Parse(invoiceReceiptNo.Day).ToString("D2") + invoiceReceiptNo.RunningNo.Value.ToString("D4");
				//var existed = true;

				//while (existed)
				//{
				//	int num = rnd.Next(1, 999999);
				//	invoiceNoString = "#" + num;
				//	if (!invoiceNoList.Contains(invoiceNoString))
				//	{
				//		existed = false;
				//	}
				//}

				//var invoiceNoList = _invoiceReceiptDBContext.Mst_InvoiceReceipt.Where(x => x.InvoiceNo.StartsWith(branchCode + "-" + currentDateString + "-")).OrderBy(x => x.InvoiceNo).Select(x => x.InvoiceNo).AsNoTracking().ToList();

				//if(invoiceNoList.Count == 0)
				//{
				//	invoiceNoString = branchCode + "-" + currentDateString + "-1";
    //            }
				//else
				//{
				//	var latestNo = invoiceNoList.LastOrDefault().Split("-")[2];
    //                invoiceNoString = branchCode + "-" + currentDateString + "-" + (int.Parse(latestNo) + 1);
    //            }


                var invoiceInfo = new InvoiceReceiptModel()
				{
					TreatmentPlanID = patientTreatmentPlan.ID,
					Branch = patientInfo.BranchID,
					InvoiceNo = invoiceNoString,
					PetName = petInfo.Name,
					OwnerName = owner.Name,
					Fee = patientTreatmentPlan.TotalCost,
					Tax = 6,
					GrandDiscount = 0,
					Status = patientTreatmentPlan.Status,
					CreatedDate = DateTime.Now,
					CreatedBy = HttpContext.Session.GetString("Username")
				};

				_invoiceReceiptDBContext.Mst_InvoiceReceipt.Add(invoiceInfo);
				_invoiceReceiptDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return patientTreatmentPlan.ID;
		}

		public void InsertPatientTreatmentPlanService([FromBody] PatientTreatmentPlanServices patientTreatmentPlanService)
		{
			try
			{
				_patientDBContext.Txn_TreatmentPlan_Services.Add(patientTreatmentPlanService);
				_patientDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}
		}

		public void InsertPatientTreatmentPlanProduct([FromBody] PatientTreatmentPlanProducts patientTreatmentPlanProducts)
		{
			try
			{
				patientTreatmentPlanProducts.CreatedDate = DateTime.Now;
				patientTreatmentPlanProducts.CreatedBy = HttpContext.Session.GetString("Username");
				_patientDBContext.Txn_TreatmentPlan_Products.Add(patientTreatmentPlanProducts);
				_patientDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}
		}

		public PatientInfoProfile GetPatientProfile(int patientid)
		{
			PatientInfoProfile patientInfo = new PatientInfoProfile();

			try
			{
				patientInfo = new PatientInfoProfile()
				{
					patient_Owners = _patientDBContext.Mst_Patients_Owner.Where(x => x.PatientID == patientid).ToList(),
					Pets = _patientDBContext.Mst_Pets.Where(x => x.PatientID == patientid).ToList()
				};
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return patientInfo;
		}

        //[Authorize(Roles = "Superadmin,Superuser,Clinic Admin,Doctor")]
        public IActionResult CreateNewPatient()
		{
			try
			{
                var branch = 0;
                var organisation = 0;
                var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
                bool havePermission = hasPermission(roles, "Patients.Add", out branch, out organisation);

				if (havePermission) 
				{
                    ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
                    ViewData["Color"] = _masterCodeDataDBContext.Mst_MasterCodeData.Where(x => x.CodeGroup == "Color").Select(y => y.CodeName).ToList();
                    ViewData["VaccinationList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Vaccination").ToList();
                    ViewData["SurgeryList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Surgery").ToList();
                    ViewData["MedicationList"] = _inventoryDBContext.Mst_ProductType.ToList();
                }
				else
				{
                    return RedirectToAction("PatientsList", "Patients");
                }
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			Program.CurrentPage = "/Patients/CreateNewPatient";

            return View();
		}

		public PatientsInfo GetPetsInfoList(int rowLimit, int page, string ownername, string petName, string species, string breed)
        {
			var patientsInfo = new PatientsInfo()
			{
				petsInfo = new List<PetsInfo>(), totalPatients = 0
			};

			int start = (page - 1) * rowLimit;

            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            bool havePermission = hasPermission(roles, "PatientListing.View", out branch, out organisation);

            //var role = HttpContext.Session.GetString("RoleName");
            //branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
            //organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

            if (havePermission)
            //if(role != "User")
            {
                try
                {
                    var petsInfoList = _patientDBContext.GetPetsInfoList(start, rowLimit, ownername, petName, species, breed, branch, organisation, out totalPets).ToList();

                    patientsInfo = new PatientsInfo() { petsInfo = petsInfoList, totalPatients = totalPets };
                }
                catch (Exception ex)
                {
                    Program.logger.Error("Controller Error >> ", ex);
                }
            }

			return patientsInfo;
		}

		public List<PatientMedicalRecordService> GetMedicalRecordServicesByPetID(int petID)
		{
			List<PatientMedicalRecordService> patientMedicalRecordServices = new List<PatientMedicalRecordService>();

			try
			{
				patientMedicalRecordServices = _patientDBContext.Mst_MedicalRecord_VaccinationSurgery.Where(x => x.PetID == petID && x.IsDeleted != 1).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return patientMedicalRecordServices;
		}

		public List<PatientMedicalRecordMedication> GetMedicalRecordMedicationByPetID(int petID)
		{
			List<PatientMedicalRecordMedication> patientMedicalRecordMedications = new List<PatientMedicalRecordMedication>();

			try
			{
				patientMedicalRecordMedications = _patientDBContext.Mst_MedicalRecord_Medication.Where(x => x.PetID == petID && x.Status != 3).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return patientMedicalRecordMedications;
		}

		public List<Pets_Breed> BreedList (string species)
		{
			List<Pets_Breed> pets_Breeds = new List<Pets_Breed>();

			try
			{
				pets_Breeds = _patientDBContext.Mst_Pets_Breed.Where(x => x.Species == species).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return pets_Breeds;
		}

		public int CreatePatientInfo([FromBody] PatientsModel patients)
		{
			try
			{
				_patientDBContext.Mst_Patients.Add(patients);

				_patientDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return patients.ID;
		}

		public void InsertPatientOwnerInfo([FromBody] Patient_Owner patientOwner)
		{
			try
			{
				_patientDBContext.Mst_Patients_Owner.Add(patientOwner);

				_patientDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}
		}

		public void UpdatePatientOwnerInfo([FromBody] Patient_Owner patientOwner)
		{
			try
			{
				if (patientOwner.ID != 0) { _patientDBContext.Update(patientOwner); }
				else { _patientDBContext.Mst_Patients_Owner.Add(patientOwner); }

				_patientDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}
		}

		public int InsertPetsInfo([FromBody] Pets pets)
		{
			try
			{
				_patientDBContext.Mst_Pets.Add(pets);

				var bmi = (pets.Weight / (pets.Height * pets.Height)) * 10000;

				_patientDBContext.SaveChanges();

				Pet_Growth pet_Growth = new Pet_Growth()
				{
					PetID = pets.ID,
					Age = pets.Age,
					Allergies = pets.Allergies,
					Height = pets.Height,
					Weight = pets.Weight,
					BMI = bmi,
					CreatedDate = pets.CreatedDate,
					CreatedBy = pets.CreatedBy
				};

				_patientDBContext.Mst_Pet_Growth.Add(pet_Growth);

				_patientDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return pets.ID;
		}

		public int UpdatePetInfo([FromBody] Pets pets)
		{
			try
			{
				var currentPetInfo = _patientDBContext.Mst_Pets.AsNoTracking().FirstOrDefault(p => p.ID == pets.ID);

				_patientDBContext.Update(pets);

				_patientDBContext.SaveChanges();

				if (currentPetInfo.Height != pets.Height || currentPetInfo.Weight != pets.Weight || currentPetInfo.Allergies != pets.Allergies || currentPetInfo.Age != pets.Age)
				{
					var bmi = (pets.Weight / (pets.Height * pets.Height)) * 10000;

					Pet_Growth pet_Growth = new Pet_Growth()
					{
						PetID = currentPetInfo.ID,
						Age = pets.Age,
						Height = pets.Height,
						Weight = pets.Weight,
						Allergies = pets.Allergies,
						BMI = bmi,
						CreatedDate = DateTime.Now,
						CreatedBy = "System"
					};

					_patientDBContext.Mst_Pet_Growth.Add(pet_Growth);

					_patientDBContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return pets.ID;
		}
		
		public void DeletePetInfo(int patientid, string petname)
		{
			try
			{
				var petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);

				if (petInfo != null)
				{
					_patientDBContext.Mst_Pets.Remove(petInfo);

					_patientDBContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}
		}

		public bool InsertMedicalRecordService([FromBody] PatientMedicalRecordService patientMedicalRecordService)
		{
			try
			{
				_patientDBContext.Add(patientMedicalRecordService);
				_patientDBContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
		}

		public bool InsertMedicalRecordMedication([FromBody] PatientMedicalRecordMedication patientMedicalRecordMedication)
		{
			try
			{
				_patientDBContext.Add(patientMedicalRecordMedication);
				_patientDBContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
		}

		public bool UpdateMedicalRecordService([FromBody] PatientMedicalRecordService patientMedicalRecordService)
		{
			try
			{
				var allCurrentServices = _patientDBContext.Mst_MedicalRecord_VaccinationSurgery.AsNoTracking().Where(x => x.PetID == patientMedicalRecordService.PetID && x.Type == patientMedicalRecordService.Type).ToList();

				if (patientMedicalRecordService.CategoryID != 0)
				{
					_patientDBContext.Update(patientMedicalRecordService);
					_patientDBContext.SaveChanges();
				}
				else
				{
					var allServices = _patientDBContext.Mst_MedicalRecord_VaccinationSurgery.AsNoTracking().Where(x => x.PetID == patientMedicalRecordService.PetID && x.Type == patientMedicalRecordService.Type).ToList();

					foreach (var service in allServices)
					{
						service.IsDeleted = 1;
						service.UpdatedDate = DateTime.Now;
						service.UpdatedBy = "System";

						_patientDBContext.Update(service);
						_patientDBContext.SaveChanges();
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
		}

		public bool DeleteMedicalRecordService([FromBody] List<int> deletedService)
		{
			try
			{
				var allServicesToDelete = _patientDBContext.Mst_MedicalRecord_VaccinationSurgery.AsNoTracking().Where(x => deletedService.Contains(x.ID)).ToList();

				foreach(var service in allServicesToDelete)
				{
					service.IsDeleted = 1;
					service.UpdatedDate = DateTime.Now;
					service.UpdatedBy = "System";

					_patientDBContext.Update(service);
					_patientDBContext.SaveChanges();
				}

				return true;
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
		}

		public bool UpdateMedicalRecordMedication([FromBody] PatientMedicalRecordMedication patientMedicalRecordMedication)
		{
			try
			{
				if (patientMedicalRecordMedication.CategoryID != 0)
				{
					_patientDBContext.Update(patientMedicalRecordMedication);
					_patientDBContext.SaveChanges();
				}
				else
				{
					var allMedications = _patientDBContext.Mst_MedicalRecord_Medication.AsNoTracking().Where(x => x.PetID == patientMedicalRecordMedication.PetID).ToList();

					foreach (var medications in allMedications)
					{
						medications.Status = 3;
						medications.UpdatedDate = DateTime.Now;
						medications.UpdatedBy = "System";

						_patientDBContext.Update(medications);
						_patientDBContext.SaveChanges();
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
		}

		public bool DeleteMedicalRecordMedication([FromBody] List<int> deletedMedication)
		{
			try
			{
				var allMedicationsToDelete = _patientDBContext.Mst_MedicalRecord_Medication.AsNoTracking().Where(x => deletedMedication.Contains(x.ID)).ToList();

				foreach (var medication in allMedicationsToDelete)
				{
					medication.Status = 3;
					medication.UpdatedDate = DateTime.Now;
					medication.UpdatedBy = "System";

					_patientDBContext.Update(medication);
					_patientDBContext.SaveChanges();
				}

				return true;
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
		}

		public List<Pet_Growth> GetPetGrowth(int petID)
		{
			List<Pet_Growth> pet_Growths = new List<Pet_Growth>();

			try
			{
				pet_Growths = _patientDBContext.Mst_Pet_Growth.Where(x => x.PetID == petID).ToList();
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			return pet_Growths;
		}

		public List<StateModel> GetState(int countryID, string countryName = "")
		{
			if(countryName != "")
			{
				//countryID = _countryDBContext.mst_countrylist.FirstOrDefault(x => x.CountryName == countryName).ID;
                var sCountryObj = _countryDBContext.mst_countrylist.FirstOrDefault(x => x.CountryName == countryName);
				if (sCountryObj != null)
				{
					countryID = sCountryObj.ID;
                }
            }

			return _countryDBContext.mst_state.Where(x => x.CountryID == countryID).ToList();

		}

		public List<CityModel> GetCity(int stateID, string stateName = "")
		{
			if (stateName != "")
			{
				//stateID = _countryDBContext.mst_state.FirstOrDefault(x => x.State == stateName).ID;
                var sStateObj = _countryDBContext.mst_state.FirstOrDefault(x => x.State == stateName);
				if (sStateObj != null)
				{
					stateID = sStateObj.ID;
				}
            }

			return _countryDBContext.mst_city.Where(x => x.StateID == stateID).ToList();
		}

		public bool hasPermission(List<string> roles, string permission, out int branchID, out int organisationID)
		{
            branchID = 0;
            organisationID = 0;
			bool havePermission = false;

            //var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            if (roles.Contains("General.Superadmin") || roles.Contains("General.Superuser"))
            {
                organisationID = (roles.Contains("General.Superuser")) ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;
                havePermission = true;
            }
            else if (roles.Contains(permission) || HttpContext.Session.GetString("IsDoctor") == "1" || HttpContext.Session.GetString("IsAdmin") == "1")
            {
                branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
                havePermission = true;
            }

            return havePermission;
		}

		public bool AllowedPatientList(int patientid)
		{
            List<int> patientList = new List<int>();
			bool allowed = false;

			try
			{
                var branch = 0;
                var organisation = 0;
                var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
                bool havePermission = hasPermission(roles, "", out branch, out organisation);

                if (roles.Contains("General.Superadmin"))
                {
                    patientList = _patientDBContext.Mst_Patients.Select(y => y.ID).ToList();
                }
                else if (organisation != 0)
                {
                    List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
                    patientList = _patientDBContext.Mst_Patients.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();
                }
                else if (branch != 0)
                {
                    patientList = _patientDBContext.Mst_Patients.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
                }

                if (patientList.Contains(patientid))
                {
					allowed = true;
                }
            }
			catch (Exception ex) 
			{
                Program.logger.Error("Controller Error >> ", ex);
            }

			return allowed;
        }

        public void SetPermission(List<string> roles)
        {
            ViewData["CanAdd"] = "false";
            ViewData["CanEdit"] = "false";
            ViewData["CanView"] = "false";

            if (roles.Where(x => x.Contains("General.")).Count() > 0 || HttpContext.Session.GetString("IsDoctor") == "1" || HttpContext.Session.GetString("IsAdmin") == "1")
            {
                ViewData["CanAdd"] = "true";
                ViewData["CanEdit"] = "true";
                ViewData["CanView"] = "true";
            }
            else
            {
                if (roles.Contains("PatientDetails.Add"))
                {
                    ViewData["CanAdd"] = "true";
                }

                if (roles.Contains("PatientDetails.Edit"))
                {
                    ViewData["CanEdit"] = "true";
                }

                if (roles.Contains("PatientDetails.View"))
                {
                    ViewData["CanView"] = "true";
                }
            }
        }
    }
}
