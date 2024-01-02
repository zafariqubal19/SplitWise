using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;

namespace SplitWiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("RegisterUser")]
        public User RegisterUser(User user)
        {
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
        [HttpGet]
        [Route("Login")]
        public bool Login(string username, string password)
        {
            var user = _userService.IdentifyUser(username, password);
            if (user != null)
            {
                return true;
            }
            else { return false; }
        }
    }
}
