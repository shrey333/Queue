using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public class QueueModel
    {
        public string UserId{ get; set; }
        public User user { get; set; }

        public string MerchantId { get; set; }
        public Merchant merchant { get; set; }
    }
}
