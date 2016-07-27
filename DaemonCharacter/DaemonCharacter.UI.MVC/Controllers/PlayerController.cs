using System;
using System.Net;
using System.Web.Mvc;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Application.Interfaces;

namespace DaemonCharacter.UI.MVC.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {

        private readonly IPlayerAppService _playerAppService;
        private readonly ICampaignAppService _campaignAppService;

        public PlayerController(IPlayerAppService playerAppService, ICampaignAppService campaignAppService)
        {
            _playerAppService = playerAppService;
            _campaignAppService = campaignAppService;
        }

        // GET: Player
        public ActionResult Index()
        {
            return View(_playerAppService.ListAll());
        }

        // GET: Player/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playerViewModel = _playerAppService.Get(id);

            if (playerViewModel == null)
            {
                return HttpNotFound();
            }

            return View(playerViewModel);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            var model = new PlayerViewModel();
            model.Campaigns = _campaignAppService.ListAvailableCampaigns();

            return View(model);
        }



        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerViewModel model)
        {
            try
            {
                model.CharacterUser = User.Identity.Name;

                if (ModelState.IsValid)
                {
                    _playerAppService.Add(model);
                    if (!model.ValidationResult.IsValid)
                        throw new Exception();

                    return RedirectToAction("Index");
                }

                throw new Exception();

            }
            catch (Exception)
            {
                foreach (var error in model.ValidationResult.Erros)
                {
                    ModelState.AddModelError(string.Empty, error.Message);
                }

                model.Campaigns = _campaignAppService.ListAvailableCampaigns();
                return View(model);
            }
        }

        // GET: Player/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _playerAppService.Get(id);
            model.Campaigns = _campaignAppService.ListAvailableCampaigns();

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _playerAppService.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playerViewModel = _playerAppService.Get(id);
            if (playerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(playerViewModel);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _playerAppService.Remove(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
