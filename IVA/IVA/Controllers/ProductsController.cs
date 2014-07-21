using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVA.Models;

namespace IVA.Controllers
{
    public class ProductsController : Controller
    {
        private IVADBEntities db = new IVADBEntities();

        //
        // GET: /Products/

        public ActionResult Index()
        {
           
            return View(db.tbl_Products.ToList());
        }

        //
        // GET: /Products/Details/5

        public ActionResult Details(long tid = 0)
        {
            tbl_Products tbl_products = db.tbl_Products.Single(t => t.ID == tid);
            if (tbl_products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_products);
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
        public ActionResult Create(tbl_Products tbl_products)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Products.Add(tbl_products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_products);
        }

        //
        // GET: /Products/Edit/5

        public ActionResult Edit(long tid = 0)
        {
            tbl_Products tbl_products = db.tbl_Products.Single(t => t.ID == tid);
            if (tbl_products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_Specs = tbl_products.tbl_Product_Specs.ToList();
            return View(tbl_products);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_Products tbl_products)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(tbl_products).State = EntityState.Modified;
              //  db.ObjectStateManager.ChangeObjectState(tbl_products, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_products);
        }

        //
        // GET: /Products/Delete/5

        public ActionResult Delete(long tid = 0)
        {
            tbl_Products tbl_products = db.tbl_Products.Single(t => t.ID == tid);
            if (tbl_products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_products);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long tid)
        {
            tbl_Products tbl_products = db.tbl_Products.Single(t => t.ID == tid);
          //  db.tbl_Products.DeleteObject(tbl_products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateDescription(long id, String Remarks)
        {
            tbl_Products tbl_products = db.tbl_Products.Single(t => t.ID == id);
            if (tbl_products == null)
            {
                return HttpNotFound();
            }
            tbl_products.Remarks = Remarks;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateSpecs(long id)
        {
            foreach (var item in db.tbl_Product_Specs.Where(spec => spec.ProductID == id))
            {
               // db.tbl_Product_Specs.DeleteObject(item);
            }
            foreach (var Spec in (new IVA.Models.IVADBEntities()).tbl_Specs)
            {
                db.tbl_Product_Specs.Add(new tbl_Product_Specs()
                {
                    IsActive = true,
                    ProductID = id,
                    Remarks = string.Empty,
                    SpecID = Spec.ID,
                    Value = Request["spec" + @Spec.Name]
                });
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}