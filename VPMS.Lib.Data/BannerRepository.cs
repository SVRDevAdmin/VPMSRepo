using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace VPMS.Lib.Data
{
    public class BannerRepository
    {
        public static List<BannerViewModel> GetBannersViewList(IConfiguration config, int pageSize, int pageIndex, out int totalRecords)
        {
            List<BannerViewModel> sResultList = new List<BannerViewModel>();
            totalRecords = 0;

            DateTime sNow = DateTime.Now;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', Count(*) OVER() as 'TotalRows', B.* " + 
                                            "FROM mst_banners As B " + 
                                            "WHERE ( " +
                                            "B.StartDate <= '" + sNow.ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                                            "B.EndDate >= '" + sNow.ToString("yyyy-MM-dd HH:mm:ss")  + "' " +
                                            ") " +
                                            "ORDER BY B.IsActive DESC, B.SeqOrder ASC " +
                                            "LIMIT " + pageSize + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                BannerViewModel sBannerObj = new BannerViewModel();
                                sBannerObj.RowNo = Convert.ToInt32(sReader["row_num"]);
                                sBannerObj.ID = Convert.ToInt32(sReader["ID"]);
                                sBannerObj.BannerType = Convert.ToInt32(sReader["BannerType"]);
                                sBannerObj.Description = sReader["Description"].ToString();
                                sBannerObj.SeqOrder = Convert.ToInt32(sReader["SeqOrder"]);
                                sBannerObj.IsActive = Convert.ToInt32(sReader["IsActive"]);
                                sBannerObj.StartDate = Convert.ToDateTime(sReader["StartDate"]);
                                sBannerObj.EndDate = Convert.ToDateTime(sReader["EndDate"]);
                                sBannerObj.BannerName = sReader["BannerName"].ToString();
                                sBannerObj.BannerFilePath = sReader["BannerFilePath"].ToString();
                                sBannerObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sBannerObj.CreatedBy = sReader["CreatedBy"].ToString();

                                sResultList.Add(sBannerObj);

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean InsertBanners(IConfiguration config, BannerModel sInputObject)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    int sBannerObj = ctx.mst_banners.Where(x => x.IsActive == 1).Max(x => x.SeqOrder).Value;

                    sInputObject.SeqOrder = (sBannerObj != null) ? (sBannerObj + 1) : 1;

                    ctx.mst_banners.Add(sInputObject);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static BannerDisplayModel GetBannerByID(IConfiguration config, int iBannerID)
        {
            BannerDisplayModel sBannerProfile = new BannerDisplayModel();

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ID, A.BannerType, A.Description, A.SeqOrder, A.IsActive," +
                                            "A.StartDate, A.EndDate, A.BannerName, A.BannerFilePath, " +
                                            "A.CreatedDate, A.CreatedBy, A.UpdatedDate, A.UpdatedBy " +
                                            "FROM mst_banners AS A " +
                                            "WHERE ID = '" + iBannerID  + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sBannerProfile.ID = Convert.ToInt32(sReader["ID"]);
                                sBannerProfile.BannerType = Convert.ToInt32(sReader["BannerType"]);
                                sBannerProfile.Description = sReader["Description"].ToString();
                                sBannerProfile.SeqOrder = Convert.ToInt32(sReader["SeqOrder"]);
                                sBannerProfile.IsActive = Convert.ToInt32(sReader["IsActive"]);
                                sBannerProfile.StartDate = Convert.ToDateTime(sReader["StartDate"]);
                                sBannerProfile.EndDate = Convert.ToDateTime(sReader["EndDate"]);
                                sBannerProfile.StartDateString = Convert.ToDateTime(sReader["StartDate"]).ToString("dd/MM/yyyy");
                                sBannerProfile.EndDateString = Convert.ToDateTime(sReader["EndDate"]).ToString("dd/MM/yyyy");
                                sBannerProfile.BannerName = sReader["BannerName"].ToString();
                                sBannerProfile.BannerFilePath = sReader["BannerFilePath"].ToString();
                                sBannerProfile.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sBannerProfile.CreatedBy = sReader["CreatedBy"].ToString();

                                if (sReader["UpdatedDate"] != null && sReader["UpdatedDate"].ToString() != "")
                                {
                                    sBannerProfile.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }

                                if (sReader["UpdatedBy"] != null && sReader["UpdatedBy"].ToString() != "")
                                {
                                    sBannerProfile.UpdatedBy = sReader["UpdatedBy"].ToString();
                                }
                                
                            }
                        }
                    }

                    sConn.Close();
                }

                return sBannerProfile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean UpdateBannerInfo(IConfiguration config, int iBannerID, BannerModel sBannerProfile, out Boolean isNochanges)
        {
            Boolean isUpdated = false;
            isNochanges = false;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    Boolean isChanges = false;

                    var sBannerObj = ctx.mst_banners.Where(x => x.ID == iBannerID).FirstOrDefault();
                    if (sBannerObj != null)
                    {
                        if (sBannerObj.Description != sBannerProfile.Description)
                        {
                            sBannerObj.Description = sBannerProfile.Description;
                            isChanges = true;
                        }

                        if (sBannerObj.StartDate != sBannerProfile.StartDate)
                        {
                            sBannerObj.StartDate = sBannerProfile.StartDate;
                            isChanges = true;
                        }

                        if (sBannerObj.EndDate != sBannerProfile.EndDate)
                        {
                            sBannerObj.EndDate = sBannerProfile.EndDate;
                            isChanges = true;
                        }

                        if (sBannerObj.IsActive != sBannerProfile.IsActive)
                        {
                            sBannerObj.IsActive = sBannerProfile.IsActive;
                            isChanges = true;
                        }

                        isNochanges = !isChanges;
                        if (isChanges)
                        {
                            sBannerObj.UpdatedDate = DateTime.Now;
                            sBannerObj.UpdatedBy = sBannerProfile.UpdatedBy;

                            ctx.SaveChanges();

                            isUpdated = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isUpdated = false;
            }

            return isUpdated;
        }

        public static List<BlogViewModel> GetBlogsViewList(IConfiguration config, int pageSize, int pageIndex, out int totalRecords)
        {
            List<BlogViewModel> sBlogList = new List<BlogViewModel>();
            totalRecords = 0;

            DateTime sNow = DateTime.Now;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', Count(*) OVER() as 'TotalRows', A.* " +
                                            "FROM mst_blogs AS A " + 
                                            "WHERE ( " +
                                            "(A.StartDate <= '" + sNow.ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                                            "A.EndDate >= '" + sNow.ToString("yyyy-MM-dd HH:mm:ss") + "') " + 
                                            "OR " +
                                            "(A.StartDate >= '" + sNow.ToString("yyyy-MM-dd HH:mm:ss") + "') " +
                                            ") " +
                                            "ORDER BY A.IsActive DESC, A.SeqOrder ASC " +
                                            "LIMIT " + pageSize + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                BlogViewModel blogObject = new BlogViewModel();
                                blogObject.RowNo = Convert.ToInt32(sReader["row_num"]);
                                blogObject.ID = Convert.ToInt32(sReader["ID"]);
                                blogObject.Title = sReader["Title"].ToString();
                                blogObject.Description = sReader["Description"].ToString();
                                blogObject.URLtoRedirect = sReader["URLtoRedirect"].ToString();
                                blogObject.SeqOrder = Convert.ToInt32(sReader["SeqOrder"]);
                                blogObject.IsActive = Convert.ToInt32(sReader["IsActive"]);
                                blogObject.StartDate = Convert.ToDateTime(sReader["StartDate"]);
                                blogObject.EndDate = Convert.ToDateTime(sReader["EndDate"]);
                                blogObject.ThumbnailImage = sReader["ThumbnailImage"].ToString();
                                blogObject.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                blogObject.CreatedBy = sReader["CreatedBy"].ToString();

                                if (sReader["UpdatedDate"] != null && sReader["UpdatedDate"].ToString() != "")
                                {
                                    blogObject.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }
                                blogObject.UpdatedBy = sReader["UpdatedBy"].ToString();

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);

                                sBlogList.Add(blogObject);
                            }
                        }
                    }
                    

                    sConn.Close();
                }

                return sBlogList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean InsertBlogs(IConfiguration config, BlogModel sInputObject)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    int sBlogObj = ctx.mst_blogs.Where(x => x.IsActive == 1).Max(x => x.SeqOrder).Value;

                    sInputObject.SeqOrder = (sBlogObj != null) ? (sBlogObj + 1) : 1;

                    ctx.mst_blogs.Add(sInputObject);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static BlogDisplayModel GetBlogByID(IConfiguration config, int iBlogID)
        {
            BlogDisplayModel sBlogObj = new BlogDisplayModel();

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT B.ID, B.Title, B.Description, B.URLtoRedirect, B.SeqOrder, B.IsActive, " +
                                            "B.StartDate, B.EndDate, B.ThumbnailImage, B.ThumbnailFilePath, B.CreatedDate, " +
                                            "B.CreatedBy, B.UpdatedDate, B.UpdatedBy " +
                                            "FROM mst_blogs AS B " +
                                            "WHERE B.ID = '" + iBlogID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sBlogObj.ID = Convert.ToInt32(sReader["ID"]);
                                sBlogObj.Title = sReader["Title"].ToString();
                                sBlogObj.Description = sReader["Description"].ToString();
                                sBlogObj.URLtoRedirect = sReader["URLtoRedirect"].ToString();
                                sBlogObj.SeqOrder = Convert.ToInt32(sReader["SeqOrder"]);
                                sBlogObj.IsActive = Convert.ToInt32(sReader["IsActive"]);
                                sBlogObj.StartDate = Convert.ToDateTime(sReader["StartDate"]);
                                sBlogObj.EndDate = Convert.ToDateTime(sReader["EndDate"]);
                                sBlogObj.StartDateString = Convert.ToDateTime(sReader["StartDate"]).ToString("dd/MM/yyyy");
                                sBlogObj.EndDateString = Convert.ToDateTime(sReader["EndDate"]).ToString("dd/MM/yyyy");
                                sBlogObj.ThumbnailImage = sReader["ThumbnailImage"].ToString();
                                sBlogObj.ThumbnailFilePath = sReader["ThumbnailFilePath"].ToString();
                                sBlogObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sBlogObj.CreatedBy = sReader["CreatedBy"].ToString();

                                if (sReader["UpdatedDate"] != null && sReader["UpdatedDate"].ToString() != "")
                                {
                                    sBlogObj.UpdatedDate = Convert.ToDateTime(sReader["UpdatedDate"]);
                                }

                                sBlogObj.UpdatedBy = sReader["UpdatedBy"].ToString();
                            }
                        }
                    }

                    sConn.Close();
                }

                return sBlogObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static Boolean UpdateBlogInfo(IConfiguration config, int iBlogID, BlogModel sBlogProfile, out Boolean isNochanges)
        {
            Boolean isUpdated = false;
            isNochanges = false;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    Boolean isChanges = false;

                    var sBlogObj = ctx.mst_blogs.Where(x => x.ID == iBlogID).FirstOrDefault();
                    if (sBlogObj != null)
                    {
                        if (sBlogObj.Title != sBlogProfile.Title)
                        {
                            sBlogObj.Title = sBlogProfile.Title;
                            isChanges = true;
                        }

                        if (sBlogObj.Description != sBlogProfile.Description)
                        {
                            sBlogObj.Description = sBlogProfile.Description;
                            isChanges = true;
                        }

                        if (sBlogObj.URLtoRedirect != sBlogProfile.URLtoRedirect)
                        {
                            sBlogObj.URLtoRedirect = sBlogProfile.URLtoRedirect;
                            isChanges = true;
                        }

                        if (sBlogObj.IsActive != sBlogProfile.IsActive)
                        {
                            sBlogObj.IsActive = sBlogProfile.IsActive;
                            isChanges = true;
                        }

                        if (sBlogObj.StartDate != sBlogProfile.StartDate)
                        {
                            sBlogObj.StartDate = sBlogProfile.StartDate;
                            isChanges = true;
                        }

                        if (sBlogObj.EndDate != sBlogProfile.EndDate)
                        {
                            sBlogObj.EndDate = sBlogProfile.EndDate;
                            isChanges = true;
                        }

                        isNochanges = !isChanges;
                        if (isChanges)
                        {
                            sBlogObj.UpdatedDate = DateTime.Now;
                            sBlogObj.UpdatedBy = sBlogProfile.UpdatedBy;

                            ctx.SaveChanges();

                            isUpdated = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isUpdated = false;
            }

            return isUpdated;
        }
    }
}
