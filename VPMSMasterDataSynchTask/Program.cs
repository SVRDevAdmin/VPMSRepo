using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Globalization;
using System.Reflection;
using VPMS.Interface.API;
using VPMS.Interface.API.VCheck.RequestMessage;
using VPMS.Lib.Data;
using VPMS.Lib.Data.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;
using log4net.Repository;

namespace VPMSMasterDataSynchTask
{
    public class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static String sProgramName = "VPMSMasterDataSyncTask";

        static void Main(string[] args)
        {
            ILoggerRepository repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));

            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                processMasterDataSynch();
            }
        }

        private static void processMasterDataSynch()
        {
            var sBuilder = new ConfigurationBuilder();
            sBuilder.SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = sBuilder.Build();

            GetMasterTestsList(config);
            GetMasterLocationList(config);
        }

        private static void GetMasterTestsList(IConfiguration config)
        {
            String sClientKey = config.GetSection("VCheckAPI:ClientKey").Value;
            VCheckAPI sVCheckAPI = new VCheckAPI();

            log.Info("GetMasterTestsList BEGIN");

            try
            {
                GetTestListRequest sReq = new GetTestListRequest();
                GetTestListRequestBody sReqBody = new GetTestListRequestBody();
                GetTestListRequestHeader sReqHeader = new GetTestListRequestHeader();
                sReqHeader.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
                sReqHeader.clientkey = sClientKey;

                sReq.header = sReqHeader;
                sReq.body = sReqBody;

                log.Info("Get data from GetTestList API.");

                var resp = sVCheckAPI.GetTestList(sReq);
                if (resp.body.responseCode == "VV.0001")
                {
                    log.Info("Received response data from GetTestList API.");

                    if (resp.body.results.Count > 0)
                    {
                        foreach(var r in resp.body.results)
                        {
                            var sTestsListObj = TestsListRepository.GetTestsListMasterByID(config, r.testid);
                            if (sTestsListObj != null)
                            {
                                TestsListRepository.UpdateTestsList(config, r.testid, r.testname, r.testdescription, sProgramName);
                                log.Info("Update Test List - [" + r.testid + " - " + r.testname + "]");
                            }
                            else
                            {
                                TestsListModel sNewTestsList = new TestsListModel();
                                sNewTestsList.System_TestID = r.testid;
                                sNewTestsList.System_TestName = r.testname;
                                sNewTestsList.System_Description = r.testdescription;
                                sNewTestsList.IsActive = 1;
                                sNewTestsList.CreatedDate = DateTime.Now;
                                sNewTestsList.CreatedBy = sProgramName;

                                TestsListRepository.InsertTestsList(config, sNewTestsList);
                                log.Info("Insert Test List - [" + r.testid + " - " + r.testname + "]");
                            }
                        }
                    }
                    else
                    {
                        log.Info("No records found in the Get Test List API.");
                    }
                }
                else
                {
                    log.Info("Get Test List API data failed.");
                }
            }
            catch (Exception ex)
            {
                log.Error("VPMSMasterDataSynchTask >>> GetMasterTestsList >>> " + ex.ToString());
            }

            log.Info("GetMasterTestsList COMPLETED");
        }


        private static void GetMasterLocationList(IConfiguration config)
        {
            String sClientKey = config.GetSection("VCheckAPI:ClientKey").Value;
            VCheckAPI sVCheckAPI = new VCheckAPI();

            log.Info("GetMasterLocationList BEGIN");

            try
            {
                GetLocationListRequest sReq = new GetLocationListRequest();
                GetLocationListReqBody sReqBody = new GetLocationListReqBody();
                GetLocationListReqHeader sReqHeader = new GetLocationListReqHeader();
                sReqHeader.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
                sReqHeader.clientkey = sClientKey;

                sReq.header = sReqHeader;
                sReq.body = sReqBody;

                log.Info("Get data from GetLocationList API.");

                var resp = sVCheckAPI.GetLocationList(sReq);
                if (resp.body.responseCode == "VV.0001")
                {
                    if (resp.body.results.Count > 0)
                    {
                        log.Info("Received response data from GetLocationList API.");
                        
                        foreach(var r in resp.body.results)
                        {
                            var sLocationListObj = LocationRepository.GetLocationListMasterByID(config, r.locationid);
                            if (sLocationListObj != null)
                            {
                                LocationRepository.UpdateLocationList(config, r.locationid, r.name, Convert.ToInt32(r.status), sProgramName);
                                log.Info("Update Location List - [" + r.locationid + " ~ " + r.name + "]");
                            }
                        }
                    }
                    else
                    {
                        log.Info("No reconds found in the GetLocationList API.");
                    }
                }
                else
                {
                    log.Info("Get Locatiob List API data failed.");
                }

            }
            catch (Exception ex)
            {
                log.Error("VPMSMasterDataSyncTask >>> GetMasterLocationList >>> " + ex.ToString());
            }

            log.Info("GetMasterLocationList COMPLETED");
        }
    }
}
