using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DaemonCharacter.UI.MVC.Controllers
{
    [Authorize]
    public class AttributeController : Controller
    {

        private readonly IAttributeAppService _attributeAppService;

        public AttributeController(IAttributeAppService _att)
        {
            _attributeAppService = _att;
        }

        public ActionResult Index()
        {
            return View(_attributeAppService.ListAll());
        }

        public ActionResult Create()
        {
            var model = new AttributeViewModel();
            model.AttributeBonus = _attributeAppService.ListAvailableForBonus(model.AttributeId);
            return View(model);
        }

        [HttpPost]
        public JsonResult Create(AttributeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadAttributeErrors(model);

                var errorList = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { error = "ModelStateError", model = errorList.Where(t => t.Value.Length > 0) });
            }

            model = _attributeAppService.Add(model);

            if (!model.ValidationResult.IsValid)
            {
                LoadAttributeErrors(model);
                return Json(new { error = "ValildationResultError", model = model.ValidationResult });
            }

            return Json(new { error = "", message = model.ValidationResult.Message });
        }

        private void LoadAttributeErrors(AttributeViewModel model)
        {
            foreach (var error in model.ValidationResult.Erros)
                ModelState.AddModelError(string.Empty, error.Message);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = _attributeAppService.Get(id);
            model.AttributeBonus = _attributeAppService.ListAvailableForBonus(model.AttributeId);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(AttributeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadAttributeErrors(model);

                var errorList = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { error = "ModelStateError", model = errorList.Where(t => t.Value.Length > 0) });
            }

            model = _attributeAppService.Update(model);

            if (model.ValidationResult.IsValid) return Json(new {error = "", message = model.ValidationResult.Message});
            LoadAttributeErrors(model);
            return Json(new { error = "ValildationResultError", model = model.ValidationResult });
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AttributeViewModel att = _attributeAppService.Get(id);
            if (att == null)
                return HttpNotFound();

            return View(att);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _attributeAppService.Remove(id);
            return RedirectToAction("Index");
        }

        public ActionResult _SearchAttributes(int skip, int take, string name)
        {
            var model = _attributeAppService.SearchByNameWithPagination(skip, take, name);

            return View(model);
        }

        [HttpGet]
        public JsonResult SearchAttributes(string name)
        {
            var model = _attributeAppService.SearchByName(name);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SearchByType(AttributeType? type)
        {
            List<AttributeViewModel> model = new List<AttributeViewModel>();

            if ((int)type == -1)
                model = _attributeAppService.SearchByAttributeType(null);
            else
                model = _attributeAppService.SearchByAttributeType(type);


            return Json(model, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}