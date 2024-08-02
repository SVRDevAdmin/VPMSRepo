using Microsoft.Extensions.Configuration;
using Mysqlx.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class ConfigurationRepository
    {
        /// <summary>
        /// Get User's Configuration Settings by User ID + Configuration Key
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sUserID"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static ConfigurationModel GetUserConfigurationSettingsByKey(IConfiguration config, String sUserID, String sKey)
        {
            try
            {
                using (var ctx = new ConfigurationDBContext(config))
                {
                    return ctx.mst_users_configuration.Where(x => x.UserID == sUserID && x.ConfigurationKey == sKey)
                                                      .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get User Configuration Settings by User ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static List<ConfigurationModel> GetUserConfigurationSettings(IConfiguration config, String sUserID)
        {
            try
            {
                using (var ctx = new ConfigurationDBContext(config))
                {
                    return ctx.mst_users_configuration.Where(x => x.UserID == sUserID).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update User's Settings By Configuration Key
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public static bool UpdateUserConfigurationSettingsByKey(IConfiguration config, ConfigurationModel sModel)
        {
            bool isSuccess = false;

            try
            {
                using (var ctx = new ConfigurationDBContext(config))
                {
                    var sSettingsObj = ctx.mst_users_configuration.Where(x => x.UserID == sModel.UserID && x.ConfigurationKey == sModel.ConfigurationKey).FirstOrDefault();
                    if (sSettingsObj != null)
                    {
                        sSettingsObj.ConfigurationValue = sModel.ConfigurationValue;
                        sSettingsObj.UpdatedDate = sModel.CreatedDate;
                        sSettingsObj.UpdatedBy = sModel.UserID;

                        ctx.SaveChanges();


                        isSuccess = true;
                    }
                    else
                    {
                        ctx.mst_users_configuration.Add(sModel);
                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
