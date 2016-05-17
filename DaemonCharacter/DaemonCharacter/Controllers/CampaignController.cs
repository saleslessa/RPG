using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaemonCharacter.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Collections;

namespace DaemonCharacter.Controllers
{
    public class CampaignController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();

        //
        // GET: /Campaign/

        public ActionResult Index()
        {
            if (Session["LoggedUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string log = Session["LoggedUser"].ToString();

                if (db.UserProfiles.Where(w => w.UserName == log).First().accessLevel != accessLevel.Admin)
                {
                    var campaignmodels = db.CampaignModels
                    .Where(w => w.userMaster.UserName == log);
                    return View(campaignmodels.ToList());
                }
                else
                {
                    var campaignmodels = db.CampaignModels;
                    return View(campaignmodels.ToList());
                }
            }
        }


        //
        // GET: /Campaign/Details/5

        public ActionResult Details(int id = 0)
        {
            CampaignModel campaignmodel = db.CampaignModels.Find(id);
            if (campaignmodel == null)
            {
                return HttpNotFound();
            }
            return View(campaignmodel);
        }

        //
        // GET: /Campaign/Create

        public ActionResult Create()
        {
            if (Session["LoggedUser"] == null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        //
        // POST: /Campaign/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampaignModel campaignmodel)
        {
            if (Session["LoggedUser"] != null)
            {
                string log = Session["LoggedUser"].ToString();

                int idMaster = db.UserProfiles
                .Where(w => w.UserName == log)
                .Select(s => s.UserId)
                .FirstOrDefault();

                campaignmodel.idMaster = idMaster;

                campaignmodel.remainingPlayers = campaignmodel.maxPlayers;

                if (ModelState.IsValid)
                {
                    db.CampaignModels.Add(campaignmodel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(campaignmodel);
        }

        //
        // GET: /Campaign/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CampaignModel campaignmodel = db.CampaignModels.Find(id);
            if (campaignmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMaster = new SelectList(db.UserProfiles, "UserId", "UserName", campaignmodel.idMaster);
            return View(campaignmodel);
        }

        //
        // POST: /Campaign/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CampaignModel campaignmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaignmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMaster = new SelectList(db.UserProfiles, "UserId", "UserName", campaignmodel.idMaster);
            return View(campaignmodel);
        }

        //
        // GET: /Campaign/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CampaignModel campaignmodel = db.CampaignModels.Find(id);
            if (campaignmodel == null)
            {
                return HttpNotFound();
            }
            return View(campaignmodel);
        }

        //
        // POST: /Campaign/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CampaignModel campaignmodel = db.CampaignModels.Find(id);
            db.CampaignModels.Remove(campaignmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public List<AvailableCampaignsModel> ListAvailableCampaigns()
        {

            return db.CampaignModels
                .Where(t => t.remainingPlayers > 0)
                .Select(s => new AvailableCampaignsModel
                {
                    id = s.id,
                    idMaster = s.idMaster,
                    name = s.name,
                    shortDescription = s.shortDescription,
                    remainingPlayers = s.remainingPlayers
                })
                .OrderBy(o => o.name)
                .ToList();

        }

        public JsonResult GetSelectedCampaign(int idCampaign = -1)
        {

            AvailableCampaignsModel a = new AvailableCampaignsModel();

            a = db.CampaignModels
                .Where(t => t.remainingPlayers > 0 && t.id == idCampaign)
                .Select(s => new AvailableCampaignsModel
                {
                    name = s.name,
                    shortDescription = s.shortDescription,
                    remainingPlayers = s.remainingPlayers,
                    userMaster = s.userMaster
                })
                .FirstOrDefault();

            if (a == null)
                return Json("", JsonRequestBehavior.AllowGet);

            JObject result = new JObject(
                new JProperty("name", a.name),
                new JProperty("shortDescription", a.shortDescription),
                new JProperty("remainingPlayers", a.remainingPlayers),
                new JProperty("userMaster", a.userMaster.UserName)
                );

            string r = a.name + "|" + a.shortDescription + "|" + a.remainingPlayers.ToString() + "|" + a.userMaster.UserName;

            //JavaScriptSerializer oSerializer = new JavaScriptSerializer();

            //string sJSON = oSerializer.Serialize(result);


            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}