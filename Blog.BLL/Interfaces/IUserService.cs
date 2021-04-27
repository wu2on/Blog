using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        void Dispose();
    }
}
