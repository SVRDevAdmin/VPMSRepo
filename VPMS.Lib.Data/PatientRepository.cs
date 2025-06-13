using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class PatientRepository
    {
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Get Patient Owner List
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static List<PatientSelectionModel> GetPatientOwnerList(IConfiguration config, int organizationID, int branchID, int isSuperadmin)
        {
            List<PatientSelectionModel> sResultList = new List<PatientSelectionModel>();

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.ID, A.PatientID, A.Name " +
                                            "FROM mst_patients_owner AS A " +
                                            "INNER JOIN mst_patients AS B ON B.ID = A.PatientID " +
                                            "INNER JOIN mst_branch AS C ON C.ID = B.BranchID " +
                                            "INNER JOIN mst_organisation AS D ON D.ID = C.OrganizationID " +
                                            "WHERE A.Status = '1' AND " +
                                            "( " +
                                            "(" + (isSuperadmin == 1) + " AND D.Level >= 2 AND C.OrganizationID = '" + organizationID + "') OR " +
                                            "(" + (isSuperadmin == 0) + " AND C.OrganizationID = '" + organizationID + "' AND C.ID = '" + branchID + "') " +
                                            ") ";

                    using (MySqlCommand cmd = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = cmd.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PatientSelectionModel sPatientOwnerObj = new PatientSelectionModel();
                                sPatientOwnerObj.ID = Convert.ToInt64(sReader["ID"]);
                                sPatientOwnerObj.PatientID = Convert.ToInt64(sReader["PatientID"]);
                                sPatientOwnerObj.Name = sReader["Name"].ToString();

                                sResultList.Add(sPatientOwnerObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultList;
            }
            catch (Exception ex)
            {
				logger.Error("PatientRepository >>> GetPatientOwnerList >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Pet list by Owner ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public static List<PetsSelectionModel> GetPetListByOwnerID(IConfiguration config, long patientID)
        {
            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    return ctx.mst_pets
                              .Where(x => x.PatientID == patientID && x.Status == 1)
                              .OrderBy(x => x.Name)
                              .Select(x => new PetsSelectionModel
                              {
                                  ID = x.ID,
                                  Name = x.Name
                              })
                              .ToList();
                }
            }
            catch (Exception ex)
            {
				logger.Error("PatientRepository >>> GetPetListByOwnerID >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Patient Information by Owner ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="ownerID"></param>
        /// <returns></returns>
        public static PatientOwnerModel GetPatientOwnerByOwnerID(IConfiguration config, long ownerID)
        {
            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    return ctx.mst_patients_owner.Where(x => x.ID == ownerID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
				logger.Error("PatientRepository >>> GetPatientOwnerByOwnerID >>> ", ex);
				return null;
            }
        }

        /// <summary>
        /// Get Patient Information by Owner ID + Pet ID
        /// </summary>
        /// <param name="config"></param>
        /// <param name="ownerID"></param>
        /// <param name="PetID"></param>
        /// <returns></returns>
        public static PatientPetInfo GetPatientPetProfileByOwnerPetID(IConfiguration config, long ownerID, long PetID)
        {
            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    var sResult = (from A in ctx.mst_patients_owner
                                   join B in ctx.mst_pets on A.PatientID equals B.PatientID
                                   where A.ID == ownerID && B.ID == PetID

                                   select new PatientPetInfo
                                   {
                                       OwnerID = A.ID,
                                       PatientID = A.PatientID,
                                       Name = A.Name,
                                       Gender = A.Gender,
                                       Address = A.Address,
                                       PostCode = A.PostCode,
                                       State = A.State,
                                       Country = A.Country,
                                       PetID = B.ID,
                                       PetName = B.Name,
                                       PetDOB = B.DOB
                                   }).FirstOrDefault();

                    return sResult;
                }
            }
            catch (Exception ex)
            {
				logger.Error("PatientRepository >>> GetPatientPetProfileByOwnerPetID >>> ", ex);
				return null;
            }
        }
    }
}
