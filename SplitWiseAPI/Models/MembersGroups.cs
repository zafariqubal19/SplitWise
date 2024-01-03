namespace SplitWiseAPI.Models
{
    public class MembersGroups
    {
        public string Member { get; set; }
        public int UserId { get; set; }
        public List<GroupsName> Groups { get; set; }
    }
    public class GroupsName
    {
        public string GroupName { get; set; }
        public int GroupId { get; set; }

    }
}
