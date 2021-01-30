using COREBlog.CORE.Service;
using COREBlog.MODEL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace COREBlog.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICoreService<User> us;

        public AccountController(ICoreService<User> _us)
        {
            us = _us;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User item)
        {
            if (us.Any(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password))
            {
                User logged = us.GetByDefault(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password);

                var claims = new List<Claim>()
                {
                    new Claim("ID", logged.ID.ToString()),
                    new Claim(ClaimTypes.Name, logged.FirstName),
                    new Claim(ClaimTypes.Surname, logged.LastName),
                    new Claim(ClaimTypes.Email, logged.EmailAddress),
                    new Claim("Image", logged.ImageURL)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //localhos://PortNo/Adminstrator/Home/Index
                return RedirectToAction("Index", "Home", new { area = "Administrator" });
            }

            return View(item);
         }
    }
}
