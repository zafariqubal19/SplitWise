using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services.Interface
{
    public interface IUserService
    {
        int RegisterUser(User user);
        List<User> GetAllUsers();
        User GetUserById(int UserId);
        User IdentifyUser(string email, string password);
        User GetUserByEmail(string email);
    }
}
