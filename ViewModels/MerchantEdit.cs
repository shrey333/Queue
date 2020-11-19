using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.ViewModels
{
    public class MerchantEdit
    {
        [Required]
        [Display(Name = "First Name")]
        public string MerchantFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string MerchantLastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Shop Name")]
        public string MerchantShopName { get; set; }

        [Required]
        [Display(Name = "Address1")]
        public string MerchantAddress1 { get; set; }

        [Required]
        [Display(Name = "Address2")]
        public string MerchantAddress2 { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Pin Code")]
        public string MerchantPinCode { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public string MerchantLongitude { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        public string MerchantLatitude { get; set; }
    }
}
