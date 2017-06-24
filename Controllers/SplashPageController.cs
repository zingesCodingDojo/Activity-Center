using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FirstBeltExam.Models;
using System.Linq;

namespace FirstBeltExam.Controllers
{
    public class SplashPageController : Controller
    {
        private YourContext _context;
 
        public SplashPageController(YourContext context)
        {
            _context = context;
        }

        // Renders splashpage, landing page, whatever you want to call an index page!
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors = new List<string>();
            ViewBag.LoginErrors = new List<string>();
            int? getMyUser = HttpContext.Session.GetInt32("UserID");
            if(getMyUser != null){
                return RedirectToAction("OurActivites", "OurActivites", new { id =  (int)getMyUser});
            }
            return View("Index");
        }

        // Validate new user registration. Refer to RegisterViewModel for error types. No dupes!
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel newUserRegistration){
            ViewBag.Errors = new List<string>();
            ViewBag.LoginErrors = new List<string>();
            ViewBag.SpecialError = null;
            if(ModelState.IsValid){
                if(_context.User.SingleOrDefault(u => u.Email == newUserRegistration.Email) == null){
                    User NewUser = new User{
                    FirstName = newUserRegistration.FirstName,
                    LastName = newUserRegistration.LastName,
                    Email = newUserRegistration.Email,
                    Password = newUserRegistration.Password,
                    UserCreated_At = DateTime.Now.Date,
                    UserUpdated_At = DateTime.Now.Date
                    };
                    _context.Add(NewUser);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("UserID", (int)NewUser.UserId);
                    return RedirectToAction("OurActivites", "OurActivites", new { id =  NewUser.UserId});
                }
                else{
                    ViewBag.Errors.Add("Invalid Email!");
                }
                
            }
            else{
                ViewBag.Errors = ModelState.Values;
            }
            return View("Index");
        }

        // Validate existing user login attempt!
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password){
            ViewBag.LoginErrors = new List<string>();
            ViewBag.Errors = new List<string>();
            if(Email == null){
                ViewBag.LoginErrors.Add("Email cannot be empty!");
            }
            if(Password == null){
                ViewBag.LoginErrors.Add("Password cannot be empty!");
            }
            else{
                User myUser = _context.User.SingleOrDefault(u => u.Email == Email);
                if(myUser == null || myUser.Password != Password){
                    ViewBag.LoginErrors.Add("Invalid Email/Password Combination!");
                }
                else{
                    HttpContext.Session.SetInt32("UserID", (int)myUser.UserId);
                    return RedirectToAction("OurActivites", "OurActivites", new { id =  myUser.UserId});
                    
                }
            }
            return View("Index");
        }
    }
}
