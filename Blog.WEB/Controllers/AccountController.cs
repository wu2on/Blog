using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService;

        public AccountController(IUserService service)
        {
            UserService = service;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreateAt = DateTime.Now,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDto
            {
                Email = "romanodnosum@gmail.com",
                Password = "123456",
                FirstName = "Roman",
                LastName = "Odnosum",
                CreateAt = DateTime.Now,
                Role="admin",
            }, new List<string> { "user", "admin" });
        }
    }
}