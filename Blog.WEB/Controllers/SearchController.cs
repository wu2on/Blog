using Blog.BLL.Dto;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using System;
using System.Web.Mvc;


namespace Blog.WEB.Controllers
{
    public class SearchController : Controller
    {
        private IBlogService BlogService;

        public SearchController(IBlogService service)
        {
            BlogService = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {

            if (ModelState.IsValid)
            {
                SearchDto search = new SearchDto { Text = model.Text };

                BlogService.SearchBlogs(search);
            }

            throw new NotImplementedException();
        }
    }
}