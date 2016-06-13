using System.Data;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Models;
using System.Collections.Generic;
using System;
using System.Transactions;

namespace DaemonCharacter.Controllers
{
    public class ItemController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /Item/

        public ActionResult Index()
        {
            return View(db.ItemModels.ToList());
        }

        //
        // GET: /Item/Details/5

        public ActionResult Details(int id = 0)
        {
            ItemModel itemmodel = db.ItemModels.Find(id);
            if (itemmodel == null)
            {
                return HttpNotFound();
            }
            return View(itemmodel);
        }

        //
        // GET: /Item/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Item/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemModel item, FormCollection f)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    List<ItemAttributeModel> ItemAttributes = GetItemAttributes(f);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        db.ItemModels.Add(item);
                        db.SaveChanges();

                        SaveItemAttribute(item, ItemAttributes);

                        scope.Complete();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    var list_errors = ModelState.Values.SelectMany(v => v.Errors);
                    string errors = "";

                    for (int i = 0; i < errors.ToList().Count; i++)
                    {
                        errors += list_errors.ToList()[i].ErrorMessage + "\n";
                    }

                    throw new Exception(errors);
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "The following error occured when trying to create this item: " + ex.Message);
                return View(item);
            }

        }

        private void SaveItemAttribute(ItemModel item, List<ItemAttributeModel> list)
        {
            try
            {

                List<ItemAttributeModel> listRemove = db.ItemAttributes.Where(t => t.item.id == item.id).ToList();

                foreach (ItemAttributeModel itemRemove in listRemove)
                {
                    db.ItemAttributes.Remove(itemRemove);
                    db.SaveChanges();
                }


                foreach (ItemAttributeModel i in list)
                {
                    i.item = item;
                    db.ItemAttributes.Add(i);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ItemAttributeModel> GetItemAttributes(FormCollection f)
        {
            List<ItemAttributeModel> result = new List<ItemAttributeModel>();

            for (int i = 0; i < f.Keys.Count; i++)
            {
                if (f.Keys[i].Split('_')[0] == "attributes")
                    if ((((string[])f.GetValue("value_" + f.Keys[i].Split('_')[1]).RawValue)[0]) != "0")
                        result.Add(
                            new ItemAttributeModel(
                                db.Attributes.Find(Convert.ToInt32((((string[])f.GetValue(f.Keys[i]).RawValue)[0]))),
                                Convert.ToInt32((((string[])f.GetValue("value_" + f.Keys[i].Split('_')[1]).RawValue)[0]))
                                )
                            );

            }

            return result;
        }

        //
        // GET: /Item/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ItemModel itemmodel = db.ItemModels.Find(id);
            if (itemmodel == null)
            {
                return HttpNotFound();
            }
            return View(itemmodel);
        }

        //
        // POST: /Item/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemModel item, FormCollection f)
        {
            if (ModelState.IsValid)
            {

                List<ItemAttributeModel> ItemAttributes = GetItemAttributes(f);

                using (TransactionScope scope = new TransactionScope())
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    SaveItemAttribute(item, ItemAttributes);

                    scope.Complete();
                }

                
                return RedirectToAction("Index");
            }
            return View(item);
        }

        //
        // GET: /Item/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ItemModel itemmodel = db.ItemModels.Find(id);
            if (itemmodel == null)
            {
                return HttpNotFound();
            }
            return View(itemmodel);
        }

        //
        // POST: /Item/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemModel itemmodel = db.ItemModels.Find(id);
            db.ItemModels.Remove(itemmodel);
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