using BlogApp.DataAccess;
using BlogApp.MVC.Models;
using BlogApp.Results.DTOs.Request;
using BlogApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _service;
        private readonly ICategoryService _categoryService;
        public HomeController(ILogger<HomeController> logger,IBlogService service, ICategoryService categoryService)
        {
            _logger = logger;
            _service = service;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var data = _service.GetAllBlogs();
            return View(data);
        }
        [Authorize(Roles ="admin,editor")]
        public IActionResult AddBlog()
        {
            ViewData["categories"] = _categoryService.GetAll();
            return View();
        }
        [Authorize(Roles = "admin,editor")]
        [HttpPost]
        public IActionResult AddBlog(BlogAddRequest blog) 
        {
            var result = _service.Add(blog);
            if (result.result)
            {
                TempData["message"] = result.message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}