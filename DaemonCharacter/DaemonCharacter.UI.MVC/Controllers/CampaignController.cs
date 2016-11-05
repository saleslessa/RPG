using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Campaign;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

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
            if (!ModelState.IsValid)
            {
                LoadAttributeErrors(model);

                var errorList = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { error = "ModelStateError", model = errorList.Where(t => t.Value.Length > 0) });
            }

            model = _campaignAppService.Add(model);

            if (model.ValidationResult.IsValid) return Json(new {error = "", message = model.ValidationResult.Message});

            LoadAttributeErrors(model);
            return Json(new { error = "ValildationResultError", model = model.ValidationResult });
        }

        private void LoadAttributeErrors(CampaignViewModel model)
        {
            foreach (var error in model.ValidationResult.Erros)
                ModelState.AddModelError(string.Empty, error.Message);
        }

        public ActionResult Edit(Guid? id)
        {
            var model = _campaignAppService.Get(id);
            return model == null ? (ActionResult)HttpNotFound() : View(model);
        }

        [HttpPost]
        public ActionResult Edit(CampaignViewModel model)
        {
            model.CampaignUserMaster = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                LoadAttributeErrors(model);

                var errorList = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { error = "ModelStateError", model = errorList.Where(t => t.Value.Length > 0) });
            }

            model = _campaignAppService.Update(model);

            if (model.ValidationResult.IsValid) return Json(new { error = "", message = model.ValidationResult.Message });

            LoadAttributeErrors(model);
            return Json(new { error = "ValildationResultError", model = model.ValidationResult });
        }

        public ActionResult Delete(Guid id)
        {
            var model = _campaignAppService.Get(id);
            return model == null ? (ActionResult)HttpNotFound() : View(model);
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