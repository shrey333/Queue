using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public class UserRepo: UserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        User UserRepository.GetUser(string email)
        {
            return context.users.Find(email);
        }

        User UserRepository.Add(User user)
        {
            context.users.Add(user);
            context.SaveChanges();
            return user;
        }

        User UserRepository.Update(User user)
        {
            var change = context.users.Attach(user);
            change.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return user;
        }

        User UserRepository.Delete(string email)
        {
            User user = context.users.Find(email);
            if(user != null)
            {
                context.users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }
    }
}
