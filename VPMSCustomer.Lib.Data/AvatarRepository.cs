using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class AvatarRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Profile avatar list by Group & Sub Group
        /// </summary>
        /// <param name="sGroup"></param>
        /// <param name="sSubGroup"></param>
        /// <returns></returns>
        public static List<AvatarModel> GetProfileAvatarList(String sGroup, String sSubGroup)
        {
            try
            {
                using (var ctx = new AvatarDBContext())
                {
                    return ctx.mst_profile_avatar.Where(x => x.EntityGroup == sGroup &&
                                                             x.EntitySubGroup == sSubGroup &&
                                                             x.Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("AvatarRepository >>> GetProfileAvatarList >>> " + ex.ToString());
                return null;
            }

        }
    }
}
