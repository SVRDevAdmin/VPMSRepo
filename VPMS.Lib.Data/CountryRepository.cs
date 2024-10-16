using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;

namespace VPMS.Lib.Data
{
	public class CountryRepository
	{
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Country Listing
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static List<CountryListModel> GetCountryList(IConfiguration config)
		{
			try
			{
				using (var ctx = new CountryDBContext(config))
				{
					return ctx.mst_countrylist.Where(x => x.IsActive == true)
											  .OrderBy(x => x.CountryName)
											  .ToList();
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