using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class PatientDBContext : DbContext
    {
        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");

        public DbSet<PatientsModel> Mst_Patients { get; set; }
        public DbSet<Pets> Mst_Pets { get; set; }
        public DbSet<Patient_Owner> Mst_Patients_Owner { get; set; }
        public DbSet<Pets_Breed> Mst_Pets_Breed { get; set; }
		public DbSet<Pet_Growth> Mst_Pet_Growth { get; set; }
		public DbSet<PatientTreatmentPlan> Txn_TreatmentPlan { get; set; }
		public DbSet<PatientTreatmentPlanServices> Txn_TreatmentPlan_Services { get; set; }
		public DbSet<PatientTreatmentPlanProducts> Txn_TreatmentPlan_Products { get; set; }
		public DbSet<PatientMedicalRecordService> Mst_MedicalRecord_VaccinationSurgery { get; set; }
		public DbSet<PatientMedicalRecordMedication> Mst_MedicalRecord_Medication { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public ObservableCollection<PetsInfo> GetPetsInfoList(int start, int total, string ownerName, string petName, string species, string breed, out int totalPets)
        {
            ObservableCollection<PetsInfo> sList = new ObservableCollection<PetsInfo>();
            int No = start + 1;
			totalPets = 0;
            
            var filter = "WHERE b.Name like '%" + ownerName + "%' AND a.Name like '%"+ petName + "%' AND a.Species like '%" + species + "%' AND a.Breed like '%" + breed + "%' ";

            var totalPetsQuery = "(select Count(a.Name) from mst_pets a join mst_patients_owner b on b.PatientID = a.PatientID AND b.ID = (SELECT MIN(ID) FROM mst_patients_owner WHERE PatientID = a.PatientID) " + filter + ")";

            var completeQuery = "select a.PatientID, a.ID, a.Name, b.Name AS 'OwnerName', a.Gender, a.Age, a.DOB, a.Species, a.Breed, a.CreatedDate, "+ totalPetsQuery + " as 'TotalPets' from mst_pets a " +
				"join mst_patients_owner b on b.PatientID = a.PatientID AND b.ID = (SELECT MIN(ID) FROM mst_patients_owner WHERE PatientID = a.PatientID) " + filter +
				"Order by a.ID LIMIT " + start + ", " + total + ";";
			
			try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(completeQuery, conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sList.Add(new PetsInfo()
                            {
                                No = No++,
								PatientID = Convert.ToInt32(reader["PatientID"]),
								ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                OwnerName = reader["OwnerName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Age = DateTime.Now.Year - DateTime.Parse(reader["DOB"].ToString()).Year,
                                Species = reader["Species"].ToString(),
                                Breed = reader["Breed"].ToString(),
                                CreatedOn = DateTime.Parse(reader["CreatedDate"].ToString())
							});

                            totalPets = Convert.ToInt32(reader["TotalPets"]);
						}
                    }
                }
            }
            catch (Exception ex)
            {

            }

			return sList;
        }
    }
}
