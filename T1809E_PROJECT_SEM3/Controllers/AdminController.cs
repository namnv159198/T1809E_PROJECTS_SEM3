using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.TotalRevenuDay = db.Orders.AsEnumerable().Where(x => x.CreateAt.Value.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")).Sum(x => x.PriceShip);
            ViewBag.TotalOrderDay = db.Orders.AsEnumerable().Count(x => x.CreateAt.Value.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"));
            return View();
        }
    }
}