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


        public JsonResult Create(string Attributes)
        {
            ArrayList listOfAttributes = new ArrayList();
            try
            {
                if (Session["idCharacter"] == null)
                    throw new Exception("Character Not Found");

                string idCharacter = Session["idCharacter"].ToString();

                for (int i = 0; i < Attributes.Split(',').Length; i++)
                {
                    listOfAttributes.Add(ValidateAttributeFromCharacterModel(Attributes.ToString().Split(',')[i]));
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    ValidateSumAttributes(listOfAttributes);

                    PersistCharacterAttributes(idCharacter, listOfAttributes);

                    scope.Complete();
                }

                return Json("Attributes Associated to new character", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("The following error occured when trying to associate a attribute to character: " + ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        private void ValidateSumAttributes(ArrayList attributes)
        {
            try
            {
                CharacterModel c = db.Characters.Find(Session["idCharacter"]);
                int total = 0;

                foreach (var item in attributes)
                    total += ((CharacterAttributeModel)item).value;

                if (total > c.pointsToDistribute)
                    throw new Exception("You used more points than permited. Please reorganize your points");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private int GetNumber(string val)
        {
            string returning = string.Empty;
            try
            {
                for (int i = 0; i < val.Length; i++)
                {
                    if (Char.IsDigit(val[i]))
                    {
                        returning += val[i].ToString();
                    }
                }
                return Convert.ToInt32(returning);
            }
            catch (Exception)
            {
                throw new Exception("invalid number");
            }
            finally
            {
                if (returning == string.Empty)
                    throw new Exception();
            }
        }

        private void PersistCharacterAttributes(string idCharacter, ArrayList listOfAttributes)
        {
            try
            {
                CharacterModel c = ValidateCharacter(idCharacter);

                ValidateCharacterAttributeBeforePersist(ReturnCompleteCharacterAttribute(c.idCharacter, listOfAttributes));

                foreach (CharacterAttributeModel item in listOfAttributes)
                    SaveCharacterAttributes(c, item);

                EditRemainingPointToDistribute(c, listOfAttributes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void EditRemainingPointToDistribute(CharacterModel c, ArrayList listOfAttributes)
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

        private void ValidateCharacterAttributeBeforePersist(ArrayList listOfAttributes)
        {
            try
            {
                foreach (CharacterAttributeModel item in listOfAttributes)
                {
                    if (db.CharacterAttributes
                        .Where(t => t.idCharacter == item.idCharacter && t.idAttribute == item.idAttribute)
                        .ToList().Count > 0)
                    {
                        db.CharacterAttributes.Remove(item);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ArrayList ReturnCompleteCharacterAttribute(int idCharacter, ArrayList listOfAttributes)
        {
            for (int i = 0; i < listOfAttributes.Count; i++)
            {
                ((CharacterAttributeModel)listOfAttributes[i]).idCharacter = idCharacter;
            }
            return listOfAttributes;
        }

        private void SaveCharacterAttributes(CharacterModel c, CharacterAttributeModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    item.character = c;
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

        private CharacterModel ValidateCharacter(string idCharacter)
        {
            try
            {
                CharacterModel c = db.Characters.Find(Convert.ToInt32(idCharacter));

                if (c == null)
                    throw new Exception("Invalid Character to input attributes. Please try again");

                return c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CharacterAttributeModel ValidateAttributeFromCharacterModel(string attribute)
        {
            try
            {
                CharacterAttributeModel returning = new CharacterAttributeModel();
                returning.attribute = db.Attributes.Find(GetNumber(attribute.Split('|')[0]));
                returning.value = GetNumber(attribute.Split('|')[1]);
                return returning;
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

        public ActionResult ListNonCharacter()
        {
            IEnumerable<AttributeModel> attributes;

            attributes = db.Attributes.ToList().OrderBy(t => t.type.name);

            return PartialView(attributes);
        }

        public ActionResult ListCharacterWithBonus(int idCharacter)
        {
            return PartialView(GetCharacterAttributeWithBonus(idCharacter));
        }

        public ActionResult ListCharacterWithNoBonus(int idCharacter)
        {
            return PartialView(GetCharacterAttributeWithNoBonus(idCharacter));
        }


        private IEnumerable<CharacterAttributeModel> GetCharacterAttributeWithBonus(int idCharacter)
        {
            IEnumerable<CharacterAttributeModel> characterAttribute;

            characterAttribute = db.CharacterAttributes
                .Where(t => t.idCharacter == idCharacter && t.attribute.type.useBonus == true)
                .ToList();

            foreach (CharacterAttributeModel item in characterAttribute)
            {
                item.bonusValues = LoadBonusValues(characterAttribute, item);
            }

            return characterAttribute;
        }

        private IEnumerable<CharacterAttributeModel> GetCharacterAttributeWithNoBonus(int idCharacter)
        {
            IEnumerable<CharacterAttributeModel> characterAttribute;

            characterAttribute = db.CharacterAttributes
                .Where(t => t.idCharacter == idCharacter && t.attribute.type.useBonus == false)
                .ToList();

            return characterAttribute;
        }

        private List<CharacterAttributeModel> LoadBonusValues(IEnumerable<CharacterAttributeModel> characterAttribute, CharacterAttributeModel c)
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