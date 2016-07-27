using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.CharacterAttribute;
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

        public ActionResult _Create()
        {
            var model = new CharacterAttributeViewModel();
            model.ListOfAvailableAttributes = _attributeAppService.ListWithPagination(0, 10);

            ViewBag.PaginationSkip = 0;
            ViewBag.PaginationTake = 10;
            
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}