using System.Linq;
using LoginReg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginReg.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext _context;
        public UserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.Users.SingleOrDefault(p => p.Email == Email);
            if (user != null && Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                var result = Hasher.VerifyHashedPassword(user, user.Password, Password);
                if (result != 0)
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("CurrUser", user.FirstName);
                    return RedirectToAction("Success");
                }
            }
            return View("Index");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            ViewBag.SessionId = HttpContext.Session.GetInt32("UserId");
            ViewBag.CurrUser = HttpContext.Session.GetString("CurrUser");
            return View();
        }
    }
}