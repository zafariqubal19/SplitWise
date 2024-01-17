using System.Data.SqlClient;
using System.Data;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;

namespace SplitWiseAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IConfiguration _configuration;
        private readonly IEncryptionService _encryptionService;
        public UserService(IConfiguration configuration, IEncryptionService encryptionService)
        {


            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration.GetConnectionString("SplitWiseDB"));
            _encryptionService = encryptionService;
        }
        public int RegisterUser(User user)
        {
            User users = GetUserByEmail(user.Email);
            if (users.Email == null)
            {
                string sp = "sp_InsertSplitwiseUser";
               string salt= _encryptionService.GenerateSalt(user.Password);
                string hashPassword=_encryptionService.EcncryptPassword(user.Password, salt);
                SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", user.Name);
                sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Password", hashPassword);
                sqlCommand.Parameters.AddWithValue("@IsRegistered", user.IsRegistered);
                sqlCommand.Parameters.AddWithValue("@Salt", salt);
                _sqlConnection.Open();
                int rowsEffected = sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
                return rowsEffected;
            }
            else if (users.IsRegistered == false)
            {
                string sp = "";
                SqlCommand sqlCommand = new SqlCommand( sp, _sqlConnection);
                sqlCommand.CommandType= CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", users.UserId);
                sqlCommand.Parameters.AddWithValue("@Name", user.Name);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                sqlCommand.Parameters.AddWithValue("@IsRegistered", true) ;
                _sqlConnection.Open ();
                int effectedRows= sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
                return effectedRows;
            }
            else
            {
                return 0;
            }

          

        }
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string sp = "SP_GetAllSplitWiseUsers";
            SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    User user = new User();
                    user.Email = Convert.ToString(reader["Email"]);
                    user.Name = Convert.ToString(reader["Name"]);
                    user.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                    user.Password = Convert.ToString(reader["Password"]);

                    users.Add(user);

                }
            }
            _sqlConnection.Close();
            return users;
        }
        public User GetUserById(int UserId)
        {
            User user = new User();
            string sp = "sp_GetUserByUserIdId";
            SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserId", UserId);
            _sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.Email = Convert.ToString(reader["Email"]);
                    user.Name = Convert.ToString(reader["Name"]);
                    user.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                    user.Password = Convert.ToString(reader["Password"]);
                }
            }
            _sqlConnection.Close();
            return user;
        }
        public User GetUserByEmail(string email)
        {
            User user = new User();
            string sp = "GetUserByEmailId";
            SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Email", email);
            _sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.Email = Convert.ToString(reader["Email"]);
                    user.Name = Convert.ToString(reader["Name"]);
                    user.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                    user.Password = Convert.ToString(reader["Password"]);
                    user.Salt = Convert.ToString(reader["Salt"]);
                }
                
            }
            _sqlConnection.Close();
            return user;

        }
        public User IsValidUser(string email, string password)
        {
            User user = new User();
            string sp = "IdentifyUser";
            SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Email", email);
            sqlCommand.Parameters.AddWithValue("@Password", password);
            _sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.UserId= Convert.ToInt32(reader["UserId"]);
                    user.Email = Convert.ToString(reader["Email"]);
                    user.Name = Convert.ToString(reader["Name"]);
                    user.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                    user.Password = Convert.ToString(reader["Password"]);
                }
            }
            _sqlConnection.Close();
            return user;


        }
    }
}
