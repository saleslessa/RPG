using System;
using System.Net;
using System.Web.Mvc;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Application.Interfaces;
using System.Linq;

namespace DaemonCharacter.UI.MVC.Controllers
{
    [Authorize]
    public class PlayerController : AsyncController
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
            if (id != null)
            {
                var playerViewModel = _playerAppService.Get(id);


                if (playerViewModel == null)
                {
                    return HttpNotFound();
                }

                return View(playerViewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        // GET: Player/Create
        public ActionResult Create()
        {
            var model = new PlayerViewModel();
            model.Campaigns = _campaignAppService.ListAvailableCampaigns();

            return View(model);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public JsonResult Create(PlayerViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ValidationResult = new DomainValidation.Validation.ValidationResult();
                model.CharacterUser = User.Identity.Name;

                model = _playerAppService.Add(model);

                if (model.ValidationResult.IsValid)
                    return Json(new {error = "", message = "Player created successfully"});
                LoadPlayerErrors(model);
                return Json(new {error = "ValidationResultError", model = model.ValidationResult});
            }


            LoadPlayerErrors(model);

            var errorList = ModelState.ToDictionary(
                kvp => string.Empty,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var firstOrDefault = model.SelectedItems
                .GroupBy(g => g.ItemId)
                .Select(s => new { Total = s.Sum(t => t.PlayerItemUnitPrice * t.PlayerItemQtd) })
                .FirstOrDefault();
            if (firstOrDefault != null && model.PlayerMoney < firstOrDefault.Total)
                errorList.Add(string.Empty, new string[] { "You used more money than you have. Please change your items in your bag or put more money" });

            return Json(new { error = "ModelStateError", model = errorList.Where(t => t.Value.Length > 0) });
        }

        private void LoadPlayerErrors(PlayerViewModel model)
        {
            foreach (var error in model.ValidationResult.Erros)
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }
            LoadSelectedAttributeErrors(model);
            LoadSelectedItemErrors(model);
        }

        private void LoadSelectedItemErrors(PlayerViewModel model)
        {
            if (model.SelectedItems == null) return;
            foreach (var error in model.SelectedItems.SelectMany(it => it.ValidationResult.Erros))
            {
                ModelState.AddModelError(string.Empty, error.Message);
            }
        }

        private void LoadSelectedAttributeErrors(PlayerViewModel model)
        {
            if (model.SelectedAttributes == null) return;
            foreach (var error in model.SelectedAttributes.SelectMany(att => att.ValidationResult.Erros))
            {
                ModelState.AddModelError(string.Empty, error.Message);
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

            return model == null ? (ActionResult) HttpNotFound() : View(model);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _playerAppService.Update(model);
            return RedirectToAction("Index");
        }

        // GET: Player/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id != null)
            {
                var playerViewModel = _playerAppService.Get(id);
                if (playerViewModel == null)
                {
                    return HttpNotFound();
                }
                return View(playerViewModel);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _playerAppService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
