using AutoMapper;
using Blog.BLL.Dto;
using Blog.BLL.Interfaces;
using Blog.WEB.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class ManageController : Controller
    {
        private IUserService UserService;

        public ManageController(IUserService service)
        {
            UserService = service;
        }
        [Authorize]
        public ActionResult Index()
        {
            string currentUserId = HttpContext.User.Identity.GetUserId();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, ViewUserModel>();
            });

            Mapper mapper = new Mapper(config);

            ViewUserModel user = mapper.Map<ViewUserModel>(UserService.GetUser(currentUserId));

            return View(user);
        }

        // GET: Manage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Edit/5
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

        // GET: Manage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manage/Delete/5
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
