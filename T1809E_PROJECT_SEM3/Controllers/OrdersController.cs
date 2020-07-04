
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using PagedList;
using T1809E_PROJECT_SEM3.Models;

namespace T1809E_PROJECT_SEM3.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index(string searchString, string currentFilter, int? page, int? status, DateTime? start, DateTime? end, string sortOrder, int? pageSize)
        {
            var order = (from l in db.Orders
                         select l);
            ViewBag.CurrentSort = sortOrder;


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


            int defaSize = (pageSize ?? 5);
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value="100", Text= "100" },
                new SelectListItem() { Value = order.ToList().Count().ToString(), Text= "All" },
            };




            if (!string.IsNullOrEmpty(searchString))
            {
                order = order.Where(s => s.ID.Contains(searchString) || s.SenderName.Contains(searchString) || s.SenderPhone.Contains(searchString) ||
                s.ReceiverName.Contains(searchString) || s.ReceiverPhone.Contains(searchString));
            }
            if (string.IsNullOrEmpty(sortOrder) || sortOrder.Equals("date-asc"))

            {
                ViewBag.DateSort = "date-desc";
                ViewBag.SortIcon = "fa fa-sort-asc";
            }
            else if (sortOrder.Equals("date-desc"))
            {
                ViewBag.DateSort = "date-asc";
                ViewBag.SortIcon = "fa fa-sort-desc";
            }
            switch (sortOrder)
            {

                case "date-asc":
                    order = order.OrderBy(p => p.CreateAt);
                    break;
                case "date-desc":
                    order = order.OrderByDescending(p => p.CreateAt);
                    break;

                default:
                    order = order.OrderByDescending(p => p.CreateAt);
                    ViewBag.SortIcon = "fa fa-sort";
                    break;
            }
     
            ViewBag.TotalEnity = order.Count();

            int pageNumber = (page ?? 1);
            if (!order.Any())
            {
                TempData["message"] = "NotFound";
            }
            return View(order.ToPagedList(pageNumber, defaSize));
        }

        public void ExportToExcel()
        {
            var order = db.Orders.ToList();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A2"].Value = "List Order";

            ws.Cells["H2"].Value = "Total Order ";
            ws.Cells["I2"].Value = order.Count();

            ws.Cells["H3"].Value = "Total Revenue";
            ws.Cells["I3"].Value = String.Format("{0:N0}", order.Sum(x => x.PriceShip)) + "$";




            ws.Cells["A3"].Value = "Print At";
            ws.Cells["B3"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", DateTimeOffset.Now);



            ws.Cells["A7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["B7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["C7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["D7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["E7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["F7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["G7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["H7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["I7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["J7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["K7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["L7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["M7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["N7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["O7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["P7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["Q7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);


            ws.Cells["A7"].Value = "OrderId";

            ws.Cells["B7"].Value = "Sender Name";
            ws.Cells["C7"].Value = "From Province";
            ws.Cells["D7"].Value = "At Office";
            ws.Cells["E7"].Value = "Sender Address ";
            ws.Cells["F7"].Value = "Sender PhoneNumber";

            ws.Cells["G7"].Value = "Receiver Name";
            ws.Cells["H7"].Value = "To Province";
            ws.Cells["I7"].Value = "At Office";
            ws.Cells["J7"].Value = "Receiver Address ";
            ws.Cells["K7"].Value = "Receiver PhonNumber";

            ws.Cells["L7"].Value = "Distance";
            ws.Cells["M7"].Value = "Weight";
            ws.Cells["N7"].Value = "Price Ship";
            ws.Cells["O7"].Value = "Type Item";
            ws.Cells["P7"].Value = "Service";
            ws.Cells["Q7"].Value = "Sent At";



            int rowStart = 8;
            foreach (var i in order)
            {
                if (i.PriceShip >= 2000)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor
                        .SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }


                ws.Cells[string.Format("A{0}", rowStart)].Value = i.ID;

                ws.Cells[string.Format("B{0}", rowStart)].Value = i.SenderName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = i.SenderProvince._name;
                ws.Cells[string.Format("D{0}", rowStart)].Value = i.SenderOffice.PinCode;
                ws.Cells[string.Format("E{0}", rowStart)].Value = i.SenderAddress;
                ws.Cells[string.Format("F{0}", rowStart)].Value = i.SenderPhone;

                ws.Cells[string.Format("G{0}", rowStart)].Value = i.ReceiverName;
                ws.Cells[string.Format("H{0}", rowStart)].Value = i.ReceiverProvince._name;
                ws.Cells[string.Format("I{0}", rowStart)].Value = i.ReceiverOffice.PinCode;
                ws.Cells[string.Format("J{0}", rowStart)].Value = i.ReceiverAddress;
                ws.Cells[string.Format("K{0}", rowStart)].Value = i.ReceiverPhone;

                ws.Cells[string.Format("L{0}", rowStart)].Value = i.Distance;
                ws.Cells[string.Format("M{0}", rowStart)].Value = i.Weight;
                ws.Cells[string.Format("N{0}", rowStart)].Value = i.PriceShip;
                ws.Cells[string.Format("O{0}", rowStart)].Value = i.TypeItem.Name;
                ws.Cells[string.Format("P{0}", rowStart)].Value = i.Service.TypeDelivery;
                if (i.CreateAt == null)
                {

                    ws.Cells[string.Format("Q{0}", rowStart)].Value = "Null";

                }
                else
                {
                    ws.Cells[string.Format("Q{0}", rowStart)].Value = i.CreateAt.Value.ToString("dd/MM/yyyy");
                }

                rowStart++;
            }


            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=ListOrder.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }


        public ActionResult CheckList(string ListCategoryIDs, int? OrdersStatusCheckList)
        {
            {
                if (ListCategoryIDs != null && OrdersStatusCheckList != null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        Order obj = db.Orders.Find(c);
                        switch (OrdersStatusCheckList)
                        {
                            case 0:
                                obj.Status = Order.EnumOrderStatus.Packaging;
                                break;
                            case 1:
                                obj.Status = Order.EnumOrderStatus.ShippingToOffice;
                                break;
                            case 2:
                                obj.Status = Order.EnumOrderStatus.ShippingToHouse;
                                break;
                            case 3:
                                obj.Status = Order.EnumOrderStatus.Shipped;
                                break;
                            case 4:
                                obj.Status = Order.EnumOrderStatus.Finished;
                                break;
                            case 5:
                                obj.Status = Order.EnumOrderStatus.Cancelled;
                                break;
                            case 6:
                                obj.Status = Order.EnumOrderStatus.Deleted;
                                break;
                            case -1:
                                obj.Status = Order.EnumOrderStatus.New;
                                break;
                        }

                    }
                    db.SaveChanges();
                    TempData["message"] = "ChangeStatus";
                    return RedirectToAction("Index");
                }
                if (ListCategoryIDs != null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        Order obj = db.Orders.Find(c);
                        db.Orders.Remove(obj);
                    }

                    db.SaveChanges();
                    TempData["message"] = "Delete";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "CheckFail";
                return RedirectToAction("Index");
            }
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
            ViewBag.ReceiverOfficeID = new SelectList(db.Offices, "ID", "PinCode");
            ViewBag.ReceiverProvinceID = new SelectList(db.Province, "id", "_name");
            ViewBag.SenderOfficeID = new SelectList(db.Offices, "ID", "PinCode");
            ViewBag.SenderProvinceID = new SelectList(db.Province, "id", "_name");
            ViewBag.TypeItemId = new SelectList(db.TypeItems, "ID", "Name");
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        public JsonResult CalPrice(calPrice cprice)
        {
            TypeItem typeItem = db.TypeItems.Find(cprice.TypeItemId);

            var service = ServiceList(cprice.ServiceId);


            if (service != null )
            {
                var ServiceDistance = service.Find(x => x.From <= cprice.Distance && x.To >= cprice.Distance && x.TypeCaculalor.GetHashCode() == 1);
                var ServiceWeight = service.Find(x => x.From <= cprice.Weight && x.To >= cprice.Weight && x.TypeCaculalor.GetHashCode() == 2);
                if (cprice.Distance > 3000 && cprice.Weight <= 3000)
                {
                    cprice.PriceShip = (0.06 * cprice.Distance) + ServiceWeight.PriceStep;

                }
                else if (cprice.Weight > 3000 && cprice.Distance <= 3000)
                {
                    cprice.PriceShip = (ServiceDistance.PriceStep * cprice.Distance) + cprice.Weight * 0.02;
                    cprice.PriceShip = cprice.PriceShip + (cprice.PriceShip * 5) / 100 +
                                       (cprice.PriceShip * 5) / 100;
                    cprice.PriceShip = Math.Round(cprice.PriceShip, 2);
                    return Json(cprice);
                }
                else if (cprice.Distance > 3000 && cprice.Weight > 3000)
                {
                    cprice.PriceShip = (0.06 * cprice.Distance) + (cprice.Weight * 0.02);
                    cprice.PriceShip = cprice.PriceShip + (cprice.PriceShip * 5) / 100 +
                                       (cprice.PriceShip * 5) / 100;
                    cprice.PriceShip = Math.Round(cprice.PriceShip, 2);
                    return Json(cprice);
                }
                else if (cprice.Distance <= 3000 && cprice.Weight <= 3000 )
                {
                    cprice.PriceShip = (ServiceDistance.PriceStep * cprice.Distance) + ServiceWeight.PriceStep;

                }

                cprice.PriceShip = cprice.PriceShip + (cprice.PriceShip * 5) / 100 +
                                   (cprice.PriceShip * ServiceWeight.VAT) / 100;
                cprice.PriceShip = Math.Round(cprice.PriceShip, 2);
                return Json(cprice);
            }

            cprice.PriceShip = 0;
            return Json(cprice);
        }


        public List<Service> ServiceList(string serviceId)
        {
            var temp = Convert.ToInt32(serviceId);
            var service = db.Services.ToList();
            switch (temp)
            {
                case 1:
                    service = service.Where(x => x.TypeDelivery == Service.EnumServiceType.Fast_Delivery).ToList();
                    break;
                case 2:
                    service = service.Where(x => x.TypeDelivery == Service.EnumServiceType.Savings_Delivery).ToList();
                    break;
                case 3:
                    service = null;
                    break;
            }

            return service;
        }
        public ActionResult GetOffice(int? id)
        {
            var listOffice = db.Offices.Where(x => x.Province_id == id);
            ViewBag.listOffice = new SelectList(listOffice, "id", "PinCode");

            return PartialView("DisplayOffice");
        }

        //POST: Orders/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                if ((User.Identity.GetUserId()) == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                order.CreatedById = String.Format(User.Identity.GetUserId());
                order.CreatedBy = db.Users.Find(User.Identity.GetUserId());
                var service = ServiceList(order.ServiceId);
                var serverID = service.FirstOrDefault(x => x.From <= order.Distance && x.To >= order.Distance);
                order.ServiceId = serverID.ID;
                order.ID = "OD" + DateTime.Now.Millisecond + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
                order.CreateAt = DateTime.Now;
                order.Status = Order.EnumOrderStatus.New;
                db.Orders.Add(order);
                db.SaveChanges();
                sendMail(order.ID);
                TempData["message"] = "Success";
                return RedirectToAction("Index");
            }

            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", order.CreatedById);
            ViewBag.ReceiverOfficeID = new SelectList(db.Offices, "ID", "PinCode", order.ReceiverOfficeID);
            ViewBag.ReceiverProvinceID = new SelectList(db.Province, "id", "_name", order.ReceiverProvinceID);
            ViewBag.SenderOfficeID = new SelectList(db.Offices, "ID", "PinCode", order.SenderOfficeID);
            ViewBag.SenderProvinceID = new SelectList(db.Province, "id", "_name", order.SenderProvinceID);
            ViewBag.TypeItemId = new SelectList(db.TypeItems, "ID", "Name", order.TypeItemId);
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName", order.UpdatedById);
            return View(order);
        }

        public void sendMail(string OrderID)
        {
            var order = db.Orders.Find(OrderID);
            var senderEmail = new MailAddress("namkun159198@gmail.com", "RocketShip");
            var receiverEmail = new MailAddress(order.Email, "Receiver");
            var password = "0963404604";
            var sub = "[ RocketShip ] : Order #" + order.ID;
            var body = "Sender Name : " +order.SenderName +"\n"+"Receiver Name : "+order.ReceiverName +
                       " - PhoneNumber : "+order.ReceiverPhone +"- Address Details : "+order.ReceiverAddress+"\n"+
                "Price Ship : " + String.Format("{0:N0}", (order.PriceShip)) + "$" + "\n"+
                "Status :" +order.Status ;
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }
        }
        //GET: Orders/Edit/5
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
            ViewBag.ReceiverOfficeID = new SelectList(db.Offices, "ID", "PinCode", order.ReceiverOfficeID);
            ViewBag.ReceiverProvinceID = new SelectList(db.Province, "id", "_name", order.ReceiverProvinceID);
            ViewBag.SenderOfficeID = new SelectList(db.Offices, "ID", "PinCode", order.SenderOfficeID);
            ViewBag.SenderProvinceID = new SelectList(db.Province, "id", "_name", order.SenderProvinceID);
            ViewBag.TypeItemId = new SelectList(db.TypeItems, "ID", "Name", order.TypeItemId);
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName", order.UpdatedById);
            return View(order);
        }

        //POST: Orders/Edit/5
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                if ((User.Identity.GetUserId()) == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                order.UpdatedById = String.Format(User.Identity.GetUserId());
                order.UpdatedBy = db.Users.Find(User.Identity.GetUserId());
                var service = ServiceList(order.ServiceId);
                var serverID = service.FirstOrDefault(x => x.From <= order.Distance && x.To >= order.Distance);
                order.ServiceId = serverID.ID;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Success";
                return RedirectToAction("Index");
            }
            ViewBag.CreatedById = new SelectList(db.Users, "Id", "FullName", order.CreatedById);
            ViewBag.ReceiverOfficeID = new SelectList(db.Offices, "ID", "PinCode", order.ReceiverOfficeID);
            ViewBag.ReceiverProvinceID = new SelectList(db.Province, "id", "_name", order.ReceiverProvinceID);
            ViewBag.SenderOfficeID = new SelectList(db.Offices, "ID", "PinCode", order.SenderOfficeID);
            ViewBag.SenderProvinceID = new SelectList(db.Province, "id", "_name", order.SenderProvinceID);
            ViewBag.ServiceId = new SelectList(db.Services, "ID", "Type", order.ServiceId);
            ViewBag.TypeItemId = new SelectList(db.TypeItems, "ID", "Name", order.TypeItemId);
            ViewBag.UpdatedById = new SelectList(db.Users, "Id", "FullName", order.UpdatedById);
            return View(order);
        }

        //GET: Orders/Delete/5
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
            order.Status = Order.EnumOrderStatus.Deleted;
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

