﻿using System.Data.SqlClient;
using System.Data;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;

namespace SplitWiseAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {


            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration.GetConnectionString("SplitWiseDB"));

        }
        public int RegisterUser(User user)
        {
            User users = GetUserById(user.UserId);
            if (users == null)
            {
                string sp = "sp_InsertSplitwiseUser";
                SqlCommand sqlCommand = new SqlCommand(sp, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", user.Name);
                sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                _sqlConnection.Open();
                int rowsEffected = sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
                return rowsEffected;
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
                }
            }
            return user;
        }
        public User IdentifyUser(string email, string password)
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
                    user.Email = Convert.ToString(reader["Email"]);
                    user.Name = Convert.ToString(reader["Name"]);
                    user.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                    user.Password = Convert.ToString(reader["Password"]);
                }
            }
            return user;


        }
    }
}
