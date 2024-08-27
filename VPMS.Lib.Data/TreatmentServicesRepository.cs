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
                return null;
            }
        }

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
                return null;
            }
        }
    }
}
