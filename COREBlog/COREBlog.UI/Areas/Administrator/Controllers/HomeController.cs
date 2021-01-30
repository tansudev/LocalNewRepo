using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using COREBlog.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COREBlog.CORE.Service;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace COREBlog.UI.areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoreService<Category> cs;
        private readonly ICoreService<Post> ps;
        private readonly ICoreService<User> us;
        private readonly ICoreService<Comment> cms;

        public HomeController(ICoreService<Category> _cs, ICoreService<Post> _ps, ICoreService<User> _us, ICoreService<Comment> _cms)
        {
            cs = _cs;
            ps = _ps;
            us = _us;
            cms = _cms;
        }

        [Area("Administrator"), Authorize]
        public IActionResult Index()
        {
            Guid id = Guid.Parse(User.FindFirst("ID").Value);
            //ViewBag.User = us.GetByID(id);

            User user = us.GetByID(id);
            List<Comment> comments = cms.GetDefault(x => x.Post.UserID == id);
            List<Post> posts = ps.GetDefault(x => x.UserID == id);
            List<Category> categories = cs.GetAcive();
            

            return View(Tuple.Create< User, List<Post>, List<Comment>, List<Category>>(user,posts,comments,categories));
        }

        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home",new {area=""});
        }
    }
}
