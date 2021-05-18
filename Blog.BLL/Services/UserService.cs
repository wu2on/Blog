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
using AutoMapper;
using System;

namespace Blog.BLL.Services
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
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await _uow.UserManager.AddToRoleAsync(user.Id, userDto.Role);

                UserProfile userProfile = new UserProfile { Id = user.Id, FirstName = userDto.FirstName, CreatedAt = userDto.CreateAt, LastName = userDto.LastName, Email = userDto.Email, IsDeleted = userDto.IsDeleted };
                _uow.UserProfileRepository.Create(userProfile);
                await _uow.SaveAsync();
                
                return new OperationDetails(true, "Registration completed successfully", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login already exists", "Email");
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

        public async Task<UserDto> GetUserAsync(string Id)
        {
             MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfile, UserDto>();
            });

            Mapper mapper = new Mapper(config);

            string role = _uow.UserManager.GetRoles(Id).FirstOrDefault();
            UserDto user = mapper.Map<UserDto>(await _uow.UserProfileRepository.GetFirstOrDefault(x => x.Id == Id));

            user.Role = role;

            return user;
        }

        public async Task<OperationDetails> ChangePasswordAsync(ChangedPasswordDto password)
        {
            var result = _uow.UserManager.ChangePasswordAsync(password.UserId, password.OldPassword, password.NewPassword);

            if(result.Result.Succeeded)
            {
                return new OperationDetails(true, "Password changed successfully", "");
            }

            return new OperationDetails(false, result.Result.Errors.FirstOrDefault(), "Password");
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