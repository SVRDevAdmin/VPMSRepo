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
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Insert new tests 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sTestObject"></param>
        /// <returns></returns>
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
                logger.Error("TestsListRepository >>> InsertTestsList >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update Tests List
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sTestID"></param>
        /// <param name="sTestName"></param>
        /// <param name="sTestDesc"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
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
                logger.Error("TestsListRepository >>> UpdateTestsList >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Get Tests Info by ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="systemID"></param>
        /// <returns></returns>
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
                logger.Error("TestsListRepository >>> GetTestsListMasterByID >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get Tests Master Listing
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
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
                logger.Error("TestsListRepository >>> GetTestsListMaster >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Insert Scheduled Test 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sSubmissionObj"></param>
        /// <param name="iID"></param>
        /// <returns></returns>
        public static Boolean InsertScheduledTestSubmission(IConfiguration config, scheduledTestsSubmission sSubmissionObj, out long iID)
        {
            Boolean isSuccess = false;
            iID = 0;

            try
            {
                using (var ctx = new TestsListDBContext(config))
                {
                    ctx.txn_scheduledtests_submission.Add(sSubmissionObj);
                    ctx.SaveChanges();

                    iID = sSubmissionObj.ID;

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error("TestsListRepository >>> InsertScheduledTestSubmission >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Get Scheduled Test Info by ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="iSubmissionID"></param>
        /// <returns></returns>
        public static scheduledTestsSubmission GetScheduledTestSubmission(IConfiguration config, long iSubmissionID)
        {
            try
            {
                using (var ctx = new TestsListDBContext(config))
                {
                    return ctx.txn_scheduledtests_submission.Where(x => x.ID == iSubmissionID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("TestsListRepository >>> GetScheduledTestSubmission >>> " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Update Scheduled Test Info
        /// </summary>
        /// <param name="config"></param>
        /// <param name="iSubmissionID"></param>
        /// <param name="sentStatus"></param>
        /// <param name="sentDate"></param>
        /// <param name="respStatus"></param>
        /// <param name="sUpdatedBy"></param>
        /// <returns></returns>
        public static Boolean UpdateScheduledTestSubmissionStatus(IConfiguration config, long iSubmissionID, int sentStatus, DateTime sentDate, String respStatus, String sUpdatedBy)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TestsListDBContext(config))
                {
                    var sSubmissionObj = ctx.txn_scheduledtests_submission.Where(x => x.ID == iSubmissionID).FirstOrDefault();
                    if (sSubmissionObj != null)
                    {
                        sSubmissionObj.Status = sentStatus;
                        sSubmissionObj.ResponseStatus = respStatus;
                        sSubmissionObj.SubmissionSent = sentDate;
                        sSubmissionObj.UpdatedDate = DateTime.Now;
                        sSubmissionObj.UpdatedBy = sUpdatedBy;

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("TestsListRepository >>> UpdateScheduledTestSubmissionStatus >>> " + ex.ToString());
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
