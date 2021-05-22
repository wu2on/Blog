using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;

using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;

namespace Blog.WEB.Controllers
{
    /// <summary>
    /// Controller to register new user or authentication
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The Users Service service
        /// </summary>
        private IUserService UserService;

        /// <summary>
        /// Initializes a new instance of the AccountController
        /// </summary>
        /// <param name="service">
        /// Users service
        /// </param>
        public AccountController(IUserService service)
        {
            UserService = service;
        }

        /// <summary>
        ///  Form for register
        /// </summary>
        /// <returns>Register view for user</returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Add new user 
        /// </summary>
        /// <param name="model">User request from form</param>
        /// <returns>SuccessRegister View or error</returns>
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
                {
                    return View("SuccessRegister");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }  
            }

            return View(model);
        }
        /// <summary>
        /// Form for sign in user
        /// </summary>
        /// <returns>View SignIn</returns>
        public ActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// User loggin into the blog
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// User logging out
        /// </summary>
        /// <returns>Redirect to main guest view</returns>
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