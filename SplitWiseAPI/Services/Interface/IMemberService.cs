namespace SplitWiseAPI.Services.Interface
{
    public interface IMemberService
    {
        int AddMembers(int groupId, string email);
        int DeleteMembers(int groupId, int userId);
    }
}
