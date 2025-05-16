using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPMS.Lib.Data;
using VPMSWeb.Lib.Settings;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;
using VPMS.Lib.Data.Models;
using VPMSWeb.Models;

namespace VPMSWeb.Controllers
{
    public class CustomerController : Controller
    {
        static String sStatusMasterCode = "Status";

        [Route("/Customer/Banners")]
        public ActionResult Banner()
        {
            return View("BannersList");
        }

        [Route("/Customer/BannerProfile")]
        public ActionResult BannerProfile()
        {
            return View("BannerProfile");
        }

        [Route("/Customer/GetBannersViewList")]
        [HttpGet()]
        public IActionResult GetBannersViewList(int pageSize, int pageIndex)
        {
            int iTotalRecords = 0;

            try
            {
                var sBannerObj = BannerRepository.GetBannersViewList(ConfigSettings.GetConfigurationSettings(), pageSize, pageIndex, out iTotalRecords);
                if (sBannerObj != null)
                {
                    return Json (new { data = sBannerObj, totalRecords = iTotalRecords });
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

        [Route("/Customer/UploadBanner")]
        [HttpPost()]
        public IActionResult UploadBanner()
        {
            ResponseBannerUploadObject sResp = new ResponseBannerUploadObject();
            //HttpPostedFileBase postedFile = Request.Form.Files[0];
            //Request.Form.Files[0];

            //IFormFile postedFile = Request.Form.Files[0];

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    BannerUploadForm sUploadData = new BannerUploadForm();

                    String strFormData = Request.Form["Data"];
                    if (strFormData != "")
                    {
                        sUploadData = Newtonsoft.Json.JsonConvert.DeserializeObject<BannerUploadForm>(strFormData);
                    }

                    DateTime dtStart = DateTime.ParseExact(sUploadData.startDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtEnd = DateTime.ParseExact(sUploadData.endDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    BannerModel sBannerObj = new BannerModel();
                    sBannerObj.BannerType = 0;
                    sBannerObj.Description = sUploadData.description;
                    sBannerObj.StartDate = dtStart;
                    sBannerObj.EndDate = dtEnd;
                    sBannerObj.IsActive = sUploadData.status;
                    sBannerObj.BannerName = "";
                    sBannerObj.BannerFilePath = "";
                    sBannerObj.CreatedDate = DateTime.Now;
                    sBannerObj.CreatedBy = "";

                    var sResult = BannerRepository.InsertBanners(ConfigSettings.GetConfigurationSettings(), sBannerObj);
                    if (sResult)
                    {
                        sResp.StatusCode = (int)StatusCodes.Status200OK;
                    }
                    else
                    {
                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                        sResp.isBannerSelected = true;
                    }
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.isBannerSelected = false;
                }

            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        [Route("/Customer/GetStatusList")]
        [HttpGet()]
        public IActionResult GetStatusListDropdown()
        {
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sStatusMasterCode);
            return Json(sMasterCodeObj);
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
