using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelReservationSystem.Areas.Managers.Models;
using HotelReservationSystem.Models;
using System.Reflection;
using Business;


namespace HotelReservationSystem.Areas.Managers.Controllers
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
    [Authorize(Roles = "Manager")]
    public class HotelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Managers/Hotels
        public ActionResult Index()
        {
           string m_Id= Session["ManagerID"].ToString();
            DataSet ds = Business.BussLogic.AllManagedHotel(m_Id);
            List<VMhotels> hotellist = ds.Tables[0].ToList<VMhotels>();


            return View(hotellist);
        }

        
        // GET: Managers/Hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Managers/Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,hotel_description,no_of_ac_rooms,no_of_non_ac_rooms,Cost_ac_adult,Cost_non_ac_adult,Cost_ac_child,Cost_non_ac_child,state,city,pincode")] VMhotels vMhotels)
        {
            if (ModelState.IsValid)
            {
                string m_id = Session["ManagerID"].ToString();
                BussLogic.AddHotel(vMhotels.name, vMhotels.hotel_description, vMhotels.pincode, vMhotels.state, vMhotels.city, m_id, vMhotels.no_of_ac_rooms, vMhotels.no_of_non_ac_rooms, vMhotels.Cost_ac_adult, vMhotels.Cost_ac_child, vMhotels.Cost_non_ac_adult, vMhotels.Cost_non_ac_child);

                return RedirectToAction("Index");
            }

            return View(vMhotels);
        }

        // GET: Managers/Hotels/Edit/5
        public ActionResult Edit(string id)
        {
            string m_id = Session["ManagerID"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DataSet ds = Business.BussLogic.AllManagedHotel(m_id);
            List<VMhotels> hotellist = ds.Tables[0].ToList<VMhotels>();
            VMhotels vMhotels = hotellist.Find(e=>e.id.Contains(id)); 
            if (vMhotels == null)
            {
                return HttpNotFound();
            }
            return View(vMhotels);
        }

        // POST: Managers/Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,hotel_description,no_of_ac_rooms,no_of_non_ac_rooms,Cost_ac_adult,Cost_non_ac_adult,Cost_ac_child,Cost_non_ac_child,state,city,pincode")] VMhotels vMhotels)
        {
            if (ModelState.IsValid)
            {
                string m_id = Session["ManagerID"].ToString();
                BussLogic.UpdateHotel(m_id, vMhotels.id, vMhotels.name, vMhotels.hotel_description, vMhotels.pincode, vMhotels.state, vMhotels.city, vMhotels.no_of_ac_rooms, vMhotels.no_of_non_ac_rooms, vMhotels.Cost_ac_adult, vMhotels.Cost_ac_child, vMhotels.Cost_non_ac_adult, vMhotels.Cost_non_ac_child);
                return RedirectToAction("Index");
            }
            return View(vMhotels);
        }

        // GET: Managers/Hotels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMhotels vMhotels = db.VMhotels.Find(id);
            if (vMhotels == null)
            {
                return HttpNotFound();
            }
            return View(vMhotels);
        }

        // POST: Managers/Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VMhotels vMhotels = db.VMhotels.Find(id);
            db.VMhotels.Remove(vMhotels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
