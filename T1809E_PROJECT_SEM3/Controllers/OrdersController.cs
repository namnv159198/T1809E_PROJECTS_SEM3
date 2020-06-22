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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index(string searchString, string currentFilter, int? page, int? status, DateTime? start, DateTime? end)
        {
            var order = (from l in db.Orders
                          select l);
           
            if (start != null)
            {
                var startDate = start.GetValueOrDefault().Date;
                startDate = startDate.Date + new TimeSpan(0, 0, 0);
                order = order.Where(p => p.CreateAt >= startDate);
            }
            if (end != null)
            {
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                order = order.Where(p => p.CreateAt <= endDate);
            }
            if (!status.HasValue)
            {
                order = order.Where(p => (int)p.Status != 6);
            }
            if (status.HasValue)
            {
                ViewBag.Status = status;

                order = order.Where(p => (int)p.Status == status.Value);
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
                order = order.Where(s => s.ID.Contains(searchString)||s.SenderName.Contains(searchString)||s.SenderPhone.Contains(searchString)||
                s.ReceiverName.Contains(searchString)||s.ReceiverPhone.Contains(searchString));
            }
            order = order.OrderBy(x => x.ID);

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(order.ToPagedList(pageNumber, pageSize));
        }

        // GET: Orders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName");
            ViewBag.ServiceId = new SelectList(db.Services, "ID", "Type");
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName");
            return View();
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SenderName,SenderAddress,SenderPhone,ReceiverName,ReceiverAddress,ReceiverPhone,ServiceName,Distance,Weight,CreateAt,PriceShip,Status,ServiceId,CreatedById,UpdatedById")] Order order)
        {
            if (ModelState.IsValid)
            {
                string timeStamp = GetTimestamp(DateTime.Now);
                order.ID = "Order" + timeStamp;
                order.CreateAt = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", order.CreatedById);
            ViewBag.ServiceId = new SelectList(db.Services, "ID", "Type", order.ServiceId);
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName", order.UpdatedById);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", order.CreatedById);
            ViewBag.ServiceId = new SelectList(db.Services, "ID", "Type", order.ServiceId);
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName", order.UpdatedById);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SenderName,SenderAddress,SenderPhone,ReceiverName,ReceiverAddress,ReceiverPhone,ServiceName,Distance,Weight,CreateAt,PriceShip,Status,ServiceId,CreatedById,UpdatedById")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", order.CreatedById);
            ViewBag.ServiceId = new SelectList(db.Services, "ID", "Type", order.ServiceId);
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName", order.UpdatedById);
            return View(order);
        }

        // GET: Orders/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = Order.EnumStatusOrder.Deleted;
            db.Entry(order).State = EntityState.Modified;
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
