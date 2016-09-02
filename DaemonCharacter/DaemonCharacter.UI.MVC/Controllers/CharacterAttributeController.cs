using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using DaemonCharacter.Application.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DaemonCharacter.UI.MVC.Controllers
{
    public class CharacterAttributeController : Controller
    {
        private readonly ICharacterAttributeAppService _characterAttributeAppService;
        private readonly IAttributeAppService _attributeAppService;

        public CharacterAttributeController(ICharacterAttributeAppService characterAttributeAppService, IAttributeAppService attributeAppService)
        {
            _characterAttributeAppService = characterAttributeAppService;
            _attributeAppService = attributeAppService;
        }

        public ActionResult _Create(PlayerViewModel player)
        {
            var model = new CharacterAttributeViewModel();
            model.ListOfAvailableAttributes = _attributeAppService.ListWithPagination(0, 10);
            model.SelectedAttributes = player == null || player.SelectedAttributes == null ? new List<SelectedCharacterAttributeViewModel>() : player.SelectedAttributes.ToList();


            ViewBag.PaginationSkip = 0;
            ViewBag.PaginationTake = 10;
            
            return View(model);
        }

        [HttpGet]
        public JsonResult GetBonusInfo(Guid CharacterId, Guid AttributeId)
        {
            var obj = _characterAttributeAppService.GetTotalBonusAttributes(CharacterId, AttributeId);
            var result = new List<object>();

            foreach (var item in obj)
            {
                result.Add(new { Key = item.Key, Value = item.Value });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetAttributeValue(Guid CharacterId, Guid AttributeId, int Value)
        {
            try
            {
                _characterAttributeAppService.SetValue(CharacterId, AttributeId, Value);
                return Json(new { status = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", error = ex.Message });
            }   
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}