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
    public class Default1Controller : Controller
    {
        private IVADBEntities db = new IVADBEntities();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.tbl_Control_Types.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(long id = 0)
        {
            tbl_Control_Types tbl_control_types = db.tbl_Control_Types.Find(id);
            if (tbl_control_types == null)
            {
                return HttpNotFound();
            }
            return View(tbl_control_types);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(tbl_Control_Types tbl_control_types)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Control_Types.Add(tbl_control_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_control_types);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(long id = 0)
        {
            tbl_Control_Types tbl_control_types = db.tbl_Control_Types.Find(id);
            if (tbl_control_types == null)
            {
                return HttpNotFound();
            }
            return View(tbl_control_types);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_Control_Types tbl_control_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_control_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_control_types);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(long id = 0)
        {
            tbl_Control_Types tbl_control_types = db.tbl_Control_Types.Find(id);
            if (tbl_control_types == null)
            {
                return HttpNotFound();
            }
            return View(tbl_control_types);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            tbl_Control_Types tbl_control_types = db.tbl_Control_Types.Find(id);
            db.tbl_Control_Types.Remove(tbl_control_types);
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