using SplitWise.Models;

namespace SplitWise.Facade
{
    public interface IApplicatioFacade
    {
        Task<List<User>> GetAllUser();
        Task<User> RegisterUser(User user);
    }
}
