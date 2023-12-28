using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services;

namespace SplitWiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SplitController : ControllerBase
    {
        private readonly IUserService _userService;
        public SplitController( IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("GetUsers")]
        public List<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }
        [HttpPost]
        [Route("RegisterUser")]
        public User RegisterUser(User user)
        {
          int rowsEffected=  _userService.RegisterUser(user);
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
            var user=_userService.IdentifyUser(username, password);
            if(user != null) {
                return true;
            }
            else { return false; }
        }
    }
}
