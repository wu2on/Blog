using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        UserDto GetUser(string Id);
        Task SetInitialData(UserDto adminDto, List<string> roles);
        void Dispose();
    }
}
