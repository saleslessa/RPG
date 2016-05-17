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

            CreateSelectListAvailableCampaigns();

            return View();
        }

        private void CreateSelectListAvailableCampaigns(string selected = "")
        {
            CampaignController c = new CampaignController();
            List<AvailableCampaignsModel> available = c.ListAvailableCampaigns();
            ViewBag.campaigns = new SelectList(available, "id", "name", selected);
        }

        private void FillModelCreate(ref CharacterModel charactermodel, FormCollection f)
        {
            charactermodel.remainingPoints = charactermodel.pointsToDistribute;
            charactermodel.remainingLife = charactermodel.maxLife;

            string log = Session["LoggedUser"].ToString();

            charactermodel.idUser = db.UserProfiles
                       .Where(w => w.UserName == log)
                       .Select(s => s.UserId)
                       .FirstOrDefault();

            charactermodel.idCampaign = Convert.ToInt32(((string[])f.GetValue("campaigns").RawValue)[0]);

        }

        //
        // POST: /Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(CharacterModel CharacterModel, FormCollection f)
        {
            if (Session["LoggedUser"] == null)
            {
                return Json("User not logged", JsonRequestBehavior.DenyGet);
            }
            else
            {
               
                FillModelCreate(ref CharacterModel, f);

                if (ModelState.IsValid)
                {
                    db.Characters.Add(CharacterModel);
                    db.SaveChanges();

                    Session["idCharacter"] = CharacterModel.idCharacter;

                    return Json("Character Created. Please select the attributes below", JsonRequestBehavior.DenyGet);
                }

                var Message = "The following error occured when trying to create a character:\n";

                foreach (ModelState states in ViewData.ModelState.Values)
                {
                    foreach (ModelError errors in states.Errors)
                    {
                        Message += errors.ErrorMessage + "\n";
                    }
                }

                return Json(Message, JsonRequestBehavior.DenyGet);

            }

            
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