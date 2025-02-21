using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class MastercodeRepository
    {
        #region "MasterCode Data"
        public static List<MasterCodeDataModel> GetMastercodeDataByGroup(String sGroup)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_mastercodedata.Where(x => x.CodeGroup == sGroup && x.IsActive == 1).OrderBy(x => x.SeqOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region "Country & State & City"
        public static List<CountryModel> GetCountryList()
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_countrylist.Where(x => x.IsActive == 1).OrderBy(x => x.CountryName).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<StateModel> GetStatesList(int CountryID)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_state.Where(x => x.CountryID == CountryID).OrderBy(x => x.State).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<CityModel> GetCityList(int StateID)
        {
            try
            {
                using (var ctx = new MasterCodeDBContext())
                {
                    return ctx.mst_city.Where(x => x.StateID == StateID).OrderBy(x => x.City).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
