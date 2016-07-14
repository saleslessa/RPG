using System;
using System.Net;
using System.Web.Mvc;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Application.Interfaces;

namespace DaemonCharacter.UI.MVC.Controllers
{
    public class PlayerController : Controller
    {

        private readonly IPlayerAppService _playerAppService;

        public PlayerController(IPlayerAppService playerAppService)
        {
            _playerAppService = playerAppService;
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
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerViewModel model)
        {
            if (ModelState.IsValid)
            {

                _playerAppService.Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(Guid? id)
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
            _playerAppService.Dispose();
            base.Dispose(disposing);
        }
    }
}
