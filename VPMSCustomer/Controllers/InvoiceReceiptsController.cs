using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data;

namespace VPMSCustomer.Controllers
{
    public class InvoiceReceiptsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get Invoice & Receipts Listing by Type
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="iViewType"></param>
        /// <param name="invReceiptNo"></param>
        /// <param name="petName"></param>
        /// <param name="doctorName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Invoice / Receipts Details by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Invoice / Receipts's Services List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Invoices / Receipt's Product List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get total amount of the Invoice / Receipts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
    }
}
