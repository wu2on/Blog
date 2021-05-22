using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using AutoMapper;



namespace Blog.WEB.Controllers
{
    /// <summary>
    /// Controller to manage user credentials
    /// </summary>
    public class ManageController : Controller
    {
        /// <summary>
        /// The Users Service service
        /// </summary>
        private IUserService UserService;

        /// <summary>
        /// Initializes a new instance of the ManageController
        /// </summary>
        /// <param name="service">
        /// Users service
        /// </param>
        public ManageController(IUserService service)
        {
            UserService = service;
        }

        /// <summary>
        /// Show user personal data
        /// </summary>
        /// <returns>Action result</returns>
        [Authorize]
        public async Task<ActionResult> Index()
        {
            string currentUserId = User.Identity.GetUserId();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, ViewUserModel>();
            });

            Mapper mapper = new Mapper(config);

            ViewUserModel user = mapper.Map<ViewUserModel>(await UserService.GetUserAsync(currentUserId));

            if(User.IsInRole("admin"))
            {
                List<UserDto> result = UserService.GetAllUsers();

                user.Users = result;

                return View(user);
            } 

            return View(user);
        }

        /// <summary>
        /// Change password view
        /// </summary>
        /// <returns>Action result</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="model">Requst from user to change password</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ChangedPasswordDto password = new ChangedPasswordDto 
                {
                    UserId = User.Identity.GetUserId(),
                    NewPassword = model.NewPassword,
                    OldPassword = model.OldPassword
                };

                OperationDetails operationDetails = await UserService.ChangePasswordAsync(password);

                if(operationDetails.Succedeed) return View("SuccessChangePassword");

                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }

        /// <summary>
        /// Change user View
        /// </summary>
        /// <param name="Id">User identifier</param>
        /// <returns>ActionResult</returns>
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ChangeUser(string Id)
        {
            bool isAdmin = User.IsInRole("admin");

            if(isAdmin)
            {
                UserDto user = await UserService.GetUserAsync(Id);
                return View(user);
            }
            
            return View();
        }

        /// <summary>
        /// Change user data
        /// </summary>
        /// <param name="user">Requst from admin to change user data</param>
        /// <returns>ActionResult</returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUser(UserDto user)
        {
            bool isAdmin = User.IsInRole("admin");

            if (isAdmin)
            {
                OperationDetails operationDetails = await UserService.UpdateUserData(user);
                var updatedUser = await UserService.GetUserAsync(user.Id);
                if(operationDetails.Succedeed)
                {
                    return View(updatedUser);
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                } 
            }

            return View();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="Id">User identifier</param>
        /// <returns>Action result</returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            bool isAdmin = User.IsInRole("admin");

            if (isAdmin)
            {
                OperationDetails operationDetails = await UserService.DeleteUser(Id);

                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
