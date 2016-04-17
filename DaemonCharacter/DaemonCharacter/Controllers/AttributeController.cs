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
    public class AttributeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /Attribute/

        public ActionResult Index()
        {
            return View(db.Attributes.ToList());
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
            return View();
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
                    AttributeTypeClass a = new AttributeTypeClass();

                    int idAttributeType;

                    int.TryParse(((string[])f.GetValue("Types").RawValue)[0].ToString(), out idAttributeType);

                    if (idAttributeType == 0)
                        throw new Exception("Invalid Attribute Type");

                    a = db.AttributeTypes.Find(idAttributeType);

                    Attribute.type = a;
                    db.Attributes.Add(Attribute);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Retorno = "An error occured while trying to create this attribute: " + ex.Message;
                    return RedirectToAction("Index");
                }
                    
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewBag.message = "The following errors occured while trying to create this Attribute:\n";

            for (int i = 0; i < errors.ToList().Count; i++)
            {
                ViewBag.message += errors.ToList()[i].ErrorMessage + "\n";
            }

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
            return View(attributeclass);
        }

        //
        // POST: /Attribute/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeClass Att, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                AttributeTypeClass a = new AttributeTypeClass();

                int idAttributeType;

                //Getting selected attribute type in form
                int.TryParse(((string[])f.GetValue("Types").RawValue)[0].ToString(), out idAttributeType);

                if (idAttributeType == 0)
                    throw new Exception("Invalid Attribute Type");


                //looking for related attribute type
                a = db.AttributeTypes.Find(idAttributeType);

                Att.type = a;

                //setting foreign key on attribute class
                Att.idAttributeType = a.idAttributeType;

                db.Entry(Att).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(Att);
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
    }
}