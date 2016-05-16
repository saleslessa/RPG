using System.Data;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Models;

namespace DaemonCharacter.Controllers
{
    public class AttributeTypeController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /AttributeType/

        public ActionResult Index()
        {
            return View(db.AttributeTypes.ToList());
        }

        public ActionResult _List(int id = 0)
        {
            ViewBag.Types = new SelectList(db.AttributeTypes.ToList(), "idAttributeType", "name", id);
            return View();
        }

        //
        // GET: /AttributeType/Details/5

        public ActionResult Details(int idAttributeType = 0)
        {
            AttributeTypeModel attributetype = db.AttributeTypes.Find(idAttributeType);
            if (attributetype == null)
            {
                return HttpNotFound();
            }
            return View(attributetype);
        }

        //
        // GET: /AttributeType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AttributeType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttributeTypeModel attributetype)
        {
            if (ModelState.IsValid)
            {
                db.AttributeTypes.Add(attributetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attributetype);
        }

        //
        // GET: /AttributeType/Edit/5

        public ActionResult Edit(int idAttributeType = 0)
        {
            AttributeTypeModel attributetype = db.AttributeTypes.Find(idAttributeType);
            if (attributetype == null)
            {
                return HttpNotFound();
            }
            return View(attributetype);
        }

        //
        // POST: /AttributeType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeTypeModel attributetype, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributetype).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attributetype);
        }

        //
        // GET: /AttributeType/Delete/5

        public ActionResult Delete(int idAttributeType = 0)
        {
            AttributeTypeModel attributetype = db.AttributeTypes.Find(idAttributeType);
            if (attributetype == null)
            {
                return HttpNotFound();
            }
            return View(attributetype);
        }

        //
        // POST: /AttributeType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idAttributeType)
        {
            AttributeTypeModel attributetype = db.AttributeTypes.Find(idAttributeType);
            db.AttributeTypes.Remove(attributetype);
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