namespace SplitWiseAPI.Models
{
    public class Members
    {
        public int MemberId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int? TotalAmountSpent { get; set; }
        public int? TotalAmountToGive { get; set;}
        public int? TotalAmountToReceive { get; set;}

    }
}
