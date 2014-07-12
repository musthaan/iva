using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVA.Models;

namespace HonyeSinga.Controllers
{
    public class SpecItemsController : Controller
    {
        private IVADBEntities db = new IVADBEntities();

        //
        // GET: /SpecItems/

        public ActionResult Index()
        {
            var tbl_spec_items = db.tbl_Spec_items.Include("tbl_Specs");
            return View(tbl_spec_items.ToList());
        }
        //
        // GET: /SpecItems/2
        public ActionResult List(long  tid)
        {
            ViewBag.SpecID = tid;
            var tbl_spec_items = db.tbl_Spec_items.Where(spec => spec.SpecID == tid).ToList();             
            return View(tbl_spec_items.ToList());
        }

        //
        // GET: /SpecItems/Details/5

        public ActionResult Details(long tid = 0)
        {
            tbl_Spec_items tbl_spec_items = db.tbl_Spec_items.Single(t => t.ID == tid);
            if (tbl_spec_items == null)
            {
                return HttpNotFound();
            }
            return View(tbl_spec_items);
        }

        //
        // GET: /SpecItems/Create

        public ActionResult Create(long tid)
        {
            ViewBag.SpecID = tid;
            
            return View();
        }

        //
        // POST: /SpecItems/Create

        [HttpPost]
        public ActionResult Create(long tid,tbl_Spec_items tbl_spec_items)
        {
            if (ModelState.IsValid)
            {
                tbl_spec_items.SpecID = tid;
                tbl_spec_items.IsActive = true;
                db.tbl_Spec_items.Add(tbl_spec_items);
                db.SaveChanges();
                return RedirectToAction("List/"+tid);
            }

            ViewBag.SpecID = new SelectList(db.tbl_Specs, "ID", "Name", tbl_spec_items.SpecID);
            return View(tbl_spec_items);
        }

        //
        // GET: /SpecItems/Edit/5

        public ActionResult Edit(long tid = 0)
        {
            tbl_Spec_items tbl_spec_items = db.tbl_Spec_items.Single(t => t.ID == tid);
            if (tbl_spec_items == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecID = new SelectList(db.tbl_Specs, "ID", "Name", tbl_spec_items.SpecID);
            return View(tbl_spec_items);
        }

        //
        // POST: /SpecItems/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_Spec_items tbl_spec_items)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Spec_items.Attach(tbl_spec_items);
                db.Entry(tbl_spec_items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecID = new SelectList(db.tbl_Specs, "ID", "Name", tbl_spec_items.SpecID);
            return View(tbl_spec_items);
        }

        //
        // GET: /SpecItems/Delete/5

        public ActionResult Delete(long tid = 0)
        {
            tbl_Spec_items tbl_spec_items = db.tbl_Spec_items.Single(t => t.ID == tid);
            if (tbl_spec_items == null)
            {
                return HttpNotFound();
            }
            return View(tbl_spec_items);
        }

        //
        // POST: /SpecItems/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long tid)
        {
            tbl_Spec_items tbl_spec_items = db.tbl_Spec_items.Single(t => t.ID == tid);
          //  db.tbl_Spec_items.DeleteObject(tbl_spec_items);
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