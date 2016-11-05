using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Campaign;
using System;
using System.Linq;
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
        public JsonResult Create(CampaignViewModel model)
        {
            model.CampaignUserMaster = User.Identity.Name;

            if (model.CampaignUserMaster == null)
                model.ValidationResult.Add(new DomainValidation.Validation.ValidationError("Not Logged"));

            if (ModelState.IsValid)
            {
                model= _campaignAppService.Add(model);
                return Json(new { error = "", message = model.ValidationResult.Message });
            }

            LoadErrors(model);
            return Json(new { error = "ValidationResultError", model = model.ValidationResult });
        }

        private void LoadErrors(CampaignViewModel model)
        {
            foreach (var error in model.ValidationResult.Erros)
                ModelState.AddModelError(string.Empty, error.Message);

            if (ModelState.IsValid) return;

            foreach (var error in ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors))
            {
                model.ValidationResult.Add(new DomainValidation.Validation.ValidationError(error.ErrorMessage));
            }
        }

        public ActionResult Edit(Guid? id)
        {
            var model = _campaignAppService.Get(id);
            return model == null ? (ActionResult)HttpNotFound() : View(model);
        }

        [HttpPost]
        public JsonResult Edit(CampaignViewModel model)
        {
            model.CampaignUserMaster = User.Identity.Name;

            if (model.CampaignUserMaster == null)
                model.ValidationResult.Add(new DomainValidation.Validation.ValidationError("Not Logged"));

            if (ModelState.IsValid)
            {
                model = _campaignAppService.Update(model);
                return Json(new { error = "", message = model.ValidationResult.Message });
            }

            LoadErrors(model);
            return Json(new { error = "ValidationResultError", model = model.ValidationResult });
        }

        public ActionResult Delete(Guid id)
        {
            var model = _campaignAppService.Get(id);
            if (model == null)
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
            base.Dispose(disposing);
        }

    }
}