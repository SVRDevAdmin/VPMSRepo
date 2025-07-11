using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class CurrencyRepository
    {
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Currency Info by Country Code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static CurrencyModel GetCurrencyInfoByCountryCode(string countryCode)
        {
            try
            {
                using (var ctx = new CurrencyDBContext())
                {
                    return ctx.mst_currency.Where(x => x.Country == countryCode).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logger.Error("CurrencyRepository >>> GetCurrencyInfoByCountryCode >>> " + ex.ToString());
                return null;
            }
        }
    }
}
