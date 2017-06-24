using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FirstBeltExam.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FirstBeltExam.Controllers
{
    public class OurActivitesController : Controller
    {
        private YourContext _context;
 
        public OurActivitesController(YourContext context)
        {
            _context = context;
        }

        // Renders splashpage, landing page, whatever you want to call an index page!
        [HttpGet]
        [Route("OurActivities/{id}")]
        public IActionResult OurActivites(int id)
        {
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "SplashPage");
            }
            ViewBag.Errors = new List<string>();
            ViewBag.MyID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.AllActivities = _context.Activity.Include( user => user.User).Include( funmaker => funmaker.FunMaker).OrderBy( p => p.ActivityDate);
            ViewBag.FunMakerPeeps = _context.FunMaker.Include( user => user.User).Include( activity => activity.Activity).ToList();
            ViewBag.CurrentUser = _context.User.Where( u => u.UserId == (int)HttpContext.Session.GetInt32("UserID")).Include( a => a.Activity).Include( f => f.FunMaker);
            return View("OurActivites");
        }

        [HttpGet]
        [Route("logoff")]
        public IActionResult Logoff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "SplashPage");
        }
        
        [HttpGet]
        [Route("goMakeActivity")]
        public IActionResult GoMakeActivity(){
            return RedirectToAction("ActivityAdder", "ActivityAdder");
        }
        
        [HttpGet]
        [Route("delete/{id}")]
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
            return RedirectToAction("OurActivites");
        }
        
        [HttpPost]
        [Route("toggler/{id}")]
        public IActionResult Toggle(int id, string Join){
            Activity singleActivity = _context.Activity.SingleOrDefault( u => u.ActivityId == id);
            
            if(Join == "Join"){
                FunMaker newInstance = new FunMaker{
                    FunMakerAction = Join,
                    ActivityId = singleActivity.ActivityId,
                    UserId = (int)HttpContext.Session.GetInt32("UserID"),
                    FunMakerCreated_At = DateTime.Now,
                    FunMakerUpdated_At = DateTime.Now
                };
                _context.Add(newInstance);
            }
            else{
                
                List<FunMaker> myFunMaker = _context.FunMaker.Where(i => i.ActivityId == id).Where( p => p.UserId == (int)HttpContext.Session.GetInt32("UserID")).ToList();
                if(myFunMaker != null || myFunMaker.Count != 0){
                    foreach(FunMaker item in myFunMaker){
                        _context.Remove(item);
                    }
                }
            }
            
            _context.SaveChanges();
            return RedirectToAction("OurActivites");
        }

        [HttpGet]
        [Route("singleActivity/{id}")]
        public IActionResult SingleActivity(int id){
            return RedirectToAction("Single", "SingleActivity", new { id = id });
        }
    }
}