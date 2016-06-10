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
            if (!ValidateAuth())
                RedirectToAction("Index", "Home");

            return View(db.Attributes.ToList());
        }

        public bool ValidateAuth()
        {
            if (!Request.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
                return false;
            }
            return true;

        }

        public ActionResult ListBonus(int id = -1)
        {
            //if (!ValidateAuth())
            //    RedirectToAction("Index", "Home");

            ViewBag.selected = GetBonusAttributeId(id);

            IEnumerable<AttributeModel> result = db.Attributes.Where(t => t.type.useBonus == true && t.id != id).ToList();

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
            if (id != -1)
                return db.Attributes.Where(t => t.ParentAttribute.Contains(
                            db.Attributes.Where(tt => tt.id == id).FirstOrDefault()
                        )
                    )
                    .ToList();
            else
                return null;
        }

        //
        // GET: /Attribute/Details/5

        public ActionResult Details(int id = 0)
        {
            if (!ValidateAuth())
                RedirectToAction("Index", "Home");

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
            if (!ValidateAuth())
                RedirectToAction("Index", "Home");

            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "id", "name");
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

                    //if (child.AttributeBonus == null) child.AttributeBonus = new List<AttributeModel>();

                    //if (attribute.ParentAttribute == null) attribute.ParentAttribute = new List<AttributeModel>();

                    //attribute.ParentAttribute.Add(child);
                    //child.AttributeBonus.Add(attribute);

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

        private AttributeTypeModel ValidateAttributeType(int idAttributeType)
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
        public ActionResult Create(AttributeModel Attribute, FormCollection f)
        {
            if (!ValidateAuth())
                RedirectToAction("Index", "Home");

            AttributeTypeModel a = ValidateAttributeType(Convert.ToInt32(((string[])f.GetValue("idAttributeType").RawValue)[0].ToString()));

            Attribute.type = a;

            if (ModelState.IsValid)
            {
                try
                {

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
                    ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "id", "name");
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
            if (!ValidateAuth())
                RedirectToAction("Index", "Home");

            AttributeModel AttributeModel = db.Attributes.Find(id);
            if (AttributeModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "id", "name", AttributeModel.type.id);
            return View(AttributeModel);
        }

        //
        // POST: /Attribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeModel Att, FormCollection f)
        {
            //if (!ValidateAuth())
            //    RedirectToAction("Index", "Home");

            try
            {
                if (ModelState.IsValid)
                {

                    AttributeTypeModel a = new AttributeTypeModel();

                    a = ValidateAttributeType(Convert.ToInt32(((string[])f.GetValue("id").RawValue)[0].ToString()));
                    Att.type = a;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        EditAttribute(Att);
                        SaveAttributeBonus(Att, SelectAttributeBonus(f));

                        scope.Complete();
                    }

                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "id", "name", Att.type.id);
                ViewBag.Message = ex.Message;
                return View(Att);
            }
            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "id", "name", Att.type.id);
            ReturnErrorModelState(ModelState);
            return View(Att);


        }

        private void ReturnErrorModelState(ModelStateDictionary ModelState)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewBag.idAttributeType = new SelectList(db.AttributeTypes, "id", "name");
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
            if (!ValidateAuth())
                RedirectToAction("Index", "Home");

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
            //if (!ValidateAuth())
            //    RedirectToAction("Index", "Home");

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

        /// <summary>
        /// Method responsible for retrieve the list of associated attributes of a character
        /// </summary>
        /// <param name="idPerson">Edited character</param>
        /// <returns>Returns a partial view</returns>
        public ActionResult ListAttributesFromCharacter(int idPerson = 0)
        {
            if (!ValidateAuth())
                RedirectToAction("Index", "Home");

            IEnumerable<AttributeModel> attributes;
            //idPerson = 0 is a new Character being created. None attribute exist yet.
            if (idPerson == 0)
            {
                return HttpNotFound();
            }
            //idPerson != 0 is a Character being edited
            else
            {
                List<CharacterAttributeModel> character = db.CharacterAttributes.Where(t => t.character.id == idPerson).ToList();
                if (character == null)
                {
                    return HttpNotFound();
                }

                attributes = db.Attributes.Where(t => character.Any(c => c.attribute.id == t.id)).OrderBy(t => t.type.name);
            }

            return PartialView(attributes);
        }

        public JsonResult FindMinimum(int id = -1)
        {

            if (id == -1 || !ValidateAuth())
                return Json(HttpNotFound());

            return Json(db.Attributes.Find(id).minimum, JsonRequestBehavior.AllowGet);

        }

    }
}