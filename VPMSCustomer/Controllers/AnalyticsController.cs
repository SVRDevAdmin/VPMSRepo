using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMSCustomer.Lib.Data;

namespace VPMSCustomer.Controllers
{
    public class AnalyticsController : Controller
    {
        // GET: AnalyticsController
        public ActionResult Index()
        {
            return View();
        }

        //GetExpensesSummaryByYear
        [Route("/Analytics/GetExpensesSummaryByYear")]
        [HttpGet()]
        public IActionResult GetExpensesSummaryByYear(String summaryYear, long iPatientID)
        {
            try
            {
                var sResult = AnalyticsRepository.GetExpensesSummaryByYear(summaryYear, iPatientID);
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

        [Route("/Analytics/GetExpensesSummaryBreakdown")]
        [HttpGet()]
        public IActionResult GetExpensesSummaryBreakdown(String summaryYear, long iPatientID)
        {
            try
            {
                var sResult = AnalyticsRepository.GetExpensesSummaryBreakdown(summaryYear, iPatientID);
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

        // GET: AnalyticsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnalyticsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalyticsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnalyticsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnalyticsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnalyticsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnalyticsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
