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
using PagedList;

namespace WebVL.Admin.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ProductContext db = new ProductContext();

        // GET: ApplicationUsers
        public async Task<ActionResult> Index(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                var usersAcc = db.Users.ToList();

                return View(usersAcc.OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize));
                //return View(await db.Users.ToListAsync());
            }
        }

        // GET: ApplicationUsers/Details/5
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
                ApplicationUser applicationUser = await db.Users.SingleOrDefaultAsync(i => i.Id == id);

                if (applicationUser == null)
                {
                    return HttpNotFound();
                }
                return View(applicationUser);
            }
        }

        // GET: ApplicationUsers/Create
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

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Address,Postcode,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(applicationUser);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(applicationUser);
            }
        }

        // GET: ApplicationUsers/Edit/5
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
                ApplicationUser applicationUser = await db.Users.SingleOrDefaultAsync(i => i.Id == id);
                if (applicationUser == null)
                {
                    return HttpNotFound();
                }
                return View(applicationUser);
            }
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,Postcode,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(applicationUser).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    TempData["EditUser"] = "Sửa Thông Tin Khách Hàng Thành Công";
                    return RedirectToAction("Index");
                }
                return View(applicationUser);
            }
        }

        // GET: ApplicationUsers/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = await db.Users.SingleOrDefaultAsync(i => i.Id == id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        //// POST: ApplicationUsers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                ApplicationUser applicationUser = await db.Users.SingleOrDefaultAsync(i => i.Id == id);
                db.Users.Remove(applicationUser);
                await db.SaveChangesAsync();
                TempData["DeleteUser"] = "Xóa Thông Tin Khách Hàng Thành Công";
                return RedirectToAction("Index");
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
