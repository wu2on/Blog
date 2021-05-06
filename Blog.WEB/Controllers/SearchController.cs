using System.Web.Mvc;


namespace Blog.WEB.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            ViewBag.Message = User.Identity;
            return View();
        }
    }
}