using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Models;
using System.Collections;
using System.Transactions;

namespace DaemonCharacter.Controllers
{
    public class CharacterAttributeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();


        private void SelectAttributesFromSelectedCheckboxes(ref List<CharacterAttributeModel> arr, FormCollection f)
        {
            try
            {
                for (int i = 0; i < f.AllKeys.Length; i++)
                {
                    if (f.GetValue(f.Keys[i]) != null && f.Keys[i].Split('_')[0] == "ChkAttr")
                    {
                        arr.Add(new CharacterAttributeModel(
                            (int)Session["idCharacter"],
                            Convert.ToInt32(f.Keys[i].Split('_')[1]),
                            Convert.ToInt32(((string[])f.GetValue("ValAttr_" + f.Keys[i].Split('_')[1].ToString()).RawValue)[0])
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object ValidatePlayerBeforeCreate<T>(string model)
        {
            try
            {
                if (Session["idCharacter"] == null)
                    throw new Exception("Character Not Found");

                return ValidateCharacter<T>(model, Convert.ToInt32(Session["idCharacter"]));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult CreateCharacter(FormCollection f)
        {
            List<CharacterAttributeModel> listOfAttributes = new List<CharacterAttributeModel>();

            try
            {

                var obj = ValidatePlayerBeforeCreate<object>(((string[])f.GetValue("model").RawValue)[0]);

                SelectAttributesFromSelectedCheckboxes(ref listOfAttributes, f);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (typeof(PlayerModel) == obj.GetType().BaseType)
                    {
                        ValidateSumAttributes(listOfAttributes);
                        EditRemainingPointToDistribute((obj as PlayerModel), listOfAttributes);
                    }

                    PersistCharacterAttributes(obj, listOfAttributes);

                    scope.Complete();
                }

                return Json("Attributes Associated to new character", JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                return Json("The following error occured when trying to associate a attribute to character: " + ex.Message, JsonRequestBehavior.DenyGet);
            }

        }

        private void ValidateSumAttributes(List<CharacterAttributeModel> attributes)
        {
            try
            {
                PlayerModel c = db.Players.Find(Session["idCharacter"]);
                int total = 0;

                foreach (var item in attributes)
                    total += item.value;

                if (total > c.pointsToDistribute)
                    throw new Exception("You used more points than permited. Please reorganize your points");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void PersistCharacterAttributes<T>(T obj, List<CharacterAttributeModel> listOfAttributes)
        {
            try
            {

                ValidateCharacterAttributeBeforePersist(listOfAttributes);

                foreach (CharacterAttributeModel item in listOfAttributes)
                    SaveCharacterAttributes(item);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void EditRemainingPointToDistribute(PlayerModel c, List<CharacterAttributeModel> listOfAttributes)
        {
            try
            {
                int sumUsedPoints = 0;

                foreach (CharacterAttributeModel item in listOfAttributes)
                    sumUsedPoints += item.value;

                c.remainingPoints = c.pointsToDistribute - sumUsedPoints;

                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateCharacterAttributeBeforePersist(List<CharacterAttributeModel> listOfAttributes)
        {
            try
            {
                CharacterModel c = db.Characters.Find(listOfAttributes[0].idCharacter);

                foreach (CharacterAttributeModel item in listOfAttributes)
                {
                    if (db.CharacterAttributes
                        .Where(t => t.idCharacter == item.idCharacter && t.idAttribute == item.idAttribute)
                        .ToList().Count > 0)
                    {
                        db.CharacterAttributes.Remove(item);
                        db.SaveChanges();
                    }
                    else
                    {
                        item.attribute = db.Attributes.Find(item.idAttribute);
                        item.character = c;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveCharacterAttributes(CharacterAttributeModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item.bonusValues = new List<CharacterAttributeModel>();
                    db.CharacterAttributes.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private CharacterModel ValidateCharacter(string idCharacter)
        //{
        //    try
        //    {
        //        CharacterModel c = db.Characters.Find(Convert.ToInt32(idCharacter));

        //        if (c == null)
        //            throw new Exception("Invalid Character to input attributes. Please try again");

        //        return c;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private object ValidateCharacter<T>(string model, int id)
        {
            try
            {

                if (model == "PlayerModel")
                {
                    var result = db.Players.Find(id);

                    if (result == null)
                        throw new Exception("Invalid Player to input attributes. Please try again");

                    return result;
                }
                else if (model == "NonPlayerModel")
                {
                    var result = db.NonPlayers.Find(id);

                    if (result == null)
                        throw new Exception("Invalid Player to input attributes. Please try again");


                    return result;
                }
                else
                    throw new Exception("model doesn't exist");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // GET: /CharacterAttribute/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CharacterAttributeModel characterAttributeModel = db.CharacterAttributes.Find(id);
            if (characterAttributeModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterAttributeModel.idAttribute);
            return View(characterAttributeModel);
        }

        //
        // POST: /CharacterAttribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CharacterAttributeModel characterAttributeModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characterAttributeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterAttributeModel.idAttribute);
            return View(characterAttributeModel);
        }

        //
        // GET: /CharacterAttribute/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CharacterAttributeModel characterAttributeModel = db.CharacterAttributes.Find(id);
            if (characterAttributeModel == null)
            {
                return HttpNotFound();
            }
            return View(characterAttributeModel);
        }

        //
        // POST: /CharacterAttribute/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterAttributeModel characterAttributeModel = db.CharacterAttributes.Find(id);
            db.CharacterAttributes.Remove(characterAttributeModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ListNonCharacter(string model)
        {
            IEnumerable<AttributeModel> attributes;

            attributes = db.Attributes.ToList().OrderBy(t => t.type.name);
            ViewBag.model = model;

            return PartialView(attributes);
        }

        public ActionResult ListCharacterWithBonus(int id)
        {
            return PartialView(GetCharacterAttributeWithBonus(id));
        }

        public ActionResult ListCharacterWithNoBonus(int id)
        {
            return PartialView(GetCharacterAttributeWithNoBonus(id));
        }


        private IEnumerable<CharacterAttributeModel> GetCharacterAttributeWithBonus(int id)
        {
            List<CharacterAttributeModel> characterAttribute;

            characterAttribute = db.CharacterAttributes
                .Where(t => t.idCharacter == id && t.attribute.type.useBonus == true)
                .ToList();

            foreach (CharacterAttributeModel item in characterAttribute)
            {
                item.bonusValues = LoadBonusValues(characterAttribute, item);
            }

            return characterAttribute;
        }

        private IEnumerable<CharacterAttributeModel> GetCharacterAttributeWithNoBonus(int idPerson)
        {
            IEnumerable<CharacterAttributeModel> characterAttribute;

            characterAttribute = db.CharacterAttributes
                .Where(t => t.idCharacter == idPerson && t.attribute.type.useBonus == false)
                .ToList();

            return characterAttribute;
        }

        private List<CharacterAttributeModel> LoadBonusValues(List<CharacterAttributeModel> characterAttribute, CharacterAttributeModel c)
        {
            try
            {
                List<CharacterAttributeModel> result = new List<CharacterAttributeModel>();
                List<AttributeBonusModel> attributeBonus = new List<AttributeBonusModel>();
                attributeBonus = db.AttributeBonus.Where(t => c.idAttribute == t.idAttributeBonusClass).ToList();

                foreach (AttributeBonusModel subitem in attributeBonus)
                {
                    CharacterAttributeModel obj = characterAttribute.FirstOrDefault(t => t.idAttribute == subitem.idAttribute);

                    if (obj != null)
                    {
                        //if (obj.attribute.type.useModifier)
                        //    obj.value =  (obj.attribute.type.baseModifier - obj.value) / 2;

                        result.Add(obj);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}