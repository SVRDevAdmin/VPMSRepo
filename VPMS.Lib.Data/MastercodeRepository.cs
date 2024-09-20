using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
	public class MastercodeRepository
	{
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Master code list by Group Name
		/// </summary>
		/// <param name="config"></param>
		/// <param name="sGroup"></param>
		/// <returns></returns>
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
				logger.Error("Database Error >> ", ex);
				return null;
			}
		}
	}
}