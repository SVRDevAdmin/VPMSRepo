using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class InventoryModel : AuditModel
    {
        [Key]
        public int ID { get; set; }
		public int ProductTypeID { get; set; }
		public int BranchID { get; set; }
        public string SKU { get; set; } = null!;
        public string InventoryName { get; set; } = null!;
        public string Name { get; set; } = null!;
		public float RecommendedWeight { get; set; }
		public float PricePerQty { get; set; }
		public string Species { get; set; } = null!;
		public string RecommendedBreed { get; set; } = null!;
		public string Usage { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string ImageFilePath { get; set; } = null!;
		public string ImageFileName { get; set; } = null!;

	}

	public class InventoryStatus : AuditPartialModelUpdated
	{
		[Key]
		public int ID { get; set; }
		public int ProductID { get; set; }
		public int StockStatus { get; set; }
		public int QtyInStores { get; set; }
		public int LowStockThreshold { get; set; }
		public DateOnly ExpiryDate { get; set; }

	}

	public class InventoryCategory : AuditModel
	{
		[Key]
		public int ID { get; set; }
		public string TypeName { get; set; }
		public int Status { get; set; }
	}

    public class InventoryInfo : AuditModel
    {
		public int ID { get; set; }
		public string ImageFile {  get; set; }
		public string ImageFileName { get; set; }
		public string InventoryName { get; set; } = null!;
		public string SKU { get; set; } = null!;
		public int ProductTypeID { get; set; }
		public string Name { get; set; } = null!;
		public string Species { get; set; } = null!;
		public int StockStatus { get; set; }
		public DateOnly ExpiryDate { get; set; }
		public int OrganisationID { get; set; }
		public int BranchID { get; set; }
		public string RecommendedBreed { get; set; } = null!;
		public float RecommendedWeight { get; set; }
		public float PricePerQty { get; set; }
		public string Usage { get; set; } = null!;
		public int Quantity { get; set; }
		public string Description { get; set; } = null!;

	}

	public class InventoryInfoLists
	{
		public List<InventoryInfoList> InventoryInfoList { get; set; }
		public int totalInventory {  get; set; }
	}

	public class InventoryInfoList
	{
		public int No {  get; set; }
		public int ID { get; set; }
		public string InventoryName { get; set; } = null!;
		public string Category { get; set; } = null!;
		public string Usage { get; set; } = null!;
		public string ProductName { get; set; } = null!;
		public string Image { get; set; }
		public string SKU { get; set; } = null!;
		public int Quantity { get; set; }
		public float PricePerQty { get; set; }
		public string Organisation { get; set; }
		public string Branch { get; set; }
		public int StockStatus { get; set; }

	}

	public class InventoryInvoice
	{
		public string ProductName { get; set; }
		public float Discount { get; set; }
		public float TotalPrice { get; set; }
		public DateOnly ExpiryDate { get; set; }
	}
}
