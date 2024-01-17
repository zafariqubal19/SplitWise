using SplitWiseAPI.Models;
namespace SplitWiseAPI.Models
{
    public class AuthRequest
    {
    }
    public class CustomPrincipalClaim
    {
        public User User{ get; set; } 
        public bool IsAuthenticated { get; set; } = false;
        public string AccessToken { get; set; }
    }
}
