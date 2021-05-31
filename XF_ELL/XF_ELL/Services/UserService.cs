using XF_ELL.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private List<UserDTO> _users = new List<UserDTO>()
        {
            new UserDTO()
            {
                 Name = "teofana",
                 Adress = "Falticeni",
                 BirthDate = new DateTime(1998,4,11),
                 Password = "parola",
                 Phone = "0768234834",
                 Type = UserType.ADMIN
            },
            new UserDTO()
            {
                 
                 Name = "paul",
                 Adress = "Suceava",
                 BirthDate = new DateTime(1996,2,23),
                 Password = "parola",
                 Phone = "0768234834",
                 Type = UserType.REGULAR
            },
            new UserDTO()
            {
                 
                 Name = "peter",
                 Adress = "Baraolt",
                 BirthDate = new DateTime(1991,6,30),
                 Password = "parola",
                 Phone = "0768234834",
                 Type = UserType.REGULAR
            },
        };

        public async Task<UserDTO> GetByName(string name)
        {

            //UserDTO res = null;

            //using (var client = new HttpClient())
            //{
            //    var result = await client.GetAsync($"https://internshipwebapp.azurewebsites.net/api/Users/{name}");
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var response = await result.Content.ReadAsStringAsync();
            //        res = JsonConvert.DeserializeObject<UserDTO>(response);
            //    }
            //}

            //return res;
            return _users.FirstOrDefault(u => u.Name.Equals(name));
        }

        public async Task<bool> Login(string username, string password)
        {
            var user =_users.FirstOrDefault(u => u.Name.Equals(username));

            if (user == null)
                return false;

            if (!user.Password.Equals(password))
                return false;

            return true;
        }

        public async Task<UserDTO> Update(string name, UserDTO user)
        {
            //UserDTO res = null;

            //using (var client = new HttpClient())
            //{

            //    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            //    var callResult = await client.PutAsync($"https://internshipwebapp.azurewebsites.net/api/Users/{name}", content);
            //    if (callResult.IsSuccessStatusCode)
            //    {
            //        var jsonResult = await callResult.Content.ReadAsStringAsync();
            //        res = JsonConvert.DeserializeObject<UserDTO>(jsonResult);
            //    }
            //}

            //return res;

            var oldUser = _users.FirstOrDefault(u => u.Name == name);
            oldUser.Name = user.Name;
            oldUser.Phone = user.Phone;
            oldUser.Adress = user.Adress;
            oldUser.BirthDate = user.BirthDate;
            oldUser.Password = user.Password;
            oldUser.EncodedImage = user.EncodedImage;
            

            return oldUser;
        }

        public async Task<UserDTO> Save(UserDTO user)
        {
            //UserDTO res = null;

            //using (var client = new HttpClient())
            //{

            //    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            //    var callResult = await client.PostAsync($"https://internshipwebapp.azurewebsites.net/api/Users/", content);
            //    if (callResult.IsSuccessStatusCode)
            //    {
            //        var jsonResult = await callResult.Content.ReadAsStringAsync();
            //        res = JsonConvert.DeserializeObject<UserDTO>(jsonResult);
            //    }
            //}

            //return res;
            _users.Add(user);
            return user;
        }

        public async Task Delete(string name)
        {
            //using (var client = new HttpClient())
            //{
            //    var callResult = await client.DeleteAsync($"https://internshipwebapp.azurewebsites.net/api/Users/{name}");
            //    Console.WriteLine(callResult.StatusCode);
            //}
            var user = _users.FirstOrDefault(u => u.Name == name);
            _users.Remove(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            //List<UserDTO> res = null;
            //using (var client = new HttpClient())
            //{
            //    var callResult = await client.GetStringAsync($"https://internshipwebapp.azurewebsites.net/api/Users/");
            //    if (callResult != null)
            //    {
            //        res = JsonConvert.DeserializeObject<List<UserDTO>>(callResult);
            //    }
            //}

            //return res;
            return _users;
        }
    }
}
