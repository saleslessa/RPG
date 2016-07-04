using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DaemonCharacter.Domain;
using Newtonsoft.Json.Linq;
using System;

namespace DaemonCharacter.Controllers
{
    public class CampaignController : Controller
    {
        private DaemonCharacterContext db = new DaemonCharacterContext();
        private string loggedUser = string.Empty;


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
        // GET: /Campaign/

        public ActionResult Index()
        {

            try
            {

                this.loggedUser = GetLoggedUser();

                if (db.UserProfiles.Where(w => w.UserName == loggedUser).First().accessLevel != accessLevel.Admin)
                {
                    var campaignmodels = db.Campaigns
                    .Where(w => w.userMaster.UserName == loggedUser);
                    return View(campaignmodels.ToList());
                }
                else
                {
                    var campaignmodels = db.Campaigns;
                    return View(campaignmodels.ToList());
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult ListAvailableCampaigns()
        {
            try
            {
                this.loggedUser = GetLoggedUser();
                List<Campaign> campaignmodels = db.Campaigns.Where(t => t.remainingPlayers > 0
                    && t.userMaster.UserName != loggedUser).ToList();

                return campaignmodels.Count > 0 ? View(campaignmodels) : null;

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //
        // GET: /Campaign/Details/5

        public ActionResult Details(int id = 0)
        {
            try
            {
                GetLoggedUser();

                Campaign campaignmodel = db.Campaigns.Find(id);
                if (campaignmodel == null)
                {
                    return HttpNotFound();
                }
                return View(campaignmodel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Campaign/Create

        public ActionResult Create()
        {
            try
            {
                GetLoggedUser();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Campaign/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Campaign campaignmodel)
        {
            this.loggedUser = GetLoggedUser();

            UserProfileModel user = db.UserProfiles
            .Where(w => w.UserName == this.loggedUser)
            .FirstOrDefault();

            campaignmodel.userMaster = user;
            campaignmodel.campaignStatus = CampaignStatus.Beginning;
            campaignmodel.remainingPlayers = campaignmodel.maxPlayers;

            if (ModelState.IsValid)
            {
                db.Campaigns.Add(campaignmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(campaignmodel);
        }

        //
        // GET: /Campaign/Edit/5

        public ActionResult Edit(int id = 0)
        {
            try
            {
                GetLoggedUser();

                Campaign campaignmodel = db.Campaigns.Find(id);
                if (campaignmodel == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idMaster = new SelectList(db.UserProfiles, "UserId", "UserName", campaignmodel.userMaster.UserId);
                return View(campaignmodel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Campaign/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Campaign campaignmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaignmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMaster = new SelectList(db.UserProfiles, "UserId", "UserName", campaignmodel.userMaster.UserId);
            return View(campaignmodel);
        }

        //
        // GET: /Campaign/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                GetLoggedUser();

                Campaign campaignmodel = db.Campaigns.Find(id);
                if (campaignmodel == null)
                {
                    return HttpNotFound();
                }
                return View(campaignmodel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Campaign/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaignmodel = db.Campaigns.Find(id);
            db.Campaigns.Remove(campaignmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public List<AvailableCampaigns> GetAvailableCampaigns()
        {

            return db.Campaigns
                .Where(t => t.remainingPlayers > 0)
                .Select(s => new AvailableCampaigns
                {
                    id = s.id,
                    userMaster = s.userMaster,
                    name = s.name,
                    shortDescription = s.shortDescription,
                    remainingPlayers = s.remainingPlayers
                })
                .OrderBy(o => o.name)
                .ToList();

        }


        //TODO: Adjust output of this method!
        public JsonResult GetSelectedCampaign(int idCampaign = -1)
        {

            AvailableCampaigns a = new AvailableCampaigns();

            a = db.Campaigns
                .Where(t => t.remainingPlayers > 0 && t.id == idCampaign)
                .Select(s => new AvailableCampaigns
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

            return Json(r, JsonRequestBehavior.AllowGet);
        }

       
    }
}