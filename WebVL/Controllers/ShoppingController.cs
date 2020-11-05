    using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVL.Context;
using WebVL.Models;

namespace WebVL.Controllers
{
    public class ShoppingController : Controller
    {
        ProductContext db = new ProductContext();
        // GET: Shopping
        public List<ShoppingCart> ShoppingCarts()
        {
            List<ShoppingCart> shoppingCarts = Session["ShoppingCart"] as List<ShoppingCart>;
            if (shoppingCarts == null)
            {
                shoppingCarts = new List<ShoppingCart>();
                Session["ShoppingCart"] = shoppingCarts;

            }
            return shoppingCarts;
        }

        private double Total()
        {
            double i = 0;
            List<ShoppingCart> shoppingCarts = Session["ShoppingCart"] as List<ShoppingCart>;
            if (shoppingCarts != null)
            {
                i = shoppingCarts.Sum(n => n.Total);
            }
            return i;
        }

        private int Amout()
        {
            int i = 0;
            List<ShoppingCart> shoppingCarts = Session["ShoppingCart"] as List<ShoppingCart>;
            if (shoppingCarts != null)
            {
                i = shoppingCarts.Sum(n => n.Number);
            }
            return i;
        }

        public ActionResult ShoppingCart()
        {
            List<ShoppingCart> shoppingCarts = ShoppingCarts();
            if (shoppingCarts.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Amout = Amout();
            ViewBag.Total = Total();
            return View(shoppingCarts);
        }

        public ActionResult AddShoppingCart(string Idpro, string Url)
        {
            List<ShoppingCart> shoppingCarts = ShoppingCarts();
            ShoppingCart shopping = shoppingCarts.Find(n => n.Id == Idpro);
            if (shopping == null)
            {
                shopping = new ShoppingCart(Idpro);
                shoppingCarts.Add(shopping);
                return Redirect(Url);
            }
            else
            {
                shopping.Number++;

                return Redirect(Url);
            }
        }

        public ActionResult ShoppingCartPartial()
        {
            ViewBag.Total = Total();
            ViewBag.Amout = Amout(); 
            return PartialView();
        }
        public ActionResult Checkout()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["ShoppingCart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

             
             

            List<ShoppingCart> shoppingCarts = ShoppingCarts();
            ViewBag.Total = Total();
            ViewBag.Amout = Amout();
            return View(shoppingCarts);
        }


        public ActionResult Payment()
        {

            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["ShoppingCart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<ShoppingCart> shoppingCarts = ShoppingCarts();
            ViewBag.Total = Total();
            ViewBag.Amout = Amout();

            Order order = new Order();
            var kh = (ApplicationUser)Session["Taikhoan"];
            order.IdCus = kh.Id;
            order.NameCus = kh.Name;
            order.Price = Total().ToString();
            order.DayBooks = DateTime.Now.ToString();
            order.Status = 2;
            order.Address = kh.Address;
            db.Orders.Add(order);
            db.SaveChanges();

            try
            {

               
                foreach(var item in shoppingCarts)
                {
                    OrdersDetails ordersDetails = new OrdersDetails();
                    ordersDetails.OrderId = order.IdBill;
                    ordersDetails.ProductId = item.Id;
                    ordersDetails.Price = item.Price;
                    db.ordersDetails.Add(ordersDetails);
                }
                db.SaveChanges();


            }catch(Exception ex)
            {
                return HttpNotFound();
            }
          

            Session["ShoppingCart"] = null;
            Session["Chitietdonhang"] = order;
            return RedirectToAction("Xacnhan", "Shopping");

        }
        public ActionResult Xacnhan()
        {
            return View();
        }

    }
}