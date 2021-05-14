using Blog.WEB.Models;
using System.Web.Mvc;


namespace Blog.WEB.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tag()
        {
            return View();
        }

        public ActionResult Text()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Tag(SearchModel model)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Text(SearchModel model)
        {
            return View();
        }
    }
}