using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaemonCharacter.Models;

namespace DaemonCharacter.Controllers
{
    public class AttributeTypeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /AttributeType/

        public ActionResult Index()
        {
            return View(db.AttributeTypes.ToList());
        }

        //
        // GET: /AttributeType/Details/5

        public ActionResult Details(int id = 0)
        {
            AttributeType attributetype = db.AttributeTypes.Find(id);
            if (attributetype == null)
            {
                return HttpNotFound();
            }
            return View(attributetype);
        }

        //
        // GET: /AttributeType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AttributeType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttributeType attributetype)
        {
            if (ModelState.IsValid)
            {
                db.AttributeTypes.Add(attributetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attributetype);
        }

        //
        // GET: /AttributeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AttributeType attributetype = db.AttributeTypes.Find(id);
            if (attributetype == null)
            {
                return HttpNotFound();
            }
            return View(attributetype);
        }

        //
        // POST: /AttributeType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeType attributetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attributetype);
        }

        //
        // GET: /AttributeType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AttributeType attributetype = db.AttributeTypes.Find(id);
            if (attributetype == null)
            {
                return HttpNotFound();
            }
            return View(attributetype);
        }

        //
        // POST: /AttributeType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttributeType attributetype = db.AttributeTypes.Find(id);
            db.AttributeTypes.Remove(attributetype);
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