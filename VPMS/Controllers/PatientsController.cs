using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
using ZstdSharp.Unsafe;
using VPMS.Lib;
using Microsoft.Extensions.Localization;
using VPMS.Interface.API.VCheck.RequestMessage;
using SkiaSharp;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
		private readonly TestsListDBContext _testsListDBContext = new TestsListDBContext(ConfigSettings.GetConfigurationSettings());
		private readonly LocationDBContext _locationDBContext = new LocationDBContext(ConfigSettings.GetConfigurationSettings());
		private readonly DoctorDBContext _doctorDBContext = new DoctorDBContext(ConfigSettings.GetConfigurationSettings());

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
                int iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
                int iBranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));

                int isSuperadmin = 0;
                int roleIsAdmin = 0;
                roleIsAdmin = Convert.ToInt32(HttpContext.Session.GetString("IsAdmin"));
                var organizationObj = OrganizationRepository.GetOrganizationByID(iOrganizationID);
                if (organizationObj != null)
                {
                    if (organizationObj.Level == 0 || organizationObj.Level == 1 || (organizationObj.Level == 2 && roleIsAdmin == 1))
                    {
                        isSuperadmin = 1;
                    }
                    else
                    {
                        isSuperadmin = 0;
                    }
                }

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
				ViewData["TestsList"] = _testsListDBContext.mst_testslist.Where(x => x.IsActive == 1).OrderBy(x => x.System_TestID).ToList();
				ViewData["LocationList"] = _locationDBContext.mst_locationlist.Where(x => x.System_Status == 1).ToList();
                //ViewData["DoctorList"] = _doctorDBContext.mst_doctor.Where(x => x.IsDeleted == 0).ToList();
                ViewData["DoctorList"] = DoctorRepository.GetDoctorListByOrganizationBranch(ConfigSettings.GetConfigurationSettings(), iOrganizationID, iBranchID, isSuperadmin);

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
                //if (!AllowedPatientList(patientid))
                //{
                //    return RedirectToAction("PatientsList", "Patients");
                //}

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

			int iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
			int iBranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));

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

                //if (!AllowedPatientList(patientid))
                //{
                //    return RedirectToAction("PatientsList", "Patients");
                //}

                petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);
			}
			catch (Exception ex)
			{
				Program.logger.Error("Controller Error >> ", ex);
			}

			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;

			//ViewData["TreatmentPlans"] = _treatmentPlanDBContext.Mst_TreatmentPlan.ToList();
			var sPlanList = _treatmentPlanDBContext.GetTreatmentPlanListByOrganizationBranch(iOrganizationID, iBranchID);
			if (sPlanList != null)
			{
				ViewData["TreatmentPlans"] = sPlanList;

            }

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

				//if (!AllowedPatientList(patientid))
				//{
				//	return RedirectToAction("PatientsList", "Patients");
				//}

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

		[Route("Patients/PatientIdentityProfile/Add")]
		public IActionResult AddIdentityPatientProfile(String Username, String EmailAddress)
		{
			Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

			try
			{
				IdentityUserObject sNewIdentityPatient = new IdentityUserObject();
				sNewIdentityPatient.Id = Guid.NewGuid().ToString();
				sNewIdentityPatient.UserName = Username;
				sNewIdentityPatient.NormalizedUserName = Username.ToUpper();
				sNewIdentityPatient.Email = EmailAddress;
				sNewIdentityPatient.NormalizedEmail = EmailAddress.ToUpper();
				sNewIdentityPatient.EmailConfirmed = 0;
				sNewIdentityPatient.SecurityStamp = Guid.NewGuid().ToString();
				sNewIdentityPatient.PhoneNumberConfirmed = 0;
				sNewIdentityPatient.TwoFactorEnabled = 0;
				sNewIdentityPatient.AccessFailedCount = 0;
				sNewIdentityPatient.LockoutEnabled = 1;

				String sPatientIdentityUserID = "";
				if (!UserRepository.ValidateIdentityUser(sNewIdentityPatient.UserName))
				{
					String sRoleID = RoleRepository.GetRoleIDByRoleName("Customer");
					String sTempPass = "Abcd@1234";

					if (UserRepository.AddIdentityUser(sNewIdentityPatient, sRoleID, sTempPass, out sPatientIdentityUserID))
					{
						sResp.StatusCode = (int)StatusCodes.Status200OK;

                    }
					else
					{
						sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
					}
				}
				else
				{
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
					sResp.isRecordExists = true;
                }
			}
			catch (Exception ex)
			{
				sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

			return Json(sResp);
		}

		[Route("Patients/SubmitScheduledTests")]
		[HttpPost()]
		public IActionResult InsertScheduledTestSubmission(String scheduledDate, String scheduledTime, String locationID, String locationName,
														   String testID, String testName, long patientID, String patientName, String gender,
														   String species, String doctorIncharges, String submittedBy)
		{
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

			try
			{
				DateTime dtScheduled = DateTime.ParseExact((scheduledDate + " " + scheduledTime), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
				DateTime dtCreated = DateTime.Now;


				scheduledTestsSubmission sSubmissionObject = new scheduledTestsSubmission();
				sSubmissionObject.PatientID = patientID;
				sSubmissionObject.ScheduledDate = dtScheduled;
				sSubmissionObject.TestID = testID;
				sSubmissionObject.TestName = testName;
				sSubmissionObject.LocationID = locationID;
				sSubmissionObject.LocationName = locationName;
				sSubmissionObject.Status = 0;
				sSubmissionObject.CreatedDate = dtCreated;
				sSubmissionObject.CreatedBy = submittedBy;

				long iSubmissionID = 0;
				if (TestsListRepository.InsertScheduledTestSubmission(ConfigSettings.GetConfigurationSettings(), sSubmissionObject, out iSubmissionID))
				{
					String sUniqueID = testID + "-" + iSubmissionID.ToString("00000");
					String sResponseStatus = "";

					DateTime sUpdatedDate;
                    int newStatus = 0;

                    Boolean isSent = SendScheduledTest(dtScheduled, locationID, locationName, sUniqueID, testID, testName, patientID,
													   patientName, gender, species, doctorIncharges, submittedBy, dtCreated, 
													   out sResponseStatus);
					if (isSent)
					{
						sUpdatedDate = DateTime.Now;
						newStatus = 1;

                        sResp.StatusCode = (int)StatusCodes.Status200OK;
                    }
					else
					{
                        sUpdatedDate = DateTime.Now;
                        newStatus = 2;

                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    }

                    TestsListRepository.UpdateScheduledTestSubmissionStatus(ConfigSettings.GetConfigurationSettings(), iSubmissionID,
																			newStatus, sUpdatedDate, sResponseStatus, submittedBy);
                }
                else
				{
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                }
			}
            catch (Exception ex)
			{
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

		public static Boolean SendScheduledTest(DateTime dtScheduled, String locationID, String locationName,
												String sUniquerID, String testID, String testName, long patientID, 
												String patientName, String gender, String species, String doctorIncharges, 
												String submiitedBy, DateTime dtCreated, out String responseStatus)
		{
			Boolean isSuccess = false;
			responseStatus = "";

			var config = ConfigSettings.GetConfigurationSettings();
			String sClientKey = config.GetSection("VCheckAPI:ClientKey").Value;

            try
			{
				VPMS.Interface.API.VCheckAPI vcheckAPI = new VPMS.Interface.API.VCheckAPI();

				CreateScheduledTestRequest sReq = new CreateScheduledTestRequest();
				CreateScheduledTestBody sReqBody = new CreateScheduledTestBody();
				CreateScheduledTestHeader sReqHeader = new CreateScheduledTestHeader();
				sReqHeader.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); ;
				sReqHeader.clientkey = sClientKey;

				sReqBody.locationid = locationID;
				sReqBody.scheduledtestname = testName;
				sReqBody.scheduleddatetime = dtScheduled.ToString("yyyyMMddHHmmss");
				sReqBody.testuniqueid = sUniquerID;

                sReqBody.scheduledby = submiitedBy;
				sReqBody.personincharges = doctorIncharges;
				sReqBody.patientid = patientID.ToString();
				sReqBody.patientname = patientName;
				sReqBody.gender = gender;
				sReqBody.species = species;
				sReqBody.ownername = "";
				sReqBody.scheduledcreateddate = dtCreated.ToString("yyyyMMddHHmmss");

				sReq.header = sReqHeader;
				sReq.body = sReqBody;

				var sResp = vcheckAPI.CreateScheduledTest(sReq);
				if (sResp.body.responseCode == "VV.0001")
				{
					isSuccess = true;
                }
				else
				{
					isSuccess = false;
                }

				responseStatus = (sResp != null) ? sResp.body.responseCode : "";
            }
			catch (Exception ex)
			{
				isSuccess = false;
            }

			return isSuccess;
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

		//[Route("/Patients/CreatePatientLoginProfile")]
		//[HttpPost()]
		//public IActionResult CreatePatientLoginProfile(Patient_Owner_Login sPatientLogin)
		//{
		//	Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

		//	try
		//	{
		//		_patientDBContext.Mst_Patients_Login.Add(sPatientLogin);
		//		_patientDBContext.SaveChanges();

		//		sResp.StatusCode = (int)StatusCodes.Status200OK;
		//	}
		//	catch (Exception ex)
		//	{
		//		Program.logger.Error("Controller Error >>> ", ex);
  //              sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
  //          }

		//	return Json(sResp);
		//}

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
            int iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
            int iBranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));

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
            int isSuperadmin = 0;
            int roleIsAdmin = 0;
            roleIsAdmin = Convert.ToInt32(HttpContext.Session.GetString("IsAdmin"));
            var organizationObj = OrganizationRepository.GetOrganizationByID(iOrganizationID);
            if (organizationObj != null)
            {
                if (organizationObj.Level == 0 || organizationObj.Level == 1 || (organizationObj.Level == 2 && roleIsAdmin == 1))
                {
                    isSuperadmin = 1;
                }
                else
                {
                    isSuperadmin = 0;
                }
            }

            if (havePermission)
            //if(role != "User")
            {
                try
                {
                    var petsInfoList = _patientDBContext.GetPetsInfoList(start, rowLimit, ownername, petName, species, breed, isSuperadmin, iBranchID, iOrganizationID, out totalPets).ToList();

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

				// --- Patients Login ------//
				Patient_Owner_Login sPatientLogin = new Patient_Owner_Login();
				sPatientLogin.PatientOwnerID = patientOwner.ID;
				sPatientLogin.ProfileActivated = 0;
				sPatientLogin.CreatedDate = DateTime.Now;
				sPatientLogin.CreatedBy = patientOwner.CreatedBy;

				_patientDBContext.Mst_Patients_Login.Add(sPatientLogin);
				_patientDBContext.SaveChanges();

				Account_Creation_Logs sCreationLog = new Account_Creation_Logs();
				sCreationLog.EmailAddress = patientOwner.EmailAddress;
				sCreationLog.InvitationCode = VPMS.Lib.Helpers.GenerateRandomKeyString(16);
				sCreationLog.LinkCreatedDate = DateTime.Now;
				sCreationLog.LinkExpiryDate = DateTime.Now.AddHours(24);
				sCreationLog.PatientOwnerID = patientOwner.ID;

				_patientDBContext.Mst_Account_Creation_Logs.Add(sCreationLog);
				_patientDBContext.SaveChanges();

				List<String> sRecipientList = new List<string>();
				sRecipientList.Add(patientOwner.EmailAddress);

				SendAccountCreationShortLink(sRecipientList, patientOwner.Name, sCreationLog.InvitationCode);
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
            Random rRnd = new Random(Environment.TickCount);

            try
			{
				int idx = -1;

				var sPetAvatarObj = _patientDBContext.Mst_Avatar.Where(x => x.Species == pets.Species).ToList();
				if (sPetAvatarObj != null && sPetAvatarObj.Count > 0)
				{
                    idx = rRnd.Next(sPetAvatarObj.Count);
                    PetAvatarObject sSelectedRandom = sPetAvatarObj[idx];

					pets.AvatarID = sSelectedRandom.ID;
                }
				else
				{
					pets.AvatarID = idx;
				}
				
				
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
			//return true;
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
                ViewData["CanAdd"] = "false";
                ViewData["CanEdit"] = "true";
                ViewData["CanView"] = "true";
            }
            else
            {
                //if (roles.Contains("PatientDetails.Add"))
				if (roles.Contains("Patients.Add"))
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

		public void SendAccountCreationShortLink(List<String> sTargetReceiver, String sTargetReceiverName, String sCreationLink)
		{
            var sEmailConfig = ConfigSettings.GetConfigurationSettings();

            String? sHost = sEmailConfig.GetSection("SMTP:Host").Value;
            int? sPortNo = Convert.ToInt32(sEmailConfig.GetSection("SMTP:Port").Value);
            String? sUsername = sEmailConfig.GetSection("SMTP:Username").Value;
            String? sPassword = sEmailConfig.GetSection("SMTP:Password").Value;
            String? sSender = sEmailConfig.GetSection("SMTP:Sender").Value;
			String? sPortalURL = sEmailConfig.GetSection("CustomerPortalAccountCreation").Value;


			var emailTemplate = TemplateRepository.GetTemplateByCodeLang(ConfigSettings.GetConfigurationSettings(), "CPMS_EN001", "en");
			emailTemplate.TemplateContent = emailTemplate.TemplateContent.Replace("###<customer>###", sTargetReceiverName)
																		 .Replace("###<creationlink>###", (sPortalURL + sCreationLink));

			try
			{
				VPMS.Lib.EmailObject sEmailObj = new VPMS.Lib.EmailObject();
                sEmailObj.SenderEmail = sSender;
                sEmailObj.RecipientEmail = sTargetReceiver;
                sEmailObj.Subject = (emailTemplate != null) ? emailTemplate.TemplateTitle : "";
				sEmailObj.Body = (emailTemplate != null) ? emailTemplate.TemplateContent : "";
                sEmailObj.SMTPHost = sHost;
                sEmailObj.PortNo = sPortNo.Value;
                sEmailObj.HostUsername = sUsername;
                sEmailObj.HostPassword = sPassword;
                sEmailObj.EnableSsl = true;
                sEmailObj.UseDefaultCredentials = false;
                sEmailObj.IsHtml = true;

				String sErrorMsg = "";
				EmailHelpers.SendEmail(sEmailObj, out sErrorMsg);
            }
			catch (Exception ex)
			{

			}
        }
    }
}
