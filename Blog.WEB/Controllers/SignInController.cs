using Blog.BLL.Dto;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class SignInController : Controller
    {
        private IUserService UserService;

        public SignInController(IUserService service)
        {
            UserService = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                
                    return RedirectToAction("Index", "Home");
                
            }
            return View(model);
        }
    }
}
