using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data;

namespace VPMSCustomer.Controllers
{
    public class InvoiceReceiptsController : Controller
    {
        // GET: InvoiceReceiptsController
        public ActionResult Index()
        {
            return View();
        }

        [Route("/InvoiceReceipts/GetInvoiceListing")]
        [HttpGet()]
        public IActionResult GetInvoiceListing(long patientID, int iViewType, String invReceiptNo, String petName, String doctorName)
        {
            try
            {
                List<long> petIDs = new List<long>();
                var sPetList = PetRepository.GetPetsListByPatientID(patientID);
                if (sPetList != null)
                {
                    petIDs = sPetList.Select(x => x.ID).Distinct().ToList();
                }

                int iTotalRecord = 0;
                var sResult = InvoiceReceiptRepository.GetInvoiceReceiptViewListing(iViewType, petIDs, invReceiptNo, petName, doctorName, 0, 0, out iTotalRecord);
                if (sResult != null)
                {
                    return Json(new { data = sResult, totalRecord = iTotalRecord });
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

        [Route("/InvoiceReceipts/Detail/{id}")]
        [HttpGet()]
        public IActionResult GetInvoiceReceiptDetailByID(int id)
        {
            try
            {
                var sDetailObj = InvoiceReceiptRepository.GetInvoiceReceiptDetailByID(id);
                if (sDetailObj != null)
                {
                    return Json(new { data = sDetailObj });
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

        [Route("/InvoiceReceipts/ServicesList/{id}")]
        [HttpGet()]
        public IActionResult GetInvoiceReceiptServicesList(int id)
        {
            try
            {
                var sServiceList = InvoiceReceiptRepository.GetInvoiceReceiptServicesList(id);
                if (sServiceList != null)
                {
                    return Json(new { data = sServiceList });
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

        [Route("/InvoiceReceipts/ProductList/{id}")]
        [HttpGet()]
        public IActionResult GetInvoiceReceiptProductList(int id)
        {
            try
            {
                var sProductList = InvoiceReceiptRepository.GetInvoiceReceiptProductList(id);
                if ( sProductList != null)
                {
                    return Json(new { data = sProductList });
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

        [Route("/InvoiceReceipts/Total/{id}")]
        [HttpGet()]
        public IActionResult GetInvoiceReceiptTotal(int id)
        {
            try
            {
                var sProductList = InvoiceReceiptRepository.GetInvoiceReceiptTotal(id);
                if (sProductList != null)
                {
                    return Json(new { data = sProductList });
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

        //// GET: InvoiceReceiptsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: InvoiceReceiptsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: InvoiceReceiptsController/Create
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

        //// GET: InvoiceReceiptsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: InvoiceReceiptsController/Edit/5
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

        //// GET: InvoiceReceiptsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: InvoiceReceiptsController/Delete/5
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
}
