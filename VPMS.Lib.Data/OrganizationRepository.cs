using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
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
		/// Get Organization List  By Level
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
                logger.Error("OrganizationRepository >>> GetOrganizationList(int level) >>> ", ex);
                return null;
            }
        }

        /// <summary>
        /// Get Organization List
        /// </summary>
        /// <returns></returns>
        public static List<OrganisationModel> GetOrganizationList()
        {
            try
            {
                using (var ctx = new OrganisationDBContext())
                {
                    return ctx.Mst_Organisation.Where(x => x.Status == 1).ToList();
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

        /// <summary>
        /// Update Organization Total Staff Count
        /// </summary>
        /// <param name="iOrganizationID"></param>
        /// <param name="iTotal"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean UpdateOrganizationTotalStaff(int iOrganizationID, int iTotal, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new OrganisationDBContext())
                {
                    var organizationObj = ctx.Mst_Organisation.Where(x => x.Id == iOrganizationID).FirstOrDefault();
                    if (organizationObj != null)
                    {
                        organizationObj.TotalStaff = iTotal;
                        organizationObj.UpdatedDate = DateTime.Now;
                        organizationObj.UpdatedBy = sUpdatedBy;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("OrganizationRepository >>> UpdateOrganizationTotalStaff >>> ", ex);
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
