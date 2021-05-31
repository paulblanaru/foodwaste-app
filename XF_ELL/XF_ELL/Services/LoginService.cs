using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XF_ELL.Services
{
    public class LoginService : ILoginService
    {

        private HttpClient _client;

        public LoginService()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://internshipwebapp.azurewebsites.net") };
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        async Task<bool> ILoginService.Login(string username, string password)
        {
            //try
            //{
            //    using (var httpResponseMessage = await _client.GetAsync($"/api/Users/{username}"))
            //    {
            //        if (httpResponseMessage.IsSuccessStatusCode)
            //        {
            //            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            //            var obj = JsonConvert.DeserializeObject<UserViewModel>(jsonString);
            //            return (obj.Name == username && obj.Password == password);
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    return false;
            //}
            //return false;

            return true;
        }
    }
}
