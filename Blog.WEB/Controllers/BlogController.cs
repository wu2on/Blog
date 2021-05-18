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
using System.Net;

namespace Blog.WEB.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService BlogService;

        public BlogController(IBlogService service)
        {
            BlogService = service;
        }

        [Authorize]
        public ActionResult Index()
        {
            string currentUserId = HttpContext.User.Identity.GetUserId();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDto, BlogPreviewModel>().ForMember("UserEmail", x => x.MapFrom(c => c.UserProfile.Email));
            });

            Mapper mapper = new Mapper(config);

            List<BlogPreviewModel> blogs = mapper.Map<List<BlogPreviewModel>>(BlogService.GetAllUserBlogs(currentUserId));

            return View(blogs);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            string currentUserId = HttpContext.User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogDto blog = BlogService.GetDetails(id);

            if(currentUserId == blog.UserProfileId)
            {
                return View(blog);
            }
            return HttpNotFound();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBlogModel model)
        {
            if(ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();

                BlogDto blogDto = new BlogDto
                {
                    Title = model.Title,
                    Text = model.Text,
                    CreateAt = DateTime.Now,
                    UserProfileId = currentUserId,
                    IsDeleted = false
                };

                OperationDetails operationDetails = await BlogService.Create(blogDto);

                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }    
            }
            
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(string postId, string comment, string url)
        {
            if(ModelState.IsValid)
            {
                var currentUser = User.Identity;

                CommentDto commentDto = new CommentDto
                {
                    Text = comment,
                    CreateAt = DateTime.Now,
                    UserProfileId = currentUser.GetUserId(),
                    UserEmail = currentUser.Name,
                    PostId = Int32.Parse(postId),
                    IsDeleted = false
                };

                await BlogService.AddComment(commentDto);
            }

            return Redirect(url);
        }
        
        [Authorize]
        public ActionResult Edit(int? id)
        {
            string currentUserId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           BlogDto blog = BlogService.GetDetails(id);

            if (currentUserId == blog.UserProfileId)
            {
                return View(blog);
            }

            return HttpNotFound();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BlogDto model)
        {
            if (ModelState.IsValid)
            {

                OperationDetails operationDetails = await BlogService.UpdateBlog(model);

                if (!operationDetails.Succedeed)
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }

            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> DeleteComment(int? Id, string url)
        {
            if(ModelState.IsValid)
            {
                var currentUser = User.Identity.GetUserId();

                OperationDetails operationDetails = await BlogService.DeleteComment(Id, currentUser);
            }

            return Redirect(url);
        }
       
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> DeletePost(BlogDto model, string url)
        {
            var currentUser = User.Identity.GetUserId();

            if (model.UserProfileId != currentUser)
            {
                return RedirectToAction("Index", "Blog");
            }

            OperationDetails operationDetails = await BlogService.DeletePost(model.Id);

            if (!operationDetails.Succedeed)
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return RedirectToAction("Index", "Blog"); 
        }
    }
}
