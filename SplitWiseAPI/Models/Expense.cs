namespace SplitWiseAPI.Models
{
    public class Expense
    {
        public int? ExpenseId { get; set; } = null;
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Spender { get; set; }
        public int TotalAmount { get;  set; }
    }
}
