using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
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

        public ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AttributeViewModel result = _attributeAppService.Get(id);
            if (result == null)
                return HttpNotFound();

            return View(result);
        }

        public ActionResult Create()
        {
            var model = new AttributeViewModel();
            model.AttributeBonus = _attributeAppService.ListAvailableForBonus(model.AttributeId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttributeViewModel att)
        {
            if (ModelState.IsValid)
            {
                att = _attributeAppService.Add(att);

                if (!att.ValidationResult.IsValid)
                {
                    foreach (var error in att.ValidationResult.Erros)
                    {
                        ModelState.AddModelError(string.Empty, error.Message);
                    }

                    return View(att);
                }

                if (!att.ValidationResult.Message.IsNullOrWhiteSpace())
                {
                    ViewBag.Success = att.ValidationResult.Message;
                    return View(att);
                }

                return RedirectToAction("Index");
            }

            return View(att);
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeViewModel att)
        {
            if (ModelState.IsValid)
            {
                _attributeAppService.Update(att);

                if (!att.ValidationResult.IsValid)
                {
                    foreach (var error in att.ValidationResult.Erros)
                    {
                        ModelState.AddModelError(string.Empty, error.Message);
                    }

                    return View(att);
                }

                if (!att.ValidationResult.Message.IsNullOrWhiteSpace())
                {
                    ViewBag.Message = att.ValidationResult.Message;
                    return View(att);
                }

                return RedirectToAction("Index");
            }

            return View(att);
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