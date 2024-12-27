﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;
using VPMS.Lib.Data.DBContext;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace VPMS.Lib.Data
{
    public class DoctorRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Doctor Listing with Search Criteria & Pagination
		/// </summary>
		/// <param name="config"></param>
		/// <param name="sSearchKeyword"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="totalRecords"></param>
		/// <returns></returns>
		public static List<DoctorExtendedModel> GetDoctorViewList(IConfiguration config, String sSearchKeyword, int branchID, int pageSize, int pageIndex, out int totalRecords)
        {
            List<DoctorExtendedModel> sDoctorList = new List<DoctorExtendedModel>();
            totalRecords = 0;

            try
            {
                using (var ctx = new DoctorDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommnd = "SELECT D.ID, D.Name, D.Gender, M.CodeName AS 'GenderName', D.LicenseNo, D.Designation, " + 
                                           "D.Specialty, D.IsDeleted, D.CreatedDate, D.CreatedBy, Count(*) OVER() as 'TotalRows' " +
                                           "FROM Mst_Doctor as D " +
                                           "LEFT JOIN (SELECT * FROM mst_mastercodedata WHERE CodeGroup='Gender') AS M ON M.CodeID = D.Gender " +
                                           "WHERE D.IsDeleted = '0' AND D.BranchID ='" + branchID  + "' AND " +
                                           "(" + (sSearchKeyword == null) + " OR D.Name LIKE '%" + sSearchKeyword + "%' )" +
                                           "ORDER BY D.ID, D.Name " +
                                           "LIMIT " + pageSize + " " +
                                           "OFFSET " + ((pageIndex - 1) * pageSize);

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

                                totalRecords = Convert.ToInt32(sReader["TotalRows"]);
                            }
                        }
                    }

                    sConn.Close();

                    return sDoctorList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("DoctorRepository >>> GetDoctorViewList >>> ", ex);
                return null;
            }
        }

        /// <summary>
        /// Add Doctor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sModel"></param>
        /// <returns></returns>
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
				logger.Error("DoctorRepository >>> AddDoctor >>> ", ex);
				isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Update Doctor Profile
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public static Boolean UpdateDoctor(IConfiguration config, DoctorModel sModel)
        {
            Boolean isValid = false;

            try
            {
                using (var ctx = new DoctorDBContext(config))
                {
                    var sDoctorProfile = ctx.mst_doctor.Where(x => x.ID == sModel.ID).FirstOrDefault();
                    if (sDoctorProfile != null)
                    {
                        Boolean isChanges = false;

                        if (sDoctorProfile.Name != sModel.Name)
                        {
                            sDoctorProfile.Name = sModel.Name;
                            isChanges = true;
                        }
                        
                        if (sDoctorProfile.LicenseNo != sModel.LicenseNo)
                        {
                            sDoctorProfile.LicenseNo = sModel.LicenseNo;
                            isChanges = true;
                        }
                        
                        if (sDoctorProfile.System_ID != sModel.System_ID)
                        {
                            sDoctorProfile.System_ID = sModel.System_ID;
                            isChanges = true;
                        }
                        
                        if (sDoctorProfile.Gender != sModel.Gender)
                        {
                            sDoctorProfile.Gender = sModel.Gender;
                            isChanges = true;
                        }
                        
                        if (sDoctorProfile.Designation != sModel.Designation)
                        {
                            sDoctorProfile.Designation = sModel.Designation;
                            isChanges = true;
                        }
                        
                        if (sDoctorProfile.Specialty != sModel.Specialty)
                        {
                            sDoctorProfile.Specialty = sModel.Specialty;
                            isChanges = true;
                        }

                        if (isChanges)
                        {
                            sDoctorProfile.UpdatedDate = DateTime.Now;
                            sDoctorProfile.UpdatedBy = sModel.UpdatedBy;

                            ctx.SaveChanges();
                        }

                        isValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
				logger.Error("DoctorRepository >>> UpdateDoctor >>> ", ex);
				isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Delete Doctor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="iDoctorID"></param>
        /// <returns></returns>
        public static Boolean DeleteDoctor(IConfiguration config, int iDoctorID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new DoctorDBContext(config))
                {
                    var sDoctorProfile = ctx.mst_doctor.Where(x => x.ID == iDoctorID).FirstOrDefault();
                    if (sDoctorProfile != null)
                    {
                        sDoctorProfile.IsDeleted = 1;
                        sDoctorProfile.UpdatedDate = DateTime.Now;
                        sDoctorProfile.UpdatedBy = "SYSTEM";

                        ctx.SaveChanges();

                        isSuccess = true;
                    }
                }
            }
            catch(Exception ex)
            {
				logger.Error("DoctorRepository >>> DeleteDoctor >>> ", ex);
				isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Get Doctor Profile Info by ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="doctorid"></param>
        /// <returns></returns>
        public static DoctorModel GetDoctorByID(IConfiguration config, int doctorid)
        {
            try
            {
                using (var ctx = new DoctorDBContext(config))
                {
                    return ctx.mst_doctor.Where(x => x.ID == doctorid && x.IsDeleted == 0).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
				logger.Error("DoctorRepository >>> GetDoctorByID >>> ", ex);
				return null;
            }
        }
    }
}
