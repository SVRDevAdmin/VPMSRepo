using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Drawing;
using VPMS.Lib.Data.DBContext;
using VPMS.Lib.Data.Models;

namespace VPMSWeb.Controllers
{
	public class InventoryController : Controller
	{
		private readonly InventoryDBContext _inventoryDBContext = new InventoryDBContext();

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
			return View();
		}

		public bool InsertInventory([FromBody] InventoryInfo inventoryInfo)
		{
			var ImageFilePath = "InventoryImage/";
			var ImageFileName = inventoryInfo.ImageFileName + ".png";

			try
			{
				var t = inventoryInfo.ImageFile.Substring(22);  // remove data:image/png;base64,

				byte[] bytes = Convert.FromBase64String(t);

				Image image;
				using (MemoryStream ms = new MemoryStream(bytes))
				{
					image = Image.FromStream(ms);
				}
				
				var randomFileName = Guid.NewGuid().ToString().Substring(0, 4) + ".png";

				bool exists = System.IO.Directory.Exists(ImageFilePath);

				if (!exists)
					System.IO.Directory.CreateDirectory(ImageFilePath);

				var fullPath = ImageFilePath + ImageFileName;
				image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
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
				UpdatedDate = DateTime.Now,
				UpdatedBy = "System"
			};

			_inventoryDBContext.Add(inventoryStatus);

			_inventoryDBContext.SaveChanges();

			return true;
		}
	}
}
