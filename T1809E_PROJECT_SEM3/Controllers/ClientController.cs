using System;
using System.Collections.Generic;
using System.Linq;
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
            client.Services = db.Services.AsEnumerable().ToList();
            client.Offices = db.Offices.AsEnumerable().ToList();
            return View(client);
        }
    }
}