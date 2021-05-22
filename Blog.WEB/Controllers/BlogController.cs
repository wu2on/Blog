using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Blog.WEB.Models;
using Blog.BLL.Dto;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using AutoMapper;


namespace Blog.WEB.Controllers
{
    /// <summary>
    /// Controller to manage blogs
    /// </summary>
    public class BlogController : Controller
    {
        /// <summary>
        /// The Blog Service service
        /// </summary>
        private IBlogService BlogService;

        /// <summary>
        /// Initializes a new instance of the BlogController
        /// </summary>
        /// <param name="service">Blog service</param>
        public BlogController(IBlogService service)
        {
            BlogService = service;
        }

        /// <summary>
        /// Shows all blogs of current user
        /// </summary>
        /// <returns>Action Result</returns>
        [Authorize]
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDto, BlogPreviewModel>().ForMember("UserEmail", x => x.MapFrom(c => c.UserProfile.Email));
            });

            Mapper mapper = new Mapper(config);

            List<BlogPreviewModel> blogs = mapper.Map<List<BlogPreviewModel>>(BlogService.GetAllUserBlogs(currentUserId));

            return View(blogs);
        }

        /// <summary>
        /// Get blog by id
        /// </summary>
        /// <param name="id">Blog identifier</param>
        /// <returns>Action Result</returns>
        [Authorize]
        public ActionResult Details(int? id)
        {
            string currentUserId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogDto blog = BlogService.GetDetails(id);

            if(blog == null) return HttpNotFound();

            if (currentUserId == blog.UserProfileId)
            {
                return View(blog);
            }
            return HttpNotFound();
        }

        /// <summary>
        /// Return form for create blog
        /// </summary>
        /// <returns>Action Result</returns>
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add new blog
        /// </summary>
        /// <param name="model">User request from create form</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBlogModel model)
        {
            if(ModelState.IsValid)
            {
                string currentUserId = HttpContext.User.Identity.GetUserId();

                BlogDto blogDto = new BlogDto
                {
                    Title = model.Title,
                    Text = model.Text,
                    CreateAt = DateTime.Now,
                    UserProfileId = currentUserId,
                    IsDeleted = false
                };

                OperationDetails operationDetails = BlogService.Create(blogDto);

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

        /// <summary>
        /// Add new comment
        /// </summary>
        /// <param name="postId">Blog identifier</param>
        /// <param name="comment">Comment from user</param>
        /// <param name="url">Url of the point where the user performed the action</param>
        /// <returns>Action result</returns>
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

        /// <summary>
        /// The view for changing the blog
        /// </summary>
        /// <param name="id">Blog identifier</param>
        /// <returns>Action Result</returns>
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

        /// <summary>
        /// Edit blog
        /// </summary>
        /// <param name="model">New data to update the blog</param>
        /// <returns>Action Result</returns>
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

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="Id">Comment identifier</param>
        /// <param name="url">Url of the point where the user performed the action</param>
        /// <returns>Action Result</returns>
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

        /// <summary>
        /// Delete blog
        /// </summary>
        /// <param name="model">Model to delete</param>
        /// <param name="url">Url of the point where the user performed the action</param>
        /// <returns></returns>
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
