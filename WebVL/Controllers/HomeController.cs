using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using WebVL.Context;
using WebVL.Models;

namespace WebVL.Controllers
{
    public class HomeController : Controller
    {
        ProductContext productContext = new ProductContext();
        public ActionResult Index()
        {

            var product = productContext.Products.ToList();
            return View(product);
        }

        public ActionResult Category()
        {
            var categories = productContext.Categories.ToList();
            return PartialView(categories);
        }

        public ActionResult Laptop(int ? Id , string ? gia)
        {
            if (Id != null)
            {
                if (Id != null && gia != null)
                {
                    if (gia == "duoi-10-trieu")
                    {
                        var products = productContext.Products.Where(n => n.CategoryId == Id.ToString() && n.productPrice <= 10000000).ToList();
                        return View(products);
                    }
                    if (gia == "10-15-trieu")
                    {
                        var products = productContext.Products.Where(n => n.productPrice >= 10000000 && n.productPrice <= 15000000 && n.CategoryId == Id.ToString()).ToList();
                        return View(products);
                    }
                    if (gia == "15-20-trieu")
                    {
                        var products = productContext.Products.Where(n => n.productPrice >= 15000000 && n.productPrice <= 20000000 && n.CategoryId == Id.ToString()).ToList();
                        return View(products);
                    }
                    if (gia == "20-25-trieu")
                    {
                        var products = productContext.Products.Where(n => n.productPrice >= 20000000 && n.productPrice <= 25000000 && n.CategoryId == Id.ToString()).ToList();
                        return View(products);
                    }
                    if (gia == "tren-25-trieu")
                    {
                        var products = productContext.Products.Where(n => n.productPrice >= 25000000 && n.CategoryId == Id.ToString()).ToList();
                        return View(products);
                    }
                }
            }
            var a = productContext.Products.Where(n => n.CategoryId == Id.ToString()).ToList();
            return View(a);
        }

        public ActionResult Product(string id = "#" )
        {
            if (id != null)
            {
                var a = productContext.Products.Where(n => n.ProductId == id).ToList();
                return View(a);
            }
            return View();
        }



        public ActionResult search(double? gia, string name, string id)
        {

            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}