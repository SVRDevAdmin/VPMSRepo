using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class TestManagementController : Controller
    {
        // GET: TestManagementController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult GetTestResultListing(String patientID, String deviceName, String sortOrder, int pageSize, int pageIndex)
        {
            int iTotalRecords;

            var sResult = TestResultRepository.GetTestResultManagementListing(ConfigSettings.GetConfigurationSettings(), patientID, deviceName, sortOrder, pageSize, pageIndex, out iTotalRecords);
            if (sResult != null)
            {
                return Json(new { data = sResult, totalRecord = iTotalRecords });
            }

            return null;
        }

        public IActionResult GetDeviceNameList()
        {
            var sDeviceList = TestResultRepository.GetTestResultDeviceNameList(ConfigSettings.GetConfigurationSettings());
            return Json(sDeviceList);
        }
        //// GET: TestManagementController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: TestManagementController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TestManagementController/Create
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

        //// GET: TestManagementController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: TestManagementController/Edit/5
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

        //// GET: TestManagementController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: TestManagementController/Delete/5
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
