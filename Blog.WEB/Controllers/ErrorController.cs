using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    /// <summary>
    /// Controller to manage blogs
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Returns a custom NotFount page
        /// </summary>
        /// <returns>Action Result</returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// Returns a custom Forbidden page
        /// </summary>
        /// <returns>Action Result</returns>
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}