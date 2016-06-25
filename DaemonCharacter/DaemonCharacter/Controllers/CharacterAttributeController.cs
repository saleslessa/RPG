using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Models;
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

                        CharacterAttributeModel c = new CharacterAttributeModel();

                        c.character = db.Characters.Find((int)Session["idCharacter"]);
                        c.attribute = db.Attributes.Find(Convert.ToInt32(f.Keys[i].Split('_')[1]));
                        c.value = Convert.ToInt32(((string[])f.GetValue("ValAttr_" + f.Keys[i].Split('_')[1].ToString()).RawValue)[0]);

                        arr.Add(c);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object ValidateCharacterBeforeCreate<T>(string model)
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

                var obj = ValidateCharacterBeforeCreate<object>(((string[])f.GetValue("model").RawValue)[0]);

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
       

        private CharacterModel GetCharacter(int id)
        {
            CharacterModel c = db.Characters.Find(id);

            try
            {
                if (c == null)
                    throw new Exception("Player not found");

                return c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateSumAttributes(List<CharacterAttributeModel> attributes)
        {
            try
            {
                PlayerModel c = (GetCharacter((int)Session["idCharacter"]) as PlayerModel);
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
                {
                    item.character = (obj as CharacterModel);
                    SaveCharacterAttributes(item);
                }

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
                CharacterModel c = db.Characters.Find(listOfAttributes[0].attribute.id);

                foreach (CharacterAttributeModel item in listOfAttributes)
                {

                    List<CharacterAttributeModel> list = db.CharacterAttributes
                        .Where(t => t.character.id == item.character.id && t.attribute.id == item.attribute.id)
                        .ToList();

                    if (list != null && list.Count > 0)
                    {
                        db.CharacterAttributes.Remove(item);
                        db.SaveChanges();
                    }
                    else
                    {
                        item.attribute = db.Attributes.Find(item.attribute.id);
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
                    db.CharacterAttributes.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object ValidateCharacter<T>(string model, int id)
        {
            try
            {
                switch (model)
                {
                    case "PlayerModel":
                        return (GetCharacter(id) as PlayerModel);

                    case "NonPlayerModel":
                        return (db.NonPlayers.Find(id) as NonPlayerModel);

                    default:
                        throw new Exception("model doesn't exist");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        protected internal void DeleteCharacter(int idCharacter)
        {
            try
            {
                List<CharacterAttributeModel> list = db.CharacterAttributes.Where(t => t.character.id == idCharacter).ToList();

                if (list == null)
                    throw new Exception("Character inexistent to delete attributes");

                foreach (var item in list)
                {
                    db.CharacterAttributes.Remove(item);
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult _ListNonCharacter(string model)
        {
            IEnumerable<AttributeModel> attributes;

            attributes = db.Attributes.ToList().OrderBy(t => t.type);
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
                .Where(t => t.character.id == id && t.attribute.type == AttributeType.Characteristic)
                .ToList();

            foreach (CharacterAttributeModel item in characterAttribute)
            {
                //item.bonusValues = LoadBonusValues(characterAttribute, item);
            }

            return characterAttribute;
        }

      
        private IEnumerable<CharacterAttributeModel> GetCharacterAttributeWithNoBonus(int idPerson)
        {
            IEnumerable<CharacterAttributeModel> characterAttribute;

            characterAttribute = db.CharacterAttributes
                .Where(t => t.character.id == idPerson && t.attribute.type != AttributeType.Characteristic)
                .ToList();

            return characterAttribute;
        }

        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}