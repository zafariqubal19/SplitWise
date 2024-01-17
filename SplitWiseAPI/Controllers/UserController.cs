using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SplitWiseAPI.Models;
using SplitWiseAPI.Models.RequestParameters;
using SplitWiseAPI.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SplitWiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService,IEncryptionService encryptionService, IConfiguration configuration)
        {
            _encryptionService = encryptionService;
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("RegisterUser")]
        public User RegisterUser(User user)
        {
            user.IsRegistered = true;
            int rowsEffected = _userService.RegisterUser(user);
            if (rowsEffected > 0)
            {
                return user;

            }
            else
            {
                return new User();
            }

        }
        //[HttpGet]
        //[Route("Login")]
        //public CustomPrincipalClaim Login(string Email, string password)
        //{
        //   return GenerateAccessToken(Email, password);

        //}
        [HttpGet]
        [Route("Login")]
        public CustomPrincipalClaim Login(string email,string password)
        {

            User user=_userService.IsValidUser(email, password);
            if(user.Email != null) { 

            var windowsIdentity = user;//User.Identity as WindowsIdentity;

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);


            return new CustomPrincipalClaim
            {
                User=user,
                IsAuthenticated = true,
                AccessToken = jwtToken
            };
            }
            else
            {
                return new CustomPrincipalClaim { };
            }

        }

    }
}
