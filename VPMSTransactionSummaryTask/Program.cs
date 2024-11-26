using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using System.IO.IsolatedStorage;
using System.Net.NetworkInformation;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMS.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic;
using VPMS.Lib.Data;
using System.Globalization;
using System;

namespace VPMSTransactionSummaryTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime executionDate = DateTime.Now.AddDays(-1);
            DateTime startDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 0, 0, 0);
            DateTime endDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 23, 59, 59);
            String sTransactionKey = "RevenueSummary";

            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                var sBuilder = new ConfigurationBuilder();
                sBuilder.SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration config = sBuilder.Build();

                if (args.Length > 0)
                {
                    if (DateTime.TryParseExact(args[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, 
                                    out executionDate) == false)
                    {
                        PrintHelp();
                        return;
                    }

                    startDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 0, 0, 0);
                    endDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 23, 59, 59);
                }

                TransactionSummaryUpdate(config, executionDate, startDate, endDate);
            }

            Console.WriteLine("Hello, World!");
        }

        private static void TransactionSummaryUpdate(IConfiguration config, DateTime executionDate, DateTime startDate, DateTime endDate)
        {
            InvoiceReceiptDBContext sContext = new InvoiceReceiptDBContext();
            int iQuarter = Helpers.GetQuarterOfYear(executionDate.Month);
            int iWeek = Helpers.GetWeekOfMonth(executionDate);

            try
            {
                //------------ Daily Revenue Summary ----------------//
                var sDailyResult = sContext.GetInvoiceTransactionSummaryByDate(startDate, endDate);
                if (sDailyResult != null)
                {
                    List<TransSummaryModel> sSummaryObj = new List<TransSummaryModel>();
                    String sSummaryType = "Overall";
                    String sGroup = "TotalRevenue";
                    String sTotalSubGroup = "Total";
                    String sDiscSubGroup = "Discount";

                    TransSummaryRepository.DeleteTransactionSummary(config, sSummaryType, executionDate.Date, sGroup);

                    foreach (var r in sDailyResult)
                    {
                        sSummaryObj.Add(new TransSummaryModel
                        {
                            SummaryDate = executionDate.Date,
                            SummaryType = sSummaryType,
                            Group = sGroup,
                            SubGroup = sTotalSubGroup,
                            BranchID = r.BranchID,
                            DateInYear = executionDate.Year.ToString(),
                            DateInMonth = executionDate.Month.ToString(),
                            Week = iWeek,
                            Quarter = iQuarter,
                            TotalAmount = r.TotalAmount,
                            CreatedDate = DateTime.Now
                        });

                        sSummaryObj.Add(new TransSummaryModel
                        {
                            SummaryDate = executionDate.Date,
                            SummaryType = sSummaryType,
                            Group = sGroup,
                            SubGroup = sDiscSubGroup,
                            BranchID = r.BranchID,
                            DateInYear = executionDate.Year.ToString(),
                            DateInMonth = executionDate.Month.ToString(),
                            Week = iWeek,
                            Quarter = iQuarter,
                            TotalAmount = r.TotalDiscount,
                            CreatedDate = DateTime.Now
                        });

                        TransSummaryRepository.InsertTransactionSummary(config, sSummaryObj);
                    } 
                }

                // -------- Daily Breakdown Summary -------------//
                String sBreakdownSummaryType = "Breakdown";
                String sTreatmentGroup = "Treatments";
                String sServiceGroup = "Services";
                String sBreedGroup = "Breeds";

                var sBreakdownObj = sContext.GetTransactionSummaryBreakdownByDate(startDate, endDate);
                if (sBreakdownObj != null)
                {
                    // ------------ Treatments ------------//
                    TransSummaryRepository.DeleteTransactionSummary(config, sBreakdownSummaryType, executionDate.Date, sTreatmentGroup);

                    var sTreatmentGrouping = sBreakdownObj.GroupBy(x => new
                                                            {
                                                                x.BranchID,
                                                                x.TreatmentPlanID,
                                                                x.TreatmentPlanName
                                                            })
                                                            .Select(x => new TransSummaryModel
                                                            {
                                                                SummaryType = sBreakdownSummaryType,
                                                                SummaryDate = executionDate.Date,
                                                                BranchID = x.Select(x => x.BranchID).FirstOrDefault(),
                                                                DateInYear = executionDate.Year.ToString(),
                                                                DateInMonth = executionDate.Month.ToString(),
                                                                Week = iWeek,
                                                                Quarter = iQuarter,
                                                                Group = sTreatmentGroup,
                                                                SubGroup = x.Select(x => x.ServiceName).FirstOrDefault(),
                                                                TotalAmount = x.Sum(x => x.ServicePrice),
                                                                CreatedDate = DateTime.Now
                                                            }).ToList();

                    if (sTreatmentGrouping.Count > 0)
                    {
                        TransSummaryRepository.InsertTransactionSummary(config, sTreatmentGrouping);
                    }

                    //-------------- Services ------------------//
                    TransSummaryRepository.DeleteTransactionSummary(config, sBreakdownSummaryType, executionDate.Date, sServiceGroup);

                    var sServiceGrouping = sBreakdownObj.GroupBy(x => new
                                                        {
                                                            x.BranchID,
                                                            x.ServiceID,
                                                            x.ServiceName
                                                        })
                                                        .Select(x => new TransSummaryModel
                                                        {
                                                            SummaryType = sBreakdownSummaryType,
                                                            SummaryDate = executionDate.Date,
                                                            BranchID = x.Select(x => x.BranchID).FirstOrDefault(),
                                                            DateInYear = executionDate.Year.ToString(),
                                                            DateInMonth = executionDate.Month.ToString(),
                                                            Week = iWeek,
                                                            Quarter = iQuarter,
                                                            Group = sServiceGroup,
                                                            SubGroup = x.Select(x => x.TreatmentPlanName).FirstOrDefault(),
                                                            TotalAmount = x.Sum(x => x.TreatmentPlanAmount),
                                                            CreatedDate = DateTime.Now
                                                        }).ToList();

                    if (sServiceGrouping.Count > 0)
                    {
                        TransSummaryRepository.InsertTransactionSummary(config, sServiceGrouping);
                    }

                    //----------- Breeds --------------------//
                    TransSummaryRepository.DeleteTransactionSummary(config, sBreakdownSummaryType, executionDate.Date, sBreedGroup);

                    var sBreedGrouping = sBreakdownObj.GroupBy(x => new
                                                        {
                                                            x.BranchID,
                                                            x.Species
                                                        })
                                                        .Select(x => new TransSummaryModel
                                                        {
                                                            SummaryType = sBreakdownSummaryType,
                                                            SummaryDate = executionDate.Date,
                                                            BranchID = x.Select(x => x.BranchID).FirstOrDefault(),
                                                            DateInYear = executionDate.Year.ToString(),
                                                            DateInMonth = executionDate.Month.ToString(),
                                                            Week = iWeek,
                                                            Quarter = iQuarter,
                                                            Group = sBreedGroup,
                                                            SubGroup = x.Select(x => x.Species).FirstOrDefault(),
                                                            TotalAmount = x.Sum(x => x.TreatmentPlanAmount),
                                                            CreatedDate = DateTime.Now
                                                        }).ToList();

                    if (sBreedGrouping.Count > 0)
                    {
                        TransSummaryRepository.InsertTransactionSummary(config, sBreedGrouping);
                    }
                }

                TransSummaryLogModel sLog = new TransSummaryLogModel();
                sLog.TransactionType = "RevenueSummary";
                sLog.TransactionDate = executionDate.Date;
                sLog.CreatedDate = DateTime.Now;
                sLog.CreatedBy = "SYSTEM";

                TransSummaryRepository.InsertTransSummaryLog(config, sLog);


                // --------- Patient Summary ---------- //
                String sPatientSummaryType = "Overall";
                String sPatientBreakdownType = "Breakdown";
                String sPatientGroup = "TotalPatients";
                String sPatientBreedGroup = "PatientByBreed";

                var sPatientSummaryObj = UserRepository.GetPatientsSummary();
                if (sPatientSummaryObj != null)
                {
                    // --------- Patient Summary ---------//
                    TransSummaryRepository.DeleteTransactionSummary(config, sPatientSummaryType, executionDate.Date, sPatientGroup);

                    var sPatientGrouping = sPatientSummaryObj.GroupBy(x => new { x.BranchID })
                                                             .Select(x => new TransSummaryModel
                                                             {
                                                                 SummaryType = sPatientSummaryType,
                                                                 SummaryDate = executionDate.Date,
                                                                 BranchID = x.Select(x => x.BranchID).FirstOrDefault(),
                                                                 DateInYear = executionDate.Year.ToString(),
                                                                 DateInMonth = executionDate.Month.ToString(),
                                                                 Week = iWeek,
                                                                 Quarter = iQuarter,
                                                                 Group = sPatientGroup,
                                                                 SubGroup = "Total",
                                                                 TotalAmount = x.Count(),
                                                                 CreatedDate = DateTime.Now
                                                             }).ToList();

                    if (sPatientGrouping.Count > 0)
                    {
                        TransSummaryRepository.InsertTransactionSummary(config, sPatientGrouping);
                    }

                    // ---------- Patient Breed Breakdown summary --------//
                    TransSummaryRepository.DeleteTransactionSummary(config, sPatientBreakdownType, executionDate.Date, sPatientBreedGroup);

                    var sPatientBreedGrouping = sPatientSummaryObj.GroupBy(x => new
                                                                    {
                                                                        x.BranchID,
                                                                        x.Species
                                                                    })
                                                                    .Select(x => new TransSummaryModel
                                                                    {
                                                                        SummaryType = sPatientBreakdownType,
                                                                        SummaryDate = executionDate.Date,
                                                                        BranchID = x.Select(x => x.BranchID).FirstOrDefault(),
                                                                        DateInYear = executionDate.Year.ToString(),
                                                                        DateInMonth = executionDate.Month.ToString(),
                                                                        Week = iWeek,
                                                                        Quarter = iQuarter,
                                                                        Group = sPatientBreedGroup,
                                                                        SubGroup = x.Select(x => x.Species).FirstOrDefault(),
                                                                        TotalAmount = x.Count(),
                                                                        CreatedDate = DateTime.Now
                                                                    }).ToList();

                    if (sPatientBreedGrouping.Count > 0)
                    {
                        TransSummaryRepository.InsertTransactionSummary(config, sPatientBreedGrouping);
                    }
                }

                TransSummaryLogModel sPatientSummaryLog = new TransSummaryLogModel();
                sPatientSummaryLog.TransactionType = "PatientSummary";
                sPatientSummaryLog.TransactionDate = executionDate.Date;
                sPatientSummaryLog.CreatedDate = DateTime.Now;
                sPatientSummaryLog.CreatedBy = "SYSTEM";

                TransSummaryRepository.InsertTransSummaryLog(config, sPatientSummaryLog);
            }
            catch (Exception ex)
            {
                //todo:
            }
        }

        private static void PrintHelp()
        {
            Console.Out.WriteLine("");
            Console.Out.WriteLine("");
            Console.Out.WriteLine("");
            Console.Out.WriteLine("");
            Console.Out.WriteLine("Usage Help");
            Console.Out.WriteLine("--------------------------------------------------------");
            Console.Out.WriteLine("VPMSTransactionSummaryTask.exe [Execution Date]");
            Console.Out.WriteLine("[Execution Date] --> Date Format yyyyMMdd");
            Console.Out.WriteLine("--------------------------------------------------------");

            Environment.Exit(-999);
        }
    }
}
