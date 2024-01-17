using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Implementations;
using SplitWiseAPI.Services.Interface;

namespace SplitWiseAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IGroupService _groupService;
        private readonly IMemberService _memberService;
        public ExpensesController( IExpenseService expenseService, IGroupService groupService, IMemberService memberService)
        {
            _expenseService = expenseService;
            _groupService = groupService;
            _memberService = memberService;
        }
        [HttpPost]
        [Route("AddExpenes")]
        public int AddExpenses(Expense expense)
        {
            int effectedRows= _expenseService.AddExpenses(expense);
            int split=SplitAmount(expense);
            return effectedRows;

        }
        [HttpGet]
        [Route("GetAllExpenses")]
        public IEnumerable<Expense> GetExpenses(int GroupId)
        {
            return _expenseService.GetExpenses(GroupId);
        }
        [HttpDelete]
        [Route("DeleteExpenses")]
        public int DeleteExpenses(int ExpenseId)
        {
            return _expenseService.DeleteExpenses(ExpenseId);
        }
        [HttpPost]
        public int SplitAmount(Expense expense)
        {
            GroupDetails groupDetails = new GroupDetails();
            int effectedRows = 0;
            groupDetails = _groupService.GetGroupDetails(expense.GroupId);
            int splitAmount = expense.TotalAmount / groupDetails.Members.Count();
            foreach (var item in groupDetails.Members)
            {
                Members members = new Members();
                if (item.UserId == expense.UserId)
                {
                    members.UserId = item.UserId;
                    members.GroupId=expense.GroupId;
                    if(item.TotalAmountToGive>0 )
                    {
                        if (item.TotalAmountToGive > (expense.TotalAmount - splitAmount))
                        {
                            members.TotalAmountToGive=item.TotalAmountToGive-(expense.TotalAmount - splitAmount);
                            members.TotalAmountToReceive = 0;

                            
                        }
                        else
                        {
                            members.TotalAmountToReceive=(expense.TotalAmount - splitAmount)-item.TotalAmountToGive;
                            members.TotalAmountToGive = 0;
                        }
                    //    members.TotalAmountToReceive= (expense.TotalAmount-splitAmount)-item.TotalAmountToGive;
                      //  members.TotalAmountToGive = 0;
                    }
                    else 
                    {
                      members.TotalAmountToReceive=item.TotalAmountToReceive+(expense.TotalAmount - splitAmount);
                        members.TotalAmountToGive = 0;
                    }
                    //members.TotalAmountToReceive = item.TotalAmountToReceive + expense.TotalAmount - splitAmount-item.TotalAmountToGive;
                    //if(members.TotalAmountToReceive <= 0)
                    //{
                    //    members.TotalAmountToReceive = 0;
                    //}
                    members.TotalAmountSpent = item.TotalAmountSpent + expense.TotalAmount;
                    //members.TotalAmountToGive = item.TotalAmountToGive +splitAmount-item.TotalAmountToReceive;
                    //if(members.TotalAmountToGive <= 0)
                    //{
                    //    members.TotalAmountToGive = 0;
                    //}
                    effectedRows = _memberService.UpdateMembersAmount(members);


                }
                else
                {
                    members.UserId = item.UserId;
                    members.GroupId = expense.GroupId;
                   if(item.TotalAmountToReceive>0)
                    {
                        if(item.TotalAmountToReceive > (splitAmount))
                        {
                            members.TotalAmountToReceive = item.TotalAmountToReceive - splitAmount;
                            members.TotalAmountToGive = 0;
                        }
                        else
                        {
                            members.TotalAmountToGive=splitAmount-item.TotalAmountToReceive;
                            members.TotalAmountToReceive = 0;
                        }
                    }
                    else
                    {
                        members.TotalAmountToGive = item.TotalAmountToGive + splitAmount;
                        members.TotalAmountToReceive = 0;
                    }
                  //  members.TotalAmountToGive = item.TotalAmountToGive + splitAmount;
                    members.TotalAmountSpent = item.TotalAmountSpent;
                    //if(splitAmount<item.TotalAmountToReceive) {
                    //    members.TotalAmountToReceive = item.TotalAmountToReceive-splitAmount;

                    //}
                    //else
                    //{

                    //}
                    
                    effectedRows = _memberService.UpdateMembersAmount(members);
                }
            }
            return effectedRows;
        }
        [HttpGet]
        [Route("GetAllMyExpenses")]
        public IEnumerable<Expense> AllMyExpenses(int UserId,int GroupId)
        {
            return _expenseService.GetAllMyExpenses(UserId, GroupId);
        }
        [HttpPut]
        [Route("SettleUp")]
        public int SettleExpenses(int GroupId)
        {
            return _expenseService.SettleUp(GroupId);
        }

    }
}
