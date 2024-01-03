using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services.Interface
{
    public interface IExpenseService
    {
        int AddExpenses(Expense expense);
    }
}
