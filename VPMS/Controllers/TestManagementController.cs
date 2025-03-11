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
using Spire.Xls;
using Microsoft.AspNetCore.Authorization;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class TestManagementController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// View Selected Test Results
        /// </summary>
        /// <param name="resultID"></param>
        /// <returns></returns>
        [Route("/TestManagement/ViewResults/{resultID}/View")]
        public ActionResult ViewResults(int resultID)
        {
            ViewData["ResultID"] = resultID;

            return View("ViewResults", resultID);
        }

        /// <summary>
        /// Get Test Result Listing Views
        /// </summary>
        /// <param name="branchID"></param>
        /// <param name="patientID"></param>
        /// <param name="deviceName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get list of device name
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDeviceNameList(int BranchID)
        {
            var sDeviceList = TestResultRepository.GetTestResultDeviceNameList(ConfigSettings.GetConfigurationSettings(), BranchID);
            return Json(sDeviceList);
        }

        /// <summary>
        /// Get selected Test Result Details
        /// </summary>
        /// <param name="resultID"></param>
        /// <returns></returns>
        public IActionResult GetTestResultBreakdownDetails(int resultID)
        {
            var sResult = TestResultRepository.GetTestResultBreakdownDetails(ConfigSettings.GetConfigurationSettings(), resultID);
            if (sResult != null)
            {
                return Json(new { data = sResult });
            }

            return null;
        }

        /// <summary>
        /// Download Test Result Listing (Return Base64String)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DownloadTestResultListing(TestResultRequestModel data)
        {
            String sBase64String = "";
            int iTotalRecords = 0;

            String sMainFolder = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Temp");
            if (!Directory.Exists(sMainFolder))
            {
                Directory.CreateDirectory(sMainFolder);
            }

            var rnd = new Random(DateTime.Now.Microsecond);
            int iRandom = rnd.Next(0, 9999);
            String sDownloadFile = Path.Combine(sMainFolder, DateTime.Now.ToString("yyyyMMddHHmmss") + iRandom.ToString() + ".xlsx");
            String sPDFDownloadFile = Path.Combine(sMainFolder, DateTime.Now.ToString("yyyyMMddHHmmss") + iRandom.ToString() + ".pdf");


            var sResult = TestResultRepository.GetTestResultManagementListing(ConfigSettings.GetConfigurationSettings(), 
                                                                            Convert.ToInt32(data.branchID), data.patientID, 
                                                                            data.deviceName, data.sortOrder, 100, 1, out iTotalRecords);
            DataTable sDataTable = Utility.ToDataTable(sResult);

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(sDataTable, "Test Results")
                         .Columns().AdjustToContents();
            wb.SaveAs(sDownloadFile);

            if (data.isPDFPrint == "1")
            {
                Spire.Xls.Workbook spireWorkbook = new Spire.Xls.Workbook();
                spireWorkbook.LoadFromFile(sDownloadFile);
                spireWorkbook.Worksheets[0].PageSetup.Orientation = PageOrientationType.Portrait;
                spireWorkbook.Worksheets[0].PageSetup.IsFitToPage = true;

                spireWorkbook.SaveToFile(sPDFDownloadFile, Spire.Xls.FileFormat.PDF);

                Byte[] PDFBytes = System.IO.File.ReadAllBytes(sPDFDownloadFile);
                sBase64String = Convert.ToBase64String(PDFBytes);
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);

                    Byte[] bytes = System.IO.File.ReadAllBytes(sDownloadFile);
                    sBase64String = Convert.ToBase64String(bytes);
                }
            }

            // ------- Delete Temp Excel File ---------//
            if (System.IO.File.Exists(sDownloadFile))
            {
                System.IO.File.Delete(sDownloadFile);
            }
            // ------ Delete Temp PDF File -----------//
            if (System.IO.File.Exists(sPDFDownloadFile))
            {
                System.IO.File.Delete(sPDFDownloadFile);
            }

            return Json(new { data = sBase64String });
        }
    }
}
