using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services.Interface
{
    public interface IMemberService
    {
        int AddMembers(int groupId, string email);
        int DeleteMembers(int groupId, int userId);
        int UpdateMembersAmount(Members members);
    }
}
