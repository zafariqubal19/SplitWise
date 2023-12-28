using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services
{
    public interface IUserService
    {
        int RegisterUser(User user);
        List<User> GetAllUsers();
        User GetUserById(string email);
        User IdentifyUser(string email, string password);
    }
}
