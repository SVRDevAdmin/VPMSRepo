using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class TransSummaryRepository
    {
        public static Boolean InsertTransactionSummary(IConfiguration config, List<TransSummaryModel> sSummaryObj)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TransSummaryDBContext(config))
                {
                    ctx.txn_transactionsummary.AddRange(sSummaryObj);
                    ctx.SaveChanges();

                    isSuccess = true;
                }
               
            }
            catch (Exception ex)
            {
                return false;
            }

            return isSuccess;
        }

        public static Boolean DeleteTransactionSummary(IConfiguration config, String sSummaryType, DateTime sSummaryDate, String sGroup)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TransSummaryDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sDeleteCommand = "DELETE FROM txn_transactionsummary " +
                                            "WHERE SummaryType = '" + sSummaryType  + "' AND " +
                                            "SummaryDate = '" + sSummaryDate.ToString("yyyy-MM-dd") + "' AND " + 
                                            "`Group` = '" + sGroup + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sDeleteCommand, sConn))
                    {
                        int iResult = sCommand.ExecuteNonQuery();
                        if (iResult >= 0)
                        {
                            isSuccess = true;
                        }
                    }

                    sConn.Close();
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public static Boolean InsertTransSummaryLog(IConfiguration config, TransSummaryLogModel sSummaryLog)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new TransSummaryDBContext(config))
                {
                    ctx.txn_transactionsummarylog.Add(sSummaryLog);
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
    }
}
