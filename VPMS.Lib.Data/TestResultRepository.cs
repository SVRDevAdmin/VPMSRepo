using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

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
                    //ctx.Txn_TestResults.AddRange(sResults);
                    //ctx.SaveChanges();

                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// List of Device Name
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static List<String> GetTestResultDeviceNameList(IConfiguration config, int BranchID)
        {
            try
            {
                using (var ctx = new TestResultsDBContext(config))
                {
                    var sResult = ctx.Txn_TestResults.Where(x => x.BranchID == BranchID)
                                                     .OrderBy(x => x.DeviceName)
                                                     .Select(x => x.DeviceName)
                                                     .Distinct();

                    return sResult.OrderBy(x => x).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Test Result Listing View
        /// </summary>
        /// <param name="config"></param>
        /// <param name="branchID"></param>
        /// <param name="sPatientID"></param>
        /// <param name="sDeviceName"></param>
        /// <param name="sSortOrder"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static List<TestResultViewModel> GetTestResultManagementListing(IConfiguration config, int branchID, String sPatientID, String sDeviceName, String sSortOrder, int PageSize, int PageIndex, out int totalRecords)
        {
            List<TestResultViewModel> sResult = new List<TestResultViewModel>();
            totalRecords = 0;

            if (String.IsNullOrEmpty(sSortOrder))
            {
                sSortOrder = "ASC";
            }

            try
            {
                using (var ctx = new TestResultsDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER (ORDER BY A.ResultDateTime " + sSortOrder + ") AS 'No', " +
                                            "A.ID as 'ResultID', A.ResultDateTime, A.ResultCategories, A.ResultType, A.OverallStatus, " +
                                            "A.PatientID, A.InchargeDoctor, A.OperatorID, " +
                                            "A.DeviceName, COUNT(*) OVER() AS 'TotalRows' " + 
                                            "FROM txn_testresults AS A " +
                                            "WHERE A.BranchID = '" + branchID  + "' AND " +
                                            "(" + (sPatientID == null) + " OR A.PatientID = '" + sPatientID + "') AND " +
                                            "(" + (sDeviceName == null) + " OR A.DeviceName = '" + sDeviceName + "') " +
                                            "ORDER BY A.ResultDateTime " + sSortOrder + " " +
                                            "LIMIT " + PageSize + " " +
                                            "OFFSET " + ((PageIndex - 1) * PageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                TestResultViewModel sTestResultObj = new TestResultViewModel();
                                sTestResultObj.ResultID = Convert.ToInt32(sReader["ResultID"]);
                                sTestResultObj.SeqNo = Convert.ToInt32(sReader["No"]);
                                sTestResultObj.ResultDateTime = Convert.ToDateTime(sReader["ResultDateTime"]);
                                sTestResultObj.ResultCategories = sReader["ResultCategories"].ToString();
                                sTestResultObj.ResultType = sReader["ResultType"].ToString();
                                sTestResultObj.InchargeDoctor = sReader["InchargeDoctor"].ToString();
                                sTestResultObj.OperatorID = sReader["OperatorID"].ToString();
                                sTestResultObj.DeviceName = sReader["DeviceName"].ToString();
                                sTestResultObj.OverallStatus = sReader["OverallStatus"].ToString();

                                sResult.Add(sTestResultObj);

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                            }
                        }
                    }

                    sConn.Close();

                    return sResult;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get selected Test result details
        /// </summary>
        /// <param name="config"></param>
        /// <param name="resultID"></param>
        /// <returns></returns>
        public static TestResultDetailModel GetTestResultBreakdownDetails(IConfiguration config, int resultID)
        {
            List<TestResultBreakdownModel> sDetailList = new List<TestResultBreakdownModel>();
            TestResultDetailModel sResult = new TestResultDetailModel();

            try
            {
                using (var ctx = new TestResultsDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ID, A.ResultDateTime, A.ResultCategories, A.ResultType, " +
                                            "A.DeviceName, A.PatientID, B.Name AS 'PatientName', A.OperatorID " + 
                                            "FROM txn_testresults AS A " +
                                            "LEFT JOIN mst_pets AS B ON B.ID = A.PetID " +
                                            "WHERE A.ID = '" + resultID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sResult.ResultID = Convert.ToInt32(sReader["ID"]);
                                sResult.ResultDateTime = Convert.ToDateTime(sReader["ResultDateTime"]);
                                sResult.ResultCategories = sReader["ResultCategories"].ToString();
                                sResult.ResultType = sReader["ResultType"].ToString();
                                sResult.DeviceName = sReader["DeviceName"].ToString();
                                sResult.PatientID = sReader["PatientID"].ToString();
                                sResult.PatientName = sReader["PatientName"].ToString();
                                sResult.OperatorID = sReader["OperatorID"].ToString();
                            }
                        }
                    }

                    if (sResult.ResultID != null)
                    {
                        String sSelectDetailCommand = "SELECT ResultParameter, ResultStatus, ResultValue, ResultUnit, ReferenceRange " +
                                                      "FROM txn_testresults_details " +
                                                      "WHERE ResultID = '" + resultID + "' " +
                                                      "ORDER BY ResultSeqID";

                        using (MySqlCommand sDetailCommand = new MySqlCommand(sSelectDetailCommand, sConn))
                        {
                            using (var sDetailReader = sDetailCommand.ExecuteReader())
                            {
                                while (sDetailReader.Read())
                                {
                                    sDetailList.Add(new TestResultBreakdownModel
                                    {
                                        ResultParameter = sDetailReader["ResultParameter"].ToString(),
                                        ResultStatus = sDetailReader["ResultStatus"].ToString(),
                                        ResultValue= sDetailReader["ResultValue"].ToString(),
                                        ResultUnit = sDetailReader["ResultUnit"].ToString(),
                                        ReferenceRange = sDetailReader["ReferenceRange"].ToString()
                                    });
                                }
                            }
                        }
                    }

                    if (sDetailList.Count > 0)
                    {
                        sResult.TestResultBreakdown = sDetailList;
                    }

                    sConn.Close();
                }
            }
            catch (Exception ex)
            {
                return null; 
            }

            return sResult;
        }
    }
}
