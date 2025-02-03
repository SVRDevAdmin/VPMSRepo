using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace VPMS.Lib.Data
{
    public class TemplateRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Template Information By Template code and Language
		/// </summary>
		/// <param name="config"></param>
		/// <param name="sTemplateCode"></param>
		/// <param name="sLangCode"></param>
		/// <returns></returns>
		public static TemplateModel GetTemplateByCodeLang(IConfiguration config, String sTemplateCode, String sLangCode = "en")
        {
            var sTemplateObj = new TemplateModel();

            try
            {
                using (var ctx = new TemplateDBContext(config))
                {
                    MySqlConnection sConn = new MySql.Data.MySqlClient.MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.TemplateID, A.TemplateType, A.TemplateCode, B.LangCode, B.TemplateTitle, B.TemplateContent " +
                                            "FROM mst_template AS A LEFT JOIN mst_template_details AS B ON B.TemplateID = A.TemplateID " +
                                            "Where A.TemplateCode = '" + sTemplateCode + "' AND B.LangCode = '" + sLangCode + "'";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sTemplateObj.TemplateID = Convert.ToInt32(sReader["TemplateID"]);
                                sTemplateObj.TemplateType = sReader["TemplateType"].ToString();
                                sTemplateObj.TemplateCode = sReader["TemplateCode"].ToString();
                                sTemplateObj.TemplateTitle = sReader["TemplateTitle"].ToString();
                                sTemplateObj.TemplateContent = sReader["TemplateContent"].ToString();
                            }
                        }
                    }

                    sConn.Close();

                    return sTemplateObj;
                }
            }
            catch (Exception ex)
            {
                logger.Error("TemplateRepository >>> GetTemplateByCodeLang >>> ", ex);
                return null;
            }
        }
    }
}
