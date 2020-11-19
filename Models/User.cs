using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public class User
    {
        [Key]
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserLatitude { get; set; }
        public string UserLongitude { get; set; }
        public string UserPhoneNumber { get; set; }

        public IList<QueueModel> queueModels { get; set; }
    }
}
