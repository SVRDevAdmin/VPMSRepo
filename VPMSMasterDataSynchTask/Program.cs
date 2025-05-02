using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Globalization;
using System.Reflection;
using VPMS.Interface.API;
using VPMS.Interface.API.VCheck.RequestMessage;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
//using VPMS.Interface.API;
//using VPMS.Interface.API.VCheck;
//using VPMS.Interface.API.VCheck.RequestMessage;

namespace VPMSMasterDataSynchTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DateTime executionDate = DateTime.Now.AddDays(-1);
            //DateTime startDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 0, 0, 0);
            //DateTime endDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 23, 59, 59);

            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {


                processMasterDataSynch();
                //if (args.Length > 0)
                //{
                //if (DateTime.TryParseExact(args[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                //{
                //PrintHelp();
                //    return;
                //}

                //sExecutionType = "Manual";

                //startDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 0, 0, 0);
                //endDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 23, 59, 59);
                //}

                //ExpensesSummaryUpdate(startDate, endDate);
            }
        }

        private static void processMasterDataSynch()
        {
            var sBuilder = new ConfigurationBuilder();
            sBuilder.SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = sBuilder.Build();

            GetMasterTestsList(config);
        }

        private static void GetMasterTestsList(IConfiguration config)
        {
            VCheckAPI sVCheckAPI = new VCheckAPI();

            try
            {
                GetTestListRequest sReq = new GetTestListRequest();
                RequestBodyObject sReqBody = new RequestBodyObject();
                RequestHeaderObject sReqHeader = new RequestHeaderObject();
                sReqHeader.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
                sReqHeader.clientkey = "";

                sReq.header = sReqHeader;
                sReq.body = sReqBody;

                var resp = sVCheckAPI.GetTestList(sReq);
                if (resp.body.responseCode == "VV.0001")
                {
                    if (resp.body.results.Count > 0)
                    {
                        foreach(var r in resp.body.results)
                        {
                            var sTestsListObj = TestsListRepository.GetTestsListMasterByID(config, r.testid);
                            if (sTestsListObj != null)
                            {
                                TestsListRepository.UpdateTestsList(config, r.testid, r.testname, r.testdescription, "");
                            }
                            else
                            {
                                TestsListModel sNewTestsList = new TestsListModel();
                                sNewTestsList.System_TestID = r.testid;
                                sNewTestsList.System_TestName = r.testname;
                                sNewTestsList.System_Description = r.testdescription;
                                sNewTestsList.IsActive = 1;
                                sNewTestsList.CreatedDate = DateTime.Now;
                                sNewTestsList.CreatedBy = "";

                                TestsListRepository.InsertTestsList(config, sNewTestsList);
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
