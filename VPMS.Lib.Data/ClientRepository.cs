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
				logger.Error("ClientRepository >>> GetClientAuth >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Client Profile 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sAuthKey"></param>
        /// <returns></returns>
        public static ClientModel GetClientProfile(IConfiguration config, String sAuthKey)
        {
            try
            {
                using (var ctx = new ClientDBContext(config))
                {
                    var sClientAuth = ctx.mst_client_auth.Where(x => x.ClientKey == sAuthKey).FirstOrDefault();
                    if (sClientAuth != null)
                    {
                        return ctx.mst_client.Where(x => x.ID == sClientAuth.ClientID).FirstOrDefault();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                logger.Error("ClientRepository >>> GetClientProfile >>> ", ex);
                return null;
            }
        }
    }
}
