using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public class QueueRepo : QueueRepository
    {
        private readonly ApplicationDbContext context;
        public QueueRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        IEnumerable<Merchant> QueueRepository.GetMerchantQueue(string Email)
        {
            var lol = from q in context.Set<QueueModel>()
                      from m in context.Set<Merchant>().Where(m => q.UserId == Email && q.MerchantId == m.MerchantEmail)
                      select new Merchant
                      {
                          MerchantEmail = m.MerchantEmail,
                          MerchantAddress1 = m.MerchantAddress1,
                          MerchantAddress2 = m.MerchantAddress2,
                          MerchantFirstName = m.MerchantFirstName,
                          MerchantLastName = m.MerchantLastName,
                          MerchantPhoneNumber = m.MerchantPhoneNumber,
                          MerchantShopName = m.MerchantShopName,
                          MerchantPinCode = m.MerchantPinCode
                      };
            //return context.queueModels.Where(q => q.UserEmail == Email); 
            return lol;
            //return context.merchants;
        }

        IEnumerable<User> QueueRepository.GetUserQueue(string Email)
        {
            var lol = from q in context.Set<QueueModel>()
                      from m in context.Set<User>().Where(m => q.MerchantId == Email && m.UserEmail == q.UserId)
                      select new User
                      {
                          UserEmail = m.UserEmail,
                          UserFirstName = m.UserFirstName,
                          UserLastName = m.UserLastName,
                          UserPhoneNumber = m.UserPhoneNumber
                      };
            //return context.queueModels.Where(q => q.MerchantEmail == Email);
            return lol;
        }

        QueueModel QueueRepository.Add(QueueModel queueModel)
        {
            context.queueModels.Add(queueModel);
            context.SaveChanges();
            return queueModel;
        }


        QueueModel QueueRepository.Delete(string uid, string mid)
        {
            QueueModel queueModel = context.queueModels.Find(uid, mid);
            if (queueModel != null)
            {
                context.queueModels.Remove(queueModel);
                context.SaveChanges();
            }
            return queueModel;
        }

        QueueModel QueueRepository.GetQueue(string uid, string mid)
        {
            return context.queueModels.Find(uid, mid);
        }
    }
}
