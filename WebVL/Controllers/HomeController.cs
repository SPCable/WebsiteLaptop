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
        int f = 0;

        public ActionResult Laptop(int? page, int ? Id , string  gia)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 20;
            var product = productContext.Products.ToList();
            if (gia == "duoi-10-trieu")
            {
                if(Id != null)
                {
                    var products = productContext.Products.Where(n => n.productPrice < 10000000 && n.CategoryId == Id.ToString()).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                    return View(products);
                }

                var a = productContext.Products.Where(n => n.productPrice < 10000000).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                return View(a);

            }
            if (gia == "10-15-trieu")
            {
                if (Id != null)
                {
                    var products = productContext.Products.Where(n => n.productPrice >= 10000000 && n.productPrice <= 15000000 && n.CategoryId == Id.ToString()).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                    return View(products);
                }
                var a = productContext.Products.Where(n => n.productPrice >= 10000000 && n.productPrice <= 15000000).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                return View(a);

            }
            if (gia == "15-20-trieu")
            {
                if (Id != null)
                {
                    var products = productContext.Products.Where(n => n.productPrice >= 15000000 && n.productPrice <= 20000000 && n.CategoryId == Id.ToString()).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                    return View(products);
                }
                var a = productContext.Products.Where(n => n.productPrice >= 15000000 && n.productPrice <= 20000000).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                return View(a);
            }
            if (gia == "20-25-trieu")
            {
                if (Id != null)
                {
                    var products = productContext.Products.Where(n => n.productPrice >= 20000000 && n.productPrice <= 25000000 && n.CategoryId == Id.ToString()).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                    return View(products);
                }
                var a = productContext.Products.Where(n => n.productPrice >= 20000000 && n.productPrice <= 25000000).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                return View(a);
            }
            if (gia == "tren-25-trieu")
            {
                if (Id != null)
                {
                    var products = productContext.Products.Where(n => n.productPrice >= 25000000 && n.CategoryId == Id.ToString()).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                    return View(products);
                }

                var a = productContext.Products.Where(n => n.productPrice >= 25000000).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                return View(a);
            }

      
            if (gia == null && Id == null)
            {
                var products = productContext.Products.OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
                return View(products);
            }
            else
            {
                return View(productContext.Products.Where(n => n.CategoryId == Id.ToString()).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize));

            }
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
        public ActionResult Result(int? page, string searching)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 20;
            var product = productContext.Products.ToList();
            var a = productContext.Products.Where(x => x.productName.Contains(searching) || searching == null).OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
            return View(a);
        }
        [HttpPost]
        public ActionResult Result(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 20;
            var product = productContext.Products.ToList().OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize);
            return View(product);
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