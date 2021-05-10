using Blog.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using System.Threading.Tasks;

namespace Blog.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IBlogService BlogService;

        public HomeController(IBlogService service)
        {
            BlogService = service;
        }
        public ActionResult Index()
        {
            List<BlogDto> blogs = BlogService.GetAllBlogs();
            return View(blogs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var result = HttpContext.GetOwinContext().Authentication.User;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}