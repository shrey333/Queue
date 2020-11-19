using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public interface QueueRepository
    {
        IEnumerable<User> GetUserQueue(string Email);
        IEnumerable<Merchant> GetMerchantQueue(string Email);
        QueueModel Add(QueueModel queueModel);
        QueueModel Delete(string uid, string mid);
        QueueModel GetQueue(string uid, string mid);
    }
}
