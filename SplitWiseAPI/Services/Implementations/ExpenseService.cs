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
   
    }
}
