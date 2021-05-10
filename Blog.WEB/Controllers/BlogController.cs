﻿using Blog.WEB.Models;
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

namespace Blog.WEB.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService BlogService;

        public BlogController(IBlogService service)
        {
            BlogService = service;
        }

        public ActionResult Index()
        {
            string currentUserId = HttpContext.User.Identity.GetUserId();
            List<BlogDto> blogs = BlogService.GetAllUserBlogs(currentUserId);
            var blog = blogs.FirstOrDefault();
            return View(blogs);
        }

        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                string currentUserId = HttpContext.User.Identity.GetUserId();

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
                    return RedirectToAction("Index", "Blog");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(string postId, string comment)
        {
            if(ModelState.IsValid)
            {
                string currentUserId = HttpContext.User.Identity.GetUserId();

                CommentDto commentDto = new CommentDto
                {
                    Text = comment,
                    CreateAt = DateTime.Now,
                    UserProfileId = currentUserId,
                    PostId = Int32.Parse(postId),
                    IsDeleted = false
                };
                await BlogService.AddComment(commentDto);
            }

            return RedirectToAction("Index", "Blog");

        }
        
        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Blog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
