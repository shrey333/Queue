using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public class Merchant
    {
        [Key]
        public string MerchantEmail { get; set; }
        public string MerchantPhoneNumber { get; set; }
        public string MerchantFirstName { get; set; }
        public string MerchantLastName { get; set; }
        public string MerchantLatitude { get; set; }
        public string MerchantLongitude { get; set; }
        public string MerchantShopName { get; set; }
        public string MerchantAddress1 { get; set; }
        public string MerchantAddress2 { get; set; }
        public string MerchantPinCode { get; set; }

        public IList<QueueModel> queueModels { get; set; }
    }
}
