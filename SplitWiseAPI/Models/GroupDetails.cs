namespace SplitWiseAPI.Models
{
    public class GroupDetails
    {

        public string GroupName { get; set; }
       public List<GroupMembers> Members { get; set;}

    }
    public class GroupMembers
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
  
}
