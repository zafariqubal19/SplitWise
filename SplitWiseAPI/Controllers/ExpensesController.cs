using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;

namespace SplitWiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController( IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        [HttpPost]
        [Route("AddExpenes")]
        public int AddExpenses(Expense expense)
        {
            return _expenseService.AddExpenses(expense);
        }
    }
}
