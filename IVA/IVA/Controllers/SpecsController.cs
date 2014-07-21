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
    public class SpecsController : Controller
    {
        private IVADBEntities db = new IVADBEntities();

        //
        // GET: /Specs/

        public ActionResult Index()
        {
            var tbl_specs = db.tbl_Specs.Include("tbl_Control_Types");
            return View(tbl_specs.ToList());
        }

        //
        // GET: /Specs/Details/5

        public ActionResult Details(long tid = 0)
        {
            tbl_Specs tbl_specs = db.tbl_Specs.Single(t => t.ID == tid);
            if (tbl_specs == null)
            {
                return HttpNotFound();
            }
            return View(tbl_specs);
        }

        //
        // GET: /Specs/Create

        public ActionResult Create()
        {
            ViewBag.ControlType = new SelectList(db.tbl_Control_Types, "ID", "Name");
            return View();
        }

        //
        // POST: /Specs/Create

        [HttpPost]
        public ActionResult Create(tbl_Specs tbl_specs)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Specs.Add(tbl_specs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ControlType = new SelectList(db.tbl_Control_Types, "ID", "Name", tbl_specs.ControlType);
            return View(tbl_specs);
        }

        //
        // GET: /Specs/Edit/5

        public ActionResult Edit(long tid = 0)
        {
            tbl_Specs tbl_specs = db.tbl_Specs.Single(t => t.ID == tid);
            if (tbl_specs == null)
            {
                return HttpNotFound();
            }
            ViewBag.ControlType = new SelectList(db.tbl_Control_Types, "ID", "Name", tbl_specs.ControlType);
            return View(tbl_specs);
        }

        //
        // POST: /Specs/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_Specs tbl_specs)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Specs.Attach(tbl_specs);
                db.Entry(tbl_specs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ControlType = new SelectList(db.tbl_Control_Types, "ID", "Name", tbl_specs.ControlType);
            return View(tbl_specs);
        }

        //
        // GET: /Specs/Delete/5

        public ActionResult Delete(long tid = 0)
        {
            tbl_Specs tbl_specs = db.tbl_Specs.Single(t => t.ID == tid);
            if (tbl_specs == null)
            {
                return HttpNotFound();
            }
            return View(tbl_specs);
        }

        //
        // POST: /Specs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long tid)
        {
            tbl_Specs tbl_specs = db.tbl_Specs.Single(t => t.ID == tid);
            db.tbl_Specs.Remove(tbl_specs);
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