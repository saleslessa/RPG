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
            CharacterModel CharacterModel = db.Characters.Find(id);
            if (CharacterModel == null)
            {
                return HttpNotFound();
            }
            return View(CharacterModel);
        }

        //
        // GET: /Character/Create
        public ActionResult Create()
        {
            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            ViewBag.display = "none";
            ViewBag.isRegistered = false;

            CampaignController c = new CampaignController();

            List<AvailableCampaignsModel> available = c.ListAvailableCampaigns();

            ViewBag.campaigns = new SelectList(available, "id", "name");

            return View();
        }

        private void FillModelCreate(ref CharacterModel charactermodel)
        {
            charactermodel.remainingPoints = charactermodel.pointsToDistribute;
            charactermodel.remainingLife = charactermodel.maxLife;

            string log = Session["LoggedUser"].ToString();

            int idUser = db.UserProfiles
           .Where(w => w.UserName == log)
           .Select(s => s.UserId)
           .FirstOrDefault();

            //charactermodel.idUser = idUser;
        }

        //
        // POST: /Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CharacterModel CharacterModel)
        {
            if (Session["LoggedUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                FillModelCreate(ref CharacterModel);

                if (ModelState.IsValid)
                {
                    db.Characters.Add(CharacterModel);
                    db.SaveChanges();

                    Session["idCharacter"] = CharacterModel.idCharacter;
                    ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>(), CharacterModel.gender);
                    ViewBag.display = "normal";
                    ViewBag.Message = "Character Created. Please select the attributes at right side";

                    return View(CharacterModel);
                }

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

            return View(CharacterModel);
        }

        //
        // GET: /Character/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CharacterModel CharacterModel = db.Characters.Find(id);
            if (CharacterModel == null)
            {
                return HttpNotFound();
            }
            return View(CharacterModel);
        }

        //
        // POST: /Character/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CharacterModel CharacterModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(CharacterModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CharacterModel);
        }

        //
        // GET: /Character/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CharacterModel CharacterModel = db.Characters.Find(id);
            if (CharacterModel == null)
            {
                return HttpNotFound();
            }
            return View(CharacterModel);
        }

        //
        // POST: /Character/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterModel CharacterModel = db.Characters.Find(id);
            db.Characters.Remove(CharacterModel);
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