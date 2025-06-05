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
				logger.Error("CountryRepository >>> GetCountryList >>> ", ex);
				return null;
			}
		}

		/// <summary>
		/// Get State List By Country
		/// </summary>
		/// <param name="config"></param>
		/// <param name="countryID"></param>
		/// <returns></returns>
		public static List<StateModel> GetStatesListByCountry(IConfiguration config, int countryID)
		{
			try
			{
				using (var ctx = new CountryDBContext(config))
				{
					return ctx.mst_state.Where(x => x.CountryID == countryID).OrderBy(x => x.State).ToList();
				}
			}
			catch (Exception ex)
			{
                logger.Error("CountryRepository >>> GetStatesListByCountry >>> ", ex);
                return null;
            }
		}

		/// <summary>
		/// Get City List by State
		/// </summary>
		/// <param name="config"></param>
		/// <param name="stateID"></param>
		/// <returns></returns>
		public static List<CityModel> GetCityListByState(IConfiguration config, int stateID)
		{
			try
			{
				using (var ctx = new CountryDBContext(config))
				{
					return ctx.mst_city.Where(x => x.StateID == stateID).OrderBy(x => x.City).ToList();
				}
			}
			catch (Exception ex)
			{
                logger.Error("CountryRepository >>> GetCityListByState >>> ", ex);
                return null;
            }
		}
	}
}