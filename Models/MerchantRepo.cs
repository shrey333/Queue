using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public class MerchantRepo: MerchantRepository
    {
        private readonly ApplicationDbContext context;
        public MerchantRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        IEnumerable<Merchant> MerchantRepository.GetLocationBased(string latitude, string longitude)
        {
            IEnumerable<Merchant> merch = context.merchants;
            List<Merchant> temp = new List<Merchant>();
            foreach(var m in merch)
            {
                double radian = (Math.PI / 180);
                double lat = double.Parse(latitude);
                double lon = double.Parse(longitude);
                double mlat = double.Parse(m.MerchantLatitude);
                double mlon = double.Parse(m.MerchantLongitude);
                double check = 6371 * Math.Acos(Math.Cos(radian * lat) * Math.Cos(radian * mlat) * Math.Cos(mlon * radian -  radian * lon) + Math.Sin(radian * lat) * Math.Sin(radian * mlat));
                if (check < 25)
                {
                    temp.Add(m);
                }
            }
            return temp;
        }

        Merchant MerchantRepository.GetMerchant(string Email)
        {
            return context.merchants.Find(Email);
        }

        IEnumerable<Merchant> MerchantRepository.GetAllMerchant()
        {
            return context.merchants;
        }
        
        Merchant MerchantRepository.Add(Merchant merchant)
        {
            context.merchants.Add(merchant);
            context.SaveChanges();
            return merchant;
        }

        Merchant MerchantRepository.Update(Merchant merchant)
        {
            var change = context.merchants.Attach(merchant);
            change.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return merchant;
        }

        Merchant MerchantRepository.Delete(string Email)
        {
            Merchant merchant = context.merchants.Find(Email);
            if (merchant != null)
            {
                context.merchants.Remove(merchant);
                context.SaveChanges();
            }
            return merchant;
        }
    }
}
