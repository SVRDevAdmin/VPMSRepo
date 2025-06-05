using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public  class TemplateModel
    {
        [Key]
        public int TemplateID { get; set; }
        public String? TemplateType { get; set; }
        public String? TemplateCode { get; set; }
        public String? TemplateTitle { get; set; }
        public String? TemplateContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class TemplateDetailsModel
    {
        [Key]
        public int ID { get; set; }
        public int TemplateID { get; set; }
        public String? LangCode { get; set; }
        public String? TemplateTitle { get; set; }
        public String? TemplateContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
