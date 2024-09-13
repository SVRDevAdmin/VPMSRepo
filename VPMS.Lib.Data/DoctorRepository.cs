using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;
using VPMS.Lib.Data.DBContext;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace VPMS.Lib.Data
{
    public class DoctorRepository
    {
        public static List<DoctorExtendedModel> GetDoctorViewList(IConfiguration config, String sSearchKeyword)
        {
            List<DoctorExtendedModel> sDoctorList = new List<DoctorExtendedModel>();

            try
            {
                using (var ctx = new DoctorDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommnd = "SELECT D.ID, D.Name, D.Gender, M.CodeName AS 'GenderName', D.LicenseNo, D.Designation, D.Specialty, D.IsDeleted, D.CreatedDate, D.CreatedBy " +
                                           "FROM Mst_Doctor as D " +
                                           "LEFT JOIN (SELECT * FROM mst_mastercodedata WHERE CodeGroup='Gender') AS M ON M.CodeID = D.Gender " +
                                           "WHERE D.IsDeleted = '0' AND " +
                                           "(" + (sSearchKeyword == "") + " OR D.Name = '" + sSearchKeyword + "' )" +
                                           "ORDER BY D.Name ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommnd, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sDoctorList.Add(new DoctorExtendedModel
                                {
                                    ID = Convert.ToInt32(sReader["ID"]),
                                    Name = sReader["Name"].ToString(),
                                    Gender = sReader["Gender"].ToString(),
                                    GenderName = sReader["GenderName"].ToString(),
                                    LicenseNo = sReader["LicenseNo"].ToString(),
                                    Designation = sReader["Designation"].ToString(),
                                    Specialty = sReader["Specialty"].ToString(),
                                    CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]),
                                    CreatedBy = sReader["CreatedBy"].ToString()
                                });
                            }
                        }
                    }

                    sConn.Close();

                    return sDoctorList;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Boolean AddDoctor(IConfiguration config, DoctorModel sModel)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new DoctorDBContext(config))
                {
                    ctx.mst_doctor.Add(sModel);
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
