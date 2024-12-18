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
                return null;
            }
        }
    }
}
