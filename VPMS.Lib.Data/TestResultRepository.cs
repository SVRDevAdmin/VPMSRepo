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

        public static List<String> GetTestResultDeviceNameList(IConfiguration config)
        {
            try
            {
                using (var ctx = new TestResultsDBContext(config))
                {
                    var sResult = ctx.Txn_TestResults.OrderBy(x => x.DeviceName)
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

        public static List<TestResultViewModel> GetTestResultManagementListing(IConfiguration config, String sPatientID, String sDeviceName, String sSortOrder, 
                                                                            int PageSize, int PageIndex, out int totalRecords)
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
                                            "A.ID as 'ResultID', A.ResultDateTime, A.ResultCategories, A.ResultType, B.ResultStatus, " +
                                            "B.ResultValue, B.ResultUnit, B.ReferenceRange, A.PatientID, A.InchargeDoctor, A.OperatorID, " +
                                            "A.DeviceName, COUNT(*) OVER() AS 'TotalRows' " + 
                                            "FROM txn_testresults AS A " +
                                            "INNER JOIN txn_testresults_details AS B ON B.ResultID = A.ID " +
                                            "WHERE (" + (sPatientID == null) + " OR A.PatientID = '" + sPatientID + "') AND " +
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
                                sTestResultObj.ResultStatus = sReader["ResultStatus"].ToString();
                                sTestResultObj.ResultValue = sReader["ResultValue"].ToString();
                                sTestResultObj.ResultUnit = sReader["ResultUnit"].ToString();
                                sTestResultObj.ReferenceRange = sReader["ReferenceRange"].ToString();
                                sTestResultObj.InchargeDoctor = sReader["InchargeDoctor"].ToString();
                                sTestResultObj.OperatorID = sReader["OperatorID"].ToString();
                                sTestResultObj.DeviceName = sReader["DeviceName"].ToString();

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
    }
}
