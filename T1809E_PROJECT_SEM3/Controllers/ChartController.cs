using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    public class ChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Chart
        public ActionResult Index()
        {

            List<ChartModel.ChartModel.DataPoint3> dataPoints7 = new List<ChartModel.ChartModel.DataPoint3>();
            List<ChartModel.ChartModel.DataPoint3> dataPoints8 = new List<ChartModel.ChartModel.DataPoint3>();
            List<ChartModel.ChartModel.DataPoint3> dataPoints9 = new List<ChartModel.ChartModel.DataPoint3>();
            List<ChartModel.ChartModel.DataPoint3> dataPoints20 = new List<ChartModel.ChartModel.DataPoint3>();
            /*var R2017 = db.Orders.Where(x => x.CreateAt.Value.Year == 2017).GroupBy(o => o.CreateAt.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.PriceShip)
            }).OrderBy(x => x.createAt);*/

            var R2018 = db.Orders.Where(x => x.CreateAt.Value.Year == 2018).GroupBy(o => o.CreateAt.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.PriceShip)
            }).OrderBy(x => x.createAt);

            var R2019 = db.Orders.Where(x => x.CreateAt.Value.Year == 2019).GroupBy(o => o.CreateAt.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.PriceShip)
            }).OrderBy(x => x.createAt);

            var R2020 = db.Orders.Where(x => x.CreateAt.Value.Year == 2020).GroupBy(o => o.CreateAt.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.PriceShip)
            }).OrderBy(x => x.createAt);


            /*foreach (var l in R2017)
            {
                dataPoints7.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }*/
            foreach (var l in R2018)
            {
                dataPoints8.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }
            foreach (var l in R2019)
            {
                dataPoints9.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }
            foreach (var l in R2020)
            {
                dataPoints20.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }
            ViewBag.DataPoints7 = JsonConvert.SerializeObject(dataPoints7);
            ViewBag.DataPoints8 = JsonConvert.SerializeObject(dataPoints8);
            ViewBag.DataPoints9 = JsonConvert.SerializeObject(dataPoints9);
            ViewBag.DataPoints20 = JsonConvert.SerializeObject(dataPoints20);

            return View();
        }
    }
}