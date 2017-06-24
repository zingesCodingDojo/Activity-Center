using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FirstBeltExam.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class SingleActivityController : Controller
    {
        private YourContext _context;
 
        public SingleActivityController(YourContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("Single/{id}")]
        public IActionResult Single(int id){
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "SplashPage");
            }
            ViewBag.Individuals = _context.FunMaker.Where(u => u.ActivityId == id).Include(user => user.User).Include(activity => activity.Activity).ToList();
            ViewBag.ActivityDeetz = _context.Activity.Where(u => u.ActivityId == id).Include(singleUser => singleUser.User);
            ViewBag.MyID = (int)HttpContext.Session.GetInt32("UserID");
            return View("SingleActivity");
        }
        [HttpGet]
        [Route("Tlogout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "SplashPage");
        }

        [HttpGet]
        [Route("SreturnOurActivities")]
        public IActionResult GotoPlanner(){
            int? getMyint = HttpContext.Session.GetInt32("UserID");
            return RedirectToAction("OurActivites", "OurActivites", new { id =  getMyint});
        }

        [HttpPost]
        [Route("removeActivity/{id}")]
        public IActionResult Delete(int id){
            List<FunMaker> RemoveFirst = _context.FunMaker.Where(w => w.ActivityId == id).Include(p => p.Activity).ToList();
            if(RemoveFirst != null){
                foreach(FunMaker item in RemoveFirst){
                    _context.Remove(item);
                }
            }
            Activity RemoveSecond = _context.Activity.SingleOrDefault(u => u.ActivityId == id);
            
            _context.Activity.Remove(RemoveSecond);
            _context.SaveChanges();
            int? getMyint = HttpContext.Session.GetInt32("UserID");
            return RedirectToAction("OurActivites", "OurActivites", new { id =  getMyint});
        }
    }
}
