using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
using Microsoft.Ajax.Utilities;
using System;
using System.Net;
using System.Web.Mvc;

namespace DaemonCharacter.UI.MVC.Controllers
{
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttributeViewModel att)
        {
            if (ModelState.IsValid)
            {
                att = _attributeAppService.Add(att);

                if (att.ValidationResult.IsValid)
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

            AttributeViewModel att = _attributeAppService.Get(id);
            if (att == null)
                return HttpNotFound();

            return View(att);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributeViewModel att)
        {
            if(ModelState.IsValid)
            {
                _attributeAppService.Update(att);

                if (!att.ValidationResult.Message.IsNullOrWhiteSpace())
                {
                    ViewBag.Success = att.ValidationResult.Message;
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

    }
}