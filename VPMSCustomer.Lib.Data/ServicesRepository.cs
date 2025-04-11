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
                return null;
            }
        }
    }
}
