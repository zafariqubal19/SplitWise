using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services.Interface
{
    public interface IGroupService
    {
        int CreateGroup(string groupName, int UserId);
        GroupDetails GetGroupDetails(int groupId);
        MembersGroups GetMembersGroup(int UserId);
        Group GetCreatedGroup(int UserId, string groupName);
        int DeletGroup(int groupId);
    }
}
