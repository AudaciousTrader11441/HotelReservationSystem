using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
          
            if (User.IsInRole("Customer"))
               return RedirectToAction("Index", "Booking", new { area = "Customers" });
            if (User.IsInRole("Manager"))
                return RedirectToAction("Index", "Hotels", new { area = "Managers" });

            
                
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}