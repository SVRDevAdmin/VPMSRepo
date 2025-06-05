using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class BannerRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get the Banners List
        /// </summary>
        /// <returns></returns>
        public static List<BannerModel> GetDashboardBannersList()
        {
            DateTime sNow = DateTime.Now;

            try
            {
                using (var ctx = new BannerDBContext())
                {
                    return ctx.mst_banners.Where(x => x.IsActive == 1 && (x.StartDate <= sNow && x.EndDate >= sNow)).OrderBy(x => x.SeqOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("BannerRepository >>> GetDashboardBannersList >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Blogs List
        /// </summary>
        /// <returns></returns>
        public static List<BlogModel> GetBlogsList()
        {
            DateTime sNow = DateTime.Now;

            try
            {
                using (var ctx = new BannerDBContext())
                {
                    return ctx.mst_blogs.Where(x => x.IsActive == 1 && (x.StartDate <= sNow && x.EndDate >= sNow)).OrderBy(x => x.SeqOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("BannerRepository >>> GetBlogsList >>> " + ex.ToString());
                return null;
            }
        }
    }
}
