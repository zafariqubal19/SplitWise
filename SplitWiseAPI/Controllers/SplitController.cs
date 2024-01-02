﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;

namespace SplitWiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SplitController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IMemberService _memberService;
        public SplitController( IUserService userService,IGroupService groupService,IMemberService memberService)
        {
            _memberService = memberService;
            _userService = userService;
            _groupService = groupService;
        }
        [HttpGet]
        [Route("GetUsers")]
        public List<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }
        [HttpPost]
        [Route("CreateGroup")]
        public string CreateGroup(Group group)
        {
                int effectedRows = _groupService.CreateGroup(group);
                if(effectedRows > 0)
                {
                    return "Group Created";
                }
                else
                {
                    return "Group Creation Failed";
                }


        }
        [HttpPost]
        [Route("AddMembers")]
        public string AddMembers(int groupdId,string email)
        {
            int effectedRows=_memberService.AddMembers(groupdId,email);
            if(effectedRows > 0)
            {
                return "Member added";

            }
            else
            {
                return "Member Addition fail";
            }
        }
            
        
      
    }
}
