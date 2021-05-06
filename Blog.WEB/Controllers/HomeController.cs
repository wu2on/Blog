﻿using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
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