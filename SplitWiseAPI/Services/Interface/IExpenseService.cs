using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services.Interface
{
    public interface IExpenseService
    {
        int AddExpenses(Expense expense);
        IEnumerable<Expense> GetExpenses(int GroupId);
        int DeleteExpenses(int ExpenseId);
        IEnumerable<Expense> GetAllMyExpenses(int UserId, int GroupId);
        int SettleUp( int GroupId);
    }
}
