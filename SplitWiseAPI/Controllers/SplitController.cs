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
        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }
        [HttpPost]
        [Route("RegisterUser")]
        public string RegisterUser(User user)
        {
          int rowsEffected=  _userService.RegisterUser(user);
            if (rowsEffected > 0)
            {
                return "User Registered";

            }
            else
            {
                return "User not Inserted";
            }

        }
    }
}
