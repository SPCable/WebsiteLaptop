using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVL.Context;

namespace WebVL.Controllers
{
    public class SearchController : Controller
    {
        ProductContext productContext = new ProductContext();

        public ActionResult Result(string searching)
        {
            var a = productContext.Products.Where(x => x.productName.Contains(searching) || searching == null).ToList();
            return View(a);
        }

    }
}