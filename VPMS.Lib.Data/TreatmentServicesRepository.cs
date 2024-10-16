using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;

namespace VPMS.Lib.Data
{
    public class TreatmentServicesRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Treatment Services list
		/// </summary>
		/// <param name="config"></param>
		/// <param name="branchID"></param>
		/// <returns></returns>
		public static List<TreatmentServicesModel> GetTreatmentServicesList(IConfiguration config, int branchID)
        {
            try
            {
                using (var ctx = new TreatmentServicesDBContext(config))
                {
                    return ctx.mst_services.Where(x => x.BranchID == branchID && x.Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
				logger.Error("Database Error >> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Doctor available
        /// </summary>
        /// <param name="config"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public static List<TreatmentServicesModel> GetServicesDoctorList(IConfiguration config, int serviceID)
        {
            try
            {
                using (var ctx = new TreatmentServicesDBContext(config))
                {
                    return ctx.mst_services.Where(x => x.ID == serviceID && x.Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
				logger.Error("Database Error >> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Treatment Service Info by ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public static TreatmentServicesModel GetServicesInfoByID(IConfiguration config, long serviceID)
        {
            try
            {
                using (var ctx = new TreatmentServicesDBContext(config))
                {
                    return ctx.mst_services.Where(x => x.ID == serviceID && x.Status == 1).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
				logger.Error("Database Error >> ", ex);
				return null;
            }
        }
    }
}
