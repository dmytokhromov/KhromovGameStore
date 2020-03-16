using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using KhromovGameStore.Models;
using KhromovGameStore.Services;

namespace KhromovGameStore.Controllers
{
    public class CartController : Controller
    {
        CartService cartService;
        DataBaseContext db;

        public CartController()
        {
            cartService = new CartService();
            db = new DataBaseContext();
        }

        public ActionResult Index()
        {
            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
                ViewBag.CartPrice = cartService.GetPrice(cartJson);

                var gamesIds = cartService.getGamesIds(cartJson);
                var games = db.Games
                    .Where(p => gamesIds.Contains(p.Id));

                ViewBag.Games = games.ToList();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(CartAddView model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
            }

            var cartStr = cartService.Add(model, cartJson);
            Session["Cart"] = cartStr;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
            }

            Session["Cart"] = cartService.Delete(id, cartJson);

            return RedirectToAction("Index", "Cart");
        }
    }
}
