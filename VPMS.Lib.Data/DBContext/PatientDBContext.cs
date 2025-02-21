using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using VPMS.Lib.Data.Models;

namespace VPMS.Lib.Data.DBContext
{
    public class PatientDBContext : DbContext
    {
        private readonly string connectionString = Host.CreateApplicationBuilder().Configuration.GetConnectionString("DefaultConnection");
		private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
		public DbSet<Patient_Owner_Login> Mst_Patients_Login {  get; set; }
		public DbSet<Account_Creation_Logs> Mst_Account_Creation_Logs { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) =>
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		public ObservableCollection<PetsInfo> GetPetsInfoList(int start, int total, string ownerName, string petName, string species, string breed, int branch, int organisation, out int totalPets)
        {
            ObservableCollection<PetsInfo> sList = new ObservableCollection<PetsInfo>();
            int No = start + 1;
			totalPets = 0;

			var roleFilter = "";

			if(branch != 0)
			{
				roleFilter += "AND d.ID = " + branch + " ";
            }
            else if (organisation != 0)
            {
                roleFilter += "AND e.ID = " + organisation + " ";
            }

            var joinQuery =
                "join mst_patients_owner b on b.PatientID = a.PatientID AND b.ID = (SELECT MIN(ID) FROM mst_patients_owner WHERE PatientID = a.PatientID) " +
                "join mst_patients c on c.ID = a.PatientID " +
                "join mst_branch d on d.ID = c.BranchID " +
                "join mst_organisation e on e.ID = d.OrganizationID ";

            var filter = "WHERE b.Name like '%" + ownerName + "%' AND a.Name like '%"+ petName + "%' AND a.Species like '%" + species + "%' AND a.Breed like '%" + breed + "%' " + roleFilter;

            var totalPetsQuery = "(select Count(a.Name) from mst_pets a " + joinQuery + filter + ")";

            var completeQuery = "select a.PatientID, a.ID, a.Name, b.Name AS 'OwnerName', a.Gender, a.Age, a.DOB, a.Species, a.Breed, a.CreatedDate, "+ totalPetsQuery + " as 'TotalPets' from mst_pets a " +
                joinQuery + filter +
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
				logger.Error("Database Error >> ", ex);
			}

			return sList;
        }

		public ObservableCollection<PatientVaccinationTreatments> GetVaccinationTreatmentList(int planID, bool upcoming, int petID)
		{
			ObservableCollection<PatientVaccinationTreatments> sList = new ObservableCollection<PatientVaccinationTreatments>();

			var completeQuery = "select d.TreatmentStart as 'Date', b.name as 'ServiceName', c.SubCategoryName as 'CategoryName' from txn_treatmentplan_services a " +
				"inner join mst_services b on b.ID = a.ServiceID " +
				"inner join mst_servicescategory c on b.CategoryID = c.ID " +
				"inner join txn_treatmentplan d on d.ID = a.PlanID " +
				"where c.Name in ('Vaccination', 'Treatment') AND ";

			if (upcoming)
			{
				completeQuery = completeQuery + "d.TreatmentStart > now() AND d.ID = " + planID + " Order by d.TreatmentStart Limit 0,1;";
			}
			else
			{
				completeQuery = completeQuery + "d.TreatmentEnd < now() AND d.PetID = " + petID + " Order by d.TreatmentStart;";
			}

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
							sList.Add(new PatientVaccinationTreatments()
							{
								CategoryName = reader["CategoryName"].ToString(),
								ServiceName = reader["ServiceName"].ToString(),
								Date = DateOnly.FromDateTime(DateTime.Parse(reader["Date"].ToString())),
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Error("Database Error >> ", ex);
			}

			return sList;
		}

		public ObservableCollection<PatientHealthCardMedication> GetHealthCardMedicationList(int petID)
		{
			ObservableCollection<PatientHealthCardMedication> sList = new ObservableCollection<PatientHealthCardMedication>();

			var completeQuery =
				"SELECT a.CreatedDate as 'Date', c.Name as 'ProductName', a.Status FROM mst_medicalrecord_medication a " +
				"inner join mst_product c on c.ID = a.ProductID " +
				"where a.PetID = " + petID + ";";

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
							sList.Add(new PatientHealthCardMedication()
							{
								Date = DateOnly.FromDateTime(DateTime.Parse(reader["Date"].ToString())),
								Name = reader["ProductName"].ToString(),
								Status = Convert.ToInt32(reader["Status"])
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Error("Database Error >> ", ex);
			}

			return sList;
		}
	}
}
