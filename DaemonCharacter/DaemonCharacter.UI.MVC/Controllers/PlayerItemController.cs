using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Application.ViewModels.PlayerItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DaemonCharacter.UI.MVC.Controllers
{
    public class PlayerItemController : Controller
    {
        private readonly IPlayerItemAppService _playerItemAppService;
        private readonly IItemAppService _itemAppService;

        public PlayerItemController(IPlayerItemAppService playerItemAppService, IItemAppService itemAppService)
        {
            _playerItemAppService = playerItemAppService;
            _itemAppService = itemAppService;
        }

        public ActionResult _Create(PlayerViewModel player)
        {
            var model = new PlayerItemViewModel();
            model.ListAvailableItems = _itemAppService.ListAll().ToList();
            model.SelectedItems = player == null || player.SelectedItems == null ? new List<SelectedPlayerItemViewModel>() : player.SelectedItems.ToList();

            return View(model);
        }

        [HttpGet]
        public JsonResult GetBonusInfo(Guid CharacterId, Guid AttributeId)
        {
            var BonusByItems = _playerItemAppService.ListBonusOfAllUsedItemsFromAttribute(CharacterId, AttributeId);

            var result = new List<object>();

            foreach (var item in BonusByItems)
                result.Add(new { Key = item.Key, Value = item.Value });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}