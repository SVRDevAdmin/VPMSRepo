using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VPMSCustomer.Lib.Data;
using VPMSCustomer.Lib.Data.DBContext;
using VPMSCustomer.Lib.Data.Models;

namespace VPMSCustomer.Lib.Data
{
    public class PetRepository
    {
        public static List<PetDataExtendedModel> GetPetsListByPatientID(long patientID)
        {
            List<PetDataExtendedModel> sResultList = new List<PetDataExtendedModel>();

            try
            {
                using (var ctx = new PetDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.*, B.CodeName AS 'GenderName', C.AvatarImage, C.ColorCode " +
                                            "FROM mst_pets AS A " +
                                            "LEFT JOIN ( " +
                                            "SELECT * FROM mst_mastercodedata WHERE CodeGroup ='Gender' " +
                                            ") AS B ON B.CodeID COLLATE UTF8MB4_GENERAL_CI = A.Gender " +
                                            "LEFT JOIN Mst_Avatar AS C ON C.ID = A.AvatarID " + 
                                            //"WHERE A.PatientID = '" + patientID + "' ";
                                            " ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PetDataExtendedModel sPetObj = new PetDataExtendedModel();
                                sPetObj.ID = Convert.ToInt64(sReader["ID"]);
                                sPetObj.PatientID = Convert.ToInt64(sReader["PatientID"]);
                                sPetObj.Name = sReader["Name"].ToString();
                                sPetObj.RegistrationNo = sReader["RegistrationNo"].ToString();
                                sPetObj.Gender = sReader["GenderName"].ToString();
                                sPetObj.DOB = Convert.ToDateTime(sReader["DOB"]);

                                DateTime dtDOB = Convert.ToDateTime(sReader["DOB"]);
                                sPetObj.Age = DateTime.Now.Year - dtDOB.Year;
                                sPetObj.Species = sReader["Species"].ToString();
                                sPetObj.Breed = sReader["Breed"].ToString();
                                sPetObj.Color = sReader["Color"].ToString();
                                sPetObj.Allergies = sReader["Allergies"].ToString();
                                sPetObj.Weight = Convert.ToDecimal(sReader["Weight"]);
                                sPetObj.WeightUnit = sReader["WeightUnit"].ToString();
                                sPetObj.Height = Convert.ToDecimal(sReader["Height"]);
                                sPetObj.HeightUnit = sReader["HeightUnit"].ToString();
                                sPetObj.AvatarImage = sReader["AvatarImage"].ToString();
                                sPetObj.ColorCode = sReader["ColorCode"].ToString();

                                sResultList.Add(sPetObj);
                            }
                        }
                    }

                    sConn.Close();

                    return sResultList;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static PetDataExtendedModel GetPetProfileByID(int petID)
        {
            PetDataExtendedModel sPetObj = new PetDataExtendedModel();

            try
            {
                using (var ctx = new PetDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.*, B.CodeName AS 'GenderName', C.AvatarImage, C.ColorCode " +
                                            "FROM mst_pets AS A " +
                                            "LEFT JOIN ( " +
                                            "SELECT * FROM mst_mastercodedata WHERE CodeGroup ='Gender' " +
                                            ") AS B ON B.CodeID COLLATE UTF8MB4_GENERAL_CI = A.Gender " +
                                            "LEFT JOIN Mst_Avatar AS C ON C.ID = A.AvatarID " +
                                            "WHERE A.ID = '" + petID + "' ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                sPetObj.ID = Convert.ToInt64(sReader["ID"]);
                                sPetObj.PatientID = Convert.ToInt64(sReader["PatientID"]);
                                sPetObj.Name = sReader["Name"].ToString();
                                sPetObj.RegistrationNo = sReader["RegistrationNo"].ToString();
                                sPetObj.Gender = sReader["GenderName"].ToString();
                                sPetObj.DOB = Convert.ToDateTime(sReader["DOB"]);

                                DateTime dtDOB = Convert.ToDateTime(sReader["DOB"]);
                                sPetObj.Age = DateTime.Now.Year - dtDOB.Year;
                                sPetObj.Species = sReader["Species"].ToString();
                                sPetObj.Breed = sReader["Breed"].ToString();
                                sPetObj.Color = sReader["Color"].ToString();
                                sPetObj.Allergies = sReader["Allergies"].ToString();
                                sPetObj.Weight = Convert.ToDecimal(sReader["Weight"]);
                                sPetObj.WeightUnit = sReader["WeightUnit"].ToString();
                                sPetObj.Height = Convert.ToDecimal(sReader["Height"]);
                                sPetObj.HeightUnit = sReader["HeightUnit"].ToString();
                                sPetObj.AvatarImage = sReader["AvatarImage"].ToString();
                                sPetObj.ColorCode = sReader["ColorCode"].ToString();
                            }
                        }
                    }

