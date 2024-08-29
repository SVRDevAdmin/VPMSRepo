using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data
{
    public class PatientRepository
    {
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
                return null;
            }
        }
    }
}
