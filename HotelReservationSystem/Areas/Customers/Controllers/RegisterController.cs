using HotelReservationSystem.Areas.Customers.Models;
using HotelReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace HotelReservationSystem.Areas.Customers.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Customers/Register
        #region Register
        [HttpGet]
        public ActionResult Register()
        {
            //return View();
           
            return View();
        }

        [HttpPost]
        public ActionResult Register(AnationsCustomer model)
        {
            if (ModelState.IsValid)

            {
                string id=Business.BussLogic.AddCustomer(model.name, model.c_contact, model.c_email, model.Dob, model.pincode, model.state, model.city, model.password);
                Session.Add("CustomerID",id);

                return RedirectToAction("Registers", "Account", new
                {
                    area = "",
                    name = id,
                    Password = model.password,
                    ConfirmPassword = model.confirmpassword,
                    PhoneNumber = model.c_contact,
                    Email = model.c_email,
                    Role= "Customer"
                });


            }

            //return "<h1>test</h1>";
            return View( model);
        }


        #endregion

        #region viewdetails 
        [HttpGet]
        public ActionResult Edit(string cid)
        {
            cid = Session["CustomerID"].ToString();



            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Entity.Customer cu = Business.BussLogic.RetriveCustomer(cid);
             if (cu == null)
            {
                return HttpNotFound();
            }
            VMECustomer acu = new VMECustomer();

            acu.id = cu.id;
            acu.name = cu.name;
            acu.Dob = cu.Dob;
            acu.c_contact = cu.c_contact;
            acu.city = cu.PinCode1.city;
            acu.state = cu.PinCode1.state;
            acu.pincode = cu.pincode;

            return View(acu);
        }
        [HttpPost]
        public ActionResult Edit(VMECustomer acu)
        {
            
            if (ModelState.IsValid)
            {
                Entity.Customer cu = Business.BussLogic.RetriveCustomer(acu.id);

                Business.BussLogic.UpdateCustomer(acu.id, acu.name, acu.c_contact, cu.c_email, acu.Dob, acu.pincode, acu.state, acu.city, cu.password);

                
                return RedirectToAction("Index","Booking",new { area = "Customers" });
            }
            return View(acu);
        }
        public ActionResult Details(string cid )
        {
            cid = Session["CustomerID"].ToString();
            Entity.Customer cdetails = Business.BussLogic.RetriveCustomer(cid);
            VMECustomer cd = new VMECustomer();
            cd.id = cdetails.id;
            cd.name = cdetails.name;
            cd.pincode = cdetails.pincode;
            cd.state = cdetails.PinCode1.state;
            cd.city = cdetails.PinCode1.city;
            cd.Dob = cdetails.Dob;
            cd.c_contact = cdetails.c_contact;

            return PartialView("_CDetails", cd);
        }

        #endregion


    }
}
