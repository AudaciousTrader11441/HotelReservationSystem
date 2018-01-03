using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using System.Data;
using HotelReservationSystem.Areas.Customers.Models;
using System.Reflection;
using Entity;
using PagedList;

namespace HotelReservationSystem.Areas.Customers.Controllers
{
    public static class Extensions
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (row[property.Name] == DBNull.Value)
                    property.SetValue(item, null, null);
                else
                    property.SetValue(item, row[property.Name], null);
            }
            return item;
        }
    }
    [Authorize(Roles = "Customer")]
    public class BookingController : Controller
    {
        // GET: Customers/Booking
        #region Listing hotels
        public static List<AvilableHotel> avilableHotel = new List<AvilableHotel>();
        public BookingController()
        {
            DataSet ds = BussLogic.AllAvilableHotel();
            avilableHotel = ds.Tables[0].ToList<AvilableHotel>();

        }
        public ActionResult Index(int page=1)
        {

            ViewBag.Hotels = avilableHotel.ToPagedList<AvilableHotel>(page,2);
            List<SelectListItem> temp =avilableHotel
                .Select(e => e.State).Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Select(e => new SelectListItem()
                {
                    Text = e,
                    Value = e
                }).ToList();
            SelectListItem zero = new SelectListItem();
            zero.Text = "Select State";
            zero.Value = "null";
            zero.Disabled = true;
            zero.Selected = true;
       
            temp.Insert(0, zero);
            ViewBag.States = temp;

            List<SelectListItem> temp1 = avilableHotel
                .Select(e => e.City).Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Select(e => new SelectListItem()
                {
                    Text = e,
                    Value = e
                }).ToList();
            SelectListItem zero1 = new SelectListItem();
            zero1.Text = "Select City";
            zero1.Value = "null";
            zero.Disabled = true;
            zero1.Selected = true;
            temp1.Insert(0, zero1);
            ViewBag.City = temp1;
            return View();
        }

        public JsonResult GetCities(string state)
        {
            List<SelectListItem> result = avilableHotel
                .Where(e => e.State.ToLower().Contains(state.ToLower()))
                .Select(e => e.City).Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Select(e => new SelectListItem()
                {
                    Text = e,
                    Value = e
                }).ToList<SelectListItem>();
            SelectListItem zero1 = new SelectListItem();
            zero1.Text = "Select City";
            zero1.Value = "null";
            zero1.Selected = true;

            result.Insert(0, zero1);
            return Json(result);
        }


        public ActionResult GetHotels(string state = null, string city = null,int page= 1)
        {


            if ((state == null && city == null)|| (state == "null" && city == "null"))
            {
                DataSet ds = BussLogic.AllAvilableHotel();
                avilableHotel = ds.Tables[0].ToList<AvilableHotel>();

            }
            else if ((city == null)||(city == "null"))
                avilableHotel = avilableHotel.Where(e => e.State.ToLower().Contains(state.ToLower())).ToList();
            else
                avilableHotel = avilableHotel.Where(e => e.State.ToLower().Contains(state.ToLower()) && e.City.ToLower().Contains(city.ToLower())).ToList();

            return PartialView("_HotelDetails", avilableHotel.ToPagedList<AvilableHotel>(page,2));

        }


        #endregion

        #region bookingdetails
        [HttpGet]
        public ActionResult BookingDetails(string hotelID)
        {
            AnationsBooking model = new AnationsBooking();
            model.Customer_id = Session["CustomerID"].ToString();
            model.Hotel_id = hotelID;


            return View(model);
        }
        [HttpPost]
        public ActionResult BookingDetails(AnationsBooking model)
        {

            var x = ModelState.IsValid;
            if(x)
            {
                if (model.TotalCost <= 0)
                {
                    ModelState.AddModelError("Date_checkout", "Not Vaild enter higher than in time");
                    return View(model);
                    
                }
                if (model.Date_checkin.CompareTo(DateTime.Today) <= 0)
                {
                    ModelState.AddModelError("Date_checkin", "Only future booking");
                    return View(model);

                }


                Session.Add("HotelID", model.Hotel_id);
                Session.Add("BookName", model.Name);
                Session.Add("TotalCost", model.TotalCost);
                Session.Add("Adult", model.no_adult);
                Session.Add("Child", model.no_child);
                Session.Add("Type", model.Type);
                Session.Add("Checkin", model.Date_checkin);
                Session.Add("Checkout", model.Date_checkout);


                return this.RedirectToAction("MakePayment");
            }
            


            return View(model);
        }
        public ActionResult BookDisplay()
        {
            string c_id = Session["CustomerID"].ToString();
            Booking model = new Booking();
            model = BussLogic.RetriveBookingByCustomer(c_id);
            if (model.Booking_id.Contains("No Booking"))
                return PartialView("NoBooking");
            AnationsBookingWid bmodel = new AnationsBookingWid();
            bmodel.Customer_id = model.Customer_id;
            bmodel.Booking_id = model.Booking_id;
            bmodel.Hotel_id = model.Hotel_id;
            bmodel.no_adult = model.no_adult;
            bmodel.no_child = (int)model.no_child;
            bmodel.Date_checkin = (DateTime)model.Date_checkin;
            bmodel.Date_checkout = (DateTime)model.Date_checkout;


            



            return PartialView(bmodel);

        }

        public int CostCalculator(string hotelid,int adult, int child,int type,DateTime checkin,DateTime checkout)
        {
            return BussLogic.FindCost(hotelid, adult, child, type, checkin, checkout);

        }


        #endregion

        #region payment
        [HttpGet]
        public ActionResult MakePayment()
        {
            VmPaymentdetails dummy = new VmPaymentdetails();
            dummy.totalcost = int.Parse(Session["TotalCost"].ToString());
            dummy.CardName = "Kumar C";
            dummy.CardNo = 123456;
            dummy.CardType = "Visa";
            dummy.CVV = 123;
            dummy.Pin = 1234;
            dummy.BankName = "SBI";
            dummy.ExpireDate = DateTime.Parse("2021-01-01");

            string Hid = Session["HotelID"].ToString();
            //string Cid = "k&698";
            string Cid = Session["CustomerID"].ToString();
            string Bname = Session["BookName"].ToString();

            string tid=BussLogic.AddPayment(Hid, Cid, Bname, dummy.totalcost, dummy.CardNo, dummy.CardType);

            int adult = int.Parse(Session["Adult"].ToString());
            int child = int.Parse(Session["Child"].ToString());
            int type = int.Parse(Session["Type"].ToString());
            DateTime cin = Convert.ToDateTime(Session["Checkin"].ToString());
            DateTime cout = Convert.ToDateTime(Session["Checkout"].ToString());
            int ac=0;
            int nonac=0;
            if (type == 0) { ac = (int)Math.Ceiling(((adult + child) / 3.0)); }
            else { nonac = (int)Math.Ceiling(((adult + child) / 3.0)); }

            string bookingid=BussLogic.AddBooking(Cid, Hid, ac, nonac, tid, DateTime.Today, cin, cout, adult, child);
            Session.Add("PaymentID", tid);
            Session.Add("BookingID", bookingid);




            return View(dummy);


        }
        [HttpPost]
        public ActionResult MakePayment(VmPaymentdetails model)
        {

            return View(model);


        }

       
        
        public ActionResult BookingConfim()
        {
            
            VMBookingConfirm vm = new VMBookingConfirm();
            string Cid = Session["CustomerID"].ToString();
            vm.Customerid = Cid;
            vm.Hotelid = Session["HotelID"].ToString(); 
            vm.Tid = Session["PaymentID"].ToString();
            vm.Bid = Session["BookingID"].ToString();

            return View(vm);


        }
        

        #endregion
    }


}