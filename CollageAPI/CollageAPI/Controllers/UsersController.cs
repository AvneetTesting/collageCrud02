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
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly AppSettings _appSetting;
        public UsersController(IdentityApplicationDbContext context,IOptions<AppSettings> appSettings, 
            ApplicationUserManager applicationUserManager, ApplicationSignInManager applicationSignInManager)
        {
            _context = context;
            _applicationUserManager = applicationUserManager;
            _applicationSignInManager = applicationSignInManager;
            _appSettings = appSettings.Value;
        }
        [HttpPost("Login")]
        public async Task<ApplicationUser> Login(LoginVM loginVM)
        {

            var result = await _applicationSignInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.FindByNameAsync(loginVM.UserName);
                applicationUser.PasswordHash = null;
                //  JWT Token
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Employee))
                    applicationUser.Role = SD.Role_Employee;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
            new Claim(ClaimTypes.Name,applicationUser.Id),
            new Claim(ClaimTypes.Email,applicationUser.Email),
            new Claim(ClaimTypes.Role,applicationUser.Role)
                  }),
                    Expires = DateTime.UtcNow.AddHours(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                  SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);
                return applicationUser;
            }
            else
                return null;

            //            var user = _context.Users.FirstOrDefault(u => u.UserName == loginVM.UserName && u.Password == loginVM.Password);
            //            if (user == null)
            //                return null;
            //            var tokenHandler = new JwtSecurityTokenHandler();
            //            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //            var tokenDescritor = new SecurityTokenDescriptor()
            //            {
            //                Subject = new ClaimsIdentity(new Claim[]
            //                {
            //                    new Claim(ClaimTypes.Name,user.UserName)
            //                }),
            //                Expires = DateTime.UtcNow.AddHours(30),
            //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
            //, SecurityAlgorithms.HmacSha256Signature)
            //            };
            //            var token = tokenHandler.CreateToken(tokenDescritor);
            //            user.Token=tokenHandler.WriteToken(token);
            //            user.Password = "";


            //            return Ok(user);

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
