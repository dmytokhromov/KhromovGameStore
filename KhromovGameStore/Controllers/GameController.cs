using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using KhromovGameStore.Models;
using System.Linq;
using System;

namespace KhromovGameStore.Controllers
{
    public class GameController : Controller
    {
        DataBaseContext db = new DataBaseContext();

        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<Game> games = db.Games.Include(g => g.Genre);

            return View(games);
        }

        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var game = db.Games.Find(id);

            if (game != null)
            {
                var genres = new SelectList(db.Genres, "Id", "Name", game.GenreId);
                ViewBag.Genres = genres;
                return View(game);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(int id)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var geme = db.Games.Find(id);
                db.Games.Remove(geme);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}
