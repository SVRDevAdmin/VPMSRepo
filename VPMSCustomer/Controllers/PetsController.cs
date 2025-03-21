using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.Models;
using VPMSCustomer.Lib.Models;

namespace VPMSCustomer.Controllers
{
    public class PetsController : Controller
    {
        // GET: PetsController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View("Listing");
        }

        [Route("/Pets/View/{petID}")]
        [HttpGet()]
        public ActionResult View(long petID)
        {
            ViewData["PetSelected"] = petID;

            return View("Details");
        }

        [Route("/Pets/GetPetsList/{patientID}")]
        [HttpGet()]
        public IActionResult GetPetsList(long patientID)
        {
            List<PetDataExtendedModel> PetList = new List<PetDataExtendedModel>();

            try
            {
                PetList = PetRepository.GetPetsListByPatientID(patientID);
                if (PetList != null)
                {
                    return Json(new { data = PetList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetPatientPetList")]
        [HttpGet()]
        public IActionResult GetPatientPetListing(long patientID, String petName, String species, String breed, int pageSize, int pageIndex)
        {
            List<PetListingDataModel> PetList = new List<PetListingDataModel>();

            try
            {
                int iTotalRecords = 0;
                PetList = PetRepository.GetPatientPetListing(patientID, petName, species, breed, pageSize, pageIndex, out iTotalRecords);
                if (PetList != null)
                {
                    return Json(new { data = PetList, totalRecord = iTotalRecords });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetPetByID/{petID}")]
        [HttpGet()]
        public IActionResult GetPetProfileByID(int petID)
        {
            try
            {
                PetDataExtendedModel PetObj = PetRepository.GetPetProfileByID(petID);
                if (PetObj != null)
                {
                    return Json(new { data = PetObj });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetTreatmentServices/{petID}")]
        [HttpGet()]
        public IActionResult GetPetTreatmentServices(int petID)
        {
            try
            {
                List<PetTreatmentServiceModel> sResultList = PetRepository.GetPetTreatmentServices(petID);
                if (sResultList != null)
                {
                    return Json(new { data = sResultList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetMedicalHistory/{petID}")]
        [HttpGet()]
        public IActionResult GetPetMedicationHistory(int petID)
        {
            try
            {
                List<PetMedicationModel> sResultList = PetRepository.GetPetMedicationHistory(petID);
                if (sResultList != null)
                {
                    return Json(new { data = sResultList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetActiveTreatmentPlan/{petID}")]
        [HttpGet()]
        public IActionResult GetPetActiveTreatmentPlanHistory(int petID)
        {
            try
            {
                List<PetTreatmentPlanModel> sResultList = PetRepository.GetPetActiveTreatmentPlanHistory(petID);
                if (sResultList != null)
                {
                    return Json(new { data = sResultList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetPastTreatmentPlan/{petID}")]
        [HttpGet()]
        public IActionResult GetPetPastTreatmentPlanHistory(int petID)
        {
            try
            {
                List<PetTreatmentPlanModel> sResultList = PetRepository.GetPetPastTreatmentPlanHistory(petID);
                if (sResultList != null)
                {
                    return Json(new { data = sResultList });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetPetTestResultHistory/{petID}/{resultType}")]
        [HttpGet()]
        public IActionResult GetPetTestResultHistory(int petID, String resultType)
        {
            int iTotalRecords = 0;
            try
            {
                List<PetTestResultsModel> sResultList = PetRepository.GetPetTestResultHistory(petID, resultType, 0, 0, out iTotalRecords);
                if (sResultList != null)
                {
                    return Json(new { data = sResultList, TotalRecords = iTotalRecords });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetSpeciesList")]
        [HttpGet()]
        public IActionResult GetSpeciesList()
        {
            try
            {
                var sResult = MastercodeRepository.GetMastercodeDataByGroup("Species");
                if (sResult != null)
                {
                    return Json(new { data = sResult });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("/Pets/GetBreedList/{species}")]
        [HttpGet()]
        public IActionResult GetBreedList(String species)
        {
            try
            {
                var sResult = PetRepository.GetBreedListBySpecies(species);
                if (sResult != null)
                {
                    return Json(new { data = sResult });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class CatRespObject : ResponseCodeObject
    {
        public List<PetDataModel> PetList { get; set; }
    }
}
