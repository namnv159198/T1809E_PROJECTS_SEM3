﻿using System;
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
            return View("OrderDetails",order);
        }
    }
}