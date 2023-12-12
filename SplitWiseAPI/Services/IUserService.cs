using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services
{
    public interface IUserService
    {
        int RegisterUser(User user);
        List<User> GetUsers();
    }
}
