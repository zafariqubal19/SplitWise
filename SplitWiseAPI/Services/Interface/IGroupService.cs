using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services.Interface
{
    public interface IGroupService
    {
        int CreateGroup(Group group);
        GroupDetails GetGroupDetails(int groupId);
        MembersGroups GetMembersGroup(int UserId);
    }
}
