using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib.Data.Models
{
    public class BannerModel
    {
        [Key]
        public int ID { get; set; }
        public int? BannerType { get; set; }
        public String? Description { get; set; }
        public int? SeqOrder { get; set; }
        public int? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public String? BannerName { get; set; }
        public String? BannerFilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class BannerViewModel : BannerModel
    {
        public int RowNo { get; set; }
    }

    public class BannerDisplayModel : BannerModel
    {
        public String? StartDateString { get; set; }
        public String? EndDateString { get; set; }
    }

    public class BannerUploadForm
    {
        public String? description { get; set; }
        public String? startDate { get; set; }
        public String? endDate { get; set;  }
        public int? status { get; set; }
        public String? uploadedBy {  get; set; }
    }

    public class BlogModel
    {
        [Key]
        public int ID { get; set; }
        public String? Title { get; set; }
        public String? Description { get; set; }
        public String? URLtoRedirect { get; set; }
        public int? SeqOrder { get; set; }
        public int? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public String? ThumbnailImage { get; set; }
        public String? ThumbnailFilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get;  set; }
        public String? UpdatedBy { get; set; }
    }

    public class BlogViewModel : BlogModel
    {
        public int? RowNo { get; set; }
    }

    public class BlogUploadForm
    {
        public String? title { get; set; }
        public String? description { get; set; }
        public String? startDate { get; set; }
        public String? endDate { get; set; }
        public String? urlLink { get; set; }
        public int? status { get; set; }
        public String? uploadedBy { get; set; }
    }

    public class BlogDisplayModel : BlogModel
    {
        public String? StartDateString { get; set; }
        public String? EndDateString { get; set; }
    }
}
