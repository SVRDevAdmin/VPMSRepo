using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using VPMS;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

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


		int totalPets;

		public IActionResult Index()
        {
            return RedirectToAction("PatientsList");
		}

		public IActionResult PatientsList()
		{
			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();

            Program.CurrentPage = "/Patients/PatientsList";

            return View();
		}

		[Route("/Patients/PatientProfile/{type}/{patientid}")]
		public IActionResult ViewPatientProfile(string type, int patientid)
		{
			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
			ViewData["Color"] = _masterCodeDataDBContext.Mst_MasterCodeData.Where(x => x.CodeGroup == "Color").Select(y => y.CodeName).ToList();

			ViewBag.PatientID = patientid;
			ViewBag.type = type;

            Program.CurrentPage = "/Patients/PatientProfile/" + type + "/" + patientid;

            return View();
		}

		[Route("/Patients/PetProfile/{type}/{patientid}/{petname}")]
		public IActionResult PetProfile(string type,int patientid, string petname)
		{
			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;
			ViewBag.type = type;

			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
			ViewData["Color"] = _masterCodeDataDBContext.Mst_MasterCodeData.Where(x => x.CodeGroup == "Color").Select(y => y.CodeName).ToList();
			ViewData["OtherPets"] = _patientDBContext.Mst_Pets.Where(x => x.PatientID == patientid && x.Name != petname).Select(y => y.Name).ToList();
			ViewData["VaccinationList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Vaccination").ToList();
			ViewData["SurgeryList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Surgery").ToList();
			ViewData["MedicationList"] = _inventoryDBContext.Mst_ProductType.ToList();

			var petProfile = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);

            Program.CurrentPage = "/Patients/PetProfile/" + type + "/" + patientid + "/" + petname;

            return View(petProfile);
		}

		[Route("/Patients/InvoiceBilling/{patientid}/{petname}")]
		public IActionResult InvoiceBilling(int patientid, string petname)
		{
			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;

			var petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);

            Program.CurrentPage = "/Patients/InvoiceBilling/" + patientid + "/" + petname;

            return View(petInfo);
		}

		[Route("/Patients/TreatmentPlan/{patientid}/{petname}")]
		public IActionResult TreatmentPlan(int patientid, string petname)
		{
			ViewData["PetName"] = petname;
			ViewBag.PatientID = patientid;

			var petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);

			ViewData["TreatmentPlans"] = _treatmentPlanDBContext.Mst_TreatmentPlan.ToList();

			ViewData["Services"] = _servicesDBContext.Mst_Services.ToList();
			ViewData["Inventories"] = _inventoryDBContext.Mst_Product.ToList();


			Program.CurrentPage = "/Patients/TreatmentPlan/" + patientid + "/" + petname;

            return View(petInfo);
		}

		public PatientTreatmentPlan GetUpcomingTreatmentPlan(int petID) 
		{
			var upcomingTreatmentPlan = _patientDBContext.Txn_TreatmentPlan.Where(x => x.PetID == petID && x.TreatmentStart > DateOnly.FromDateTime(DateTime.Now)).OrderBy(x => x.TreatmentStart).FirstOrDefault();

			if(upcomingTreatmentPlan == null) { return new PatientTreatmentPlan(); }

			return upcomingTreatmentPlan;
		}

		public List<PatientTreatmentPlanServices> getUpcomingTreatmentPlanServices(int planID)
		{
			return _patientDBContext.Txn_TreatmentPlan_Services.Where(x => x.PlanID == planID).ToList();
		}

		public List<PatientTreatmentPlanProducts> getUpcomingTreatmentPlanProducts(int planID)
		{
			return _patientDBContext.Txn_TreatmentPlan_Products.Where(x => x.PlanID == planID).ToList();
		}

		public int InsertPatientTreatmentPlan([FromBody] PatientTreatmentPlan patientTreatmentPlan)
		{
			_patientDBContext.Txn_TreatmentPlan.Add(patientTreatmentPlan);
			_patientDBContext.SaveChanges();

			return patientTreatmentPlan.ID;
		}

		public void InsertPatientTreatmentPlanService([FromBody] PatientTreatmentPlanServices patientTreatmentPlanService)
		{
			_patientDBContext.Txn_TreatmentPlan_Services.Add(patientTreatmentPlanService);
			_patientDBContext.SaveChanges();
		}

		public void InsertPatientTreatmentPlanProduct([FromBody] PatientTreatmentPlanProducts patientTreatmentPlanProducts)
		{
			_patientDBContext.Txn_TreatmentPlan_Products.Add(patientTreatmentPlanProducts);
			_patientDBContext.SaveChanges();
		}

		public PatientInfoProfile GetPatientProfile(int patientid)
		{
			PatientInfoProfile patientInfo = new PatientInfoProfile()
			{
				patient_Owners = _patientDBContext.Mst_Patients_Owner.Where(x => x.PatientID == patientid).ToList(),
				Pets = _patientDBContext.Mst_Pets.Where(x => x.PatientID == patientid).ToList()
			};

			return patientInfo;
		}

		public IActionResult CreateNewPatient()
		{
			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
			ViewData["Color"] = _masterCodeDataDBContext.Mst_MasterCodeData.Where(x => x.CodeGroup == "Color").Select(y => y.CodeName).ToList();
			ViewData["VaccinationList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Vaccination").ToList();
			ViewData["SurgeryList"] = _servicesDBContext.Mst_ServicesCategory.Where(x => x.Name == "Surgery").ToList();
			ViewData["MedicationList"] = _inventoryDBContext.Mst_ProductType.ToList();

			Program.CurrentPage = "/Patients/CreateNewPatient";

            return View();
		}

		public PatientsInfo GetPetsInfoList(int rowLimit, int page, string ownername, string petName, string species, string breed)
        {
            int start = (page - 1) * rowLimit;

			var petsInfoList = _patientDBContext.GetPetsInfoList(start, rowLimit, ownername, petName, species, breed, out totalPets).ToList();

			var patientsInfo = new PatientsInfo() { petsInfo = petsInfoList, totalPatients = totalPets };

			return patientsInfo;
		}

		public List<PatientMedicalRecordService> GetMedicalRecordServicesByPetID(int petID)
		{
			return _patientDBContext.Mst_MedicalRecord_VaccinationSurgery.Where(x => x.PetID == petID && x.IsDeleted != 1).ToList();
		}

		public List<PatientMedicalRecordMedication> GetMedicalRecordMedicationByPetID(int petID)
		{
			return _patientDBContext.Mst_MedicalRecord_Medication.Where(x => x.PetID== petID && x.Status != 3).ToList();
		}

		public List<Pets_Breed> BreedList (string species)
		{
			return _patientDBContext.Mst_Pets_Breed.Where(x => x.Species == species).ToList();
		}

		public int CreatePatientInfo([FromBody] PatientsModel patients)
		{
			_patientDBContext.Mst_Patients.Add(patients);

			_patientDBContext.SaveChanges();

			return patients.ID;
		}

		public void InsertPatientOwnerInfo([FromBody] Patient_Owner patientOwner)
		{
			_patientDBContext.Mst_Patients_Owner.Add(patientOwner);

			_patientDBContext.SaveChanges();
		}

		public void UpdatePatientOwnerInfo([FromBody] Patient_Owner patientOwner)
		{
			if(patientOwner.ID != 0) { _patientDBContext.Update(patientOwner); }
			else { _patientDBContext.Mst_Patients_Owner.Add(patientOwner); }

			_patientDBContext.SaveChanges();
		}

		public int InsertPetsInfo([FromBody] Pets pets)
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

			return pets.ID;
		}

		public int UpdatePetInfo([FromBody] Pets pets)
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

			return pets.ID;
		}
		
		public void DeletePetInfo(int patientid, string petname)
		{
			var petInfo = _patientDBContext.Mst_Pets.FirstOrDefault(x => x.PatientID == patientid && x.Name == petname);

			if(petInfo != null)
			{
				_patientDBContext.Mst_Pets.Remove(petInfo);

				_patientDBContext.SaveChanges();
			}
		}

		public bool InsertMedicalRecordService([FromBody] PatientMedicalRecordService patientMedicalRecordService)
		{
			_patientDBContext.Add(patientMedicalRecordService);
			_patientDBContext.SaveChanges();

			return true;
		}

		public bool InsertMedicalRecordMedication([FromBody] PatientMedicalRecordMedication patientMedicalRecordMedication)
		{
			_patientDBContext.Add(patientMedicalRecordMedication);
			_patientDBContext.SaveChanges();

			return true;
		}

		public bool UpdateMedicalRecordService([FromBody] PatientMedicalRecordService patientMedicalRecordService)
		{
			if(patientMedicalRecordService.CategoryID != 0)
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

		public bool UpdateMedicalRecordMedication([FromBody] PatientMedicalRecordMedication patientMedicalRecordMedication)
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

		public List<Pet_Growth> GetPetGrowth(int petID)
		{
			return _patientDBContext.Mst_Pet_Growth.Where(x => x.PetID == petID).ToList(); ;
		}
	}
}
