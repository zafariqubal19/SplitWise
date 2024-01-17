using System.Numerics;

namespace SplitWiseAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean? IsRegistered { get; set; }=false;
        public string? Salt { get; set; } = null;
    }
}
