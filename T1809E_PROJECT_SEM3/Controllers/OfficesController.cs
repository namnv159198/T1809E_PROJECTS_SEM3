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

        public ActionResult Index(string sortOrder, string searchString, string currentFilter , int? page ,int? status)
        {

            var office = (from l in db.Offices
                          select l);
            ViewBag.CurrentSort = sortOrder;
            if (!status.HasValue)
            {
                office = office.Where(p => (int)p.Status != 2);
            }
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

            if (string.IsNullOrEmpty(sortOrder) || sortOrder.Equals("status-asc"))

            {
                ViewBag.StatusSort = "status-desc";
                ViewBag.SortIcon = "fa fa-sort-asc";
            }
            else if (sortOrder.Equals("status-desc"))
            {
                ViewBag.StatusSort = "status-asc";
                ViewBag.SortIcon = "fa fa-sort-desc";
            }
            switch (sortOrder)
            {
                case "status-desc":
                    office = office.OrderByDescending(s => s.Status);
                    break;
                case "status-asc":
                    office = office.OrderBy(s => s.Status);
                    break;

                default:
                    office = office.OrderByDescending(s => s.Status);
                    break;
            }

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
            ViewBag.District_id = new SelectList(db.District, "id", "_name");
            ViewBag.Province_id = new SelectList(db.Province, "id", "_name");
            return View();
        }

        public ActionResult GetDistrict(int? id)
        {
            var listDistrict = db.District.Where(x => x.province_id == id);
            ViewBag.listDistrict = new SelectList(listDistrict, "id", "_name");

            return PartialView("DisplayDistrict");
        }
        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Office office)
        {
            if (ModelState.IsValid)
            {
                office.ID = "Office" + db.Offices.Count();
                db.Offices.Add(office);
                db.SaveChanges();
                TempData["message"] = "Create";
                return RedirectToAction("Index");
            }
            ViewBag.District_id = new SelectList(db.District, "id", "_name", office.District_id);
            ViewBag.Province_id = new SelectList(db.Province, "id", "_name", office.Province_id);
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
            ViewBag.District_id = new SelectList(db.District, "id", "_name");
            ViewBag.Province_id = new SelectList(db.Province, "id", "_name");
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
            ViewBag.District_id = new SelectList(db.District, "id", "_name", office.District_id);
            ViewBag.Province_id = new SelectList(db.Province, "id", "_name", office.Province_id);
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
            office.Status = Office.StatusEnum.Delete;
            db.Entry(office).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = "Delete";
            return RedirectToAction("Index");
        }

        // POST: Offices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Office office = db.Offices.Find(id);
        //    db.Offices.Remove(office);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
