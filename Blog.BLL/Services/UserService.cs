using Blog.BLL.Dto;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Blog.BLL.Service
{
    public class UserService : IUserService
    {
        IUnitOfWork _uow { get; set; }

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<OperationDetails> Create(UserDto userDto)
        {
            User user = await _uow.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email };
                var result = await _uow.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await _uow.UserManager.AddToRoleAsync(user.Id, userDto.Role);

                ClientProfile clientProfile = new ClientProfile { Id = user.Id, FirstName = userDto.FirstName, CreatedAt = userDto.CreateAt, LastName = userDto.LastName, Email = userDto.Email };
                _uow.ClientManager.Create(clientProfile);
                await _uow.SaveAsync();
                
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            ClaimsIdentity claim = null;

            User user = await _uow.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await _uow.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(UserDto adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _uow.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new Role { Name = roleName };
                    await _uow.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}