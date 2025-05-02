using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.DBContext;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace VPMS.Lib.Data
{
    public class TestsListRepository
    {
        public static Boolean InsertTestsList(IConfiguration config, TestsListModel sTestObject)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TestsListDBContext(config))
                {
                    ctx.mst_testslist.Add(sTestObject);
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

        public static Boolean UpdateTestsList(IConfiguration config, String sTestID, String sTestName, String sTestDesc, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TestsListDBContext(config))
                {
                    var sResult = ctx.mst_testslist.Where(x => x.System_TestID == sTestID).FirstOrDefault();
                    if (sResult != null)
                    {
                        sResult.System_TestName = sTestName;
                        sResult.System_Description = sTestDesc;
                        sResult.UpdatedDate = DateTime.Now;
                        sResult.UpdatedBy = sUpdatedBy;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                };
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static TestsListModel GetTestsListMasterByID(IConfiguration config, String systemID)
        {
            try
            {
                using (var ctx = new TestsListDBContext(config))
                {
                    return ctx.mst_testslist.Where(x => x.System_TestID == systemID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<TestsListModel> GetTestsListMaster(IConfiguration config)
        {
            try
            {
                using (var ctx = new TestsListDBContext(config))
                {
                    return ctx.mst_testslist.Where(x => x.IsActive == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
