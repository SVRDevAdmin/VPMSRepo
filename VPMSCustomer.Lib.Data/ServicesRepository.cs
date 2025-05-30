using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class ServicesRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Services List By Branch ID
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public static List<ServiceModel> GetServicesListByBranchID(int branchID)
        {
            try
            {
                using (var ctx = new ServiceDBContext())
                {
                    return ctx.mst_services.Where(x => x.BranchID == branchID && x.Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ServicesRepository >>> GetServicesListByBranchID >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Service Details by Service ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public static ServiceModel GetServiceDetailsByID(long serviceID)
        {
            try
            {
                using (var ctx = new ServiceDBContext())
                {
                    return ctx.mst_services.Where(x => x.ID == serviceID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ServicesRepository >>> GetServiceDetailsByID >>> " + ex.ToString());
                return null;
            }
        }
    }
}
