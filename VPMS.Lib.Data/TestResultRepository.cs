using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class TestResultRepository
    {
        /// <summary>
        /// Add Test Results record
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sResults"></param>
        /// <returns></returns>
        public static Boolean InsertTestResults(IConfiguration config, List<TestResultModel> sResults)
        {
            Boolean isValid = false;

            try
            {
                using (var ctx = new TestResultsDBContext(config))
                {
                    ctx.Txn_TestResults.AddRange(sResults);
                    ctx.SaveChanges();

                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
