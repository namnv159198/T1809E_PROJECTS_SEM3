using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    public class OfficesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Offices
        public ActionResult Index(string searchString, string currentFilter , int? page ,int? status)
        {

            var office = (from l in db.Offices
                          select l);
            if (status.HasValue)
            {
                ViewBag.Status = status;

                office = office.Where(p => (int)p.Status == status.Value);
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
           
            if (!string.IsNullOrEmpty(searchString))
            {
                office = office.Where(s => s.Name.Contains(searchString));
            }
            office = office.OrderBy(x => x.ID);

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(office.ToPagedList(pageNumber, pageSize));


        }

        // GET: Offices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // GET: Offices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PinCode,Name,Status,Email,VAT,PhoneNumber,Address,District,Province")] Office office)
        {
            if (ModelState.IsValid)
            {
                office.ID = "Office" + db.Offices.Count();
                db.Offices.Add(office);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(office);
        }

        // GET: Offices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PinCode,Name,Status,Email,VAT,PhoneNumber,Address,District,Province")] Office office)
        {
            if (ModelState.IsValid)
            {
                db.Entry(office).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(office);
        }

        // GET: Offices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Office office = db.Offices.Find(id);
            db.Offices.Remove(office);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
