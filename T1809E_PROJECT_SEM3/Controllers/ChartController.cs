﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    public class ChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Chart
        public ActionResult Index(DateTime? start, DateTime? end,int? ex)
        {
            var R2020 = db.Orders.GroupBy(o => o.CreateAt.Value).Select(group => new
            {
                creatAt = group.Key,
                Revenue = group.Sum(x => x.PriceShip)
            }).OrderBy(x => x.creatAt);

            Dictionary<DateTime?, Double> listR2020 = new Dictionary<DateTime?, double>();
            if (start != null  && end != null)
            {
                var startDate = start.GetValueOrDefault().Date;
                startDate = startDate.Date + new TimeSpan(0, 0, 0);
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                foreach (var i in R2020)
                {
                    if (i.creatAt >= startDate && i.creatAt <= endDate)
                    {
                        listR2020.Add(i.creatAt, i.Revenue);
                    }
                }
            }
            else
            {
                foreach (var i in R2020)
                {
                   
                    listR2020.Add(i.creatAt, i.Revenue);
                }
            }

            if (ex == 1)
            {
                ExportVoid(listR2020);
            }
          

          

            List<ChartModel.ChartModel.DataPoint3> dataPoints20 = new List<ChartModel.ChartModel.DataPoint3>();
           


            /*foreach (var l in R2017)
            {
                dataPoints7.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }*/

            foreach (var l in listR2020)
            {
                dataPoints20.Add(new ChartModel.ChartModel.DataPoint3(l.Key.Value.Date,(double)l.Value));
            }
          
            ViewBag.DataPoints20 = JsonConvert.SerializeObject(dataPoints20);

            return View();
        }

        public void ExportVoid(Dictionary<DateTime?,double> ex)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            int rowStart = 8;


            ws.Cells["A2"].Value = "List Revenue ";

            ws.Cells["H3"].Value = "Total Revenue ";
            ws.Cells["I3"].Value = String.Format("{0:N0}", (ex.Sum(x => x.Value))) + "USD";

            ws.Cells["A3"].Value = "Sprint At";
            ws.Cells["B3"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", DateTimeOffset.Now);

            ws.Cells["A7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["B7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["A7"].Value = "Date";
            ws.Cells["B7"].Value = "Revenue";
          

            foreach (var i in ex)
            {
                if (i.Value >= 3000)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor
                        .SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }
                ws.Cells[string.Format("K{0}", rowStart)].Value = string.Format("{0:dd/MM/yyyy}", i.Key); 
                ws.Cells[string.Format("H{0}", rowStart)].Value = i.Value +"USD";

                rowStart++;
            }


            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=ListRevenue.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

    }
}