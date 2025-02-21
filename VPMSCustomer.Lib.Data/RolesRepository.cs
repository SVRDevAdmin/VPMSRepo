using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.DBContext;

namespace VPMSCustomer.Lib.Data
{
    public class RolesRepository
    {
        public static String GetRoleIDByName(String roleName)
        {
            try
            {
                using (var ctx = new RoleDBContext())
                {
                    return ctx.mst_roles.Where(x => x.RoleName.ToLower() == roleName.ToLower() && 
                                                    x.Status == 1)
                                        .FirstOrDefault()
                                        .RoleID;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
