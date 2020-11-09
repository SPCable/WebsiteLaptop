    using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MomoGateWay.Model;
using MultiShop.Models;
using Newtonsoft.Json.Linq;
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

        public ActionResult XoaSP(string Idpro)
        {
            {
                List<ShoppingCart> shoppingCarts = ShoppingCarts();
                ShoppingCart card = shoppingCarts.SingleOrDefault(n => n.Id == Idpro);
                if (card != null)
                {
                    shoppingCarts.RemoveAll(n => n.Id == Idpro);
                    return RedirectToAction("ShoppingCart");
                }
                if (shoppingCarts.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("ShoppingCart");
            }
        }

        public ActionResult Capnhat(string Idpro, FormCollection f)
        {
            {
                List<ShoppingCart> shoppingCarts = ShoppingCarts();
                ShoppingCart card = shoppingCarts.SingleOrDefault(n => n.Id == Idpro);
                if (card != null)
                {
                    card.Number = int.Parse(f["txt"].ToString());
                }
                return RedirectToAction("ShoppingCart");
            }
        }
        public ActionResult PayMomo()
        {
            List<ShoppingCart> shoppingCarts = ShoppingCarts();
            var model = new Order();
            //model.CustomerId = User.Identity.Name;
            //model.OrderDate = DateTime.Now.Date;
            model.Price = Total().ToString();

            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO5RGX20191128";
            string accessKey = "M8brj9K6E22vXoDB";
            string serectKey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
            string orderInfo = "DH" + DateTime.Now.ToString("yyyyMMđHHmmss");
            string returnUrl = "https://momo.vn/return";
            string notifyurl = "https://momo.vn/notify";

            string amount = model.Price;
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";


            string rawHash = "partnerCode=" +
               partnerCode + "&accessKey=" +
               accessKey + "&requestId=" +
               requestId + "&amount=" +
               amount + "&orderId=" +
               orderid + "&orderInfo=" +
               orderInfo + "&returnUrl=" +
               returnUrl + "&notifyUrl=" +
               notifyurl + "&extraData=" +
               extraData;

            MomoSecurity crypto = new MomoSecurity();
            string signature = crypto.signSHA256(rawHash, serectKey);
            JObject message = new JObject()
            {
               { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };
            string respon = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(respon);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
    }
}