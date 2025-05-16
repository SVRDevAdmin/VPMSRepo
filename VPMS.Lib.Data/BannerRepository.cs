using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace VPMS.Lib.Data
{
    public class BannerRepository
    {
        public static List<BannerViewModel> GetBannersViewList(IConfiguration config, int pageSize, int pageIndex, out int totalRecords)
        {
            List<BannerViewModel> sResultList = new List<BannerViewModel>();
            totalRecords = 0;

            DateTime sNow = DateTime.Now;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER () AS 'row_num', Count(*) OVER() as 'TotalRows', B.* " + 
                                            "FROM mst_banners As B " + 
                                            "WHERE ( " +
                                            "B.StartDate <= '" + sNow.ToString("yyyy-MM-dd HH:mm:ss") + "' AND " +
                                            "B.EndDate >= '" + sNow.ToString("yyyy-MM-dd HH:mm:ss")  + "' " +
                                            ") " +
                                            "ORDER BY B.IsActive DESC, B.SeqOrder ASC " +
                                            "LIMIT " + pageSize + " " +
                                            "OFFSET " + ((pageIndex - 1) * pageSize);

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                BannerViewModel sBannerObj = new BannerViewModel();
                                sBannerObj.RowNo = Convert.ToInt32(sReader["row_num"]);
                                sBannerObj.ID = Convert.ToInt32(sReader["ID"]);
                                sBannerObj.BannerType = Convert.ToInt32(sReader["BannerType"]);
                                sBannerObj.Description = sReader["Description"].ToString();
                                sBannerObj.SeqOrder = Convert.ToInt32(sReader["SeqOrder"]);
                                sBannerObj.IsActive = Convert.ToInt32(sReader["IsActive"]);
                                sBannerObj.StartDate = Convert.ToDateTime(sReader["StartDate"]);
                                sBannerObj.EndDate = Convert.ToDateTime(sReader["EndDate"]);
                                sBannerObj.BannerName = sReader["BannerName"].ToString();
                                sBannerObj.BannerFilePath = sReader["BannerFilePath"].ToString();
                                sBannerObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sBannerObj.CreatedBy = sReader["CreatedBy"].ToString();

                                sResultList.Add(sBannerObj);

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean InsertBanners(IConfiguration config, BannerModel sInputObject)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new BannerDBContext(config))
                {
                    int sBannerObj = ctx.mst_banners.Where(x => x.IsActive == 1).Max(x => x.SeqOrder).Value;

                    sInputObject.SeqOrder = (sBannerObj != null) ? (sBannerObj + 1) : 1;

                    ctx.mst_banners.Add(sInputObject);
                    ctx.SaveChanges();

                    isSuccess = true;
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
