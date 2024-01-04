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
        public int CreateGroup(string groupName,int UserId)
        {

            string sp = "sp_CreateGroup";
            SqlCommand sqlCommand=new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@GroupName", groupName);
            sqlCommand.Parameters.AddWithValue("@UserId", UserId);
            _sqlConnection.Open();
            int effecrRows=sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            return effecrRows;
        }
        public GroupDetails GetGroupDetails(int groupId) {
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
                    member.MemberId = Convert.ToInt32(reader["MemberId"]);
                    member.UserId= Convert.ToInt32(reader["UserId"]);
                    if (reader["TotalAmountSpent"] != DBNull.Value) {
                        member.TotalAmountSpent = Convert.ToInt32(reader["TotalAmountSpent"]);
                    }
                    if( reader["TotalAmountToGive"] != DBNull.Value)
                    {
                        member.TotalAmountToGive = Convert.ToInt32(reader["TotalAmountToGive"]);
                    }
                    if (reader["TotalAmountToReceive"] != DBNull.Value)
                    {
                        member.TotalAmountToReceive = Convert.ToInt32(reader["TotalAmountToReceive"]);
                    }
                    member.Name = Convert.ToString(reader["Name"]);
                    member.Email = Convert.ToString(reader["Email"]);
                 
                    groupDetails.GroupName = Convert.ToString(reader["GroupName"]);
                    groupDetails.GroupId = Convert.ToInt32(reader["GroupId"]);
                    members.Add(member);
                }
            }
            groupDetails.Members = members;
            _sqlConnection.Close();
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
                    membersGroups.UserId = Convert.ToInt32(reader["UserId"]);
                    group.GroupName = Convert.ToString(reader["GroupName"]);
                    group.GroupId = Convert.ToInt32(reader["GroupId"]);
                    group.CreatorId = Convert.ToInt32(reader["CreatorId"]);

                    groupName.Add(group);
                    
                    
                }
            }
            membersGroups.Groups = groupName;
            _sqlConnection.Close ();
            
        return membersGroups;
        }
        public Group GetCreatedGroup(int UserId,string groupName) 
        {
            string sp = "sp_getCreatedGroup";
            Group createdGroup = new Group();
            SqlCommand sqlCommand= new SqlCommand(sp, _sqlConnection);
            sqlCommand.CommandType= CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserId", UserId);
            sqlCommand.Parameters.AddWithValue("@GroupName", groupName);
            _sqlConnection.Open();
            SqlDataReader sqlDataReader= sqlCommand.ExecuteReader();
            if(sqlDataReader.HasRows)
            {
                while(sqlDataReader.Read())
                {
                    createdGroup.GroupId = Convert.ToInt32(sqlDataReader["GroupId"]);
                    createdGroup.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                    createdGroup.GroupName = Convert.ToString(sqlDataReader["GroupName"]);
                    createdGroup.CreatorEmail = Convert.ToString(sqlDataReader["Email"]);
                }
            }
            _sqlConnection.Close();
            return createdGroup;

        }
        public int DeletGroup(int groupId)
        {
            string sp = "sp_DeleteGroup";
            int effectedRows;
            using (SqlCommand sqlCommand= new SqlCommand( sp, _sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@GroupId", groupId);
                _sqlConnection.Open();
                 effectedRows = sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();

            }
            return effectedRows;
        }
    }
}
