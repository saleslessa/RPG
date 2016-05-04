using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Models;
using System.Collections;


namespace DaemonCharacter.Controllers
{
    public class CharacterAttributeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /CharacterAttribute/

        public ActionResult Index()
        {
            var characterattributes = db.CharacterAttributes.Include(c => c.attribute);
            return View(characterattributes.ToList());
        }

        //
        // GET: /CharacterAttribute/Details/5

        public ActionResult Details(int id = 0)
        {
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            if (characterattributeclass == null)
            {
                return HttpNotFound();
            }
            return View(characterattributeclass);
        }

        //
        // GET: /CharacterAttribute/Create

        //public ActionResult Create()
        //{
        //    ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name");
        //    return View();
        //}

        //
        // POST: /CharacterAttribute/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CharacterAttributeClass characterattributeclass, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                db.CharacterAttributes.Add(characterattributeclass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterattributeclass.idAttribute);
            return View(characterattributeclass);
        }

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
                    listOfAttributes.Add(ValidateAttributeFromCharacterClass(Attributes.ToString().Split(',')[i]));
                }

                ValidateSumAttributes(listOfAttributes);

                PersistCharacterAttributes(idCharacter, listOfAttributes);

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
                CharacterClass c = db.Characters.Find(Session["idCharacter"]);
                int total = 0;

                foreach (var item in attributes)
                    total += ((CharacterAttributeClass)item).value;

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
                CharacterClass c = ValidateCharacter(idCharacter);


                ValidateCharacterAttributeBeforePersist(ReturnCompleteCharacterAttribute(c.idCharacter, listOfAttributes));

                foreach (CharacterAttributeClass item in listOfAttributes)
                    SaveCharacterAttributes(c, item);
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
                foreach (CharacterAttributeClass item in listOfAttributes)
                {
                    if (db.CharacterAttributes
                        .Where(t => t.idCharacter == item.idCharacter)
                        .Where(x => x.idAttribute == item.idAttribute).ToList().Count > 0)
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
                ((CharacterAttributeClass)listOfAttributes[i]).idCharacter = idCharacter;
            }
            return listOfAttributes;
        }

        private void SaveCharacterAttributes(CharacterClass c, CharacterAttributeClass item)
        {
            try
            {
                item.character = c;
                db.CharacterAttributes.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CharacterClass ValidateCharacter(string idCharacter)
        {
            try
            {
                CharacterClass c = db.Characters.Find(Convert.ToInt32(idCharacter));

                if (c == null)
                    throw new Exception("Invalid Character to input attributes. Please try again");

                return c;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private CharacterAttributeClass ValidateAttributeFromCharacterClass(string attribute)
        {
            try
            {
                CharacterAttributeClass returning = new CharacterAttributeClass();
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
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            if (characterattributeclass == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterattributeclass.idAttribute);
            return View(characterattributeclass);
        }

        //
        // POST: /CharacterAttribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CharacterAttributeClass characterattributeclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(characterattributeclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAttribute = new SelectList(db.Attributes, "idAttribute", "name", characterattributeclass.idAttribute);
            return View(characterattributeclass);
        }

        //
        // GET: /CharacterAttribute/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            if (characterattributeclass == null)
            {
                return HttpNotFound();
            }
            return View(characterattributeclass);
        }

        //
        // POST: /CharacterAttribute/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CharacterAttributeClass characterattributeclass = db.CharacterAttributes.Find(id);
            db.CharacterAttributes.Remove(characterattributeclass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ListNonCharacter()
        {
            IEnumerable<AttributeClass> attributes;

            attributes = db.Attributes.ToList().OrderBy(t => t.type.name);

            return PartialView(attributes);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}