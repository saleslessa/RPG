using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Models;
using System.Transactions;
using System.Net;

namespace DaemonCharacter.Controllers
{
    public class CharacterController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();
        private string loggedUser;


        private string GetLoggedUser()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                    return User.Identity.Name;

                throw new Exception("User not logged");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        private void PrepareScreenCreate()
        {
            ViewBag.Genders = new SelectList(Enum.GetValues(typeof(Genders)).Cast<Genders>());
            ViewBag.display = "none";

            ViewBag.Races = new SelectList(Enum.GetValues(typeof(Races)).Cast<Races>());
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(NonPlayerTypes)).Cast<NonPlayerTypes>());
        }

        public ActionResult CreateNPC()
        {
            try
            {
                GetLoggedUser();
                PrepareScreenCreate();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateNPC(NonPlayerModel NonPlayer, FormCollection f)
        {
            try
            {
                this.loggedUser = User.Identity.Name;

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
            catch (Exception)
            {
                return Json(HttpStatusCode.Forbidden, JsonRequestBehavior.DenyGet);
            }
        }

        //
        // GET: /Character/Create
        public ActionResult CreatePlayer()
        {

            PrepareScreenCreate();
            CreateSelectListAvailableCampaigns();

            return View();
        }

        private void CreateSelectListAvailableCampaigns(string selected = "")
        {
            CampaignController c = new CampaignController();
            List<AvailableCampaignsModel> available = c.GetAvailableCampaigns();
            ViewBag.campaigns = new SelectList(available, "id", "name", selected);
        }

        private void FillProperties<T>(ref T obj, FormCollection f)
        {
            try
            {

                (obj as CharacterModel).user = db.UserProfiles
                           .Where(w => w.UserName == this.loggedUser)
                           .FirstOrDefault();

                (obj as CharacterModel).race = (Races)Enum.Parse(typeof(Races), (((object[])f.GetValue("races").RawValue)[0]).ToString(), true);

                (obj as CharacterModel).gender = (Genders)Enum.Parse(typeof(Genders), (((object[])f.GetValue("genders").RawValue)[0]).ToString(), true);

                if (typeof(T) == typeof(PlayerModel))
                {
                    (obj as PlayerModel).remainingPoints = (obj as PlayerModel).pointsToDistribute;
                    (obj as PlayerModel).remainingLife = (obj as PlayerModel).maxLife;
                    (obj as PlayerModel).campaign = db.Campaigns.Find(Convert.ToInt32(((string[])f.GetValue("campaigns").RawValue)[0]));

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
        public JsonResult CreatePlayer(PlayerModel player, FormCollection f)
        {
            this.loggedUser = User.Identity.Name;
            FillProperties(ref player, f);

            if (ModelState.IsValid)
            {
                db.Characters.Add(player);
                db.SaveChanges();

                Session["idCharacter"] = player.id;

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

            ViewBag.Races = new SelectList(Enum.GetValues(typeof(Races)).Cast<Races>(), CharacterModel.race);

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
        public ActionResult Edit(CharacterModel CharacterModel, FormCollection f)
        {
            this.loggedUser = User.Identity.Name;
            if (ModelState.IsValid)
            {
                CharacterModel.race = (Races)Enum.Parse(typeof(Races), (((object[])f.GetValue("races").RawValue)[0]).ToString(), true);
                CharacterModel.gender = (Genders)Enum.Parse(typeof(Genders), (((object[])f.GetValue("genders").RawValue)[0]).ToString(), true);


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

        public ActionResult _ListNPCForSession()
        {
            return View(db.NonPlayers.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}