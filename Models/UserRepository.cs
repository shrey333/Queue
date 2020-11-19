using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public interface UserRepository
    {
        User GetUser(string Email);
        User Add(User user);
        User Update(User user);
        User Delete(string Email);
    }
}
