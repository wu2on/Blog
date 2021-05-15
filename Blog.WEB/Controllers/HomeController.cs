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
using AutoMapper;

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
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDto, BlogPreviewModel>().ForMember("UserEmail", x => x.MapFrom(c => c.UserProfile.Email));
            });

            Mapper mapper = new Mapper(config);

            List<BlogPreviewModel> blogs = mapper.Map<List<BlogPreviewModel>>(BlogService.GetAllBlogs());

            return View(blogs);
        }
       
        public ActionResult Details(int Id)
        {
            var blog  = BlogService.GetDetails(Id);

            return View(blog);
        }

        //public ActionResult Contact()
        //{
        //    MapperConfiguration config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<BlogDto, BlogPreviewModel>();
        //    });

        //    Mapper mapper = new Mapper(config);

        //    List<BlogPreviewModel> blogs = mapper.Map<List<BlogPreviewModel>>(BlogService.GetAllBlogs());

        //    return View(blogs);
        //}
    }
}