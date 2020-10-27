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

        public ActionResult Index()
        {
            var order = from tt in db.Orders select tt;
            return View(order);
        }
        public ActionResult Delete(int id)
        {
            var cargo = db.Orders.First(m => m.IdBill == id);
            return View(cargo);
        }
        public ActionResult Edit(int id)
        {
            var Eb = db.Orders.First(m => m.IdBill == id);
            return View(Eb);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var cargo = db.Orders.Where(m => m.IdBill == id).First();
            db.Orders.Remove(cargo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Edit(Order order, int id, FormCollection collection)
        {
            
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

                cargo.Cargo= namecargo;
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