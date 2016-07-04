using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaemonCharacter.Domain;

namespace DaemonCharacter.Controllers
{
    public class SessionController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /Session/

        public ActionResult Index()
        {
            var campaigns = db.Campaigns
                .Select(s => new ListCampaignsModel()
                {
                    id = s.id,
                    name = s.name,
                    qtdSessions = db.CampaignSession
                        .Where(w => w.idCampaign == s.id)
                        .Count()
                })
                .OrderBy(o => o.name);

            return View(campaigns.Where(t => t.qtdSessions > 0).ToList());
        }

        public ActionResult _ListSessions(int? id)
        {
            var sessions = db.CampaignSession.Where(t => t.idCampaign == id)
                .OrderBy(o => o.dayScheduled);

            return View(sessions);
        }

        //
        // GET: /Session/Details/5

        public ActionResult Details(int id = 0)
        {
            Session sessionmodel = db.CampaignSession.Find(id);
            SessionModel sessionmodel = db.CampaignSession.SingleOrDefault(t => t.id == id);

            if (sessionmodel == null)
            {
                return HttpNotFound();
            }
            return View(sessionmodel);
        }

        //
        // GET: /Session/Create

        public ActionResult Create()
        {
            ViewBag.idCampaign = new SelectList(db.Campaigns, "id", "name");
            return View();
        }

        //
        // POST: /Session/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Session sessionmodel)
        {
            if (ModelState.IsValid)
            {
                db.CampaignSession.Add(sessionmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCampaign = new SelectList(db.Campaigns, "id", "name", sessionmodel.idCampaign);
            return View(sessionmodel);
        }

        //
        // GET: /Session/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Session sessionmodel = db.CampaignSession.Find(id);
            if (sessionmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCampaign = new SelectList(db.Campaigns, "id", "name", sessionmodel.idCampaign);
            return View(sessionmodel);
        }

        //
        // POST: /Session/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Session sessionmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCampaign = new SelectList(db.Campaigns, "id", "name", sessionmodel.idCampaign);
            return View(sessionmodel);
        }

        //
        // GET: /Session/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Session sessionmodel = db.CampaignSession.Find(id);
            if (sessionmodel == null)
            {
                return HttpNotFound();
            }
            return View(sessionmodel);
        }

        //
        // POST: /Session/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session sessionmodel = db.CampaignSession.Find(id);
            db.CampaignSession.Remove(sessionmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult GetUserMaster(int? id)
        {
            var userMaster = db.CampaignSession
                .Where(t => t.id == id)
                .Select(s => new { s.campaign.userMaster.UserName }).SingleOrDefault();

            return Json(userMaster.UserName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrepareSession()
        {
            return View();
        }

        public ActionResult _GridNonPLayer(int idNPC)
        {
            var result = new NonPlayerGridSession() {
                    nonPlayer = db.NonPlayers.SingleOrDefault(t=> t.id == idNPC)
                };
                
            return View(result);
        }

        //public JsonResult ReturnNonPlayerCampaign(int idNonPLayer)
        //{
        //    return Json(db.NonPlayers.FirstOrDefault(t => t.id == idNonPLayer), JsonRequestBehavior.AllowGet);
        //}
    }
}