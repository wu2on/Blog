using AutoMapper;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class ManageController : Controller
    {
        private IUserService UserService;

        public ManageController(IUserService service)
        {
            UserService = service;
        }
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

            return View(user);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
