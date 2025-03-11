using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlX.XDevAPI;
using System.Drawing;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
    [Authorize]
    public class InventoryController : Controller
	{
		int totalInventory;

		private readonly PatientDBContext _patientDBContext = new PatientDBContext();
		private readonly InventoryDBContext _inventoryDBContext = new InventoryDBContext();
		private readonly OrganisationDBContext _organisationDBContext = new OrganisationDBContext();
		private readonly BranchDBContext _branchDBContext = new BranchDBContext();

		public IActionResult Index()
		{
            return RedirectToAction("InventoryList");
        }
		public IActionResult InventoryList()
		{
			return View();
		}

		public IActionResult CreateNewInventory()
		{
			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
			ViewData["Organisation"] = _organisationDBContext.Mst_Organisation.Where(x => x.Level != 0 && x.Level != 1).ToList();
			ViewData["Category"] = _inventoryDBContext.Mst_ProductType.ToList();

			return View();
		}

		[Route("/Inventory/ViewEditInventory/{type}/{inventoryId}")]
		public IActionResult ViewEditInventory(string type, int inventoryId)
		{
			ViewData["Species"] = _patientDBContext.Mst_Pets_Breed.Select(x => x.Species).Distinct().ToList();
			ViewData["Organisation"] = _organisationDBContext.Mst_Organisation.Where(x => x.Level != 0 && x.Level != 1).ToList();
			ViewData["Category"] = _inventoryDBContext.Mst_ProductType.ToList();
			var inventoryInfo = _inventoryDBContext.Mst_Product.FirstOrDefault(x => x.ID == inventoryId);
			var productStatus = _inventoryDBContext.Mst_Product_Status.FirstOrDefault(x => x.ProductID == inventoryInfo.ID);
			ViewData["Inventory"] = inventoryInfo;
			ViewData["StockStatus"] = productStatus.StockStatus;
			ViewData["Quantity"] = productStatus.QtyInStores;
			ViewData["ExpiryDate"] = productStatus.ExpiryDate.ToString();
			ViewData["OrganisationID"] = _branchDBContext.Mst_Branch.FirstOrDefault(x => x.ID == inventoryInfo.BranchID).OrganizationID;
			ViewData["ImageFile"] = Path.Combine(Directory.GetCurrentDirectory(), @"InventoryImages", inventoryInfo.ImageFileName);
			ViewData["Type"] = type;

			return View();
		}

		public InventoryInfoLists GetInventoryList(int rowLimit, int page, string search = "") 
		{
			int start = (page - 1) * rowLimit;

			var inventoryList = _inventoryDBContext.GetInventoryList(start, rowLimit, out totalInventory, search).ToList();

			var inventoryInfo = new InventoryInfoLists() { InventoryInfoList = inventoryList, totalInventory = totalInventory };

			return inventoryInfo;
        }

        public List<List<InventoryInfoList>> PrintInventoryList(string search = "")
        {
			List<List<InventoryInfoList>> inventoryInfoLists = new List<List<InventoryInfoList>>();
            var inventoryList = _inventoryDBContext.GetInventoryList(0, 0, out totalInventory, search).ToList();

			var distinctBranch = inventoryList.DistinctBy(x => x.Branch).Select(y => y.Branch).ToList();

			foreach (var branch in distinctBranch) 
			{
				inventoryInfoLists.Add(inventoryList.Where(x => x.Branch == branch).ToList());
            }

            return inventoryInfoLists;
        }

        public List<InventoryModel> GetInventoryByCategory(int categoryID)
		{
			return _inventoryDBContext.Mst_Product.Where(x => x.ProductTypeID == categoryID).ToList();
		}

		public InventoryModel GetInventoryById(int id)
		{
			return _inventoryDBContext.Mst_Product.FirstOrDefault(x => x.ID == id);
		}

		public bool InsertInventory([FromBody] InventoryInfo inventoryInfo)
		{
			//var ImageFilePath = "InventoryImage/";
			var ImageFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"InventoryImages");
			var ImageFileName = inventoryInfo.ImageFileName + ".png";

			var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"InventoryImages", ImageFileName);


			try
			{
				var t = inventoryInfo.ImageFile.Substring(22);  // remove data:image/png;base64,

				byte[] bytes = Convert.FromBase64String(t);

				Image image;
				using (MemoryStream ms = new MemoryStream(bytes))
				{
					image = Image.FromStream(ms);
				}

				bool exists = System.IO.Directory.Exists(ImageFilePath);

				if (!exists)
					System.IO.Directory.CreateDirectory(ImageFilePath);

				var fullPath = ImageFilePath + ImageFileName;
				image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
			}
			catch (Exception ex) 
			{
				return false;
			}

			InventoryModel inventoryModel = new InventoryModel()
			{
				ProductTypeID = inventoryInfo.ProductTypeID,
				BranchID = inventoryInfo.BranchID,
				SKU = inventoryInfo.SKU,
				InventoryName = inventoryInfo.InventoryName,
				Name = inventoryInfo.Name,
				RecommendedWeight = inventoryInfo.RecommendedWeight,
				PricePerQty = inventoryInfo.PricePerQty,
				Species = inventoryInfo.Species,
				RecommendedBreed = inventoryInfo.RecommendedBreed,
				Usage = inventoryInfo.Usage,
				Description = inventoryInfo.Description,
				ImageFilePath = ImageFilePath,
				ImageFileName = ImageFileName,
				CreatedDate = DateTime.Now,
				CreatedBy = "System"
			};

			_inventoryDBContext.Add(inventoryModel);

			_inventoryDBContext.SaveChanges();

			InventoryStatus inventoryStatus = new InventoryStatus()
			{
				ProductID = inventoryModel.ID,
				QtyInStores = inventoryInfo.Quantity,
				LowStockThreshold = 3,
				StockStatus = inventoryInfo.StockStatus,
				ExpiryDate = inventoryInfo.ExpiryDate,
				UpdatedDate = DateTime.Now,
				UpdatedBy = "System"
			};

			_inventoryDBContext.Add(inventoryStatus);

			_inventoryDBContext.SaveChanges();

			return true;
		}

		public bool UpdateInventory([FromBody] InventoryInfo inventoryInfo)
		{
			var ImageFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"InventoryImages");
			var ImageFileName = inventoryInfo.ImageFileName + ".png";

			var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"InventoryImages", ImageFileName);


			try
			{
				var t = inventoryInfo.ImageFile.Substring(22);  // remove data:image/png;base64,

				byte[] bytes = Convert.FromBase64String(t);

				Image image;
				using (MemoryStream ms = new MemoryStream(bytes))
				{
					image = Image.FromStream(ms);
				}

				bool exists = System.IO.Directory.Exists(ImageFilePath);

				if (!exists)
					System.IO.Directory.CreateDirectory(ImageFilePath);

				var fullPath = ImageFilePath + ImageFileName;
				image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
			}
			catch (Exception ex)
			{
				return false;
			}

			InventoryModel inventoryModel = new InventoryModel()
			{
				ID = inventoryInfo.ID,
				ProductTypeID = inventoryInfo.ProductTypeID,
				BranchID = inventoryInfo.BranchID,
				SKU = inventoryInfo.SKU,
				InventoryName = inventoryInfo.InventoryName,
				Name = inventoryInfo.Name,
				RecommendedWeight = inventoryInfo.RecommendedWeight,
				PricePerQty = inventoryInfo.PricePerQty,
				Species = inventoryInfo.Species,
				RecommendedBreed = inventoryInfo.RecommendedBreed,
				Usage = inventoryInfo.Usage,
				Description = inventoryInfo.Description,
				ImageFilePath = ImageFilePath,
				ImageFileName = ImageFileName,
				CreatedDate = inventoryInfo.CreatedDate,
				CreatedBy = inventoryInfo.CreatedBy,
				UpdatedDate = DateTime.Now,
				UpdatedBy = "System"
			};

			_inventoryDBContext.Update(inventoryModel);

			_inventoryDBContext.SaveChanges();

			InventoryStatus inventoryStatus = _inventoryDBContext.Mst_Product_Status.FirstOrDefault(x => x.ProductID == inventoryInfo.ID);

			inventoryStatus.QtyInStores = inventoryInfo.Quantity;
			inventoryStatus.StockStatus = inventoryInfo.StockStatus;
			inventoryStatus.ExpiryDate = inventoryInfo.ExpiryDate;
			inventoryStatus.UpdatedDate = DateTime.Now;
			inventoryStatus.UpdatedBy = "System";

			_inventoryDBContext.Update(inventoryStatus);

			_inventoryDBContext.SaveChanges();

			//Response.Cookies.Append("reload", "true");

			return true;
		}

		public bool UpdateReload()
		{
			Response.Cookies.Append("reload", "false");

			return true;
		}
	}
}
