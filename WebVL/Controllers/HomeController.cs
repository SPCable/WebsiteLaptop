using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebVL.Context;
using WebVL.Models;

namespace WebVL.Controllers
{
    public class HomeController : Controller
    {
        ProductContext productContext = new ProductContext();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 20;
            var product = productContext.Products.ToList();

            return View(product.OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize));

            
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
        [HttpGet]
        public ActionResult Result(string searching)
        {
            var a = productContext.Products.Where(x => x.productName.Contains(searching) || searching == null).ToList();
            return View(a);
        }
        [HttpPost]
        public ActionResult Result()
        {
            var a = productContext.Products.ToList();
            return View(a);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Trangchu()
        {
            return View();
        }
    }
}