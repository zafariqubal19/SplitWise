using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;
using System.Data;
using System.Data.SqlClient;

namespace SplitWiseAPI.Services.Implementations
{
    public class ExpenseService: IExpenseService
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public ExpenseService( IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("SplitWiseDB"));
        }
        public int AddExpenses(Expense expense)
        {
            string sp = "AddExpenses";
            using(SqlCommand cmd = new SqlCommand(sp,_connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", expense.GroupId);
                cmd.Parameters.AddWithValue("@UserId", expense.UserId);
                cmd.Parameters.AddWithValue("@Description", expense.Description);
                cmd.Parameters.AddWithValue("@Spender", expense.Spender);
                cmd.Parameters.AddWithValue("@TotalAmount", expense.TotalAmount);
                _connection.Open();
                int deffectedRows=cmd.ExecuteNonQuery();
                return deffectedRows;
            }
        }
        public IEnumerable<Expense> GetExpenses(int GroupId)
        {
            string sp = "sp_GetAllExpenses";
            List<Expense> expenses = new List<Expense>();
            using (SqlCommand cmd = new SqlCommand(sp, _connection))
            {
                
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", GroupId);
                _connection.Open();
                SqlDataReader reader= cmd.ExecuteReader();
              if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Expense expense = new Expense();
                        expense.ExpenseId = Convert.ToInt32(reader["ExpenseId"]);
                        expense.GroupId = Convert.ToInt32(reader["GroupId"]);
                        expense.UserId = Convert.ToInt32(reader["UserId"]);
                        expense.Spender = Convert.ToString(reader["Spender"]);
                        expense.Description = Convert.ToString(reader["Description"]);
                        expense.TotalAmount = Convert.ToInt32(reader["TotalAmount"]);
                        expenses.Add(expense);
                    }
                }
            }
            return expenses;
        }
        public int DeleteExpenses(int ExpenseId)
        {
            string sp = "sp_DeleteExpenses";
            using(SqlCommand cmd = new SqlCommand(sp,_connection))
            {
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExpenseId", ExpenseId);
                _connection.Open();
               int effectedRows= cmd.ExecuteNonQuery();
                return effectedRows;
            }
        }
        public IEnumerable<Expense> GetAllMyExpenses(int UserId,int GroupId)
        {
            //string sp = "SELECT * FROM EXPENSES WHERE UserId=3 AND GroupId=1";
            string sp = "sp_GetAllMyExpenses";
            List<Expense> expenses =new  List<Expense>();
            SqlCommand sqlCommand = new SqlCommand(sp,_connection);
            sqlCommand.CommandType=CommandType.StoredProcedure;
           //sqlCommand.CommandText = sp;
            sqlCommand.Parameters.AddWithValue("@UserId", UserId);
            sqlCommand.Parameters.AddWithValue("@GroupId", GroupId);
            _connection.Open ();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    Expense expense = new Expense();
                    expense.ExpenseId = Convert.ToInt32(reader["ExpenseId"]);
                    expense.UserId = Convert.ToInt32(reader["UserId"]);
                    expense.GroupId = Convert.ToInt32(reader["GroupId"]);
                    expense.Description = Convert.ToString(reader["Description"]);
                    expense.Spender = Convert.ToString(reader["Spender"]);
                    expense.TotalAmount = Convert.ToInt32(reader["TotalAmount"]);
                    expenses.Add(expense);
                }
            }
            _connection.Close();
            return expenses;

        }
        public int SettleUp(int GroupId)
        {
            string sp = "sp_SettleExpenses";
            SqlCommand sqlCommand= new SqlCommand(sp,_connection);
            sqlCommand.CommandType= CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@GroupId",GroupId); 
            _connection.Open();
             int effectedRows=sqlCommand.ExecuteNonQuery();
            _connection.Close();
            return effectedRows;
        }
   
    }
}
