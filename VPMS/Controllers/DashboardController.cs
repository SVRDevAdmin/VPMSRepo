using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using VPMS;
using VPMS.Lib;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly PatientDBContext _patientDBContext = new PatientDBContext();
        private readonly UserDBContext _userDBContext = new UserDBContext();
        private readonly RoleDBContext _roleDBContext = new RoleDBContext();
        private readonly BranchDBContext _branchDBContext = new BranchDBContext();
        private readonly AppointmentDBContext _appointmentDBContext = new AppointmentDBContext(Host.CreateApplicationBuilder().Configuration);
        private readonly TransSummaryDBContext _transSummaryDBContext = new TransSummaryDBContext(Host.CreateApplicationBuilder().Configuration);
        private readonly DoctorDBContext _doctorDBContext = new DoctorDBContext(Host.CreateApplicationBuilder().Configuration);

        private readonly InvoiceReceiptDBContext _invoiceReceiptDBContext = new InvoiceReceiptDBContext();

        //public List<string> colorList = ["#e9e9eb", "#cbcfda", "#ebe3be", "#c4db67", "#b2f2e4", "#33f5d3", "#bcd2e0", "#59b1d7", "#7a9fbc", "#7a9fbc", "#d699a0", "#d96aa5", "#d3aac8", "#c15ae9", "#d6acfe", "#896bdf", "#6669db", "#681feb"];
        public List<string> colorList = ["#cbcfda", "#ebe3be", "#c4db67", "#b2f2e4", "#33f5d3", "#bcd2e0", "#59b1d7", "#7a9fbc", "#7a9fbc", "#d699a0", "#d96aa5", "#d3aac8", "#c15ae9", "#d6acfe", "#896bdf", "#6669db", "#681feb"];
        public List<string> fillColorList = ["rgba(233,233,235,0.5)", "rgba(203,207,218,0.5)", "rgba(235,227,190,0.5)", "rgba(196,219,103,0.5)", "rgba(178,242,228,0.5)", "rgba(51,245,211,0.5)", "rgba(185,208,222,0.5)", "rgba(89,177,215,0.5)", "rgba(122,159,188,0.5)", "rgba(59,128,197,0.5)", "rgba(214,153,160,0.5)", "rgba(217,106,165,0.5)", "rgba(211,170,200,0.5)", "rgba(193,90,233,0.5)", "rgba(214,172,254,0.5)", "rgba(137,107,223,0.5)", "rgba(102,105,219,0.5)", "rgba(104,31,235,0.5)"];
        public List<string> monthList = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };

        private class axis { public string x { get; set; }  public decimal? y { get; set; } }

        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }


        public IActionResult Dashboard()
        {
            var latestUpdateLog = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == "RevenueSummary").OrderByDescending(x => x.TransactionDate).FirstOrDefault();
            DateTime? latestUpdateDate;

            if(latestUpdateLog != null)
            {
                latestUpdateDate = latestUpdateLog.TransactionDate;
            }
            else
            {
                latestUpdateDate = DateTime.Now;
            }            

            int year = latestUpdateDate.Value.Year;
            int month = latestUpdateDate.Value.Month;
            int day = latestUpdateDate.Value.Day;
            var todayDate = DateTime.Now.Date;

            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var role = HttpContext.Session.GetString("RoleName");
            var branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
            var organisationID = int.Parse(HttpContext.Session.GetString("OrganisationID"));
            List<TransSummaryModel> SummaryList = _transSummaryDBContext.txn_transactionsummary.Where(x => x.DateInYear == year.ToString()).ToList();
            List<UserModel> userList = _userDBContext.Mst_User.ToList();
            var appointmentList = _appointmentDBContext.mst_appointment.Select(x => new { x.ApptDate, x.BranchID }).Where(y => y.ApptDate.Value.Year == year).ToList();
            var doctorList = _doctorDBContext.mst_doctor.ToList();

            if (roles.Contains("General.Superuser"))
            //if (role == "Superuser")
            {
                var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).Select(y => y.ID).ToList();
                SummaryList = SummaryList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
                userList = userList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
                appointmentList = appointmentList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
                doctorList = doctorList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
            }
            else if (!roles.Contains("General.Superadmin"))
            //else if (role != "Superadmin")
            {
                SummaryList = SummaryList.Where(x => x.BranchID == branchID).ToList();
                userList = userList.Where(x => x.BranchID == branchID).ToList();
                appointmentList = appointmentList.Where(x => x.BranchID == branchID).ToList();
                doctorList = doctorList.Where(x => x.BranchID == branchID).ToList();
            }

            var totalSales = SummaryList.Where(x => x.Group == "TotalRevenue" && x.SubGroup == "Total" && x.DateInMonth == month.ToString()).GroupBy(x => x.Group).Select(x => x.Sum(c => c.TotalAmount).Value).FirstOrDefault();
            var totalSaleString = string.Format("{0:#,##0.##}", totalSales);
            if (totalSales > 1000000000)
            {
                totalSaleString = Math.Round((totalSales / 1000000000), 2) + "B";
            }
            else if (totalSales > 1000000)
            {
                totalSaleString = Math.Round((totalSales / 1000000), 2) + "M";
            }

            var totalPatient = SummaryList.Where(x => x.Group == "TotalPatients").LastOrDefault();

            ViewData["Doctors"] = doctorList.Select(x => x.Name).ToList();
            ViewData["UpdatedOn"] = latestUpdateDate.Value.ToString("dd MMM yyyy");
            ViewData["TotalSales"] = totalSaleString;
            ViewData["TotalStaff"] = userList.Count;
            ViewData["TotalPatients"] = totalPatient == null ? 0 : string.Format("{0:#,##0.##}", totalPatient.TotalAmount);
            ViewData["TotalAppointment"] = appointmentList.Where(x => x.ApptDate.Value.Month == month).ToList().Count;

            Program.CurrentPage = "/Dashboard/Dashboard";

            return View();
        }

        public Dictionary<string, dynamic> GetPatientSummary()
        {
            var latestUpdateLog = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == "PatientSummary").OrderByDescending(x => x.TransactionDate).FirstOrDefault();
            DateTime? latestUpdateDate;

            if (latestUpdateLog != null)
            {
                latestUpdateDate = latestUpdateLog.TransactionDate;
            }
            else
            {
                latestUpdateDate = DateTime.Now;
            }

            var role = HttpContext.Session.GetString("RoleName");
            var branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
            var organisationID = int.Parse(HttpContext.Session.GetString("OrganisationID"));
            List<TransSummaryModel> SummaryList = _transSummaryDBContext.txn_transactionsummary.Where(x => x.Group == "PatientByBreed" && x.DateInYear == latestUpdateDate.Value.Year.ToString()).ToList();

            if (role == "Superuser")
            {
                var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).Select(y => y.ID).ToList();
                SummaryList = SummaryList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
            }
            else if (role != "Superadmin")
            {
                SummaryList = SummaryList.Where(x => x.BranchID == branchID).ToList();
            }

            
            var monthList = new List<string>() { "", "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"};

            Dictionary<string, dynamic> chartSetup = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> chart = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> dataset = new List<Dictionary<string, dynamic>>();
            List<Dictionary<string, dynamic>> legend = new List<Dictionary<string, dynamic>>();

            double tension = 0.5;
            //List<string> color = ["#9faabb", "#1e76fb", "#94bbf5"];
            //List<string> fillColor = ["rgba(159,170,187,0.5)", "rgba(30,118,251,0.5)", "rgba(148,187,245,0.5)"];
            int i = 0;
            int year = DateTime.Now.Year;

            var patientSummary = SummaryList.GroupBy(x => new { x.DateInMonth, x.SubGroup }).Select(x => new { x.Last().DateInMonth, x.Last().TotalAmount.Value, x.Last().SubGroup }).ToList();

            var breedList = patientSummary.Select(x => x.SubGroup).Distinct().ToList();

            //List<IEnumerable<Color>> colorList = new List<IEnumerable<Color>>();
            //colorList.Add(GetGradients(Color.DarkGray, Color.White, 2));
            //colorList.Add(GetGradients(Color.Blue, Color.White, 2));
            //colorList.Add(GetGradients(Color.Green, Color.White, 2));

            foreach (var breed in breedList) 
            {
                var dataByBreed = patientSummary.Where(x => x.SubGroup == breed);

                var axisData = new List<axis>();

                axisData.Add(new axis() { x = "", y = 0 });

                //foreach (var data in dataByBreed)
                //{
                //    //axisData.Add(new axis() { x = GetMonthString(int.Parse(data.DateInMonth) - 1), y = data.Value });
                //    axisData.Add(new axis() { x = monthList[int.Parse(data.DateInMonth)], y = data.Value });
                //}

                for(int j = 1; j <= 12; j++)
                {
                    var value = dataByBreed.FirstOrDefault(x => x.DateInMonth == j.ToString());
                    axisData.Add(new axis() { x = monthList[j], y = (value == null ? 0 : value.Value) });
                }

                dataset.Add(new Dictionary<string, dynamic>());
                legend.Add(new Dictionary<string, dynamic>());

                dataset[i].Add("label", breed);
                dataset[i].Add("data", axisData);
                dataset[i].Add("fill", false);
                dataset[i].Add("tension", tension);
                dataset[i].Add("borderColor", colorList[i]);
                dataset[i].Add("backgroundColor", fillColorList[i]);
                //dataset[i].Add("backgroundColor", colorList[i]);
                dataset[i].Add("spanGaps", true);

                legend[i].Add("label", breed);
                legend[i].Add("color", colorList[i]);

                i++;
            }

            chartSetup.Add("datasets", dataset);
            chartSetup.Add("labels", monthList);

            chart.Add("data", chartSetup);
            chart.Add("legend", legend);


            return chart;
        }

        public Dictionary<string, dynamic> GetRevenueSummary(string type)
        {
            var latestUpdateLog = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == "PatientSummary").OrderByDescending(x => x.TransactionDate).FirstOrDefault();
            DateTime? latestUpdateDate;

            if (latestUpdateLog != null)
            {
                latestUpdateDate = latestUpdateLog.TransactionDate;
            }
            else
            {
                latestUpdateDate = DateTime.Now;
            }

            Dictionary<string, dynamic> chart = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> dataset = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> borderRadius = new Dictionary<string, dynamic>();
            var labelList = new List<string>();
            //var latestUpdateDate = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == "RevenueSummary").OrderByDescending(x => x.TransactionDate).FirstOrDefault().TransactionDate;
            int year = latestUpdateDate.Value.Year;
            int month = latestUpdateDate.Value.Month;
            int day = latestUpdateDate.Value.Day;
            int week = Helpers.GetWeekOfMonth(latestUpdateDate.Value);
            var axisData = new List<axis>();

            var role = HttpContext.Session.GetString("RoleName");
            var branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
            var organisationID = int.Parse(HttpContext.Session.GetString("OrganisationID"));
            List<TransSummaryModel> SummaryList = _transSummaryDBContext.txn_transactionsummary.Where(x => x.Group == "TotalRevenue" && x.SubGroup == "Total").ToList();

            if (role == "Superuser")
            {
                var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).Select(y => y.ID).ToList();
                SummaryList = SummaryList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
            }
            else if (role != "Superadmin")
            {
                SummaryList = SummaryList.Where(x => x.BranchID == branchID).ToList();
            }

            if (type == "Yearly")
            {
                var revenueSummary = SummaryList.GroupBy(y => y.DateInYear).Select(x => new { x.First().DateInYear, x.Sum(c => c.TotalAmount).Value }).TakeLast(10).ToList();
                var yearList = revenueSummary.Select(x => x.DateInYear).ToList();
                int currentYear = 0;

                if(yearList.Count == 0)
                {
                    labelList = [year.ToString()];
                }
                else 
                {
                    List<string> tempYearList = new List<string>();

                    foreach (var yearNo in yearList)
                    {
                        if (currentYear != 0 && (int.Parse(yearNo) - currentYear) > 1)
                        {
                            var differ = int.Parse(yearNo) - currentYear;
                            int start = 1;

                            while (start < differ)
                            {
                                tempYearList.Add((currentYear + start).ToString());
                                start++;
                            }
                        }
                        
                        tempYearList.Add(yearNo);
                        currentYear = int.Parse(yearNo);
                    }

                    labelList = tempYearList.TakeLast(10).ToList();
                }

                foreach (var yearNo in labelList)
                {
                    var value = revenueSummary.FirstOrDefault(x => x.DateInYear == yearNo);
                    axisData.Add(new axis() { x = yearNo, y = (value == null ? 0 : value.Value) });
                }

                //if (revenueSummary.Count == 0)
                //{
                //    axisData.Add(new axis() { x = year.ToString(), y = 0 });
                //    labelList = [year.ToString()];
                //}
                //else
                //{
                //    //foreach (var item in revenueSummary)
                //    //{
                //    //    axisData.Add(new axis() { x = item.DateInYear, y = item.Value });
                //    //}

                //    //labelList = revenueSummary.Select(x => x.DateInYear).ToList();

                //    foreach (var yearNo in revenueSummary.Select(x => x.DateInYear).ToList())
                //    {
                //        if(currentYear != 0 && (int.Parse(yearNo) - currentYear) > 1)
                //        {
                //            var differ = int.Parse(yearNo) - currentYear;
                //            int start = 1;

                //            while(start < differ)
                //            {
                //                axisData.Add(new axis() { x = (currentYear + 1).ToString(), y = 0 });
                //                start++;
                //            }
                //        }

                //        var value = revenueSummary.First(x => x.DateInYear == yearNo);
                //        axisData.Add(new axis() { x = yearNo, y = value.Value });

                //        currentYear = int.Parse(yearNo);
                //    }
                //}

                
            }
            else if (type == "Monthly")
            {
                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString()).GroupBy(y => y.DateInMonth).Select(x => new { x.First().DateInMonth, x.Sum(c => c.TotalAmount).Value }).ToList();

                //foreach (var item in revenueSummary)
                //{
                //    axisData.Add(new axis() { x = GetMonthString(int.Parse(item.DateInMonth) - 1), y = item.Value });
                //}

                for (int i = 1; i <= 12; i++)
                {
                    var value = revenueSummary.FirstOrDefault(x => x.DateInMonth == i.ToString());
                    axisData.Add(new axis() { x = monthList[i - 1], y = (value == null ? 0 : value.Value) });
                }

                labelList = monthList;
            }
            else if (type == "Weekly")
            {
                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString() && x.DateInMonth == month.ToString()).GroupBy(y => y.Week).Select(x => new { x.First().Week, x.Sum(c => c.TotalAmount).Value }).ToList();

                //foreach (var item in revenueSummary)
                //{
                //    axisData.Add(new axis() { x = "Week " + item.Week, y = item.Value });

                //    //labelList.Add("Week " + item.Week);
                //}

                foreach (var weekNo in getWeeksInMonth(latestUpdateDate.Value))
                {
                    var value = revenueSummary.FirstOrDefault(x => x.Week == int.Parse(weekNo.Replace("Week ", "")));
                    axisData.Add(new axis() { x = weekNo, y = (value == null ? 0 : value.Value) });
                }

                labelList = getWeeksInMonth(latestUpdateDate.Value);
            }
            else if (type == "Daily")
            {
                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString() && x.DateInMonth == month.ToString() && x.Week == week).GroupBy(y => y.SummaryDate).Select(x => new { x.First().SummaryDate, x.Sum(c => c.TotalAmount).Value }).ToList();

                //if (revenueSummary.Count == 0)
                //{
                //    //axisData.Add(new axis() { x = latestUpdateDate.Value.ToString("dd/M/yyyy"), y = 0});

                //    foreach (var date in getDaysInWeek(latestUpdateDate.Value))
                //    {
                //        var value = revenueSummary.FirstOrDefault(x => x.SummaryDate.Value.ToString("dd/M/yyyy") == date);
                //        //axisData.Add(new axis() { x = date, y = (revenueSummary.FirstOrDefault(x => x.SummaryDate.Value.ToString("dd/M/yyyy") == date).Value) });

                //        //labelList.Add(item.SummaryDate.Value.ToString("dd/M/yyyy"));
                //    }
                //}
                //else
                //{
                //    foreach (var item in revenueSummary)
                //    {
                //        axisData.Add(new axis() { x = item.SummaryDate.Value.ToString("dd/M/yyyy"), y = item.Value });

                //        //labelList.Add(item.SummaryDate.Value.ToString("dd/M/yyyy"));
                //    }
                //}

                foreach (var date in getDaysInWeek(latestUpdateDate.Value))
                {
                    var value = revenueSummary.FirstOrDefault(x => x.SummaryDate.Value.ToString("dd/M/yyyy") == date);
                    axisData.Add(new axis() { x = date, y = (value == null ? 0 : value.Value) });

                    //labelList.Add(item.SummaryDate.Value.ToString("dd/M/yyyy"));
                }

                labelList = getDaysInWeek(latestUpdateDate.Value);
            }
            else
            {
                var quarter = int.Parse(type.Replace("Q",""));

                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString() && x.Quarter == quarter).GroupBy(y => y.Quarter).Select(x => new { x.First().Quarter, x.Sum(c => c.TotalAmount).Value }).ToList();

                if(revenueSummary.Count > 0)
                {
                    foreach (var item in revenueSummary)
                    {
                        axisData.Add(new axis() { x = "Q " + item.Quarter, y = item.Value });

                        labelList.Add("Q " + item.Quarter);
                    }
                }
                else
                {
                    axisData.Add(new axis() { x = "Q " + quarter, y = 0 });
                    labelList = ["Q " + quarter];
                }

            }



            borderRadius.Add("topLeft",10);
            borderRadius.Add("topRight", 10);
            borderRadius.Add("bottomLeft", 0);
            borderRadius.Add("bottomRight", 0);

            dataset.Add(new Dictionary<string, dynamic>());
            dataset[0].Add("data", axisData);
            dataset[0].Add("backgroundColor", "#e9f2fd");
            dataset[0].Add("hoverBackgroundColor", "#5e8cd1");
            dataset[0].Add("barPercentage", 1.2);
            dataset[0].Add("borderRadius", borderRadius);

            
            chart.Add("datasets", dataset);
            chart.Add("labels", labelList);

            return chart;
        }

        public Dictionary<string, dynamic> GetStaffSummary()
        {
            List<string> genderList = ["Male", "Female"];
            List<string> roleList = new List<string>();
            List<string> backgroundColor = ["#1d76fb", "#5e8cd1"];
            //List<string> staffBackgroundColor = ["#1E88E5", "#42A5F5", "#90CAF9", "#B0BEC5"];
            List<int> genderBreakdown = new List<int>();
            List<int> roleBreakdown = new List<int>();
            Dictionary<string, dynamic> chartList = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> staffChart = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> genderChart = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> staffDataset = new List<Dictionary<string, dynamic>>();
            List<Dictionary<string, dynamic>> genderDataset = new List<Dictionary<string, dynamic>>();
            var role = HttpContext.Session.GetString("RoleName");
            var branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
            var organisationID = int.Parse(HttpContext.Session.GetString("OrganisationID"));

            var userList = _userDBContext.Mst_User.ToList();

            if (role == "Superuser")
            {
                var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).Select(y => y.ID).ToList();
                userList = userList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
            }
            else if (role != "Superadmin")
            {
                userList = userList.Where(x => x.BranchID == branchID).ToList();
            }

            var staffRole = userList.GroupBy(x => x.RoleID).Select(y => new { y.First().RoleID, y.ToList().Count });

            foreach (var staff in staffRole)
            {
                roleList.Add(_roleDBContext.Mst_Roles.FirstOrDefault(x => x.RoleID == staff.RoleID).RoleName);
                roleBreakdown.Add(staff.Count);
            }

            staffDataset.Add(new Dictionary<string, dynamic>());
            staffDataset[0].Add("backgroundColor", colorList);
            staffDataset[0].Add("data", roleBreakdown);

            staffChart.Add("labels", roleList);
            staffChart.Add("datasets", staffDataset);

            genderBreakdown.Add(userList.Where(x => x.Gender == "M").ToList().Count);
            genderBreakdown.Add(userList.Where(x => x.Gender == "F").ToList().Count);

            genderDataset.Add(new Dictionary<string, dynamic>());
            genderDataset[0].Add("backgroundColor", backgroundColor);
            genderDataset[0].Add("data", genderBreakdown);

            genderChart.Add("labels", genderList);
            genderChart.Add("datasets", genderDataset);

            chartList.Add("gender", genderChart);
            chartList.Add("staff", staffChart);

            return chartList;


        }

        public Dictionary<string, dynamic> GetDoughnutSummary(string group, string type)
        {
            string transactionType = "";

            if(group == "PatientByBreed")
            {
                transactionType = "PatientSummary";
            }
            else
            {
                transactionType = "RevenueSummary";
            }

            var latestUpdateLog = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == transactionType).OrderByDescending(x => x.TransactionDate).FirstOrDefault();
            DateTime? latestUpdateDate;

            if (latestUpdateLog != null)
            {
                latestUpdateDate = latestUpdateLog.TransactionDate;
            }
            else
            {
                latestUpdateDate = DateTime.Now;
            }

            Dictionary<string, dynamic> chart = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> dataset = new List<Dictionary<string, dynamic>>();
            int year = latestUpdateDate.Value.Year;
            int month = latestUpdateDate.Value.Month;
            int day = latestUpdateDate.Value.Day;
            int week = Helpers.GetWeekOfMonth(latestUpdateDate.Value);
            var patientList = new List<TransSummaryModel>();
            //List<string> color = ["#9faabb", "#1e76fb", "#94bbf5"];

            var role = HttpContext.Session.GetString("RoleName");
            var branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
            var organisationID = int.Parse(HttpContext.Session.GetString("OrganisationID"));
            List<TransSummaryModel> SummaryList = _transSummaryDBContext.txn_transactionsummary.Where(x => x.Group == group).ToList();

            if (role == "Superuser")
            {
                var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).Select(y => y.ID).ToList();
                SummaryList = SummaryList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
            }
            else if (role != "Superadmin")
            {
                SummaryList = SummaryList.Where(x => x.BranchID == branchID).ToList();
            }

            if (type == "Yearly")
            {
                patientList = SummaryList.Where(x => x.DateInYear == year.ToString()).ToList();
            }
            else if (type == "Monthly")
            {
                patientList = SummaryList.Where(x => x.DateInYear == year.ToString() && x.DateInMonth == month.ToString()).ToList();
            }
            else if (type == "Weekly")
            {
                patientList = SummaryList.Where(x => x.DateInYear == year.ToString() && x.DateInMonth == month.ToString() && x.Week == week).ToList();
            }
            else if (type == "Daily") {
                patientList = SummaryList.Where(x => x.DateInYear == year.ToString() && x.DateInMonth == month.ToString() && x.SummaryDate.Value.Day == day).ToList();
            }
            else
            {
                var quarter = int.Parse(type.Replace("Q", ""));

                patientList = SummaryList.Where(x => x.DateInYear == year.ToString() && x.Quarter == quarter).ToList();
            }

            var patientSummary = patientList.GroupBy(x => x.SubGroup).Select(x => new { x.Sum(c => c.TotalAmount).Value, x.First().SubGroup }).ToList();
            if (group == "PatientByBreed")
            {
                patientSummary = patientList.GroupBy(x => x.SubGroup).Select(x => new { x.Last().TotalAmount.Value, x.Last().SubGroup }).ToList();
            }

            dataset.Add(new Dictionary<string, dynamic>());
            dataset[0].Add("data", patientSummary.OrderBy(x => x.SubGroup).Select(x => x.Value));
            dataset[0].Add("backgroundColor", colorList);

            chart.Add("labels", patientSummary.OrderBy(x => x.SubGroup).Select(x => x.SubGroup));
            chart.Add("datasets", dataset);

            return chart;
        }

        public List<UpcomingAppointment> GetUpcomingAppointment(string doctor)
        {
            var role = HttpContext.Session.GetString("RoleName");
            var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
            var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

            //List<UpcomingAppointment> test = AppointmentRepository.GetTodayUpcomingAppointmentList(Host.CreateApplicationBuilder().Configuration, doctor, organisation, branch);
            //test.AddRange(test);
            //test.AddRange(test);

            //return test;
            return AppointmentRepository.GetTodayUpcomingAppointmentList(Host.CreateApplicationBuilder().Configuration, doctor, organisation, branch);
        }

        private string GetMonthString(int month)
        {
            var monthList = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
            return monthList[month];
        }

        private IEnumerable<Color> GetGradients(Color start, Color end, int steps)
        {
            Color stepper = Color.FromArgb((byte)((end.A - start.A) / (steps - 1)),
                                           (byte)((end.R - start.R) / (steps - 1)),
                                           (byte)((end.G - start.G) / (steps - 1)),
                                           (byte)((end.B - start.B) / (steps - 1)));

            for (int i = 0; i < steps; i++)
            {
                yield return Color.FromArgb(start.A + (stepper.A * i),
                                            start.R + (stepper.R * i),
                                            start.G + (stepper.G * i),
                                            start.B + (stepper.B * i));
            }
        }

        private List<string> getDaysInWeek(DateTime date)
        {
            List<string> dayList = new List<string>();
            int week = Helpers.GetWeekOfMonth(date);
            DateTime startingDay = date;
            DateTime endingDay = date;

            while (true)
            {
                if (Helpers.GetWeekOfMonth(startingDay.AddDays(-1)) != week)
                {
                    break;
                }
                else
                {
                    startingDay = startingDay.AddDays(-1);
                }
            }

            while (true)
            {
                if (Helpers.GetWeekOfMonth(endingDay.AddDays(1)) != week)
                {
                    break;
                }
                else
                {
                    endingDay = endingDay.AddDays(1);
                }
            }

            while (true)
            {
                if (startingDay != endingDay)
                {
                    dayList.Add(startingDay.ToString("dd/M/yyyy"));
                    startingDay = startingDay.AddDays(1);
                }
                else
                {
                    dayList.Add(startingDay.ToString("dd/M/yyyy"));
                    break;
                }
            }

            return dayList;

        }

        private List<string> getWeeksInMonth(DateTime date)
        {
            List<string> weekList = new List<string>();
            var currentDay = date.Day;
            var nextMont = date.AddMonths(1);
            var lastDayOfMonth = nextMont.AddDays(0 - currentDay);
            var lastWeekOfMonth = Helpers.GetWeekOfMonth(lastDayOfMonth);

            for(int i = 1; i <= lastWeekOfMonth; i++)
            {
                weekList.Add("Week " + i);
            }

            return weekList;
        }
    }
}
