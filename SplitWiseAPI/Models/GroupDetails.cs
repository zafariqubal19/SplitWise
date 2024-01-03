namespace SplitWiseAPI.Models
{
    public class GroupDetails
    {

        public string GroupName { get; set; }
        public int GroupId { get; set; }

       public List<GroupMembers> Members { get; set;}

    }
    public class GroupMembers
    {
        public int MemberId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? TotalAmountSpent { get; set; } = 0;
        public int? TotalAmountToGive { get; set; } = 0;
        public int? TotalAmountToReceive { get; set; } = 0;
    }
  
}
