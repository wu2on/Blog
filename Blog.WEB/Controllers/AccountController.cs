using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace Blog.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService;

        public AccountController(IUserService service)
        {
            UserService = service;
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreateAt = DateTime.Now,
                    Role = "user",
                    IsDeleted = false
                    
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInModel model)
        {
            //await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
                else
                {
                    HttpContext.GetOwinContext().Authentication.SignOut();
                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //private async Task SetInitialDataAsync()
        //{
        //    await UserService.SetInitialData(new UserDto
        //    {
        //        Email = "Romanodnosum@gmail.com",
        //        FirstName = "Roman",
        //        LastName = "Odnosum",
        //        Password = "123456",
        //        Role = "admin",
        //        CreateAt = DateTime.Now,
        //        IsDeleted = false
        //    }, new List<string> { "user", "admin" });
        //}
    }
}