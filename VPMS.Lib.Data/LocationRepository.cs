using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class LocationRepository
    {
        public static Boolean InsertLocationList(IConfiguration config, LocationModel sLocationObject)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new LocationDBContext(config))
                {
                    ctx.mst_locationlist.Add(sLocationObject);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static Boolean UpdateLocationList(IConfiguration config, String sLocationID, String sLocationName, int sLocationStatus, String sUpdatedBy)
        {
            Boolean isSuccess = true;

            try
            {
                using (var ctx = new LocationDBContext(config))
                {
                    var sResult = ctx.mst_locationlist.Where(x => x.System_LocationID == sLocationID).FirstOrDefault();
                    if (sResult != null)
                    {
                        sResult.System_LocationID = sLocationID;
                        sResult.System_LocationName = sLocationName;
                        sResult.System_Status = sLocationStatus;
                        sResult.UpdatedDate = DateTime.Now;
                        sResult.UpdatedBy = sUpdatedBy;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static LocationModel GetLocationListMasterByID(IConfiguration config, String systemID)
        {
            try
            {
                using (var ctx = new LocationDBContext(config))
                {
                    return ctx.mst_locationlist.Where(x => x.System_LocationID == systemID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<LocationModel> GetLocationListMaster(IConfiguration config)
        {
            try
            {
                using (var ctx = new LocationDBContext(config))
                {
                    return ctx.mst_locationlist.Where(x => x.System_Status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
