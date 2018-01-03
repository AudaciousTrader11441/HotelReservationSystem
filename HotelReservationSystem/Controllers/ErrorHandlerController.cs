using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class ErrorHandlerController : Controller
    {
        // GET: ErrorHandler
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Badrequest()
        {
            return View();

        }
        public ActionResult Unauthorized()
        {
            return View();

        }
    }
}