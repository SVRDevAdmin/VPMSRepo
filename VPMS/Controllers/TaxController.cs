using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    public class TaxController : Controller
    {
        String sGroupName = "TaxType";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        /// <summary>
        /// Get Tax Configuration Listing
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [Route("/Tax/GetTaxConfigurationListing")]
        [HttpGet()]
        public IActionResult GetTaxConfigurationListing(int pageSize, int pageIndex)
        {
            int iTotalRecords = 0;

            var sResult = TaxRepository.GetTaxConfigurationListing(ConfigSettings.GetConfigurationSettings(), pageSize, pageIndex, out iTotalRecords);
            if (sResult != null)
            {
                return Json(new { data = sResult, totalRecord = iTotalRecords });
            }

            return null;
        }

        /// <summary>
        /// Add New Tax Configuration
        /// </summary>
        /// <param name="sTaxType"></param>
        /// <param name="sDescription"></param>
        /// <param name="sChargesRate"></param>
        /// <param name="sCreatedBy"></param>
        /// <returns></returns>
        [Route("/Tax/InsertTax")]
        [HttpPost()]
        public IActionResult InsertTax(String sTaxType, String sDescription, String sChargesRate, String sCreatedBy)
        {
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

            TaxModel sTaxModel = new TaxModel();
            sTaxModel.TaxType = sTaxType;
            sTaxModel.Description = sDescription;
            sTaxModel.ChargesRate = Convert.ToDecimal(sChargesRate);
            sTaxModel.Status = 1;
            sTaxModel.CreatedDate = DateTime.Now;
            sTaxModel.CreatedBy = sCreatedBy;

            if (!TaxRepository.IsTaxConfigurationExists(ConfigSettings.GetConfigurationSettings(), sTaxType))
            {
                if (TaxRepository.insertTaxConfiguration(ConfigSettings.GetConfigurationSettings(), sTaxModel))
                {
                    sResp.StatusCode = (int)StatusCodes.Status200OK;
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.isRecordExists = false;
                }
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                sResp.isRecordExists = true;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Get Tax Type List
        /// </summary>
        /// <returns></returns>
        [Route("/Tax/GetTaxTypeList")]
        [HttpGet()]
        public IActionResult GetTaxTypeListDropdown()
        {
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sGroupName);
            return Json(sMasterCodeObj);
        }

        /// <summary>
        /// View Tax Configuration Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ViewType"></param>
        /// <returns></returns>
        [Route("/Tax/ViewTaxConfiguration/{id}/{ViewType}")]
        [HttpGet()]
        public IActionResult ViewTaxConfiguration(int? id, String ViewType)
        {
            TaxModel sTaxObject = new TaxModel();
            sTaxObject = TaxRepository.GetTaxConfigurationByID(ConfigSettings.GetConfigurationSettings(), id.Value);

            ViewData["TaxConfiguration"] = sTaxObject;
            ViewData["ViewType"] = ViewType;

            return View("Details", sTaxObject);
        }

        /// <summary>
        /// Update Tax Configuration Details
        /// </summary>
        /// <param name="sID"></param>
        /// <param name="sDescription"></param>
        /// <param name="sChargeRate"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        [Route("/Tax/UpdateTaxConfiguration")]
        [HttpPost()]
        public IActionResult UpdateTaxConfiguration(int sID, String sDescription, String sChargeRate, String sUpdatedBy)
        {
            Models.ResponseStatusObject sResp = new Models.ResponseStatusObject();

            Decimal dRate = 0;
            if (!String.IsNullOrEmpty(sChargeRate))
            {
                dRate = Convert.ToDecimal(sChargeRate);
            }

            if (TaxRepository.UpdateTaxConfiguration(ConfigSettings.GetConfigurationSettings(), sID, sDescription, dRate, sUpdatedBy))
            {
                sResp.StatusCode = (int)StatusCodes.Status200OK;
            }
            else
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }
    }
}
