using XF_ELL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<UserDTO> GetByName(string name);
        Task<UserDTO> Save(UserDTO user);
        Task<IEnumerable<UserDTO>> GetAll();
        Task Delete(string name);
        Task<UserDTO> Update(string name, UserDTO user);
        Task<bool> Login(string username, string password);
    }
}
