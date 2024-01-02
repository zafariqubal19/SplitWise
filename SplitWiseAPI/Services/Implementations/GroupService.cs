using SplitWiseAPI.Models;
using SplitWiseAPI.Services.Interface;
using System.Data;
using System.Data.SqlClient;

namespace SplitWiseAPI.Services.Implementations
{
    public class GroupService:IGroupService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly IConfiguration _configuration;
        
        public GroupService(IConfiguration configuration)
        {


            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration.GetConnectionString("SplitWiseDB"));

        }
        public int CreateGroup(Group group)
        {

            string sp = "sp_CreateGroup";
            SqlCommand sqlCommand=new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@GroupName", group.GroupName);
            sqlCommand.Parameters.AddWithValue("@UserId", group.UserId);
            _sqlConnection.Open();
            int effecrRows=sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            return effecrRows;
        }
        public GroupDetails GetGroupDetails(int groupId ) {
            string sp = "sp_GetGroupDetails";
            GroupDetails groupDetails = new GroupDetails();
            List<GroupMembers> members = new List<GroupMembers>();
            SqlCommand sqlCommand=new SqlCommand( sp, _sqlConnection);
            sqlCommand.CommandType= CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@GroupId", groupId);
            _sqlConnection.Open ();
           SqlDataReader reader= sqlCommand.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    GroupMembers member = new GroupMembers();
                    member.Name = Convert.ToString(reader["Name"]);
                    member.Email = Convert.ToString(reader["Email"]);
                    groupDetails.GroupName = Convert.ToString(reader["GroupName"]);
                    members.Add(member);
                }
            }
            groupDetails.Members = members;
         return groupDetails;
        
        }
      public MembersGroups GetMembersGroup(int UserId) 
        {
            string sp = "sp_GetUsersAllGroup";
            SqlCommand sqlCommand= new SqlCommand( sp, _sqlConnection); 
            sqlCommand.CommandType= CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserId", UserId);
            _sqlConnection.Open ();

            List<GroupsName> groupName = new List<GroupsName>();
           MembersGroups membersGroups = new MembersGroups();
            SqlDataReader reader= sqlCommand.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    GroupsName group=new GroupsName();
                    membersGroups.Member = Convert.ToString(reader["Name"]);
                    group.GroupName = Convert.ToString(reader["GroupName"]);
                    group.GroupId = Convert.ToInt32(reader["GroupId"]);
                    groupName.Add(group);
                    
                    
                }
            }
            membersGroups.Groups = groupName;
            
        return membersGroups;
        }
    }
}
