using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class DoctorRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Doctor List by Branch ID
        /// </summary>
        /// <param name="branchid"></param>
        /// <returns></returns>
        public static List<DoctorModel> GetDoctorList(int branchid)
        {
            try
            {
                using (var ctx = new DoctorDBContext())
                {
                    return ctx.Mst_Doctor.Where(x => x.BranchID == branchid && x.IsDeleted == 0).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error("DoctorRepository >>> GetDoctorList >>> " + ex.ToString());
                return null;
            }
        }
    }
}
