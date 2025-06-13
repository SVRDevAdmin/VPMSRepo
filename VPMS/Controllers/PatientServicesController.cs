using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using VPMS;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;
using VPMSWeb.Lib.Settings;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class PatientServicesController : Controller
    {
		private readonly ServicesDBContext _servicesDBContext = new ServicesDBContext();
		private readonly InventoryDBContext _inventoryDBContext = new InventoryDBContext();
		private readonly OrganisationDBContext _organisationDBContext = new OrganisationDBContext();
		private readonly BranchDBContext _branchDBContext = new BranchDBContext();
		private readonly DoctorDBContext _doctorDBContext = new DoctorDBContext(ConfigSettings.GetConfigurationSettings());
		private readonly TreatmentPlanDBContext _treatmentPlanDBContext = new TreatmentPlanDBContext();
		private readonly UserDBContext _userDBContext = new UserDBContext();

		int totalServices;
		int totalTreatmentPlan;

		public IActionResult Index()
        {
            return View();
        }

		public IActionResult TreatmentPlanList()
        {
            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "TreatmentListing.View", out branch, out organisation);

            if (!havaPermission)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

            SetTreatmentPlanPermission(roles);

            return View();
		}

		public IActionResult ServiceList()
		{
            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "ServiceListing.View", out branch, out organisation);

			if (!havaPermission)
			{
                return RedirectToAction("AccessDenied", "Login");
            }

            SetPermission(roles);

            return View();
		}

		public IActionResult CreateNewTreatment()
		{
            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "TreatmentPlan.Add", out branch, out organisation);

            if (!havaPermission)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

			ViewData["OrganizationList"] = _organisationDBContext.Mst_Organisation.Where(x => x.Level != 0 && x.Level != 1 && x.Status == 1).ToList();
            ViewData["Services"] = _servicesDBContext.Mst_Services.ToList();
			ViewData["Inventories"] = _inventoryDBContext.Mst_Product.ToList();
			ViewData["UserList"] = _userDBContext.Mst_User.Where(x => x.BranchID == int.Parse(HttpContext.Session.GetString("BranchID"))).ToList();

			return View();
		}

		public IActionResult CreateNewService()
		{
            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "Service.Add", out branch, out organisation);

            if (!havaPermission)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

            ViewData["Organisation"] = _organisationDBContext.Mst_Organisation.Where(x => x.Level != 0 && x.Level != 1).ToList();
			ViewData["Category"] = _servicesDBContext.Mst_ServicesCategory.ToList();
			ViewData["DoctorList"] = _doctorDBContext.mst_doctor.ToList();

			return View();
		}

		[Route("/PatientServices/ViewEditTreatmentPlan/{type}/{treatmentPlanId}")]
		public IActionResult ViewEditTreatmentPlan(string type, int treatmentPlanId)
		{
            List<int> treatmentPlanList = new List<int>();

            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var permission = (type == "Edit") ? "TreatmentPlan.Edit" : "TreatmentPlan.View";
            var havaPermission = hasPermission(roles, permission, out branch, out organisation);

			//var role = HttpContext.Session.GetString("RoleName");
			//var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
			//var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

			if (roles.Contains("General.Superadmin"))
			{
				treatmentPlanList = _treatmentPlanDBContext.Mst_TreatmentPlan.Select(y => y.ID).ToList();
			}
			else if (organisation != 0)
			{
				List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
				treatmentPlanList = _treatmentPlanDBContext.Mst_TreatmentPlan.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();
			}
			else if (branch != 0)
			{
				treatmentPlanList = _treatmentPlanDBContext.Mst_TreatmentPlan.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
			}

			if (!treatmentPlanList.Contains(treatmentPlanId))
			{
				return RedirectToAction("TreatmentPlanList", "PatientServices");
			}

			var sTreatmentPlanObj = _treatmentPlanDBContext.GetTreatmentPlanByPlanID(treatmentPlanId);
			if (sTreatmentPlanObj != null)
			{
				ViewData["TreatmentPlan"] = sTreatmentPlanObj;
            }
            //var treatmentInfo = _treatmentPlanDBContext.Mst_TreatmentPlan.FirstOrDefault(x => x.ID == treatmentPlanId);
            //ViewData["TreatmentPlan"] = treatmentInfo;
            ViewData["OrganizationList"] = _organisationDBContext.Mst_Organisation.Where(x => x.Level != 0 && x.Level != 1 && x.Status == 1).ToList();
            ViewData["Services"] = _servicesDBContext.Mst_Services.ToList();
			ViewData["Inventories"] = _inventoryDBContext.Mst_Product.ToList();
			ViewData["Type"] = type;
			ViewData["UserList"] = _userDBContext.Mst_User.Where(x => x.BranchID == int.Parse(HttpContext.Session.GetString("BranchID"))).ToList();
			return View();
		}

		[Route("/PatientServices/ViewEditService/{type}/{serviceId}")]
		public IActionResult ViewEditService(string type, int serviceID)
		{
			List<int> serviceList = new List<int>();
            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var permission = (type == "Edit") ? "ServiceDetails.Edit" : "ServiceDetails.View";
            var havaPermission = hasPermission(roles, permission, out branch, out organisation);

			//var role = HttpContext.Session.GetString("RoleName");
			//var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
			//var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

			if (roles.Contains("General.Superadmin"))
			{
				serviceList = _servicesDBContext.Mst_Services.Select(y => y.ID).ToList();
			}
			else if (organisation != 0)
			{
				List<int> branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisation).Select(y => y.ID).ToList();
				serviceList = _servicesDBContext.Mst_Services.Where(x => branchList.Contains(x.BranchID)).Select(y => y.ID).ToList();

			}
			else if (branch != 0)
			{
				serviceList = _servicesDBContext.Mst_Services.Where(x => x.BranchID == branch).Select(y => y.ID).ToList();
			}

			if (!serviceList.Contains(serviceID))
			{
				return RedirectToAction("ServiceList", "PatientServices");
			}

			ViewData["DoctorList"] = _doctorDBContext.mst_doctor.ToList();
			ViewData["Organisation"] = _organisationDBContext.Mst_Organisation.Where(x => x.Level != 0 && x.Level != 1).ToList();
			ViewData["Category"] = _servicesDBContext.Mst_ServicesCategory.ToList();
			var serviceInfo = _servicesDBContext.Mst_Services.FirstOrDefault(x => x.ID == serviceID);
			ViewData["Service"] = serviceInfo;
			ViewData["Type"] = type;
			ViewData["OrganisationID"] = _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == serviceInfo.BranchID).OrganizationID;

			return View();
		}

		public TreatmentPlanInfos GetTreatmentPlanList(int rowLimit, int page, string search = "")
		{
			int iOrganizationID = Convert.ToInt32(HttpContext.Session.GetString("OrganisationID"));
			int iBranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));

            int start = (page - 1) * rowLimit;
			var treatmentPlansInfos = new TreatmentPlanInfos() { 
										totalTreatmentPlan = 0, 
										treatmentPlans = new List<TreatmentPlanExtendedModel>() 
									  };

            //var role = HttpContext.Session.GetString("RoleName");
            //var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
            //var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

            //if (role != "User")
            //{
            //    var treatmentPlanList = _treatmentPlanDBContext.GetTreatmentPlanList(start, rowLimit, branch, organisation, out totalTreatmentPlan, search).ToList();

            //    treatmentPlansInfos = new TreatmentPlanInfos() { treatmentPlans = treatmentPlanList, totalTreatmentPlan = totalTreatmentPlan };
            //}

            //var treatmentPlanList = _treatmentPlanDBContext.GetTreatmentPlanList(start, rowLimit, 0, 0, out totalTreatmentPlan, search).ToList();
            //treatmentPlansInfos = new TreatmentPlanInfos() { treatmentPlans = treatmentPlanList, totalTreatmentPlan = totalTreatmentPlan };

            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "TreatmentListing.View", out branch, out organisation);

            int isSuperadmin = 0;
			int roleIsAdmin = 0;
            roleIsAdmin = Convert.ToInt32(HttpContext.Session.GetString("IsAdmin"));
            var organizationObj = OrganizationRepository.GetOrganizationByID(iOrganizationID);
            if (organizationObj != null)
            {
                if (organizationObj.Level == 0 || organizationObj.Level == 1 || (organizationObj.Level == 2 && roleIsAdmin == 1))
                {
                    isSuperadmin = 1;
                }
                else
                {
                    isSuperadmin = 0;
                }
            }

            if (havaPermission) 
			{
                var treatmentPlanList = _treatmentPlanDBContext.GetTreatmentPlanList(start, rowLimit, isSuperadmin, iBranchID, iOrganizationID, out totalTreatmentPlan, search).ToList();
                treatmentPlansInfos = new TreatmentPlanInfos() { treatmentPlans = treatmentPlanList, totalTreatmentPlan = totalTreatmentPlan };
            }


            return treatmentPlansInfos;
		}

		public TreatmentPlanModel GetTreatmentPlanByID(int planID)
		{
			return _treatmentPlanDBContext.Mst_TreatmentPlan.FirstOrDefault(x => x.ID == planID);
		}

		public List<TreatmentPlanService> GetTreatmentPlanServicesList(int planID)
		{
			var treatmentPlanServiceList = _treatmentPlanDBContext.Mst_TreatmentPlan_Services.Where(x => x.PlanID == planID && x.IsDeleted == 0).ToList();
			return treatmentPlanServiceList;
		}

		public List<TreatmentPlanProduct> GetTreatmentPlanProductsList(int planID)
		{
			var treatmentPlanProductList = _treatmentPlanDBContext.Mst_TreatmentPlan_Products.Where(x => x.PlanID == planID && x.IsDeleted == 0).ToList();
			return treatmentPlanProductList;
		}

		public List<ServicesModel> GetServiceByCategory(int categoryID) 
		{
			return _servicesDBContext.Mst_Services.Where(x => x.CategoryID == categoryID).ToList();
		}

		public ServicesModel GetServiceById(int id)
		{
			return _servicesDBContext.Mst_Services.FirstOrDefault(x => x.ID == id);
		}

		public ServicesInfo GetServiceList(int rowLimit, int page, string search = "")
		{
			int start = (page - 1) * rowLimit;
            ServicesInfo servicesInfo = new ServicesInfo() { ServiceList = new List<ServiceList>(), totalServices = 0 };

            //var role = HttpContext.Session.GetString("RoleName");
            //var branch = (role == "Doctor" || role == "Clinic Admin") ? int.Parse(HttpContext.Session.GetString("BranchID")) : 0;
            //var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

            //if (role != "User")
            //{
            //    var serviceList = _servicesDBContext.GetServiceList(start, rowLimit, branch, organisation, out totalServices, search).ToList();

            //    servicesInfo = new ServicesInfo() { ServiceList = serviceList, totalServices = totalServices };
            //}

            //var serviceList = _servicesDBContext.GetServiceList(start, rowLimit, 0, 0, out totalServices, search).ToList();
            //servicesInfo = new ServicesInfo() { ServiceList = serviceList, totalServices = totalServices };

            var branch = 0;
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "ServiceListing.View", out branch, out organisation);

            if (havaPermission)
            {
                var serviceList = _servicesDBContext.GetServiceList(start, rowLimit, branch, organisation, out totalServices, search).ToList();
                servicesInfo = new ServicesInfo() { ServiceList = serviceList, totalServices = totalServices };
            }

            return servicesInfo;
		}

        [Route("/PatientServices/GetServicesListByOrganizationBranchID")]
		[HttpGet()]
        public List<ServiceList> GetServicesListByOrganizationBranchID(int organizationID, int branchID)
		{
			int iTotal = 0;
			return _servicesDBContext.GetServiceList(1, 500, branchID, organizationID, out iTotal).ToList();
		}

        [Route("/PatientServices/GetMedicationProductListByOrganizationBranchID")]
        [HttpGet()]
        public List<InventoryInfoList> GetMedicationProductListByOrganizationBranchID(int organizationID, int branchID)
		{
			int iTotal = 0;
			return _inventoryDBContext.GetInventoryList(organizationID, branchID, 0, 0, 0, out iTotal).ToList();
        }

		public List<BranchModel> GetBranchList(int organisationID)
		{
			var branchList = _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID).ToList();

			return branchList;
		}

		public int InsertTreatmentPlan([FromBody] TreatmentPlanModel treatmentPlanModel)
		{
			treatmentPlanModel.CreatedDate = DateTime.Now;

			_treatmentPlanDBContext.Add(treatmentPlanModel);

			_treatmentPlanDBContext.SaveChanges();

			return treatmentPlanModel.ID;
		}

		public bool InsertTreatmentPlanServices([FromBody] List<TreatmentPlanService> treatmentPlanService)
		{
			_treatmentPlanDBContext.AddRange(treatmentPlanService);

			_treatmentPlanDBContext.SaveChanges();

			return true;
		}

		public bool InsertTreatmentPlanProducts([FromBody] List<TreatmentPlanProduct> treatmentPlanProduct)
		{
			_treatmentPlanDBContext.AddRange(treatmentPlanProduct);

			_treatmentPlanDBContext.SaveChanges();

			return true;
		}

		public int InsertService([FromBody] ServicesModel servicesModel)
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
				return 0;
			}

			return servicesModel.ID;
		}

		public bool InsertServiceDoctor(int serviceID ,[FromBody] List<int> doctorListID)
		{
			try
			{
				for (int i = 0; i < doctorListID.Count; i++)
				{
					ServiceDoctor serviceDoctor = new ServiceDoctor()
					{
						ServiceID = serviceID,
						DoctorID = doctorListID[i],
						IsDeleted = 0,
						CreatedDate = DateTime.Now,
						CreatedBy = "System"
					};

					_servicesDBContext.Mst_Service_Doctor.Add(serviceDoctor);

					_servicesDBContext.SaveChanges();
				}
			}
			catch (Exception ex) 
			{
				return false;
			}
			

			return true;
		}

		public int UpdateTreatmentPlan([FromBody] TreatmentPlanModel treatmentPlanModel)
		{
			_treatmentPlanDBContext.Update(treatmentPlanModel);
			_treatmentPlanDBContext.SaveChanges();

			return treatmentPlanModel.ID;
		}

		public bool UpdateTreatmentPlanServices([FromBody] List<TreatmentPlanService> treatmentPlanService)
		{
			var treatmentPlanServiceOriginal = _treatmentPlanDBContext.Mst_TreatmentPlan_Services.AsNoTracking().Where(x => x.PlanID == treatmentPlanService[0].PlanID).ToList();

			foreach(var service in treatmentPlanService)
			{
				if(service.ServiceID != 0)
				{
					var serviceSelected = treatmentPlanServiceOriginal.FirstOrDefault(x => x.ServiceID == service.ServiceID);

					if (serviceSelected == null)
					{
						_treatmentPlanDBContext.Add(service);
						_treatmentPlanDBContext.SaveChanges();
					}
					else if (serviceSelected.IsDeleted == 1)
					{
						service.ID = serviceSelected.ID;
						_treatmentPlanDBContext.Update(service);
						_treatmentPlanDBContext.SaveChanges();
					}
					else
					{
						service.ID = serviceSelected.ID;
					}
				}
			}

			var needToDelete = treatmentPlanServiceOriginal.Where(x => !treatmentPlanService.Select(y => y.ID).Contains(x.ID)).ToList();

            foreach (var item in needToDelete)
            {
				item.IsDeleted = 1;
				_treatmentPlanDBContext.Update(item);
				_treatmentPlanDBContext.SaveChanges();
			}

            return true;
		}

		public bool UpdateTreatmentPlanProducts([FromBody] List<TreatmentPlanProduct> treatmentPlanProduct)
		{
			var treatmentPlanProductOriginal = _treatmentPlanDBContext.Mst_TreatmentPlan_Products.AsNoTracking().Where(x => x.PlanID == treatmentPlanProduct[0].PlanID).ToList();

			foreach (var product in treatmentPlanProduct)
			{
				if (product.ProductID != 0)
				{
					var productSelected = treatmentPlanProductOriginal.FirstOrDefault(x => x.ProductID == product.ProductID);

					if (productSelected == null)
					{
						_treatmentPlanDBContext.Add(product);
						_treatmentPlanDBContext.SaveChanges();
					}
					else if (productSelected.IsDeleted == 1)
					{
						product.ID = productSelected.ID;
						_treatmentPlanDBContext.Update(product);
						_treatmentPlanDBContext.SaveChanges();
					}
					else
					{
						product.ID = productSelected.ID;
					}
				}
			}

			var needToDelete = treatmentPlanProductOriginal.Where(x => !treatmentPlanProduct.Select(y => y.ID).Contains(x.ID)).ToList();

			foreach (var item in needToDelete)
			{
				item.IsDeleted = 1;
				_treatmentPlanDBContext.Update(item);
				_treatmentPlanDBContext.SaveChanges();
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

		public bool UpdateServiceDoctor(int serviceID, [FromBody] List<int> doctorListID)
		{
			var currentDoctroListID = _servicesDBContext.Mst_Service_Doctor.Where(x => x.ServiceID == serviceID).Select(y => y.DoctorID).ToList();
			var currentDoctroListIDDeleted = _servicesDBContext.Mst_Service_Doctor.Where(x => x.ServiceID == serviceID && x.IsDeleted == 1).Select(y => y.DoctorID).ToList();

			var needToDelete = currentDoctroListID.Except(doctorListID).ToList().Except(currentDoctroListIDDeleted).ToList();
			var needToAdd = doctorListID.Except(currentDoctroListID).ToList();
			var needToUndeleted = doctorListID.Intersect(currentDoctroListIDDeleted).ToList();

			try
			{
				for (int i = 0; i < needToAdd.Count; i++)
				{
					ServiceDoctor serviceDoctor = new ServiceDoctor()
					{
						ServiceID = serviceID,
						DoctorID = needToAdd[i],
						IsDeleted = 0,
						CreatedDate = DateTime.Now,
						CreatedBy = "System"
					};

					_servicesDBContext.Mst_Service_Doctor.Add(serviceDoctor);
					_servicesDBContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return false;
			}

			try
			{
				for (int i = 0; i < needToDelete.Count; i++)
				{
					var deleteServiceDoctor = _servicesDBContext.Mst_Service_Doctor.AsNoTracking().FirstOrDefault(x => x.ServiceID == serviceID && x.DoctorID == needToDelete[i]);

					deleteServiceDoctor.IsDeleted = 1;
					deleteServiceDoctor.UpdatedDate = DateTime.Now;
					deleteServiceDoctor.UpdatedBy = "System";

					_servicesDBContext.Update(deleteServiceDoctor);
					_servicesDBContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return false;
			}

			try
			{
				for (int i = 0; i < needToUndeleted.Count; i++)
				{
					var undeleteServiceDoctor = _servicesDBContext.Mst_Service_Doctor.AsNoTracking().FirstOrDefault(x => x.ServiceID == serviceID && x.DoctorID == needToUndeleted[i]);

					undeleteServiceDoctor.IsDeleted = 0;
					undeleteServiceDoctor.UpdatedDate = DateTime.Now;
					undeleteServiceDoctor.UpdatedBy = "System";

					//_servicesDBContext.Update(undeleteServiceDoctor);
					//_servicesDBContext.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return false;
			}


			return true;
		}

		public bool ChangeTreatmentPlanStatus(int treatmentPlanID, int newStatus)
		{
			try
			{
				var treatmentPlan = _treatmentPlanDBContext.Mst_TreatmentPlan.FirstOrDefault(x => x.ID == treatmentPlanID);
				treatmentPlan.Status = newStatus;

				_treatmentPlanDBContext.Mst_TreatmentPlan.Update(treatmentPlan);
				_treatmentPlanDBContext.SaveChanges();

				return true;
			}
			catch (Exception ex) 
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
			
			
		}

        public bool hasPermission(List<string> roles, string permission, out int branchID, out int organisationID)
        {
            branchID = 0;
            organisationID = 0;
            bool havePermission = false;

            //var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            if (roles.Contains("General.Superadmin") || roles.Contains("General.Superuser"))
            {
                organisationID = (roles.Contains("General.Superuser")) ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;
                havePermission = true;
            }
            else if (roles.Contains(permission) || HttpContext.Session.GetString("IsDoctor") == "1" || HttpContext.Session.GetString("IsAdmin") == "1")
            {
                branchID = int.Parse(HttpContext.Session.GetString("BranchID"));
                havePermission = true;
            }

            return havePermission;
        }

        public void SetPermission(List<string> roles)
        {
            ViewData["CanAdd"] = "false";
            ViewData["CanEdit"] = "false";
            ViewData["CanView"] = "false";

            if (roles.Where(x => x.Contains("General.")).Count() > 0 || HttpContext.Session.GetString("IsDoctor") == "1" || HttpContext.Session.GetString("IsAdmin") == "1")
            {
                ViewData["CanAdd"] = "true";
                ViewData["CanEdit"] = "true";
                ViewData["CanView"] = "true";
            }
            else
            {
                if (roles.Contains("Service.Add"))
				{
                    ViewData["CanAdd"] = "true";
                }

                if (roles.Contains("ServiceDetails.Add"))
                {
                    ViewData["CanAdd"] = "true";
                }

                if (roles.Contains("ServiceDetails.Edit"))
                {
                    ViewData["CanEdit"] = "true";
                }

                if (roles.Contains("ServiceDetails.View"))
                {
                    ViewData["CanView"] = "true";
                }
            }
        }

		public void SetTreatmentPlanPermission(List<String> roles)
		{
            ViewData["CanAdd"] = "false";
            ViewData["CanEdit"] = "false";
            ViewData["CanView"] = "false";

            if (roles.Where(x => x.Contains("General.")).Count() > 0 || HttpContext.Session.GetString("IsDoctor") == "1" || HttpContext.Session.GetString("IsAdmin") == "1")
            {
                ViewData["CanAdd"] = "true";
                ViewData["CanEdit"] = "true";
                ViewData["CanView"] = "true";
            }
            else
            {
                if (roles.Contains("TreatmentPlan.Add"))
                {
                    ViewData["CanAdd"] = "true";
                }

                if (roles.Contains("TreatmentPlan.Edit"))
                {
                    ViewData["CanEdit"] = "true";
                }

                if (roles.Contains("TreatmentPlan.View"))
                {
                    ViewData["CanView"] = "true";
                }
            }
        }
    }
}
