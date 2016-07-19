using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Campaign;
using System;
using System.Web.Mvc;

namespace DaemonCharacter.UI.MVC.Controllers
{
    [Authorize]
    public class CampaignController : Controller
    {

        private readonly ICampaignAppService _campaignAppService;

        public CampaignController(ICampaignAppService campaignAppService)
        {
            _campaignAppService = campaignAppService;
        }

        // GET: Campaign
        public ActionResult Index()
        {
            return View(_campaignAppService.ListAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampaignViewModel model)
        {

            model.CampaignUserMaster = User.Identity.Name;

            if (model.CampaignUserMaster == null)
                model.ValidationResult.Add(new DomainValidation.Validation.ValidationError("Not Logged"));


            if (ModelState.IsValid)
            {
                _campaignAppService.Add(model);
                return RedirectToAction("Index");
            }

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    model.ValidationResult.Add(new DomainValidation.Validation.ValidationError(error.ErrorMessage.ToString()));
                }
            }

            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            var model = _campaignAppService.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CampaignViewModel model)
        {
            if (ModelState.IsValid)
            {
                _campaignAppService.Update(model);
                return RedirectToAction("Index");
            }

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    model.ValidationResult.Add(new DomainValidation.Validation.ValidationError(error.ErrorMessage.ToString()));
                }
            }

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var model = _campaignAppService.Get(id);
            if(model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _campaignAppService.Remove(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _campaignAppService.Dispose();
            base.Dispose(disposing);
        }

    }
}