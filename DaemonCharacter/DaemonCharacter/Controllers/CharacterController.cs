using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaemonCharacter.Models;
using System.Transactions;

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
        // GET: /Character/PlayerDetails/5
        public ActionResult PlayerDetails(int id = 0)
        {
            PlayerModel model = db.Players.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult NonPlayerDetails(int id = 0)
        {
            NonPlayerModel model = db.NonPlayers.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }



        private void PrepareCreateScreen()
        {
            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Genders)).Cast<Genders>());
            ViewBag.display = "none";
            ViewBag.isRegistered = false;

            ViewBag.Races = new SelectList(db.Races.ToList(), "id", "name");


            ViewBag.Types = new SelectList(Enum.GetValues(typeof(NonPlayerTypes)).Cast<NonPlayerTypes>());
        }

        public ActionResult CreateNPC()
        {
            PrepareCreateScreen();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateNPC(NonPlayerModel NonPlayer, FormCollection f)
        {


            FillProperties(ref NonPlayer, f);

            if (ModelState.IsValid)
            {
                db.NonPlayers.Add(NonPlayer);
                db.SaveChanges();

                Session["idCharacter"] = NonPlayer.id;

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

        //
        // GET: /Character/Create
        public ActionResult CreatePlayer()
        {
            PrepareCreateScreen();

            CreateSelectListAvailableCampaigns();

            return View();
        }

        private void CreateSelectListAvailableCampaigns(string selected = "")
        {
            CampaignController c = new CampaignController();
            List<AvailableCampaignsModel> available = c.ListAvailableCampaigns();
            ViewBag.campaigns = new SelectList(available, "id", "name", selected);
        }

        private void FillProperties<T>(ref T obj, FormCollection f)
        {

            try
            {

                string log = Session["LoggedUser"].ToString();

                (obj as CharacterModel).user = db.UserProfiles
                           .Where(w => w.UserName == log)
                           .FirstOrDefault();

                (obj as CharacterModel).race = db.Races.Find(Convert.ToInt32(((string[])f.GetValue("races").RawValue)[0]));

                (obj as CharacterModel).gender = (Genders)Enum.Parse(typeof(Genders), (((object[])f.GetValue("genders").RawValue)[0]).ToString(), true);



                if (typeof(T) == typeof(PlayerModel))
                {
                    (obj as PlayerModel).remainingPoints = (obj as PlayerModel).pointsToDistribute;
                    (obj as PlayerModel).remainingLife = (obj as PlayerModel).maxLife;
                    (obj as PlayerModel).campaign = db.CampaignModels.Find(Convert.ToInt32(((string[])f.GetValue("campaigns").RawValue)[0]));

                }
                if (typeof(T) == typeof(NonPlayerModel))
                {
                    (obj as NonPlayerModel).type = (NonPlayerTypes)Enum.Parse(typeof(NonPlayerTypes), (((object[])f.GetValue("types").RawValue)[0]).ToString(), true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //
        // POST: /Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreatePlayer(PlayerModel character, FormCollection f)
        {

            FillProperties(ref character, f);

            if (ModelState.IsValid)
            {
                db.Characters.Add(character);
                db.SaveChanges();

                Session["idCharacter"] = character.id;

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
            CharacterAttributeController ca = new CharacterAttributeController();

            try
            {
                
                using (TransactionScope scope = new TransactionScope())
                {
                    CharacterModel CharacterModel = db.Characters.Find(id);

                    ca.DeleteCharacter(id);

                    db.Characters.Remove(CharacterModel);
                    db.SaveChanges();

                    scope.Complete();
                }
                    
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}