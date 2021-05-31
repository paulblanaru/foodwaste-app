using Newtonsoft.Json;
using System;

namespace XF_ELL.DTO
{
    
    public class UserDTO
    {
        public UserDTO()
        {
        }

        public string Name { get; set; }
        
        public string Adress { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string EncodedImage { get; set; }

        public UserType Type { get; set; }

    }
}
