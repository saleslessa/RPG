using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
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
        private readonly IPlayerItemAppService _playerItemAppService;

        public CharacterAttributeController(ICharacterAttributeAppService characterAttributeAppService, IAttributeAppService attributeAppService
            , IPlayerItemAppService playerItemAppService)
        {
            _characterAttributeAppService = characterAttributeAppService;
            _attributeAppService = attributeAppService;
            _playerItemAppService = playerItemAppService;
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
            var BonusByAttributes = _characterAttributeAppService.GetTotalBonusAttributes(CharacterId, AttributeId);
            var BonusByItems = _playerItemAppService.ListBonusOfAllUsedItemsFromAttribute(CharacterId, AttributeId);

            var result = new List<object>();

            foreach (var item in BonusByAttributes)
                result.Add(new { Key = item.Key, Value = item.Value });

            if (BonusByItems.Count > 0)
                result.Add(new { Key = "Items", Value = "" });

            foreach (var item in BonusByItems)
                result.Add(new { Key = item.Key, Value = item.Value });

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