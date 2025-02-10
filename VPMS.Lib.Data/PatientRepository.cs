using Microsoft.Extensions.Configuration;
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
		public static List<PatientSelectionModel> GetPatientOwnerList(IConfiguration config)
        {
            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    return ctx.mst_patients_owner
                              .Where(x => x.Status == 1)
                              .OrderBy(x => x.Name)
                              .Select(x => new PatientSelectionModel
                              {
                                  ID = x.ID,
                                  Name = x.Name,
                                  PatientID = x.PatientID

                              }).ToList();
                }
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
