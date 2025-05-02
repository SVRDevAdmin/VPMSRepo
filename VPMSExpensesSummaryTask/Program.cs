using Microsoft.Extensions.Configuration;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.Models;
using log4net.Repository;
using System.Reflection;

namespace VPMSExpensesSummaryTask
{
    public class Program
    {
        static String sTransactionType = "CustomerExpensesSummary";
        static String sExecutionType = "Scheduler";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            ILoggerRepository repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));

            DateTime executionDate = DateTime.Now.AddDays(-1);
            DateTime startDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 0, 0, 0);
            DateTime endDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 23, 59, 59);

            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                var sBuilder = new ConfigurationBuilder();
                sBuilder.SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                if (args.Length > 0)
                {
                    if (DateTime.TryParseExact(args[0], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                                    out executionDate) == false)
                    {
                        PrintHelp();
                        return;
                    }

                    sExecutionType = "Manual";

                    startDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 0, 0, 0);
                    endDate = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, 23, 59, 59);
                }

                ExpensesSummaryUpdate(startDate, endDate);
            }      
        }

        private static void ExpensesSummaryUpdate(DateTime sStartDate, DateTime sEndDate)
        {
            List<AnalyticsModel> sSummaryObj = new List<AnalyticsModel>();

            try
            {
                AnalyticsRepository.DeleteExpensesSummaryByDate(sStartDate);

                var sDailyExpenses = InvoiceReceiptRepository.GetCustomerExpensesSummary(sStartDate, sEndDate);
                if (sDailyExpenses != null && sDailyExpenses.Count > 0)
                {
                    foreach(var expenses in sDailyExpenses)
                    {
                        sSummaryObj.Add(new AnalyticsModel
                        {
                           TransDate = expenses.InvoiceDate.Date,
                           TransDateInMonth = expenses.InvoiceDate.Month.ToString("00"), 
                           TransDateInYear = expenses.InvoiceDate.Year.ToString(),
                           PatientID = expenses.PatientID,
                           PetID = expenses.PetID,
                           ServiceID = expenses.ServiceID,
                           ServiceName = expenses.ServiceName,
                           TotalValue = expenses.ServicePrice,
                           CreatedDate = DateTime.Now,
                           CreatedBy = "SYSTEM"
                        });

                        AnalyticsRepository.InsertExpensesSummary(sSummaryObj);
                    }
                }

                // ------ Insert Execution Log ------- //
                ExpensesSummaryLog sLog = new ExpensesSummaryLog();
                sLog.TransactionDate = sStartDate.Date;
                sLog.TransactionType = sTransactionType;
                sLog.ExecutionType = sExecutionType;
                sLog.CreatedDate = DateTime.Now;
                sLog.CreatedBy = "SYSTEM";

                AnalyticsRepository.InsertExpensesSummaryLog(sLog);
            }
            catch (Exception ex)
            {
                
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
            Console.Out.WriteLine("VPMSExpensesSummaryTask.exe [Execution Date]");
            Console.Out.WriteLine("[Execution Date] --> Date Format yyyyMMdd");
            Console.Out.WriteLine("--------------------------------------------------------");

            Environment.Exit(-999);
        }
    }
}
