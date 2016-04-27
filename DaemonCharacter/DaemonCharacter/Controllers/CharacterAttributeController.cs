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
    public class CharacterAttributeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /CharacterAttribute/

        public ActionResult Index()
        {
            var characterattributes = db.CharacterAttributes.Include(c => c.attribute);
            return View(characterattributes.ToList());
        }

        //
        // GET: /CharacterAttribute/Details/5

        public ActionResult Details(int id = 0)
        {
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            if (characterattributeclass == null)
            {
                return HttpNotFound();
            }
            return View(characterattributeclass);
        }

        //
        // GET: /CharacterAttribute/Create

        public ActionResult _Create()
        {
            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name");
            return View();
        }

        //
        // POST: /CharacterAttribute/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(CharacterAttributeClass characterattributeclass)
        {
            if (ModelState.IsValid)
            {
                db.CharacterAttributes.Add(characterattributeclass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterattributeclass.idAttribute);
            return View(characterattributeclass);
        }

        //
        // GET: /CharacterAttribute/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            if (characterattributeclass == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterattributeclass.idAttribute);
            return View(characterattributeclass);
        }

        //
        // POST: /CharacterAttribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CharacterAttributeClass characterattributeclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characterattributeclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterattributeclass.idAttribute);
            return View(characterattributeclass);
        }

        //
        // GET: /CharacterAttribute/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            if (characterattributeclass == null)
            {
                return HttpNotFound();
            }
            return View(characterattributeclass);
        }

        //
        // POST: /CharacterAttribute/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            db.CharacterAttributes.Remove(characterattributeclass);
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