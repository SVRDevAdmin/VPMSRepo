using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VPMS;
using VPMS.Lib.Data;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
	public class AccessControlController : Controller
	{
		private readonly OrganisationDBContext _organisationDBContext = new OrganisationDBContext();
		private readonly BranchDBContext _branchDBContext = new BranchDBContext();

		int totalOrganisations;

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult OrganisationList()
		{
            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "OrganizationListing.View", out organisation);

            if (!havaPermission)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

			SetPermission(roles);

            return View();
		}

		public OrganisationInfo GetOrganisationList(int rowLimit, int page, string search)
		{
			int start = (page - 1) * rowLimit;
			var organisationInfos = new OrganisationInfo() { OrganisationList = new List<OrganisationList>(), TotalOrganisation = 0 };

            //var role = HttpContext.Session.GetString("RoleName");
            //var organisation = (role == "Superuser") ? int.Parse(HttpContext.Session.GetString("OrganisationID")) : 0;

            //var vendorID = _organisationDBContext.Mst_Organisation.FirstOrDefault(x => x.Id == organisation).ParentID;
            //var vendor = _organisationDBContext.Mst_Organisation.FirstOrDefault(x => x.Id == vendorID);


            var organisation = 0;
            var roles = RoleRepository.GetRolePermissionsByRoleID(HttpContext.Session.GetString("RoleID"));
            var havaPermission = hasPermission(roles, "OrganizationListing.View", out organisation);

            if (havaPermission)
			{
				var organisationList = _organisationDBContext.GetOrganisationList(start, rowLimit, organisation, out totalOrganisations, search).ToList(); ;

				organisationInfos = new OrganisationInfo() { OrganisationList = organisationList, TotalOrganisation = totalOrganisations };
			}

            return organisationInfos;
		}

		[Route("/AccessControl/GetOrganisationByLevelID/{iLevel}")]
		[HttpGet()]
		public IActionResult GetOrganisationByLevelID(int iLevel)
		{
			try
			{
				var sOrgList = _organisationDBContext.Mst_Organisation.Where(x => x.Level == iLevel && x.Status == 1).ToList();
				if (sOrgList != null)
				{
					return Json(new { data = sOrgList });
				}
				else
				{
					return null;
				}
            }
			catch (Exception ex)
			{
				return null;
			}
			
		}

        public int InsertUpdateOrganisation([FromBody] OrganisationModel organisationModel)
		{
			var Level1ID = int.Parse(HttpContext.Session.GetString("Level1ID"));
			organisationModel.TotalStaff = 1;
			organisationModel.Level = 2;
			organisationModel.ParentID = Level1ID;

			if(organisationModel.Id != 0)
			{
				organisationModel.UpdatedDate = DateTime.Now;
				organisationModel.UpdatedBy = HttpContext.Session.GetString("Username");
			}

			_organisationDBContext.Mst_Organisation.Update(organisationModel);

			_organisationDBContext.SaveChanges();

			return organisationModel.Id;
		}

		public bool InsertUpdateBranch([FromBody] BranchModel branchModel)
		{
			if(branchModel.ID != 0)
			{
				branchModel.UpdatedDate = DateTime.Now;
				branchModel.UpdatedBy = HttpContext.Session.GetString("Username");
			}

			_branchDBContext.Mst_Branch.Update(branchModel);
			_branchDBContext.SaveChangesAsync();

			return true;
		}

		public bool DeleteBranch([FromBody] List<int> deletedBranch)
		{
			try
			{
				var allBranchesToDelete = _branchDBContext.Mst_Branch.AsNoTracking().Where(x => deletedBranch.Contains(x.ID)).ToList();

				foreach (var branch in allBranchesToDelete)
				{
					branch.Status = 2;
					branch.UpdatedDate = DateTime.Now;
					branch.UpdatedBy = HttpContext.Session.GetString("Username");

					_branchDBContext.Update(branch);
					_branchDBContext.SaveChanges();
				}

				return true;
			}
			catch (Exception ex) 
			{
				Program.logger.Error("Controller Error >> ", ex);

				return false;
			}
		}

		public OrganisationModel GetOrganisationInfo(int organisationID)
		{
			return _organisationDBContext.Mst_Organisation.FirstOrDefault(x => x.Id == organisationID && x.Status == 1);
		}

		public List<BranchModel> GetBranchInfoByOrganisation(int organisationID)
		{
			return _branchDBContext.Mst_Branch.Where(x => x.OrganizationID == organisationID && x.Status == 1).ToList();
		}

        public bool hasPermission(List<string> roles, string permission, out int organisationID)
        {
            organisationID = 0;
            bool havePermission = false;

            if (roles.Contains("General.Superadmin"))
            {
                havePermission = true;
            }
            else if (roles.Contains(permission) || roles.Contains("General.Superuser"))
            {
                organisationID = int.Parse(HttpContext.Session.GetString("OrganisationID"));
                havePermission = true;
            }

            return havePermission;
        }

		public void SetPermission(List<string> roles)
		{
            ViewData["CanAddOrganisation"] = "false";
            ViewData["CanEditOrganisation"] = "false";
            ViewData["CanViewOrganisation"] = "false";
            ViewData["CanAddBranch"] = "false";
            ViewData["CanEditBranch"] = "false";

            if (roles.Contains("General.Superadmin"))
            {
                ViewData["CanAddOrganisation"] = "true";
                ViewData["CanEditOrganisation"] = "true";
                ViewData["CanViewOrganisation"] = "true";
                ViewData["CanAddBranch"] = "true";
                ViewData["CanEditBranch"] = "true";
            }
            else
            {
                if (roles.Contains("General.Superuser"))
                {
                    ViewData["CanEditOrganisation"] = "true";
                    ViewData["CanViewOrganisation"] = "true";
                    ViewData["CanAddBranch"] = "true";
					ViewData["CanEditBranch"] = "true";
				}

                if (roles.Contains("OrganizationDetails.View"))
                {
                    ViewData["CanViewOrganisation"] = "true";
                }

                if (roles.Contains("OrganizationDetails.Edit"))
                {
                    ViewData["CanEditOrganisation"] = "true";
                }

                if (roles.Contains("Branch.Add"))
                {
                    ViewData["CanAddBranch"] = "true";
                }

                if (roles.Contains("Branch.Edit"))
                {
                    ViewData["CanEditBranch"] = "true";
                }
            }
        }
    }
}
