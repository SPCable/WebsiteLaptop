using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebVL.Context;

namespace WebVL.Controllers
{
    public class StattController : Controller
    {
        // GET: Statt
        ProductContext db = new ProductContext();
        public ActionResult Index()
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                var statt = from tt in db.Statts select tt;
                return View(statt);
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
                var statt = db.Statts.First(m => m.Idpost == id);
                return View(statt);
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
                var statt = db.Statts.First(m => m.Idpost == id);
                return View(statt);
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
                var statt = db.Statts.Where(m => m.Idpost == id).First();
                db.Statts.Remove(statt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                var statt = db.Statts.First(m => m.Idpost == id);
                var detail = collection["Details"];

                statt.Idpost = id;
                if (string.IsNullOrEmpty(detail))
                {
                    ViewData["Error"] = "Stattus is emty";
                }
                else
                {
                    statt.Details = detail;
                    UpdateModel(detail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return this.Edit(id);
            }
        }
    }
}