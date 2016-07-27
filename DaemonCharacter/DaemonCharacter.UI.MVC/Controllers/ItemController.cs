using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel model)
        {
            _itemAppService.Add(model);

            if (!model.ValidationResult.IsValid)
                return View(model);

            ViewBag.Sucesso = model.ValidationResult.Message;
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var model = _itemAppService.Get(id);
            if(model == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemViewModel model)
        {
            model = _itemAppService.Update(model);
            if (!model.ValidationResult.IsValid)
                return View(model);

            ViewBag.Sucesso = model.ValidationResult.Message;

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}