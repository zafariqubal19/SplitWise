using System.Drawing.Text;
using System.Collections.Generic;
using SplitWise.Models;
using Newtonsoft.Json;

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
    }
}
