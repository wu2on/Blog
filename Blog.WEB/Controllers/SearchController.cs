using AutoMapper;
using Blog.BLL.Dto;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using System;
using System.Collections.Generic;
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