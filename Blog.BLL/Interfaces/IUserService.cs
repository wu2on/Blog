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
        Task<OperationDetails> UpdateUserData(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task<UserDto> GetUserAsync(string Id);
        List<UserDto> GetAllUsers();
        Task<OperationDetails> ChangePasswordAsync(ChangedPasswordDto password);
        Task SetInitialData(UserDto adminDto, List<string> roles);
        void Dispose();
    }
}
