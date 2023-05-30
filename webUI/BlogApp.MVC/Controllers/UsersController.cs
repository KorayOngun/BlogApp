using BlogApp.MVC.Models;
using BlogApp.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Login(string? page)
        {
            ViewBag.page = page;    
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin User,string? page)
        {
            if (ModelState.IsValid)
            {
                var user = _service.Login(User.Name, User.Password);
                if (user != default) 
                {
                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Role,user.Role)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (!string.IsNullOrEmpty(page))
                    {
                        return Redirect(page);
                    }
                    return Redirect("/");
                }
                ModelState.AddModelError("login", "kullanıcı adı veya şifre hatalı");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
