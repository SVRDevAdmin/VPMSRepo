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
                return null;
            }
        }
    }
}
