﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            Program.CurrentPage = "/Patients/TreatmentPlan/" + patientid + "/" + petname;

            return View(petInfo);
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
			_patientDBContext.Update(patientOwner);

			_patientDBContext.SaveChanges();
		}

		public void InsertPetsInfo([FromBody] Pets pets)
		{
			_patientDBContext.Mst_Pets.Add(pets);

			_patientDBContext.SaveChanges();
		}

		public void UpdatePetInfo([FromBody] Pets pets)
		{
			_patientDBContext.Update(pets);

			_patientDBContext.SaveChanges();
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
	}
}
