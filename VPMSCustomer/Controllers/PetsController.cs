using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //// GET: PetsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PetsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PetsController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PetsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PetsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PetsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PetsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }

    public class CatRespObject : ResponseCodeObject
    {
        public List<PetDataModel> PetList { get; set; }
    }
}
