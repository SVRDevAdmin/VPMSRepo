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
        private static IWebHostEnvironment _webHosting;
        static String sStatusMasterCode = "Status";

        public CustomerController(IWebHostEnvironment iWebHostEnv)
        {
            _webHosting = iWebHostEnv;
        }

        /// <summary>
        /// Banner Listing page
        /// </summary>
        /// <returns></returns>
        [Route("/Customer/Banners")]
        public ActionResult Banner()
        {
            return View("BannersList");
        }

        /// <summary>
        /// Banner Profile page
        /// </summary>
        /// <returns></returns>
        [Route("/Customer/BannerProfile")]
        public ActionResult BannerProfile()
        {
            return View("BannerProfile");
        }

        /// <summary>
        /// Get Banners View Listing
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Upload Banner Info
        /// </summary>
        /// <returns></returns>
        [Route("/Customer/UploadBanner")]
        [HttpPost()]
        public IActionResult UploadBanner()
        {
            ResponseBannerUploadObject sResp = new ResponseBannerUploadObject();

            String wwwrootPath = _webHosting.WebRootPath;
            String sDomainHost = HttpContext.Request.Host.Value;
            String sScheme = HttpContext.Request.Scheme;
            String sBannerFolder = "/upload/banners/";
            String sBannerUrlPath = sScheme + "://" + sDomainHost + sBannerFolder;

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile postedFile = Request.Form.Files[0];

                    String sUploadFolderPath = Path.Combine(wwwrootPath + sBannerFolder);
                    String sDestinationPath = Path.Combine(sUploadFolderPath, postedFile.FileName);

                    if (!Directory.Exists(sUploadFolderPath))
                    {
                        Directory.CreateDirectory(sUploadFolderPath);
                    }
                    
                    using (Stream fs = new FileStream(sDestinationPath, FileMode.Create))
                    {
                        postedFile.CopyTo(fs);
                    }


                    if (System.IO.File.Exists(sDestinationPath))
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
                        sBannerObj.BannerName = postedFile.FileName;
                        sBannerObj.BannerFilePath = sBannerUrlPath + postedFile.FileName;
                        sBannerObj.CreatedDate = DateTime.Now;
                        sBannerObj.CreatedBy = sUploadData.uploadedBy;

                        var sResult = BannerRepository.InsertBanners(ConfigSettings.GetConfigurationSettings(), sBannerObj);
                        if (sResult)
                        {
                            sResp.StatusCode = (int)StatusCodes.Status200OK;
                            sResp.isFileUploadSuccess = true;
                        }
                        else
                        {
                            sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                            sResp.isBannerSelected = true;
                            sResp.isFileUploadSuccess = true;
                        }

                    }
                    else
                    {
                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                        sResp.isBannerSelected = true;
                        sResp.isFileUploadSuccess = false;
                    }
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.isBannerSelected = false;
                    sResp.isFileUploadSuccess = false;
                }

            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        /// <summary>
        /// Get Status List
        /// </summary>
        /// <returns></returns>
        [Route("/Customer/GetStatusList")]
        [HttpGet()]
        public IActionResult GetStatusListDropdown()
        {
            var sMasterCodeObj = MastercodeRepository.GetMastercodeByGroup(ConfigSettings.GetConfigurationSettings(), sStatusMasterCode);
            return Json(sMasterCodeObj);
        }

        /// <summary>
        /// View Banner Profile
        /// </summary>
        /// <param name="bannerID"></param>
        /// <param name="ViewType"></param>
        /// <returns></returns>
        [Route("/Customer/ViewBannerProfile/{bannerID}/{ViewType}")]
        public IActionResult ViewBannerProfile(int bannerID, String ViewType)
        {
            BannerModel sBannerObject = new BannerModel();
            sBannerObject = BannerRepository.GetBannerByID(ConfigSettings.GetConfigurationSettings(), bannerID);
            ViewData["BannerProfile"] = sBannerObject;
            ViewData["ViewType"] = ViewType;

            return View("BannerProfile", sBannerObject);
        }

        /// <summary>
        /// Update Banner Info
        /// </summary>
        /// <param name="bannerID"></param>
        /// <param name="sUpdateForm"></param>
        /// <returns></returns>
        [Route("/Customer/UpdateBannerInfo")]
        public IActionResult UpdateBannerInfo(int bannerID, BannerUploadForm sUpdateForm)
        {
            ResponseBannerUploadObject sResp = new ResponseBannerUploadObject();
            Boolean isNochanges = false;

            try
            {
                BannerModel sUpdateObj = new BannerModel();
                sUpdateObj.Description = sUpdateForm.description;

                DateTime dtStart = DateTime.ParseExact(sUpdateForm.startDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = DateTime.ParseExact(sUpdateForm.endDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                sUpdateObj.StartDate = dtStart;
                sUpdateObj.EndDate = dtEnd;
                sUpdateObj.IsActive = sUpdateForm.status;
                sUpdateObj.UpdatedBy = sUpdateForm.uploadedBy;

                if (BannerRepository.UpdateBannerInfo(ConfigSettings.GetConfigurationSettings(), bannerID, sUpdateObj, out isNochanges))
                {
                    sResp.StatusCode = (int)StatusCodes.Status200OK;
                    sResp.isNoChanges = false;
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    if (isNochanges)
                    {
                        sResp.isNoChanges = true;
                    }
                }
            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                sResp.isNoChanges = false;
            }

            return Json(sResp);
        }

        [Route("/Customer/Blogs")]
        public ActionResult Blogs()
        {
            return View("BlogsList");
        }

        [Route("/Customer/BlogProfile")]
        public ActionResult BlogProfile()
        {
            return View("BlogProfile");
        }

        [Route("/Customer/GetBlogsViewList")]
        [HttpGet()]
        public IActionResult GetBlogsViewList(int pageSize, int pageIndex)
        {
            int iTotalRecords = 0;

            try
            {
                var sBlogObject = BannerRepository.GetBlogsViewList(ConfigSettings.GetConfigurationSettings(), pageSize, pageIndex, out iTotalRecords);
                if (sBlogObject != null)
                {
                    return Json(new { data = sBlogObject, totalRecords = iTotalRecords });
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

        [Route("/Customer/UploadBlog")]
        [HttpPost()]
        public IActionResult UploadBlog()
        {
            ResponseBlogUploadObject sResp = new ResponseBlogUploadObject();

            String wwwrootPath = _webHosting.WebRootPath;
            String sDomainHost = HttpContext.Request.Host.Value;
            String sScheme = HttpContext.Request.Scheme;
            String sThumbnailFolder = "/upload/thumbnail/";
            String sThumbnailUrlPath = sScheme + "://" + sDomainHost + sThumbnailFolder;

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile postedFile = Request.Form.Files[0];

                    String sUploadFolderPath = Path.Combine(wwwrootPath + sThumbnailFolder);
                    String sDestinationPath = Path.Combine(sUploadFolderPath, postedFile.FileName);

                    if (!Directory.Exists(sUploadFolderPath))
                    {
                        Directory.CreateDirectory(sUploadFolderPath);
                    }

                    using (Stream fs = new FileStream(sDestinationPath, FileMode.Create))
                    {
                        postedFile.CopyTo(fs);
                    }

                    if (System.IO.File.Exists(sDestinationPath))
                    {
                        BlogUploadForm sUploadBlog = new BlogUploadForm();

                        String strFormData = Request.Form["Data"];
                        if (strFormData != null)
                        {
                            sUploadBlog = Newtonsoft.Json.JsonConvert.DeserializeObject<BlogUploadForm>(strFormData);
                        }

                        DateTime dtStart = DateTime.ParseExact(sUploadBlog.startDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime dtEnd = DateTime.ParseExact(sUploadBlog.endDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                        BlogModel sBlogObj = new BlogModel();
                        sBlogObj.Title = sUploadBlog.title;
                        sBlogObj.Description = sUploadBlog.description;
                        sBlogObj.URLtoRedirect = sUploadBlog.urlLink;
                        sBlogObj.StartDate = dtStart;
                        sBlogObj.EndDate = dtEnd;
                        sBlogObj.IsActive = sUploadBlog.status;
                        sBlogObj.ThumbnailImage = postedFile.FileName;
                        sBlogObj.ThumbnailFilePath = sThumbnailUrlPath + postedFile.FileName;
                        sBlogObj.CreatedDate = DateTime.Now;
                        sBlogObj.CreatedBy = sUploadBlog.uploadedBy;

                        var sResult = BannerRepository.InsertBlogs(ConfigSettings.GetConfigurationSettings(), sBlogObj);
                        if (sResult)
                        {
                            sResp.StatusCode = (int)StatusCodes.Status200OK;
                            sResp.isThumbnailSelected = true;
                            sResp.isThumbnailUploadSuccess = true;

                        }
                        else
                        {
                            sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                            sResp.isThumbnailSelected = true;
                            sResp.isThumbnailUploadSuccess = true;
                        }
                    }
                    else
                    {
                        sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                        sResp.isThumbnailSelected = true;
                        sResp.isThumbnailUploadSuccess = false;
                    }

                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    sResp.isThumbnailSelected = false;
                    sResp.isThumbnailUploadSuccess = false;
                }

            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
            }

            return Json(sResp);
        }

        [Route("/Customer/ViewBlog/{blogID}/{ViewType}")]
        public IActionResult ViewBlogProfile(int blogID, String ViewType)
        {
            BlogDisplayModel sBlogObject = new BlogDisplayModel();
            sBlogObject = BannerRepository.GetBlogByID(ConfigSettings.GetConfigurationSettings(), blogID);

            ViewData["BlogProfile"] = sBlogObject;
            ViewData["ViewType"] = ViewType;

            return View("BlogProfile", sBlogObject);
        }

        [Route("/Customer/UpdateBlogInfo")]
        public IActionResult UpdateBlogInfo(int blogID, BlogUploadForm sUpdateForm)
        {
            ResponseBlogUploadObject sResp = new ResponseBlogUploadObject();
            Boolean isNochanges = false;

            try
            {
                BlogModel sUpdateObj = new BlogModel();
                sUpdateObj.Description = sUpdateForm.description;
                sUpdateObj.Title = sUpdateForm.title;
                sUpdateObj.URLtoRedirect = sUpdateForm.urlLink;

                DateTime dtStart = DateTime.ParseExact(sUpdateForm.startDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtEnd = DateTime.ParseExact(sUpdateForm.endDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                sUpdateObj.StartDate = dtStart;
                sUpdateObj.EndDate = dtEnd;
                sUpdateObj.IsActive = sUpdateForm.status;
                sUpdateObj.UpdatedBy = sUpdateForm.uploadedBy;

                if (BannerRepository.UpdateBlogInfo(ConfigSettings.GetConfigurationSettings(), blogID, sUpdateObj, out isNochanges))
                {
                    sResp.StatusCode = (int)StatusCodes.Status200OK;
                    sResp.isNoChanges = false;
                }
                else
                {
                    sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                    if (isNochanges)
                    {
                        sResp.isNoChanges = true;
                    }
                }
            }
            catch (Exception ex)
            {
                sResp.StatusCode = (int)StatusCodes.Status400BadRequest;
                sResp.isNoChanges = false;
            }

            return Json(sResp);
        }
    }
}
