using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVL.Context;
using WebVL.Models;

namespace WebVL.Controllers
{
    public class OrderSpController : Controller
    {
        // GET: OrderSp
        ProductContext db = new ProductContext();

        public ActionResult Index(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {

                int pageNumber = (page ?? 1);
                int pageSize = 8;
                var order = from tt in db.Orders select tt;

                return View(order.OrderBy(n => n.IdBill).ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                var cargo = db.Orders.First(m => m.IdBill == id);
                return View(cargo);
            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                var Eb = db.Orders.First(m => m.IdBill == id);
                return View(Eb);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                var cargo = db.Orders.Where(m => m.IdBill == id).First();
                db.Orders.Remove(cargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Edit(Order order, int id, FormCollection collection)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                cargo.Address = address;
                cargo.DayBooks = daybook;
                cargo.NameCus = namecus;
                cargo.Price = price;
                cargo.Status = Int32.Parse(status);

                var cargo = db.Orders.First(m => m.IdBill == id);
                var namecargo = collection["Cargo"];
                var daybook = collection["DayBooks"];
                var namecus = collection["NameCus"];
                var count = collection["Count"];
                var price = collection["Price"];
                var address = collection["Address"];
                var status = collection["Status"];
                cargo.IdBill = id;
                if (string.IsNullOrEmpty(namecargo))
                {
                    ViewData["Error"] = "Bill id is null";
                }
                else
                {

                    cargo.Cargo = namecargo;
                    cargo.Count = count;
                    cargo.Address = address;
                    cargo.DayBooks = daybook;
                    cargo.NameCus = namecus;
                    cargo.Price = price;
                    cargo.Status = Int32.Parse(status);


                    UpdateModel(namecargo);
                    UpdateModel(price);
                    UpdateModel(daybook);
                    UpdateModel(address);
                    UpdateModel(namecus);
                    UpdateModel(count);
                    UpdateModel(status);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return this.Edit(id);
            }
        }
    }
}