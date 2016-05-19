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
    public class RaceController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /Race/

        public ActionResult Index()
        {
            return View(db.RaceModels.ToList());
        }

        //
        // GET: /Race/Details/5

        public ActionResult Details(int id = 0)
        {
            RaceModel racemodel = db.RaceModels.Find(id);
            if (racemodel == null)
            {
                return HttpNotFound();
            }
            return View(racemodel);
        }

        //
        // GET: /Race/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Race/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RaceModel racemodel)
        {
            if (ModelState.IsValid)
            {
                db.RaceModels.Add(racemodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(racemodel);
        }

        //
        // GET: /Race/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RaceModel racemodel = db.RaceModels.Find(id);
            if (racemodel == null)
            {
                return HttpNotFound();
            }
            return View(racemodel);
        }

        //
        // POST: /Race/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RaceModel racemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(racemodel);
        }

        //
        // GET: /Race/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RaceModel racemodel = db.RaceModels.Find(id);
            if (racemodel == null)
            {
                return HttpNotFound();
            }
            return View(racemodel);
        }

        //
        // POST: /Race/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RaceModel racemodel = db.RaceModels.Find(id);
            db.RaceModels.Remove(racemodel);
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