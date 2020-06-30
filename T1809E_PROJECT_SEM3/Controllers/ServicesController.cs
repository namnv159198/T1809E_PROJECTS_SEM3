using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    public class ServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Services
        public ActionResult Index(string searchString, string currentFilter, int? status, int? page)
        {
            var services = db.Services.AsEnumerable();
            if (status.HasValue)
            {
                ViewBag.Status = status;

                services = services.Where(p => (int)p.Status == status.Value);
            }
            if (searchString == null)
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;
           
            services = services.OrderByDescending(x => x.Status);

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(services.ToPagedList(pageNumber, pageSize));
        }

        // GET: Services/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Service service)
        {
            if (ModelState.IsValid)
            {
                /*service.TimeUsed = 0;*/
                service.Status = Service.StatusEnumService.Online; 
                service.ID = "Service" + db.Services.Count();
                db.Services.Add(service);
                db.SaveChanges();
                TempData["message"] = "Create";
                return RedirectToAction("Index");
            }
            TempData["message"] = "Fail";
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type,PriceStep,DistanceStep,PriceWeight, Status,Description,TimeUsed")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Edit";
                return RedirectToAction("Index");
            }
            TempData["message"] = "Fail";
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            if (service.Status == Service.StatusEnumService.Deleted)
            {
                TempData["message"] = "Fail Delete";
                return RedirectToAction("Index");
            }
            service.Status = Service.StatusEnumService.Deleted;
            db.Entry(service).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = "Delete";
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