                    sConn.Close();
                }

                return sPetObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<PetTreatmentServiceModel> GetPetTreatmentServices(int petID)
        {
            List<PetTreatmentServiceModel> sResultlist = new List<PetTreatmentServiceModel>();

            try
            {
                using (var ctx = new PetDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT T.ID AS 'ServiceID', T.Name AS 'ServiceName', T.CategoryName, " +
                                            "T.SubCategoryName, A.CreatedDate " + 
                                            "FROM txn_treatmentplan AS A " + 
                                            "INNER JOIN txn_treatmentplan_services AS B ON B.PlanID = A.ID " + 
                                            "INNER JOIN (" + 
                                            "SELECT T1.ID, T1.Name, T1.Description, T2.Name AS 'CategoryName', T2.SubCategoryName " + 
                                            "FROM mst_services AS T1 " +
                                            "INNER JOIN mst_servicescategory AS T2 ON T2.ID = T1.CategoryID " + 
                                            ") AS T ON T.ID = B.ServiceID " + 
                                            "WHERE A.PetID = '" + petID + "' " +
                                            "ORDER BY A.CreatedDate DESC";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PetTreatmentServiceModel sServiceObj = new PetTreatmentServiceModel();
                                sServiceObj.ServiceID = Convert.ToInt32(sReader["ServiceID"]);
                                sServiceObj.ServiceName = sReader["ServiceName"].ToString();
                                sServiceObj.CategoryName = sReader["CategoryName"].ToString();
                                sServiceObj.SubCategoryName = sReader["SubCategoryName"].ToString();
                                sServiceObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);

                                sResultlist.Add(sServiceObj);
                            }
                        }
                    }

                    sConn.Close();
                }

                return sResultlist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<PetMedicationModel> GetPetMedicationHistory(int petID)
        {
            List<PetMedicationModel> sResultList = new List<PetMedicationModel>();

            try
            {
                using (var ctx = new PetDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT A.PlanName, A.CreatedDate,  B.ProductID, B.ProductName, C.Usage, C.Description " +
                                            "FROM txn_treatmentplan AS A " +
                                            "INNER JOIN txn_treatmentplan_products AS B ON B.PlanID = A.ID " +
                                            "LEFT JOIN mst_product AS C ON C.ID = B.ProductID " +
                                            "WHERE A.PetID = '" + petID + "' " +
                                            "ORDER BY A.CreatedDate DESC ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PetMedicationModel sMedObj = new PetMedicationModel();
                                sMedObj.PlanName = sReader["PlanName"].ToString();
                                sMedObj.CreatedDate = Convert.ToDateTime(sReader["CreatedDate"]);
                                sMedObj.ProductID = Convert.ToInt32(sReader["ProductID"]);
                                sMedObj.ProductName = sReader["ProductName"].ToString();
                                sMedObj.Usage = sReader["Usage"].ToString();
                                sMedObj.Description = sReader["Description"].ToString();

                                sResultList.Add(sMedObj);
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

        public static List<PetTreatmentPlanModel> GetPetActiveTreatmentPlanHistory(int petID)
        {
            List<PetTreatmentPlanModel> sResultList = new List<PetTreatmentPlanModel>();

            try
            {
                using (var ctx = new PetDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT TreatmentPlanID, PlanName, TreatmentStart, TreatmentEnd, Remarks " +
                                            "FROM txn_treatmentplan " +
                                            "WHERE PetID = '" + petID + "' AND " +
                                            "(TreatmentStart <= NOW() AND TreatmentEnd >= NOW()) ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PetTreatmentPlanModel sTreatmentObj = new PetTreatmentPlanModel();
                                sTreatmentObj.TreatmentPlanID = Convert.ToInt32(sReader["TreatmentPlanID"]);
                                sTreatmentObj.PlanName = sReader["PlanName"].ToString();
                                sTreatmentObj.TreatmentStart = Convert.ToDateTime(sReader["TreatmentStart"]);
                                sTreatmentObj.TreatmentEnd = Convert.ToDateTime(sReader["TreatmentEnd"]);
                                sTreatmentObj.Remarks = sReader["Remarks"].ToString();

                                sResultList.Add(sTreatmentObj);
                                   
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

        public static List<PetTreatmentPlanModel> GetPetPastTreatmentPlanHistory(int petID)
        {
            List<PetTreatmentPlanModel> sResultList = new List<PetTreatmentPlanModel>();

            try
            {
                using (var ctx = new PetDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT TreatmentPlanID, PlanName, TreatmentStart, TreatmentEnd, Remarks " +
                                            "FROM txn_treatmentplan " +
                                            "WHERE PetID = '" + petID + "' AND " +
                                            "NOT (TreatmentStart <= NOW() AND TreatmentEnd >= NOW()) ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PetTreatmentPlanModel sTreatmentObj = new PetTreatmentPlanModel();
                                sTreatmentObj.TreatmentPlanID = Convert.ToInt32(sReader["TreatmentPlanID"]);
                                sTreatmentObj.PlanName = sReader["PlanName"].ToString();
                                sTreatmentObj.TreatmentStart = Convert.ToDateTime(sReader["TreatmentStart"]);
                                sTreatmentObj.TreatmentEnd = Convert.ToDateTime(sReader["TreatmentEnd"]);
                                sTreatmentObj.Remarks = sReader["Remarks"].ToString();

                                sResultList.Add(sTreatmentObj);

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

        public static List<PetTestResultsModel> GetPetTestResultHistory(int petID, int pageSize, int pageIndex, out int totalRecords)
        {
            List<PetTestResultsModel> sResultList = new List<PetTestResultsModel>();
            totalRecords = 0;

            try
            {
                using (var ctx = new PetDBContext())
                {
                    MySqlConnection sConn = new MySqlConnection(ctx.Database.GetConnectionString());
                    sConn.Open();

                    String sSelectCommand = "SELECT ROW_NUMBER() OVER (ORDER BY A.ResultDateTime) AS 'SeqNo', " +
                                            "A.ResultDateTime, A.ResultType, A.OverallStatus, A.PatientID, A.OperatorID, " +
                                            "A.DeviceName, COUNT(*) OVER() AS 'TotalRows' " +
                                            "FROM txn_testresults AS A " +
                                            "WHERE A.PetID = '" + petID + "' " +
                                            "ORDER BY A.ResultDateTime " +
                                            "LIMIT 20 ";

                    using (MySqlCommand sCommand = new MySqlCommand(sSelectCommand, sConn))
                    {
                        using (var sReader = sCommand.ExecuteReader())
                        {
                            while (sReader.Read())
                            {
                                PetTestResultsModel PetTestResultObj = new PetTestResultsModel();
                                PetTestResultObj.SeqNo = Convert.ToInt32(sReader["SeqNo"]);
                                PetTestResultObj.ResultDateTime = Convert.ToDateTime(sReader["ResultDateTime"]);
                                PetTestResultObj.ResultType = sReader["ResultType"].ToString();
                                PetTestResultObj.OverallStatus = sReader["OverallStatus"].ToString();
                                PetTestResultObj.PatientID = sReader["PatientID"].ToString();
                                PetTestResultObj.OperatorID = sReader["OperatorID"].ToString();
                                PetTestResultObj.DeviceName = sReader["DeviceName"].ToString();
                                PetTestResultObj.TotalRows = Convert.ToInt32(sReader["TotalRows"]);

                                totalRecords = PetTestResultObj.TotalRows.Value;

                                sResultList.Add(PetTestResultObj);
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
    }
}
