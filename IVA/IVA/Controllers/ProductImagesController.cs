using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IVA.Models;
using System.Web.Configuration;
using System.Net.Http;

namespace IVA.Controllers
{
    public class ProductImagesController : Controller
    {
        private IVADBEntities db = new IVADBEntities();
        //
        // GET: /ProductImages/
        private const string TempPath = @"Images\Products\";
        public ActionResult Index()
        {
            tbl_Product_Images c = new tbl_Product_Images();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImageInsert(long id, string Caption, string IsDefault, string Remarks, HttpPostedFileBase imageFile)
        {
            var fil = Request["imageFile"];
            var fils = Request["hdfile"];
            var pro = db.tbl_Products.FirstOrDefault(p => p.ID == id);
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files != null && files.Count > 0)
                {

                    var file = files[0];
                    if (file != null && file.ContentLength > 0)
                    {

                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // TODO: need to define destination
                        string imageDirectoryPath = WebConfigurationManager.AppSettings.Get("productImageDirectory") + id.ToString() + "//";
                        (new System.IO.DirectoryInfo(Server.MapPath("~" + imageDirectoryPath))).Create();
                        var path = Path.Combine(Server.MapPath("~" + imageDirectoryPath), fileName);
                        file.SaveAs(path);

                        tbl_Product_Images pImg = new tbl_Product_Images();
                        pImg.Caption = Caption;
                        pImg.IsDefault = !(IsDefault == null);
                        pImg.Remarks = Remarks;
                        pImg.ImagePath = imageDirectoryPath + fileName;
                        pImg.IsActive = true;
                        pro.tbl_Product_Images.Add(pImg);
                        fileName = null;
                        imageDirectoryPath = null;
                        path = null;

                        pImg = null;
                    }
                    file = null;
                    db.SaveChanges();
                    pro = db.tbl_Products.FirstOrDefault(p => p.ID == id);
                }
                files = null;
            }
            catch (Exception ex)
            {
            }
            return (new ProductImagesController().GetProductImageListByProductID(id));
            //return PartialView("_productImageList", pro.tbl_Product_Images.Where(pi => pi.IsActive).OrderBy(pi => pi.IsDefault));
        }

        [HttpPost]
        public ActionResult ImageInsertx(long id, string Caption, bool IsDefault, string Remarks)
        {
            var pro = db.tbl_Products.FirstOrDefault(p => p.ID == id);
            try
            {
                HttpFileCollectionBase files = Request.Files;
                if (files != null && files.Count > 0)
                {

                    var file = files[0];
                    if (file != null && file.ContentLength > 0)
                    {

                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // TODO: need to define destination
                        string imageDirectoryPath = WebConfigurationManager.AppSettings.Get("productImageDirectory") + id.ToString() + "//";
                        (new System.IO.DirectoryInfo(Server.MapPath("~" + imageDirectoryPath))).Create();
                        var path = Path.Combine(Server.MapPath("~" + imageDirectoryPath), fileName);
                        file.SaveAs(path);

                        tbl_Product_Images pImg = new tbl_Product_Images();
                        pImg.Caption = Caption;
                        pImg.IsDefault = IsDefault;
                        pImg.Remarks = Remarks;
                        pImg.ImagePath = imageDirectoryPath + fileName;
                        pImg.IsActive = true;
                        pro.tbl_Product_Images.Add(pImg);
                        fileName = null;
                        imageDirectoryPath = null;
                        path = null;

                        pImg = null;
                    }
                    file = null;
                    db.SaveChanges();
                    pro = db.tbl_Products.FirstOrDefault(p => p.ID == id);
                }
                files = null;
            }
            catch (Exception ex)
            {
            }
            return (new ProductImagesController().GetProductImageListByProductID(id));
        }

        [HttpDelete]
        public ActionResult ImageDeletex(long id)
        {
            tbl_Product_Images tp = null;
            try
            {
                var productImages = db.tbl_Product_Images.Where(p => p.ID == id);
                if (productImages != null)
                {

                    tp = productImages.FirstOrDefault();
                    db.tbl_Product_Images.Remove(tp);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            return (new ProductImagesController().GetProductImageListByProductID(tp.ProductID));
        }
        [HttpPut]
        public ActionResult SetDefaultImage(long id)
        {
            tbl_Product_Images tp = null;
            try
            {
                var productImages = db.tbl_Product_Images.Where(p => p.ID == id);
                if (productImages != null)
                {
                    tp = productImages.FirstOrDefault();
                    foreach (var item in db.tbl_Product_Images.Where(pi => pi.ProductID == tp.ProductID))
                    {
                        item.IsDefault = false;
                    }
                    tp.IsDefault = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            return (new ProductImagesController().GetProductImageListByProductID(tp.ProductID));
        }
        //[HttpPost]
        //public ActionResult UploadFiles(long tid, IEnumerable<HttpPostedFileBase> files)
        //{
        //    if (!System.IO.Directory.Exists(Server.MapPath("~\\" + TempPath+"\\"+tid.ToString())))
        //    {
        //        System.IO.Directory.CreateDirectory(Server.MapPath("~\\" + TempPath));
        //    }
        //    foreach (HttpPostedFileBase file in files)
        //    {
        //        string filePath = Path.Combine(Server.MapPath("~\\" + TempPath), file.FileName);
        //        System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));
        //        db.tbl_Product_Images.AddObject(new tbl_Product_Images() { 
        //         Caption =string.Empty ,
        //         ImagePath = TempPath + "\\" + tid.ToString(),
        //          ProductID =
        //        });
        //    }

        //    return Json("All files have been successfully stored.");
        //}

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }


        [HttpGet]
        public ActionResult GetProductImageListByProductID(long ProductID)
        {
            return PartialView("_productImageList", db.tbl_Product_Images.Where(p => p.ProductID == ProductID));
        }
    }
}
