using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMSCustomer.Lib.Data.Models
{
    public class AccountCreationModel
    {
        [Key]
        public int ID { get; set; }
        public String? EmailAddress {  get; set; }
        public String? InvitationCode { get; set; }
        public DateTime? LinkCreatedDate { get; set; }
        public DateTime? LinkExpiryDate { get; set; }
        public DateTime? AccountCreationDate { get; set; }
        public int? PatientOwnerID { get; set; }
    }

    public class AccountCreationExtendedObj 
    {
        public Boolean? isLinkExists { get; set; }
        public Boolean? isLinkExpired { get; set; }
        public Boolean? isActivated { get; set; }
        public String? EmailAddress { get; set; }
        public int? PatientOwnerID { get; set; }
        public String? InvitationCode { get; set; }
    }
}
