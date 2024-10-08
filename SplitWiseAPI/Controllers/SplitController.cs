﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Models.RequestParameters;
using SplitWiseAPI.Services.Interface;

namespace SplitWiseAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    
    public class SplitController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IMemberService _memberService;
        public SplitController(IUserService userService, IGroupService groupService, IMemberService memberService)
        {
            _memberService = memberService;
            _userService = userService;
            _groupService = groupService;
        }
        [HttpGet]
        [Route("GetUsers")]
        [Authorize]
        public List<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }
        [HttpPost]
        [Route("CreateGroup")]
        public int CreateGroup(GroupCreation group)
        {
            Group created = _groupService.GetCreatedGroup(group.UserId, group.GroupName);
            if (created.GroupId == 0)
            {
                int effectRows = _groupService.CreateGroup(group.GroupName, group.UserId);
                if (effectRows > 0)
                {
                    Group groups = _groupService.GetCreatedGroup(group.UserId, group.GroupName);
                    int effected = _memberService.AddMembers(groups.GroupId, groups.CreatorEmail);
                    return effectRows;

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 409;
            }
        }
        [HttpPost]
        [Route("AddMembers")]
        public int AddMembers(AddMembersModel model)
        {
            User user=_userService.GetUserByEmail(model.Email);
            int effectedRows=0;
            if(user.Email != null) 
            {

               effectedRows=  _memberService.AddMembers(model.GroupId, model.Email);
            }
            else
            {
                User newuser= new User();
                newuser.Email = model.Email;
                newuser.Name = model.Name;
                newuser.PhoneNumber = "";
                newuser.Password = "";
                newuser.IsRegistered = false;
                
                
                int UserInsert=_userService.RegisterUser(newuser);
                if(UserInsert > 0)
                {
                    User newInsertedUser=_userService.GetUserByEmail(newuser.Email);
                    if(newInsertedUser != null)
                    {
                        effectedRows = _memberService.AddMembers(model.GroupId, model.Email);
                    }
                }

            }
            return effectedRows;
            

        }
        [HttpGet]
        [Route("GetGroupDetails")]
        public GroupDetails GetGroupDetails(int groupdId)
        {
            return _groupService.GetGroupDetails(groupdId);
        }
        [HttpGet]
        [Route("GetUsersGroups")]
        public MembersGroups GetUsersGroups(int UserId)
        {
            return _groupService.GetMembersGroup(UserId);
        }
        [HttpDelete]
        [Route("DeleteGroup")]
        public int DeleteGroup(int groupId)
        {
            GroupDetails groupDetails = _groupService.GetGroupDetails(groupId);
            foreach (var item in groupDetails.Members)
            {
                _memberService.DeleteMembers(groupId, item.UserId);
            }
            return _groupService.DeletGroup(groupId);
        }
        [HttpDelete]
        [Route("DeleteMember")]
        public int DeleteMembers(int groupId, int userId)
        {
            return _memberService.DeleteMembers(groupId, userId);
        }
        



    }
}
