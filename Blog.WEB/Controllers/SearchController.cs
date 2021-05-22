using System.Collections.Generic;
using System.Web.Mvc;

using Blog.BLL.Dto;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using AutoMapper;


namespace Blog.WEB.Controllers
{
    /// <summary>
    /// Controller to search blogs by tag or text
    /// </summary>
    public class SearchController : Controller
    {
        /// <summary>
        /// The Blog Service service
        /// </summary>
        private IBlogService BlogService;

        /// <summary>
        /// Initializes a new instance of the SearchController
        /// </summary>
        /// <param name="service">Blog service</param>
        public SearchController(IBlogService service)
        {
            BlogService = service;
        }

        /// <summary>
        /// Show search view
        /// </summary>
        /// <returns>Action reslut</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search blogs by tag or text
        /// </summary>
        /// <param name="model">Request from user to search blogs</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Result(SearchModel model)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDto, BlogPreviewModel>().ForMember("UserEmail", x => x.MapFrom(c => c.UserProfile.Email));
            });

            Mapper mapper = new Mapper(config);

            if (ModelState.IsValid)
            {
                SearchDto search = new SearchDto { Text = model.Text };

                var result = BlogService.SearchBlogs(search);

                List<BlogPreviewModel> blogs = mapper.Map<List<BlogPreviewModel>>(result);

                return View(blogs);
            }

            return View();
        }
    }
}