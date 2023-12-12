using SplitWiseAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace SplitWiseAPI.Services
{
    public class UserService:IUserService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlCommand _command;
        private readonly IConfiguration _configuration;
        public UserService(SqlCommand command, IConfiguration configuration)
        {
            
            _command = command;
            _configuration = configuration;
            _sqlConnection = new SqlConnection(configuration.GetConnectionString(""));

        }
        public void RegisterUser(User user)
        {
            string sp = "";
            SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Name", user.Name);
            sqlCommand.Parameters.AddWithValue("@Email", user.Email);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            _sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();     
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string sp = "";
            SqlCommand sqlCommand = new SqlCommand( sp, _sqlConnection);
            sqlCommand.CommandType= CommandType.StoredProcedure;
            _sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.Name = Convert.ToString(reader["Name"]);
                    user.PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]);
                    user.Email = Convert.ToString(reader["Email"]);
                    users.Add(user);

                }
            }
            return users;
        }
    }
}
