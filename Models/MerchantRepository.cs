using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public interface MerchantRepository
    {
        Merchant GetMerchant(string Email);
        IEnumerable<Merchant> GetAllMerchant();
        Merchant Add(Merchant merchant);
        Merchant Update(Merchant merchant);
        Merchant Delete(string Email);
        IEnumerable<Merchant> GetLocationBased(string latitude, string longitude);
    }
}
