using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.ViewModels
{
    public class Location
    {
        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Latitude { get; set; }
    }
}
