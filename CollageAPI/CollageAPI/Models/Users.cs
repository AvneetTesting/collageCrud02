using CollageAPI.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Models
{
    public class Users: ApplicationUser
    {
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Roles { get; set; }

    }
}
