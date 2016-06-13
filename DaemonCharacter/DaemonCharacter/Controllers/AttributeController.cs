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

        public AttributeController()
        {
            //if (!Request.IsAuthenticated)
            //    RedirectToAction("Index", "Home");
        }

        //
        // GET: /Attribute/

        public ActionResult Index()
        {
            return View(db.Attributes.ToList());
        }

        public ActionResult ListBonus(int id = -1)
        {
            ViewBag.selected = GetBonusAttributeId(id);
            IEnumerable<AttributeModel> result = db.Attributes.Where(t => t.type != AttributeType.Characteristic && t.id != id).ToList();

            return View(result);
        }

        private List<int> GetBonusAttributeId(int id)
        {
            if (id != -1)
                return db.Attributes.Where(t => t.ParentAttribute.Contains(
                            db.Attributes.Where(tt => tt.id == id).FirstOrDefault()
                        )
                    )
                    .Select(s => s.id).ToList();
            else
                return new List<int>() { id };
        }

        private List<AttributeModel> GetBonusAttribute(int id)
        {
            return db.Attributes.Where(t => t.ParentAttribute.Contains(
                        db.Attributes.Where(tt => tt.id == id).FirstOrDefault()
                    )
                )
                .ToList();
        }

        //
        // GET: /Attribute/Details/5

        public ActionResult Details(int id = 0)
        {

            AttributeModel AttributeModel = db.Attributes.Find(id);
            if (AttributeModel == null)
            {
                return HttpNotFound();
            }
            return View(AttributeModel);
        }

        //
        // GET: /Attribute/Create
        public ActionResult Create()
        {
            ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>());
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

        private void SaveAttribute(ref AttributeModel Attribute)
        {
            db.Attributes.Add(Attribute);
            db.SaveChanges();
        }

        private void SaveAttributeBonus(AttributeModel attribute, ArrayList Bonus)
        {
            try
            {
                RemoveChilds(attribute);

                for (int i = 0; i < Bonus.Count; i++)
                {
                    AttributeModel child = db.Attributes.Find(Bonus[i]);

                    if (child.ParentAttribute == null) child.ParentAttribute = new List<AttributeModel>();

                    if (attribute.AttributeBonus == null) attribute.AttributeBonus = new List<AttributeModel>();

                    attribute.AttributeBonus.Add(child);
                    child.ParentAttribute.Add(attribute);

                    db.Entry(child).State = EntityState.Modified;
                    db.Entry(attribute).State = EntityState.Modified;

                    db.SaveChanges();
                }
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
        public ActionResult Create(AttributeModel Attribute, FormCollection f)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    Attribute.type = (AttributeType)Enum.Parse(typeof(AttributeType), (((object[])f.GetValue("idAttributeType").RawValue)[0]).ToString(), true);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        SaveAttribute(ref Attribute);
                        SaveAttributeBonus(Attribute, SelectAttributeBonus(f));

                        scope.Complete();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Retorno = "An error occured while trying to create this attribute: " + ex.Message;
                    ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>());
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
            AttributeModel AttributeModel = db.Attributes.Find(id);
            if (AttributeModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>(), AttributeModel.type);
            return View(AttributeModel);
        }

        //
        // POST: /Attribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeModel Att, FormCollection f)
        {

            try
            {
                Att.type = (AttributeType)Enum.Parse(typeof(AttributeType), (((object[])f.GetValue("idAttributeType").RawValue)[0]).ToString(), true);

                if (ModelState.IsValid)
                {

                    using (TransactionScope scope = new TransactionScope())
                    {
                        EditAttribute(Att);
                        SaveAttributeBonus(Att, SelectAttributeBonus(f));

                        scope.Complete();
                    }

                    return RedirectToAction("Index");
                }
                ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>());
                ReturnErrorModelState(ModelState);
                return View(Att);
            }
            catch (Exception ex)
            {
                ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>());
                ViewBag.Message = ex.Message;
                return View(Att);
            }
        }

        private void ReturnErrorModelState(ModelStateDictionary ModelState)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>());
            ViewBag.message = "The following errors occured while trying to create this Attribute:\n";

            for (int i = 0; i < errors.ToList().Count; i++)
            {
                ViewBag.message += errors.ToList()[i].ErrorMessage + "\n";
            }
        }

        private void EditAttribute(AttributeModel Att)
        {
            db.Entry(Att).State = EntityState.Modified;
            db.SaveChanges();
        }

        //
        // GET: /Attribute/Delete/5

        public ActionResult Delete(int id = 0)
        {

            AttributeModel AttributeModel = db.Attributes.Find(id);
            if (AttributeModel == null)
            {
                return HttpNotFound();
            }
            return View(AttributeModel);
        }

        //
        // POST: /Attribute/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                AttributeModel AttributeModel = db.Attributes.Find(id);

                RemoveChilds(AttributeModel);
                RemoveParents(AttributeModel);

                db.Attributes.Remove(AttributeModel);
                db.SaveChanges();

                scope.Complete();
            }

            return RedirectToAction("Index");
        }

        private void RemoveParents(AttributeModel attribute)
        {
            try
            {
                attribute.ParentAttribute.Clear();
                db.Entry(attribute).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RemoveChilds(AttributeModel attribute)
        {
            try
            {
                List<AttributeModel> listChild = GetBonusAttribute(attribute.id);

                foreach (AttributeModel item in listChild)
                {
                    item.ParentAttribute.Remove(attribute);
                    db.Entry(item).State = EntityState.Modified;
                }

                db.SaveChanges();
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

        ///// <summary>
        ///// Method responsible for retrieve the list of associated attributes of a character
        ///// </summary>
        ///// <param name="idCharacter">Edited character</param>
        ///// <returns>Returns a partial view</returns>
        //public ActionResult ListAttributesFromCharacter(int idCharacter = 0)
        //{
        //    if (!ValidateAuth())
        //        RedirectToAction("Index", "Home");

        //    IEnumerable<AttributeModel> attributes;
        //    //idPerson = 0 is a new Character being created. None attribute exist yet.
        //    if (idCharacter == 0)
        //    {
        //        return HttpNotFound();
        //    }
        //    //idPerson != 0 is a Character being edited
        //    else
        //    {
        //        List<CharacterAttributeModel> character = db.CharacterAttributes.Where(t => t.character.id == idCharacter).ToList();
        //        if (character == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        attributes = db.Attributes.Where(t => character.Any(c => c.attribute.id == t.id)).OrderBy(t => t.type.name);
        //    }

        //    return PartialView(attributes);
        //}

        public JsonResult FindMinimum(int id = -1)
        {
            AttributeModel result = db.Attributes.Find(id);
            if (result == null)
                return Json(HttpNotFound());

            return Json(result.minimum, JsonRequestBehavior.AllowGet);
        }

    }
}