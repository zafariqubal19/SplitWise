using System.Drawing.Text;
using System.Collections.Generic;
using SplitWise.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace SplitWise.Facade
{
    public class ApplicationFacade:IApplicatioFacade
    {
        private readonly HttpClient _httpClient;
        public ApplicationFacade()
        {
            _httpClient=new HttpClient();
        }
    
       public async Task<List<User>> GetAllUser()
        {
            List<User> users=new List<User>();
            string url = "https://localhost:7154/api/Split/GetUsers";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content=await response.Content.ReadAsStringAsync();  
               users = JsonConvert.DeserializeObject<List<User>>(content);
            }
            else
            {
                users = new List<User>();
            }
            return users;
        }
        public async Task<User> RegisterUser(User user)
        {
            string url = "https://localhost:7154/api/Split/RegisterUser";
            var UserDetails=JsonConvert.SerializeObject(user);
            var content = new StringContent(UserDetails, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url,content);
            if (response.IsSuccessStatusCode)
            {
                return user;
            }
            else
            {
                user=new User();
            }
            return user;
        }
    }
}
