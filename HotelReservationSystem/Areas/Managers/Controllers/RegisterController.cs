using HotelReservationSystem.Areas.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Areas.Managers.Controllers
{
    public class RegisterController : Controller
    {
        #region REgistration

        // GET: Managers/Register
        [HttpGet]
        public ActionResult Register()
        {
            // return View();
            return View();
        }

        [HttpPost]
        public ActionResult Register(AnationsManager model)
        {
            if (ModelState.IsValid)
            {
                string id = Business.BussLogic.AddManger(model.name, model.password, model.m_contact, model.m_email);
                Session.Add("ManagerID", id);
                return RedirectToAction("Registers", "Account", new
                {
                    area = "",
                    name = id,
                    Password = model.password,
                    ConfirmPassword = model.confirmpassword,
                    PhoneNumber = model.m_contact,
                    Email = model.m_email,
                    Role = "Manager"
                });


            }


            // return View();
            return View(model);
        }
        #endregion

        #region viewdetails 
        [HttpGet]
        public ActionResult Edit(string Mid)
        {
            Mid = Session["ManagerID"].ToString();



            if (Mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Entity.Manager mu = Business.BussLogic.RetriveManger(Mid);
            if (mu == null)
            {
                return HttpNotFound();
            }


            VMEmanager acu = new VMEmanager();

            acu.id = mu.id;
            acu.name = mu.name;
            acu.m_contact = mu.m_contact;
            

            return View(acu);
        }
        [HttpPost]
        public ActionResult Edit(VMEmanager acu)
        {

            if (ModelState.IsValid)
            {
                Entity.Manager cu = Business.BussLogic.RetriveManger(acu.id);

                Business.BussLogic.UpdateManger(acu.id,  acu.name, cu.password, acu.m_contact, cu.m_email );


                return RedirectToAction("Index", "Hotels", new { area = "Managers" });
            }
            return View(acu);
        }
        public ActionResult Details(string Mid)
        {
            Mid = Session["ManagerID"].ToString();
            Entity.Manager Mdetails = Business.BussLogic.RetriveManger(Mid);

            VMEmanager cd = new VMEmanager();
            cd.id = Mdetails.id;
            cd.name = Mdetails.name;
            cd.m_contact = Mdetails.m_contact;

            return PartialView(cd);
        }

        #endregion

    }
}


