using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services
{
    public interface IUserService
    {
        void RegisterUser(User user);
        List<User> GetUsers();
    }
}
