using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
    //[Authorize]
    public class PatientsController : Controller
    {
        private readonly PatientDBContext _patientDBContext = new PatientDBContext();
		private readonly MasterCodeDataDBContext _masterCodeDataDBContext = new MasterCodeDataDBContext();

		int totalPets;

		public IActionResult Index()
        {
			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();

			return View();
		}

		[Route("/Patients/ViewPatientProfile/{patientid}")]
		public IActionResult ViewPatientProfile(int patientid)
		{
			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
			ViewData["Color"] = _masterCodeDataDBContext.Mst_MasterCodeData.Where(x => x.CodeGroup == "Color").Select(y => y.CodeName).ToList();

			ViewBag.PatientID = patientid;

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
				
			return View(petProfile);
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

		public void InsertPetsInfo([FromBody] Pets pets)
		{
			_patientDBContext.Mst_Pets.Add(pets);

			_patientDBContext.SaveChanges();
		}
	}
}
