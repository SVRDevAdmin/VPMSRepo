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
		public Decimal RecommendedWeight { get; set; }
		public Decimal PricePerQty { get; set; }
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

	}

    public class InventoryInfo
    {
		public string ImageFile {  get; set; }
		public string ImageFileName { get; set; }
		public string InventoryName { get; set; } = null!;
		public string SKU { get; set; } = null!;
		public int ProductTypeID { get; set; }
		public string Name { get; set; } = null!;
		public string Species { get; set; } = null!;
		public int StockStatus { get; set; }
		public DateOnly ExpiryDate { get; set; }
		public int BranchID { get; set; }
		public string RecommendedBreed { get; set; } = null!;
		public Decimal RecommendedWeight { get; set; }
		public Decimal PricePerQty { get; set; }
		public string Usage { get; set; } = null!;
		public int Quantity { get; set; }
		public string Description { get; set; } = null!;

	}
}
