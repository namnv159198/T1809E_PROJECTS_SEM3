using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Client
        public ActionResult Index()
        {
            ClientViewModel client = new ClientViewModel();
            var service = db.Services.AsEnumerable();
            var office = db.Offices.AsEnumerable();
            client.Services = service.Where(s => s.Status == Service.StatusEnumService.Online).Take(4).ToList();
            client.Offices = office.Where(s => s.Status == Office.StatusEnum.Online).Take(10).ToList();
            return View(client);
        }

        [HttpGet]
        public ActionResult Details(string id, string senderName, string senderPhone)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            if (order != null)
            {
                if (order.SenderName == senderName && order.SenderPhone == senderPhone)
                {
                    return View("OrderDetails", order);
                }

                return HttpNotFound();
            }

            return HttpNotFound();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult shippingCalculator()
        {
            ViewBag.TypeItemId = new SelectList(db.TypeItems, "ID", "Name");

            return View();
        }
    }
}

