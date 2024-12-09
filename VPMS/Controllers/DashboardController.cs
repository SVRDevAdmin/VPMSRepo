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

        private class axis { public string x { get; set; }  public decimal? y { get; set; } }

        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }


        public IActionResult Dashboard()
        {
            var latestUpdateDate = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == "RevenueSummary").OrderByDescending(x => x.TransactionDate).FirstOrDefault().TransactionDate;

            int year = latestUpdateDate.Value.Year;
            int month = latestUpdateDate.Value.Month;
            int day = latestUpdateDate.Value.Day;
            var todayDate = DateTime.Now.Date;

            var role = HttpContext.Session.GetString("RoleName");
            var branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
            var organisationID = int.Parse(HttpContext.Session.GetString("OrganisationID"));
            List<TransSummaryModel> SummaryList = _transSummaryDBContext.txn_transactionsummary.Where(x => x.DateInYear == year.ToString()).ToList();
            List<UserModel> userList = _userDBContext.Mst_User.ToList();
            var appointmentList = _appointmentDBContext.mst_appointment.Select(x => new { x.ApptDate, x.BranchID }).Where(y => y.ApptDate.Value.Year == year).ToList();

            if (role == "Superuser")
            {
                var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).Select(y => y.ID).ToList();
                SummaryList = SummaryList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
                userList = userList.Where(x => branchList.Contains(x.BranchID)).ToList();
                appointmentList = appointmentList.Where(x => branchList.Contains(x.BranchID.Value)).ToList();
            }
            else if (role != "Superadmin")
            {
                SummaryList = SummaryList.Where(x => x.BranchID == branchID).ToList();
                userList = userList.Where(x => x.BranchID == branchID).ToList();
                appointmentList = appointmentList.Where(x => x.BranchID == branchID).ToList();
            }

            ViewData["UpdatedOn"] = latestUpdateDate.Value.ToString("dd MMM yyyy");
            ViewData["TotalSales"] = SummaryList.Where(x => x.Group == "TotalRevenue" && x.SubGroup == "Total").GroupBy(x => x.Group).Select(x => x.Sum(c => c.TotalAmount).Value).FirstOrDefault();
            ViewData["TotalStaff"] = userList.Count;
            ViewData["TotalPatients"] = SummaryList.Where(x => x.Group == "TotalPatients").GroupBy(x => x.Group).Select(x => x.Sum(c => c.TotalAmount).Value).FirstOrDefault();
            ViewData["TotalAppointment"] = appointmentList.Count;

            Program.CurrentPage = "/Dashboard/Dashboard";

            return View();
        }

        public Dictionary<string, dynamic> GetPatientSummary()
        {
            var latestUpdateDate = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == "PatientSummary").OrderByDescending(x => x.TransactionDate).FirstOrDefault().TransactionDate;
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

            
            var monthList = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };

            Dictionary<string, dynamic> chartSetup = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> chart = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> dataset = new List<Dictionary<string, dynamic>>();
            List<Dictionary<string, dynamic>> legend = new List<Dictionary<string, dynamic>>();

            double tension = 0.5;
            List<string> color = ["#9faabb", "#1e76fb", "#94bbf5"];
            List<string> fillColor = ["rgba(159,170,187,0.5)", "rgba(30,118,251,0.5)", "rgba(148,187,245,0.5)"];
            int i = 0;
            int year = DateTime.Now.Year;

            var patientSummary = SummaryList.GroupBy(x => new { x.DateInMonth, x.SubGroup }).Select(x => new { x.First().DateInMonth, x.Sum(c => c.TotalAmount).Value, x.First().SubGroup }).ToList();

            var breedList = patientSummary.Select(x => x.SubGroup).Distinct().ToList();

            List<IEnumerable<Color>> colorList = new List<IEnumerable<Color>>();
            colorList.Add(GetGradients(Color.DarkGray, Color.White, 2));
            colorList.Add(GetGradients(Color.Blue, Color.White, 2));
            colorList.Add(GetGradients(Color.Green, Color.White, 2));

            foreach (var breed in breedList) 
            {
                var dataByBreed = patientSummary.Where(x => x.SubGroup == breed);

                var axisData = new List<axis>();

                foreach (var data in dataByBreed)
                {
                    //axisData.Add(new axis() { x = GetMonthString(int.Parse(data.DateInMonth) - 1), y = data.Value });
                    axisData.Add(new axis() { x = monthList[int.Parse(data.DateInMonth) - 1], y = data.Value });
                }

                dataset.Add(new Dictionary<string, dynamic>());
                legend.Add(new Dictionary<string, dynamic>());

                dataset[i].Add("label", breed);
                dataset[i].Add("data", axisData);
                dataset[i].Add("fill", true);
                dataset[i].Add("tension", tension);
                dataset[i].Add("borderColor", color[i]);
                dataset[i].Add("backgroundColor", fillColor[i]);
                //dataset[i].Add("backgroundColor", colorList[i]);
                dataset[i].Add("spanGaps", true);

                legend[i].Add("label", breed);
                legend[i].Add("color", color[i]);

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
            Dictionary<string, dynamic> chart = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> dataset = new List<Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> borderRadius = new Dictionary<string, dynamic>();
            var labelList = new List<string>();
            var latestUpdateDate = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == "RevenueSummary").OrderByDescending(x => x.TransactionDate).FirstOrDefault().TransactionDate;
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
                var revenueSummary = SummaryList.GroupBy(y => y.DateInYear).Select(x => new { x.First().DateInYear, x.Sum(c => c.TotalAmount).Value }).ToList();

                foreach (var item in revenueSummary)
                {
                    axisData.Add(new axis() { x = item.DateInYear, y = item.Value });
                }

                labelList = revenueSummary.Select(x => x.DateInYear).ToList();
            }
            else if (type == "Monthly")
            {
                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString()).GroupBy(y => y.DateInMonth).Select(x => new { x.First().DateInMonth, x.Sum(c => c.TotalAmount).Value }).ToList();

                foreach (var item in revenueSummary)
                {
                    axisData.Add(new axis() { x = GetMonthString(int.Parse(item.DateInMonth) - 1), y = item.Value });
                }

                labelList = ["Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"];
            }
            else if (type == "Weekly")
            {
                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString() && x.DateInMonth == month.ToString()).GroupBy(y => y.Week).Select(x => new { x.First().Week, x.Sum(c => c.TotalAmount).Value }).ToList();

                foreach (var item in revenueSummary)
                {
                    axisData.Add(new axis() { x = "Week " + item.Week, y = item.Value });

                    labelList.Add("Week " + item.Week);
                }
            }
            else if (type == "Daily")
            {
                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString() && x.DateInMonth == month.ToString() && x.Week == week).GroupBy(y => y.SummaryDate).Select(x => new { x.First().SummaryDate, x.Sum(c => c.TotalAmount).Value }).ToList();

                foreach (var item in revenueSummary)
                {
                    axisData.Add(new axis() { x = item.SummaryDate.Value.ToString("dd/M/yyyy") , y = item.Value });

                    labelList.Add(item.SummaryDate.Value.ToString("dd/M/yyyy"));
                }
            }
            else
            {
                var quarter = int.Parse(type.Replace("Q",""));

                var revenueSummary = SummaryList.Where(x => x.DateInYear == year.ToString() && x.Quarter == quarter).GroupBy(y => y.Quarter).Select(x => new { x.First().Quarter, x.Sum(c => c.TotalAmount).Value }).ToList();

                foreach (var item in revenueSummary)
                {
                    axisData.Add(new axis() { x = "Q " + item.Quarter, y = item.Value });

                    labelList.Add("Q " + item.Quarter);
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
            List<string> staffBackgroundColor = ["#1E88E5", "#42A5F5", "#90CAF9", "#B0BEC5"];
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
                userList = userList.Where(x => branchList.Contains(x.BranchID)).ToList();
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
            staffDataset[0].Add("backgroundColor", staffBackgroundColor);
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

            var latestUpdateDate = _transSummaryDBContext.txn_transactionsummarylog.Where(x => x.TransactionType == transactionType).OrderByDescending(x => x.TransactionDate).FirstOrDefault().TransactionDate;

            Dictionary<string, dynamic> chart = new Dictionary<string, dynamic>();
            List<Dictionary<string, dynamic>> dataset = new List<Dictionary<string, dynamic>>();
            int year = latestUpdateDate.Value.Year;
            int month = latestUpdateDate.Value.Month;
            int day = latestUpdateDate.Value.Day;
            int week = Helpers.GetWeekOfMonth(latestUpdateDate.Value);
            var patientList = new List<TransSummaryModel>();
            List<string> color = ["#9faabb", "#1e76fb", "#94bbf5"];

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

            dataset.Add(new Dictionary<string, dynamic>());
            dataset[0].Add("data", patientSummary.OrderBy(x => x.SubGroup).Select(x => x.Value));
            dataset[0].Add("backgroundColor", color);

            chart.Add("labels", patientSummary.OrderBy(x => x.SubGroup).Select(x => x.SubGroup));
            chart.Add("datasets", dataset);

            return chart;
        }

        public List<UpcomingAppointment> GetUpcomingAppointment(string doctor)
        {
            var role = HttpContext.Session.GetString("RoleName");
            var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
            var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

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
    }
}
