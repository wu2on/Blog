using System.Collections.Generic;
using System.Web.Mvc;

using Blog.WEB.Models;
using Blog.BLL.Dto;
using Blog.BLL.Interfaces;
using AutoMapper;

namespace Blog.WEB.Controllers
{
    /// <summary>
    /// Controller to show blogs
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The Blog Service service
        /// </summary>
        private IBlogService BlogService;

        /// <summary>
        /// Initializes a new instance of the HomeController
        /// </summary>
        /// <param name="service">Blog service</param>
        public HomeController(IBlogService service)
        {
            BlogService = service;
        }

        /// <summary>
        /// Shows all blogs of all users
        /// </summary>
        /// <returns>Action Result</returns>
        public ActionResult Index()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDto, BlogPreviewModel>().ForMember("UserEmail", x => x.MapFrom(c => c.UserProfile.Email));
            });

            Mapper mapper = new Mapper(config);

            List<BlogPreviewModel> blogs = mapper.Map<List<BlogPreviewModel>>(BlogService.GetAllBlogs());

            return View(blogs);
        }

        /// <summary>
        /// Read Blog
        /// </summary>
        /// <param name="Id">Blog identifier</param>
        /// <returns></returns>
        public ActionResult Details(int Id)
        {
            var blog  = BlogService.GetDetails(Id);

            return View(blog);
        }
    }
}