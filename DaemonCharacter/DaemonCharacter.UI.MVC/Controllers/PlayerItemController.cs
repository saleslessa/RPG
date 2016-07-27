using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.PlayerItem;
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

        public ActionResult _Create()
        {
            var model = new PlayerItemViewModel();
            model.ListAvailableItems = _itemAppService.ListAll().ToList();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}