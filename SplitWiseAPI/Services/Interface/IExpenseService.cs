using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services.Interface
{
    public interface IExpenseService
    {
        int AddExpenses(Expense expense);
        IEnumerable<Expense> GetExpenses(int GroupId);
        int DeleteExpenses(int ExpenseId);
    }
}
