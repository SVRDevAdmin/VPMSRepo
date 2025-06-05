using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class OrganizationRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Organization List
		/// </summary>
		/// <param name="iLevel"></param>
		/// <returns></returns>
		public static List<OrganisationModel> GetOrganizationList(int iLevel)
        {
            try
            {
                using (var ctx = new OrganisationDBContext())
                {
                    return ctx.Mst_Organisation.Where(x => x.Level == iLevel && x.Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("OrganizationRepository >>> GetOrganizationList >>> ", ex);
                return null;
            }
        }

        /// <summary>
        /// Get Organization Profile by ID
        /// </summary>
        /// <param name="iOrganizationID"></param>
        /// <returns></returns>
        public static OrganisationModel GetOrganizationByID(int iOrganizationID)
        {
            try
            {
                using (var ctx = new OrganisationDBContext())
                {
                    return ctx.Mst_Organisation.Where(x => x.Id == iOrganizationID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("OrganizationRepository >>> GetOrganizationByID >>> ", ex);
                return null;
            }
        }
    }
}
