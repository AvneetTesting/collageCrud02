using CollageAPI.Identity;
using CollageAPI.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IdentityApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public UsersController(IdentityApplicationDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginVM loginVM)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginVM.UserName && u.Password == loginVM.Password);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescritor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescritor);
            user.Token=tokenHandler.WriteToken(token);
            user.Password = "";


            return Ok(user);

        }


//        private IActionResult GenrateJwtToken(string name, string email)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
//            var tokenDescritor = new SecurityTokenDescriptor()
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Name,name),
//                    new Claim(ClaimTypes.Email,email)
//                }),
//                Expires = DateTime.UtcNow.AddHours(30),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
//, SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescritor);
//            return tokenHandler.WriteToken(token);

//        }


        [HttpPost("testing")]
        public IActionResult Testing()
        {
            return Ok();
        }

    }
}
