using CollageAPI.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI.Models
{
    public class Users: IdentityUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public string Roles { get; set; }
    }
}
