using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VPMSCustomer.Lib.Data.Models
{
    public class CountryModel
    {
        [Key]
        public int ID { get; set; }
        public String? CountryCode {  get; set; }
        public String? CountryName { get; set; }
        public int? IsActive {  get; set; }
    }

    public class StateModel
    {
        [Key]
        public int ID { get; set; }
        public int? CountryID { get; set; }
        public String? State { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }

    public class CityModel
    {
        [Key]
        public int ID { get; set; }
        public int? StateID { get; set; }
        public String? City { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public String? UpdatedBy { get; set; }
    }
}
