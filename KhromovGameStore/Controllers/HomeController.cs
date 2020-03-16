using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using KhromovGameStore.Models;
using System.Data.Entity;
using KhromovGameStore.Services;

namespace KhromovGameStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataBaseContext db = new DataBaseContext();

            var games = db.Games
                .Include(g => g.Genre);

            ViewBag.Games = games;

            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
            }

            var cartService = new CartService();

            ViewBag.CartCount = cartService.GetCount(cartJson);
            ViewBag.CartPrice = cartService.GetPrice(cartJson);

            return View();
        }
    }
}
