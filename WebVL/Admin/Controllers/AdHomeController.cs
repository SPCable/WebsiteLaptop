using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using PagedList;
using PagedList.Mvc;
using System.Web;
using System.Web.Mvc;
using WebVL.Context;
using WebVL.Models;

namespace WebVL.Admin.Ad.Controllers
{
    public class AdHomeController : Controller
    {

        public ProductContext db = new ProductContext();


        // GET: AdHome
        public ActionResult Index(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                var dssp = db.Products.ToList();

                return View(dssp.OrderBy(n => n.ProductId).ToPagedList(pageNumber, pageSize));
            }

        }

        public ActionResult CreateSP()
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateSP(Product product, HttpPostedFileBase fileUpload0, HttpPostedFileBase fileUpload1, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3)
        {

            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (product.productDesc == null)
                {
                    product.productDesc = " ";
                }

                var checkIDProduct = db.Products.Where(a => a.ProductId == product.ProductId).SingleOrDefault();
                if (checkIDProduct != null)
                {
                    ViewBag.checkIDProduct = "Trùng ID, xin nhập ID khác!";
                    return View();
                }

                if (product.ProductId == null)
                {
                    ViewBag.ThongbaoProductId = "Phải nhập thông tin này";
                    return View();
                }
                if (product.productName == null)
                {
                    ViewBag.ThongbaoProductName = "Phải nhập thông tin này";
                    return View();
                }


                if (fileUpload0 == null)
                {
                    ViewBag.Thongbao0 = "Phải có ít nhất 1 ảnh";
                    return View();
                }
                else
                {

                    if (ModelState.IsValid)
                    {
                        string fileName0, fileName1, fileName2, fileName3;

                        fileName0 = Path.GetFileName(fileUpload0.FileName);
                        ////////////////////////////////
                        if (fileUpload1 == null)
                        {
                            fileName1 = "none.jpg";
                        }
                        else
                        {
                            fileName1 = Path.GetFileName(fileUpload0.FileName);
                        }
                        ////////////////////////////////
                        if (fileUpload2 == null)
                        {
                            fileName2 = "none.jpg";
                        }
                        else
                        {
                            fileName2 = Path.GetFileName(fileUpload2.FileName);
                        }
                        //////////////////////////////////
                        if (fileUpload3 == null)
                        {
                            fileName3 = "none.jpg";
                        }
                        else
                        {
                            fileName3 = Path.GetFileName(fileUpload3.FileName);
                        }

                        {
                            //var path = Path.Combine(Server.MapPath("~/img/product/"), fileName0);

                            //if(System.IO.File.Exists(path))
                            //{
                            //    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                            //}
                            //else
                            //{
                            //    fileUpload0.SaveAs(path);
                            //}
                        }

                        var sp = new Product
                        {
                            ProductId = product.ProductId,
                            productName = product.productName,
                            Cpu = product.Cpu,
                            OpSys = product.OpSys,
                            productPrice = product.productPrice,
                            productDiscout = product.productDiscout,
                            productView = product.productView,
                            productImg = fileName0,
                            productImg1 = fileName1,
                            productImg2 = fileName2,
                            productImg3 = fileName3,
                            productAmount = product.productAmount,
                            productDesc = product.productDesc,
                            CategoryId = product.CategoryId
                            //Tự động lấy ngày nhập sp
                        };


                        db.Products.Add(sp);
                        db.SaveChanges();
                    }
                    TempData["AddProduct"] = "Thêm Sản Phẩm thành công";
                    return RedirectToAction("Index");
                }
            }
        }


        public ActionResult EditSP(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                var product = db.Products.Find(id);

                if (product.productImg != null)
                {
                    TempData["imgValiable0"] = "(Ảnh hiện tại!)";
                }
                /////////////////////////////
                if (product.productImg1 == "none.jpg" || product.productImg1 == null)
                {

                    TempData["imgNotValiable1"] = "(Chưa có ảnh!)";
                }
                else
                {
                    TempData["imgValiable1"] = "(Ảnh hiện tại!)";
                }
                /////////////////////////////
                if (product.productImg2 == "none.jpg" || product.productImg2 == null)
                {

                    TempData["imgNotValiable2"] = "(Chưa có ảnh!)";
                }
                else
                {
                    TempData["imgValiable2"] = "(Ảnh hiện tại!)";
                }
                //////////////////////////////
                if (product.productImg3 == "none.jpg" || product.productImg3 == null)
                {

                    TempData["imgNotValiable3"] = "(Chưa có ảnh!)";
                }
                else
                {
                    TempData["imgValiable3"] = "(Ảnh hiện tại!)";
                }



                Session["imgPath0"] = product.productImg;
                Session["imgPath1"] = product.productImg1;
                Session["imgPath2"] = product.productImg2;
                Session["imgPath3"] = product.productImg3;
                return View(product);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSP(Product product, HttpPostedFileBase fileUpload0, HttpPostedFileBase fileUpload1, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (product.productDesc == null)
                {
                    product.productDesc = " ";
                }

                if (fileUpload0 != null)
                {
                    if (ModelState.IsValid)
                    {
                        var fileName0 = Path.GetFileName(fileUpload0.FileName);
                        product.productImg = fileName0;
                    }
                }
                else
                {
                    product.productImg = Session["imgPath0"].ToString();
                }

                if (fileUpload1 != null)
                {
                    if (ModelState.IsValid)
                    {
                        var fileName1 = Path.GetFileName(fileUpload1.FileName);
                        product.productImg1 = fileName1;
                    }
                }
                else
                {
                    if (fileUpload1 == null && Session["imgPath1"] == null)
                    {
                        product.productImg1 = "none.jpg";
                    }
                    else
                    {
                        if (fileUpload1 == null && Session["imgPath1"] != null)
                        {
                            product.productImg1 = Session["imgPath1"].ToString();
                        }
                    }
                }

                if (fileUpload2 != null)
                {
                    if (ModelState.IsValid)
                    {
                        var fileName2 = Path.GetFileName(fileUpload2.FileName);
                        product.productImg2 = fileName2;
                    }
                }
                else
                {
                    if (fileUpload2 == null && Session["imgPath2"] == null)
                    {
                        product.productImg2 = "none.jpg";
                    }
                    else
                    {
                        if (fileUpload2 == null && Session["imgPath2"] != null)
                        {
                            product.productImg2 = Session["imgPath2"].ToString();
                        }
                    }
                }

                if (fileUpload3 != null)
                {
                    if (ModelState.IsValid)
                    {
                        var fileName3 = Path.GetFileName(fileUpload3.FileName);
                        product.productImg3 = fileName3;
                    }
                }
                else
                {
                    if (fileUpload3 == null && Session["imgPath3"] == null)
                    {
                        product.productImg3 = "none.jpg";
                    }
                    else
                    {
                        if (fileUpload3 == null && Session["imgPath3"] != null)
                        {
                            product.productImg3 = Session["imgPath3"].ToString();
                        }
                    }
                }

                db.Entry(product).State = EntityState.Modified;




                if (db.SaveChanges() > 0)
                {

                    TempData["msg"] = "Cập nhật Sản Phẩm thành công";
                    return RedirectToAction("Index");
                }


                return View();
            }
        }


        public ActionResult DeleteSP(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                Product spId = db.Products.Single(i => i.ProductId == id);
                db.Products.Remove(spId);
                db.SaveChanges();
                TempData["DeleteProduct"] = "Xóa Sản Phẩm thành công";
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>


        public ActionResult Category(int? page)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {

                int pageNumber = (page ?? 1);
                int pageSize = 8;
                var categoryList = db.Categories.ToList();

                return View(categoryList.OrderBy(n => n.categoryId).ToPagedList(pageNumber, pageSize));

            }
        }

        public ActionResult CreateCate()
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCate(Category category, HttpPostedFileBase PicDanhMucUpload)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var checkIDCate = db.Categories.Where(a => a.categoryId == category.categoryId).SingleOrDefault();
                    if (checkIDCate != null)
                    {
                        ViewBag.checkIDCate = "Trùng ID, xin nhập ID khác!";
                        return View();
                    }

                    if (category.categoryId == null)
                    {
                        ViewBag.categoryId = "phải nhập thông tin này";
                        return View();
                    }

                    if (category.CategoryName == null)
                    {
                        ViewBag.CategoryName = "phải nhập thông tin này";
                        return View();
                    }
                    if (category.BrandId == null)
                    {
                        ViewBag.BrandId = "phải nhập thông tin này";
                        return View();
                    }

                    if (category.BrandName == null)
                    {
                        ViewBag.BrandName = "phải nhập thông tin này";
                        return View();
                    }

                    string filePicDM;
                    if (PicDanhMucUpload == null)
                    {
                        filePicDM = "none.jpg";
                    }
                    else
                    {
                        filePicDM = Path.GetFileName(PicDanhMucUpload.FileName);
                    }

                    var Addcate = new Category
                    {
                        categoryId = category.categoryId,
                        CategoryImg = filePicDM,
                        CategoryName = category.CategoryName,
                        BrandId = category.BrandId,
                        BrandName = category.BrandName

                    };
                    db.Categories.Add(Addcate);
                    db.SaveChanges();
                }
                TempData["AddCate"] = "Thêm Danh Mục thành công";
                return RedirectToAction("Category");
            }
        }

        public ActionResult EditCate(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                var category = db.Categories.Find(id);



                if (category.CategoryImg == "none.jpg" || category.CategoryImg == null)
                {
                    TempData["imgCateEmpty"] = "(Chưa có ảnh!)";
                }
                else
                {
                    TempData["imgCate"] = "(Ảnh hiện tại!)";
                }


                Session["imgCatePath"] = category.CategoryImg;

                return View(category);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCate(Category category, HttpPostedFileBase PicDanhMucUpload)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                if (PicDanhMucUpload != null)
                {
                    if (ModelState.IsValid)
                    {
                        var fileCate = Path.GetFileName(PicDanhMucUpload.FileName);
                        category.CategoryImg = fileCate;
                    }
                }
                else
                {
                    category.CategoryImg = Session["imgCatePath"].ToString();
                }
                db.Entry(category).State = EntityState.Modified;

                if (db.SaveChanges() > 0)
                {
                    TempData["msgCate"] = "Cập nhật Danh Mục thành công";
                    return RedirectToAction("Category");
                }
                return View();
            }
        }


        public ActionResult DeleteCate(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
            {
                return RedirectToAction("LoginAdmin", "AdHome");
            }
            else
            {
                Category cateId = db.Categories.Single(i => i.categoryId == id);
                db.Categories.Remove(cateId);
                db.SaveChanges();
                TempData["DeleteCate"] = "Xóa Danh Mục thành công";
                return RedirectToAction("Category");
            }
        }



        /////////////////////////////////////////////
        ///

        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(FormCollection collection)
        {
            var userAdmin = collection["useradmin"];
            var MatKhauAdmin = collection["passadmin"];
            if (String.IsNullOrEmpty(userAdmin))
            {
                ViewData["loi1"] = "Phải nhập tên Đăng nhập";
            }
            else if (String.IsNullOrEmpty(MatKhauAdmin))
            {
                ViewData["loi2"] = "Phải nhập Mật khẩu";
            }
            else
            {
                var adlogin = db.AdminAccounts.SingleOrDefault(n => n.UserAdmin == userAdmin && n.PassAdmin == MatKhauAdmin);
                if (adlogin != null)
                {

                    Session["TaikhoanAdmin"] = adlogin;
                    Session["TaikhoanAdminName"] = adlogin.AdminName.ToString();
                    Session["TaikhoanAdminID"] = adlogin.AdminId.ToString();


                    return RedirectToAction("Index", "AdHome");
                }
                else
                {
                    ViewBag.LoginFail = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }

        public ActionResult LogoutAdmin()
        {
            Session["TaikhoanAdmin"] = null;
            return RedirectToAction("Index", "AdHome");

        }

        public ActionResult TopProductSales()
        {
            return View();
        }

        /////////////////////////
    }
}