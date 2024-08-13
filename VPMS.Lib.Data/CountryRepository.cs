using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;

namespace VPMS.Lib.Data
{
    public class CountryRepository
    {
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
                return null;
            }
        }
    }
}
