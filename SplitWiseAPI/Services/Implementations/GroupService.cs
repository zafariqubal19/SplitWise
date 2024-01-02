using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;
using System.Data;
using System.Data.SqlClient;

namespace SplitWiseAPI.Services.Implementations
{
    public class GroupService:IGroupService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IConfiguration _configuration;
        
        public GroupService(IConfiguration configuration)
        {


            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration.GetConnectionString("SplitWiseDB"));

        }
        public int CreateGroup(Group group)
        {

            string sp = "sp_CreateGroup";
            SqlCommand sqlCommand=new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@GroupName", group.GroupName);
            sqlCommand.Parameters.AddWithValue("@UserId", group.UserId);
            _sqlConnection.Open();
            int effecrRows=sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            return effecrRows;
        }

    }
}
