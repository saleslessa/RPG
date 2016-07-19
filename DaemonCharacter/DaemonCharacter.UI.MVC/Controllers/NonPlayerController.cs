using DaemonCharacter.Application.Interfaces;
using System.Web.Mvc;

namespace DaemonCharacter.UI.MVC.Controllers
{
    [Authorize]
    public class NonPlayerController : Controller
    {

        private readonly INonPlayerAppService _nonPlayerAppService;

        public NonPlayerController(INonPlayerAppService nonPlayerAppService)
        {
            _nonPlayerAppService = nonPlayerAppService;
        }

        public ActionResult Index()
        {
            return View(_nonPlayerAppService.ListAll());
        }
    }
}