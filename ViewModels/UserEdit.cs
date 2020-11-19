using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.ViewModels
{
    public class UserEdit
    {
        [Required]
        [Display(Name = "First Name")]
        public string UserFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string UserLastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }
    }
}
