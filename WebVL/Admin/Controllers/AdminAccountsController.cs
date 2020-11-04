using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebVL.Context;
using WebVL.Models;

namespace WebVL.Admin.Controllers
{
    public class AdminAccountsController : Controller
    {
        private ProductContext db = new ProductContext();

        // GET: AdminAccounts
        public async Task<ActionResult> Index()
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                return View(await db.AdminAccounts.ToListAsync());
            }
        }

        // GET: AdminAccounts/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AdminAccount adminAccount = await db.AdminAccounts.FindAsync(id);
                if (adminAccount == null)
                {
                    return HttpNotFound();
                }
                return View(adminAccount);
            }
        }

        // GET: AdminAccounts/Create
        public ActionResult Create()
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                return View();
            }
        }

        // POST: AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AdminId,UserAdmin,PassAdmin,AdminName")] AdminAccount adminAccount)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.AdminAccounts.Add(adminAccount);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(adminAccount);
            }
        }

        // GET: AdminAccounts/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AdminAccount adminAccount = await db.AdminAccounts.FindAsync(id);
                if (adminAccount == null)
                {
                    return HttpNotFound();
                }
                return View(adminAccount);
            }
        }

        // POST: AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AdminId,UserAdmin,PassAdmin,AdminName")] AdminAccount adminAccount)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(adminAccount).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Details", new { id = Session["TaikhoanAdminID"] });
                }
                return View(adminAccount);
            }
        }

        // GET: AdminAccounts/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AdminAccount adminAccount = await db.AdminAccounts.FindAsync(id);
                if (adminAccount == null)
                {
                    return HttpNotFound();
                }
                return View(adminAccount);
            }
        }

        // POST: AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                AdminAccount adminAccount = await db.AdminAccounts.FindAsync(id);
                db.AdminAccounts.Remove(adminAccount);
                await db.SaveChangesAsync();
                return RedirectToAction("LoginAdmin", "AdHome");
            }
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
