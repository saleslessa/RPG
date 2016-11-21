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
            var model = new CharacterAttributeViewModel()
            {
                ListOfAvailableAttributes = _attributeAppService.ListAll().Where(t => t.AttributeType != AttributeType.Talent).ToList(),
                SelectedAttributes =
                    player == null || player.SelectedAttributes == null
                        ? new List<SelectedCharacterAttributeViewModel>()
                        : player.SelectedAttributes.Where(t => t.AttributeType != AttributeType.Talent).ToList()
            };

            return View(model);
        }

        public ActionResult _CreateTalent(PlayerViewModel player)
        {
            var model = new CharacterAttributeViewModel()
            {
                ListOfAvailableAttributes = _attributeAppService.ListAll().Where(t=>t.AttributeType==AttributeType.Talent).ToList(),
                SelectedAttributes =
                     player == null || player.SelectedAttributes == null
                         ? new List<SelectedCharacterAttributeViewModel>()
                         : player.SelectedAttributes.Where(t => t.AttributeType == AttributeType.Talent).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetBonusInfo(Guid CharacterId, Guid AttributeId)
        {
            var BonusByAttributes = _characterAttributeAppService.GetTotalBonusAttributes(CharacterId, AttributeId);
            var BonusByItems = _playerItemAppService.ListBonusOfAllUsedItemsFromAttribute(CharacterId, AttributeId);

            var result = new List<object>();

            foreach (var item in BonusByAttributes)
                result.Add(new { item.Key, item.Value });

            if (BonusByItems.Count > 0)
                result.Add(new { Key = "Items", Value = "" });

            foreach (var item in BonusByItems)
                result.Add(new {item.Key, item.Value });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetAttributeValue(Guid characterId, Guid attributeId, int value)
        {
            try
            {
                _characterAttributeAppService.SetValue(characterId, attributeId, value);
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