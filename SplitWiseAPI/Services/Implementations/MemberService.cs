using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;
using System.Data;
using System.Data.SqlClient;

namespace SplitWiseAPI.Services.Implementations
{
    public class MemberService:IMemberService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public MemberService(IConfiguration configuration, IUserService userService)
        {


            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration.GetConnectionString("SplitWiseDB"));
            _userService = userService;
        }
        public int AddMembers(int groupId,string email)
        {
            User user=_userService.GetUserByEmail(email);
            if(user.Email!=null) {
                string sp = "sp_AddMembers";
                SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@GroupId", groupId);
                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                _sqlConnection.Open();
                int effectedRows = sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
                return effectedRows;
            }
            else { return 0; }
           
        }
    }
}
