using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DaemonCharacter.UI.MVC.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemAppService _itemAppService;

        public ItemController(IItemAppService itemAppService)
        {
            _itemAppService = itemAppService;
        }


        public ActionResult Index()
        {
            return View(_itemAppService.ListAll());
        }

        public ActionResult Create()
        {
            var model = new ItemViewModel();
            model.ItemAttribute = _itemAppService.ListAvailableForBonus(null).ToList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Create(ItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadItemErrors(model);

                var errorList = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { error = "ModelStateError", model = errorList.Where(t => t.Value.Length > 0) });
            }

            model = _itemAppService.Add(model);

            if (!model.ValidationResult.IsValid)
            {
                LoadItemErrors(model);
                return Json(new { error = "ValildationResultError", model = model.ValidationResult });
            }

            return Json(new { error = "", message = model.ValidationResult.Message });
        }

        private void LoadItemErrors(ItemViewModel model)
        {
            foreach (var error in model.ValidationResult.Erros)
                ModelState.AddModelError(string.Empty, error.Message);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _itemAppService.Get(id);
            if (model == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            model.ItemAttribute = _itemAppService.ListAvailableForBonus(model.ItemId).ToList();

            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(ItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadItemErrors(model);

                var errorList = ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { error = "ModelStateError", model = errorList.Where(t => t.Value.Length > 0) });
            }

            model = _itemAppService.Update(model);

            if (!model.ValidationResult.IsValid)
            {
                LoadItemErrors(model);
                return Json(new { error = "ValildationResultError", model = model.ValidationResult });
            }

            return Json(new { error = "", message = model.ValidationResult.Message });
        }

        [HttpGet]
        public JsonResult GetInfo(Guid ItemId)
        {
            var obj = _itemAppService.Get(ItemId);
            obj.ItemAttribute = _itemAppService.ListAvailableForBonus(ItemId).Where(t => t.ItemAttributeValue != 0).ToList();
            var result = new List<object>();

            if (obj.ItemEffect != null)
                result.Add(new { Key = "Effect", Value = obj.ItemEffect });

            if (obj.ItemAttribute.Count() > 0)
                result.Add(new { Key = "Attributes", Value = "" });

            foreach (var item in obj.ItemAttribute)
                result.Add(new { Key = item.Attribute.AttributeName, Value = item.ItemAttributeValue });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var att = _itemAppService.Get(id);
            if (att == null)
                return HttpNotFound();

            return View(att);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _itemAppService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}