using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
    public class PatientServicesController : Controller
    {
		private readonly ServicesDBContext _servicesDBContext = new ServicesDBContext();
		private readonly OrganisationDBContext _organisationDBContext = new OrganisationDBContext();
		private readonly BranchDBContext _branchDBContext = new BranchDBContext();

		int totalServices;

		public IActionResult Index()
        {
            return View();
        }

		public IActionResult TreatmentPlanList()
		{
			return View();
		}

		public IActionResult ServiceList()
		{
			return View();
		}

		public IActionResult CreateNewTreatment()
		{
			return View();
		}

		public IActionResult CreateNewService()
		{
			ViewData["Organisation"] = _organisationDBContext.Mst_Organisation.ToList();
			ViewData["Category"] = _servicesDBContext.Mst_ServicesCategory.ToList();

			return View();
		}

		[Route("/PatientServices/ViewEditService/{type}/{serviceId}")]
		public IActionResult ViewEditService(string type, int serviceID)
		{
			ViewData["Organisation"] = _organisationDBContext.Mst_Organisation.ToList();
			ViewData["Category"] = _servicesDBContext.Mst_ServicesCategory.ToList();
			var serviceInfo = _servicesDBContext.Mst_Services.FirstOrDefault(x => x.ID == serviceID);
			ViewData["Service"] = serviceInfo;
			ViewData["Type"] = type;
			ViewData["OrganisationID"] = _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == serviceInfo.BranchID).OrganizationID;

			return View();
		}

		public string GetTreatmentPlanList()
		{
			return "";
		}

		public ServicesInfo GetServiceList(int rowLimit, int page, string search = "")
		{
			int start = (page - 1) * rowLimit;

			var serviceList = _servicesDBContext.GetServiceList(start, rowLimit, out totalServices, search).ToList();

			var servicesInfo = new ServicesInfo() { ServiceList = serviceList, totalServices = totalServices };

			return servicesInfo;
		}

		public List<BranchModel> GetBranchList(int organisationID)
		{
			var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).ToList();

			return branchList;
		}

		public bool InsertService([FromBody] ServicesModel servicesModel)
		{
			try
			{
				servicesModel.CreatedDate = DateTime.Now;
				servicesModel.CreatedBy = Request.Cookies["user"];

				_servicesDBContext.Mst_Services.Add(servicesModel);
				_servicesDBContext.SaveChanges();
			}
			catch (Exception ex) 
			{
				return false;
			}

			return true;
		}

		public bool UpdateService([FromBody] ServicesModel servicesModel)
		{
			try
			{
				servicesModel.UpdatedDate = DateTime.Now;
				servicesModel.UpdatedBy = Request.Cookies["user"];

				_servicesDBContext.Update(servicesModel);
				_servicesDBContext.SaveChanges();
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}
	}
}
