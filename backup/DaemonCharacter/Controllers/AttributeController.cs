using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Domain;
using System.Collections;
using System.Transactions;

namespace DaemonCharacter.Controllers
{
    public class AttributeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

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


        //
        // GET: /Attribute/

        public ActionResult Index()
        {
            try
            {
                GetLoggedUser();
                return View(db.Attributes.ToList());
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ListBonus(int id = -1)
        {
            try
            {
                GetLoggedUser();
                ViewBag.selected = GetBonusAttributeId(id);
                IEnumerable<Models.Attributes> result = db.Attributes.Where(t => t.type != AttributeType.Characteristic && t.id != id).ToList();

                return View(result);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
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

        private List<Models.Attributes> GetBonusAttribute(int id)
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
            try
            {
                GetLoggedUser();

                Models.Attributes AttributeModel = db.Attributes.Find(id);
                if (AttributeModel == null)
                {
                    return HttpNotFound();
                }
                return View(AttributeModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        //
        // GET: /Attribute/Create
        public ActionResult Create()
        {
            try
            {
                GetLoggedUser();

                ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>());
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
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

        private void SaveAttribute(ref Models.Attributes Attribute)
        {
            db.Attributes.Add(Attribute);
            db.SaveChanges();
        }

        private void SaveAttributeBonus(Models.Attributes attribute, ArrayList Bonus)
        {
            try
            {
                RemoveChilds(attribute);

                for (int i = 0; i < Bonus.Count; i++)
                {
                    Models.Attributes child = db.Attributes.Find(Bonus[i]);

                    if (child.ParentAttribute == null) child.ParentAttribute = new List<Models.Attributes>();

                    if (attribute.AttributeBonus == null) attribute.AttributeBonus = new List<Models.Attributes>();

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
        public ActionResult Create(Attributes Attribute, FormCollection f)
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
            try
            {
                GetLoggedUser();

                Attributes AttributeModel = db.Attributes.Find(id);
                if (AttributeModel == null)
                {
                    return HttpNotFound();
                }

                ViewBag.idAttributeType = new SelectList(Enum.GetValues(typeof(AttributeType)).Cast<AttributeType>(), AttributeModel.type);
                return View(AttributeModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Attribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Attributes Att, FormCollection f)
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

        private void EditAttribute(Models.Attributes Att)
        {
            db.Entry(Att).State = EntityState.Modified;
            db.SaveChanges();
        }

        //
        // GET: /Attribute/Delete/5

        public ActionResult Delete(int id = 0)
        {

            try
            {
                GetLoggedUser();

                Attributes AttributeModel = db.Attributes.Find(id);
                if (AttributeModel == null)
                {
                    return HttpNotFound();
                }
                return View(AttributeModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Attribute/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            using (TransactionScope scope = new TransactionScope())
            {
                var AttributeModel = db.Attributes.Find(id);

                RemoveChilds(AttributeModel);
                RemoveParents(AttributeModel);

                db.Attributes.Remove(AttributeModel);
                db.SaveChanges();

                scope.Complete();
            }

            return RedirectToAction("Index");
        }

        private void RemoveParents(Models.Attributes attribute)
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

        private void RemoveChilds(Models.Attributes attribute)
        {
            try
            {
                List<Models.Attributes> listChild = GetBonusAttribute(attribute.id);

                foreach (Models.Attributes item in listChild)
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

        public JsonResult FindMinimum(int id = -1)
        {
            Models.Attributes result = db.Attributes.Find(id);
            if (result == null)
                return Json(HttpNotFound());

            return Json(result.minimum, JsonRequestBehavior.AllowGet);
        }

    }
}