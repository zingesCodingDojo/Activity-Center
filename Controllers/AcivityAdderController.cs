using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FirstBeltExam.Models;
using System.Linq;

namespace FirstBeltExam.Controllers
{
    public class ActivityAdderController : Controller
    {
        private YourContext _context;
 
        public ActivityAdderController(YourContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("ActivityAdder")]
        public IActionResult ActivityAdder(){
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "SplashPage");
            }
            
            ViewBag.Errors = new List<string>();
            return View("ActivityAdder");
        }

        [HttpGet]
        [Route("returnOurActivities")]
        public IActionResult ReturnOurActivities(){
            int? getMyint = HttpContext.Session.GetInt32("UserID");
            return RedirectToAction("OurActivites", "OurActivites", new { id =  getMyint});
        }

        [HttpPost]
        [Route("createActivity")]
        public IActionResult CreateActivity(RegisterActivityModel myActivity){
            ViewBag.Errors = new List<string>();

            if(ModelState.IsValid){
                if(myActivity.ActivityDate > DateTime.Now){
                    FirstBeltExam.Models.Activity newActivity = new FirstBeltExam.Models.Activity{
                        ActivityName = myActivity.ActivityName,
                        Time = myActivity.Time,
                        ActivityDate = myActivity.ActivityDate,
                        ActivityDuration = myActivity.ActivityDuration,
                        ActivityDescription = myActivity.ActivityDescription,
                        ActivityCreated_At = DateTime.Now,
                        ActivityUpdated_At = DateTime.Now,
                        HDM = myActivity.HDM,
                        UserId  = (int)HttpContext.Session.GetInt32("UserID")
                    };
                    _context.Add(newActivity);
                    FirstBeltExam.Models.FunMaker newFunMaker = new FirstBeltExam.Models.FunMaker{
                        FunMakerAction = "Join",
                        ActivityId = newActivity.ActivityId,
                        UserId = (int)HttpContext.Session.GetInt32("UserID")
                    };
                    _context.Add(newFunMaker);
                    _context.SaveChanges();
                    return RedirectToAction("Single", "SingleActivity", new { id =  newActivity.ActivityId}); 
                }
                ViewBag.BadDate = "Date cannot be from the past!";
            }
            else{
                ViewBag.Errors = ModelState.Values;
            }
            return View("ActivityAdder");
        }

    }
}