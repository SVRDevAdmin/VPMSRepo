using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class BranchRepository
    {
        /// <summary>
        /// Get Branch List By Organization ID
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public static List<BranchModel> GetBranchListByOrgID(int organizationID)
        {
            try
            {
                using (var ctx = new BranchDBContext())
                {
                    return ctx.Mst_Branch.Where(x => x.OrganizationID == organizationID && x.Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
