using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
	public class MastercodeRepository
	{
		public static List<MastercodeModel> GetMastercodeByGroup(IConfiguration config, String sGroup)
		{
			try
			{
				using (var ctx = new MastercodeDBContext(config))
				{
					return ctx.mst_mastercodedata.Where(x => x.CodeGroup == sGroup && x.IsActive == true)
												 .OrderBy(x => x.SeqOrder)
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