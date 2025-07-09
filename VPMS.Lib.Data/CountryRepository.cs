using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

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

		public static CountryListModel GetCountryByID(IConfiguration config, int countryID)
		{
			try
			{
				using (var ctx = new CountryDBContext(config))
				{
					return ctx.mst_countrylist.Where(x => x.ID == countryID).FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
                logger.Error("CountryRepository >>> GetCountryByID >>> ", ex);
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

		/// <summary>
		/// Get Country Info by State Name
		/// </summary>
		/// <param name="config"></param>
		/// <param name="stateName"></param>
		/// <returns></returns>
		public static StateModel GetCountryByState(IConfiguration config, String stateName)
		{
			try
			{
				using (var ctx = new CountryDBContext(config))
				{
					return ctx.mst_state.Where(x => x.State.ToLower() == stateName.ToLower()).FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
                logger.Error("CountryRepository >>> GetCountryByState >>> ", ex);
                return null;
            }
		}

		/// <summary>
		/// Get Currency Details by Country Code
		/// </summary>
		/// <param name="config"></param>
		/// <param name="countryCode"></param>
		/// <returns></returns>
		public static CurrencyModel GetCurrencyMasterByCountryCode(IConfiguration config, String countryCode)
		{
			try
			{
				using (var ctx = new CountryDBContext(config))
				{
					return ctx.mst_currency.Where(x => x.Country == countryCode).FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
                logger.Error("CountryRepository >>> GetCurrencyMasterByCountryCode >>> ", ex);
                return null;
            }
		}
    }
}