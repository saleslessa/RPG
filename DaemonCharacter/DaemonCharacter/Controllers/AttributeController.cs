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
    public class AttributeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /Attribute/

        public ActionResult Index()
        {
            return View(db.Attributes.ToList());
        }

        public ActionResult ListBonus(int id = -1)
        {

            ViewBag.selected = GetBonusAttribute(id);

            IEnumerable<AttributeClass> result = db.Attributes.Where(t => t.type.useBonus == true).ToList();

            return View(result);

        }

        private List<int> GetBonusAttribute(int id)
        {
            if (id != -1)
                return db.AttributeBonus.Where(t => t.idAttribute == id)
                    .Select(s => s.idAttributeBonusClass).ToList();
            else
                return new List<int>() { id };
        }
        //
        // GET: /Attribute/Details/5

        public ActionResult Details(int id = 0)
        {
            AttributeClass attributeclass = db.Attributes.Find(id);
            if (attributeclass == null)
            {
                return HttpNotFound();
            }
            return View(attributeclass);
        }

        //
        // GET: /Attribute/Create
        public ActionResult Create()
        {
            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "idAttributeType", "name");
            return View();
        }

        private ArrayList SelectAttributeBonus(FormCollection f)
        {
            ArrayList Ret = new ArrayList();

            try
            {
                for (int i = 0; i < f.Keys.Count; i++)
                {
                    if (f.Keys[i].Split('_')[0] == "Bonus")
                        if ((((string[])f.GetValue(f.Keys[i]).RawValue)[0]) == "true")
                            Ret.Add(Convert.ToInt32(f.Keys[i].Split('_')[1]));

                }

                return Ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveAttribute(ref AttributeClass Attribute)
        {
            Attribute.type = db.AttributeTypes.Find(Attribute.idAttributeType);
            db.Attributes.Add(Attribute);
            db.SaveChanges();
        }

        private void ValidateDuplicateBonus(AttributeBonusClass a)
        {
            try
            {
                List<AttributeBonusClass> result = db.AttributeBonus
                    .Where(t => t.idAttribute == a.idAttributeBonusClass && t.idAttributeBonusClass == a.idAttribute)
                    .ToList();

                if (result.Count > 0)
                    throw new Exception("You cannot use cyclic bonus");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveAttributeBonus(int idAttribute, ArrayList Bonus)
        {
            try
            {
                List<AttributeBonusClass> attNot = db.AttributeBonus
                .Where(t => t.idAttribute == idAttribute).ToList();

                foreach (AttributeBonusClass item in attNot)
                    db.AttributeBonus.Remove(item);

                db.SaveChanges();

                for (int i = 0; i < Bonus.Count; i++)
                {
                    AttributeBonusClass a = new AttributeBonusClass();
                    a.idAttribute = idAttribute;
                    a.idAttributeBonusClass = Convert.ToInt32(Bonus[i]);

                    ValidateDuplicateBonus(a);

                    db.AttributeBonus.Add(a);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private AttributeTypeClass ValidateAttributeType(int idAttributeType)
        {
            try
            {
                if (idAttributeType == 0)
                    throw new Exception("Invalid Attribute Type");

                return db.AttributeTypes.Find(idAttributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // POST: /Attribute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttributeClass Attribute, FormCollection f)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        ValidateAttributeType(Attribute.idAttributeType);

                        SaveAttribute(ref Attribute);
                        SaveAttributeBonus(Attribute.idAttribute, SelectAttributeBonus(f));

                        scope.Complete();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Retorno = "An error occured while trying to create this attribute: " + ex.Message;
                    ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "idAttributeType", "name");
                    return View(Attribute);
                }

            }

            ReturnErrorModelState(ModelState);

            return View(Attribute);
        }

        //
        // GET: /Attribute/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AttributeClass attributeclass = db.Attributes.Find(id);
            if (attributeclass == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "idAttributeType", "name", attributeclass.idAttributeType);
            return View(attributeclass);
        }

        //
        // POST: /Attribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeClass Att, FormCollection f)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    AttributeTypeClass a = new AttributeTypeClass();

                    a.idAttributeType = Convert.ToInt32(((string[])f.GetValue("idAttributeType").RawValue)[0].ToString());

                    a = ValidateAttributeType(a.idAttributeType);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        EditAttribute(Att, a);
                        SaveAttributeBonus(Att.idAttribute, SelectAttributeBonus(f));

                        scope.Complete();
                    }

                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "idAttributeType", "name", Att.idAttributeType);
                ViewBag.Message = ex.Message;
                return View(Att);
            }
            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "idAttributeType", "name", Att.idAttributeType);
            ReturnErrorModelState(ModelState);
            return View(Att);


        }

        private void ReturnErrorModelState(ModelStateDictionary ModelState)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "idAttributeType", "name");
            ViewBag.message = "The following errors occured while trying to create this Attribute:\n";

            for (int i = 0; i < errors.ToList().Count; i++)
            {
                ViewBag.message += errors.ToList()[i].ErrorMessage + "\n";
            }
        }

        private void EditAttribute(AttributeClass Att, AttributeTypeClass type)
        {
            Att.type = type;
            Att.idAttributeType = type.idAttributeType;
            db.Entry(Att).State = EntityState.Modified;
            db.SaveChanges();
        }

        //
        // GET: /Attribute/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AttributeClass attributeclass = db.Attributes.Find(id);
            if (attributeclass == null)
            {
                return HttpNotFound();
            }
            return View(attributeclass);
        }

        //
        // POST: /Attribute/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttributeClass attributeclass = db.Attributes.Find(id);
            db.Attributes.Remove(attributeclass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Method responsible for retrieve the list of associated attributes of a character
        /// </summary>
        /// <param name="idCharacter">Edited character</param>
        /// <returns>Returns a partial view</returns>
        public ActionResult ListAttributesFromCharacter(int idCharacter = 0)
        {
            IEnumerable<AttributeClass> attributes;
            //idCharacter = 0 is a new Character being created. None attribute exist yet.
            if (idCharacter == 0)
            {
                return HttpNotFound();
            }
            //idCharacter != 0 is a Character being edited
            else
            {
                CharacterClass character = db.Characters.Find(idCharacter);
                if (character == null)
                {
                    return HttpNotFound();
                }
                attributes = db.Attributes.Where(t => character.attributes.Any(c => c.idAttribute == t.idAttribute)).OrderBy(t => t.type.name);
            }

            return PartialView(attributes);
        }

        public JsonResult FindMinimum(int id=-1)
        {
            if (id == -1)
                return Json(HttpNotFound());

            return Json(db.Attributes.Find(id).minimum, JsonRequestBehavior.AllowGet);

        }

    }
}