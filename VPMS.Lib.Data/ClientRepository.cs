using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class ClientRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Client Auth record by Auth Token
		/// </summary>
		/// <param name="config"></param>
		/// <param name="sAuthKey"></param>
		/// <returns></returns>
		public static ClientAuthModel GetClientAuth(IConfiguration config, String sAuthKey)
        {
            try
            {
                using (var ctx = new ClientDBContext(config))
                {
                    return ctx.mst_client_auth.Where(x => x.ClientKey == sAuthKey).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
				logger.Error("Database Error >> ", ex);
				return null;
            }
        }
    }
}
