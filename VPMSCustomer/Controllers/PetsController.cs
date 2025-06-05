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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View("Listing");
        }

        /// <summary>
        /// Get Pet Profile BY Pet ID
        /// </summary>
        /// <param name="petID"></param>
        /// <returns></returns>
        [Route("/Pets/View/{petID}")]
        [HttpGet()]
        public ActionResult View(long petID)
        {
            ViewData["PetSelected"] = petID;

            return View("Details");
        }

        /// <summary>
        /// Get Patient's Pet List
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Patient's Pet List View
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="petName"></param>
        /// <param name="species"></param>
        /// <param name="breed"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Pet Profile By Pet ID
        /// </summary>
        /// <param name="petID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Pet's Treatment Services History
        /// </summary>
        /// <param name="petID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Pet's Medical History
        /// </summary>
        /// <param name="petID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Pet's Current Treatment Plan
        /// </summary>
        /// <param name="petID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Pet's Past Treatment Plan
        /// </summary>
        /// <param name="petID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Pet's Test Result History
        /// </summary>
        /// <param name="petID"></param>
        /// <param name="resultType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Species Master List
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Get Breed List by Species
        /// </summary>
        /// <param name="species"></param>
        /// <returns></returns>
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
