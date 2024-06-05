using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WebApplication1.Models;
using WebApplication1.Models.ToRender;
using WebApplication1.Models.Users;
using WebApplication1.Services;
using WebApplication1.Services.Auth;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationContext db;
        public UsersService _service;

        public UsersController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
            _service = new(new PasswordHasher(), db);
        }

        public IActionResult GetUserLogin(string login, string password)
        {
            var u = _service.Login(login, password);
            if (u == null)
                return BadRequest();
            else
            {
                u.TryUpdateSessionId();
                u.SetSessionCookie(HttpContext);
                db.SaveChanges();
            }
            return RedirectToAction("Home", "Test");
        }
        public IActionResult GetUserRegister(string name, string login, string password)
        {
            var u = _service.Register(name, login, password);
            if (u != null)
            {
                db.Users.Add(u);
                u.TryUpdateSessionId();
                u.SetSessionCookie(HttpContext);
                u.Role = db.Role.Where(r => r.Name == "Default").ToList()[0];
                db.SaveChanges();
                return RedirectToAction("Test", "Home");
            }
            else
            {
                //пользователь уже существует
                return BadRequest();
            }
        }
        public IActionResult UserLogin()
        {
            return View();
        }
        public IActionResult UserRegister()
        {
            return View();
        }

        public IActionResult HeaderHrefs()
        {
            var m = new RenderBase();
            m.User = _service.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            return PartialView(m);
        }
    }
}
