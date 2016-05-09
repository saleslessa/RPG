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
    public class CharacterController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /Character/

        public ActionResult Index()
        {
            return View(db.Characters.ToList());
        }

        //
        // GET: /Character/Details/5
        public ActionResult Details(int id = 0)
        {
            CharacterClass characterclass = db.Characters.Find(id);
            if (characterclass == null)
            {
                return HttpNotFound();
            }
            return View(characterclass);
        }

        //
        // GET: /Character/Create
        public ActionResult Create()
        {
            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            ViewBag.display = "none";
            ViewBag.isRegistered = false;

            return View();
        }

        //
        // POST: /Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CharacterClass characterclass)
        {
            characterclass.remainingPoints = characterclass.pointsToDistribute;
            characterclass.remainingLife = characterclass.maxLife;

            if (ModelState.IsValid)
            {
                db.Characters.Add(characterclass);
                db.SaveChanges();

                Session["idCharacter"] = characterclass.idCharacter;
                ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>(), characterclass.gender);
                ViewBag.display = "normal";
                ViewBag.Message = "Character Created. Please select the attributes at right side";

                return View(characterclass);
            }


            //When an error accours
            ViewBag.isRegistered = false;
            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            ViewBag.Display = "none";
            ViewBag.Message = "The following error occured when trying to create a character:\n";

            foreach (ModelState states in ViewData.ModelState.Values)
            {
                foreach (ModelError errors in states.Errors)
                {
                    ViewBag.Message += errors.ErrorMessage + "\n";
                }
            }

            return View(characterclass);
        }

        //
        // GET: /Character/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CharacterClass characterclass = db.Characters.Find(id);
            if (characterclass == null)
            {
                return HttpNotFound();
            }
            return View(characterclass);
        }

        //
        // POST: /Character/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CharacterClass characterclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characterclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(characterclass);
        }

        //
        // GET: /Character/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CharacterClass characterclass = db.Characters.Find(id);
            if (characterclass == null)
            {
                return HttpNotFound();
            }
            return View(characterclass);
        }

        //
        // POST: /Character/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterClass characterclass = db.Characters.Find(id);
            db.Characters.Remove(characterclass);
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