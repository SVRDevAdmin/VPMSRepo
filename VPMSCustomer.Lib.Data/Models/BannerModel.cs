using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class BannerModel
    {
        [Key]
        public int ID { get; set; }
        public int? BannerType { get; set; }
        public String? Description {  get; set; }
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
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get;set; }
    }
}
