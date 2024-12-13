using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Data;
using System.Web;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using VPMS.Lib.Data;
using VPMSWeb.Lib.General;
using VPMSWeb.Lib.Settings;
using ClosedXML.Excel;
using System.Collections;
using VPMS.Lib.Data.Models;
using Org.BouncyCastle.Utilities;
using Microsoft.Extensions.Hosting.Internal;
using System.Drawing.Text;

namespace VPMSWeb.Controllers
{
    public class TestManagementController : Controller
    {
        // GET: TestManagementController
        public ActionResult Index()
        {
            return View();
        }

        [Route("/TestManagement/ViewResults/{resultID}/View")]
        public ActionResult ViewResults(int resultID)
        {
            //var sBreakdownResult = TestResultRepository.GetTestResultBreakdownDetails(ConfigSettings.GetConfigurationSettings(), resultID);
            //if (sBreakdownResult != null)
            //{
            //    ViewData["ResultBreakdown"] = sBreakdownResult;
            //}
            ViewData["ResultID"] = resultID;

            return View("ViewResults", resultID);
        }

        public IActionResult GetTestResultListing(int branchID, String patientID, String deviceName, String sortOrder, int pageSize, int pageIndex)
        {
            int iTotalRecords;

            var sResult = TestResultRepository.GetTestResultManagementListing(ConfigSettings.GetConfigurationSettings(), branchID, patientID, deviceName, sortOrder, pageSize, pageIndex, out iTotalRecords);
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

        public IActionResult GetTestResultBreakdownDetails(int resultID)
        {
            var sResult = TestResultRepository.GetTestResultBreakdownDetails(ConfigSettings.GetConfigurationSettings(), resultID);
            if (sResult != null)
            {
                return Json(new { data = sResult });
            }

            return null;
        }

        [HttpPost]
        public IActionResult DownloadTestResultListing(TestResultRequestModel data)
        {
            FileContentResult fObject;
            int iTotalRecords = 0;

            var sResult = TestResultRepository.GetTestResultManagementListing(ConfigSettings.GetConfigurationSettings(), Convert.ToInt32(data.branchID), data.patientID, data.deviceName, data.sortOrder, 100, 1, out iTotalRecords);

            DataTable sDataTable = Utility.ToDataTable(sResult);
            String sBase64String = "";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(sDataTable, "Test Results");

                String sMainFolder = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Temp");
                if (!Directory.Exists(sMainFolder))
                {
                    Directory.CreateDirectory(sMainFolder);
                }

                String sDownloadFile = Path.Combine(sMainFolder, "456.xlsx");
                wb.SaveAs(sDownloadFile);

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    //var bytesdata = File(ms.ToArray(), "application/ms-excel", "Report.xlsx");
                    var bytesdata = File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");

                    //Byte[] bytes = System.IO.File.ReadAllBytes("c:\\test\\456.xlsx");
                    Byte[] bytes = System.IO.File.ReadAllBytes(sDownloadFile);
                    sBase64String = Convert.ToBase64String(bytes);
                    fObject = bytesdata;
                }
            }

            return Json(new { data = sBase64String });
        }
    }
}
