using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VPMS.Lib.Data.Models;
using VPMS.Lib.Data.DBContext;

namespace VPMS.Lib.Data
{
    public class AppointmentRepository
    {
        public static Boolean CreateAppointment(IConfiguration config, AppointmentModel sModel, List<long> ServiceID)
        {
            Boolean isSuccess = false;

            try
            {
                using (var ctx = new AppointmentDBContext(config))
                {
                    ctx.mst_appointment.Add(sModel);
                    ctx.SaveChanges();


                    foreach (var s in ServiceID)
                    {
                        AppointmentServiceModel sServiceModel = new AppointmentServiceModel();
                        sServiceModel.ApptID = sModel.AppointmentID;
                        sServiceModel.ServicesID = s;
                        sServiceModel.IsDeleted = false;
                        sServiceModel.CreatedDate = DateTime.Now;
                        sServiceModel.CreatedBy = sModel.CreatedBy;

                        ctx.mst_appointment_services.Add(sServiceModel);
                        ctx.SaveChanges();
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        // --- Temporary sit here --- //
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
