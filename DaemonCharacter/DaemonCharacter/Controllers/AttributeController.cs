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
        public ActionResult Create(AttributeClass attributeclass, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AttributeTypeClass a = new AttributeTypeClass();
                     
                    for(int i=0;i<f.Count;i++)
                    {
                        if (f.Keys[i] == "attr_Type")
                        {
                            a = db.AttributeTypes.Find(f.GetValues(i).ToString());
                        }
                    }

                    attributeclass.type = a;
                    db.Attributes.Add(attributeclass);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Retorno = "An error occured while trying to create this attribute: " + ex.Message;
                    return RedirectToAction("Index");
                }
                    
            }

            ViewBag.errorMessage = "Error while trying to create this Attribute";
            return View(attributeclass);
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
        public ActionResult Edit(AttributeClass attributeclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributeclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attributeclass);
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