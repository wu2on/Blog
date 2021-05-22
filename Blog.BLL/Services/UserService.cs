using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

using Blog.BLL.Dto;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;

using Microsoft.AspNet.Identity;
using AutoMapper;

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
        public async Task<OperationDetails> UpdateUserData(UserDto userDto)
        {
            var user = _uow.UserProfileRepository.GetFirstOrDefault(x => x.Id == userDto.Id);
            string role = _uow.UserManager.GetRoles(userDto.Id).FirstOrDefault();

            if (user != null)
            {
                if(userDto.Role != role || role == null)
                {
                    if(role != null) _uow.UserManager.RemoveFromRole(user.Id, role);
                    _uow.UserManager.AddToRole(user.Id, userDto.Role);
                }
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                _uow.UserProfileRepository.Update(user);

                await _uow.SaveAsync();

                return new OperationDetails(true, "Profile has been successfully updated", "");
            }

            return new OperationDetails(false, "Profile hasn't been updated", "profile edit");
        }

        public async Task<OperationDetails> DeleteUser(string Id)
        {
            if(Id != null)
            {
                UserProfile user = _uow.UserProfileRepository.GetFirstOrDefault(x => x.Id == Id);
                user.IsDeleted = true;
                _uow.UserProfileRepository.Update(user);
                await _uow.SaveAsync();
                return new OperationDetails(true, "Account has been deleted", "");
            }

            return new OperationDetails(false, "Account hasn't been deleted", "Delete profile");
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

            UserDto user = mapper.Map<UserDto>(await _uow.UserProfileRepository.GetFirstOrDefaultAsync(x => x.Id == Id));

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

        public List<UserDto> GetAllUsers()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfile, UserDto>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => _uow.UserManager.GetRoles(src.Id).FirstOrDefault()));
            });

            Mapper mapper = new Mapper(config);

            List<UserDto> users = mapper.Map<List<UserDto>>(_uow.UserProfileRepository.GetAll());

            return users;
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