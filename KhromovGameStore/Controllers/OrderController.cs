using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using KhromovGameStore.Models;
using KhromovGameStore.Services;

namespace KhromovGameStore.Controllers
{
    public class OrderController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        CartService cartService = new CartService();

        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            var orders = db.Orders.Include(o => o.User);

            return View(orders.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var order = db.Orders
                .Include(p => p.User)
                .FirstOrDefault(t => t.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderGames = db.OrderGames
                .Where(op => op.OrderId == id)
                .Include(op => op.Game)
                .Include(op => op.Game.Genre);

            return View(order);
        }

        public ActionResult Create()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("index", "Home");
            }

            var userId = int.Parse(Session["UserId"].ToString());

            var cartSession = Session["Cart"];
            var cartJson = "";

            if (cartSession != null)
            {
                cartJson = cartSession.ToString();
            }

            var sum = cartService.GetPrice(cartJson);

            Order order = new Order
            {
                UserId = userId,
                Sum = sum
            };

            db.Orders.Add(order);
            db.SaveChanges();

            var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

            foreach (CartItem item in cart.CartItems)
            {
                Game game = db.Games.Find(item.GameId);
                OrderGames orderGames = new OrderGames
                {
                    OrderId = order.Id,
                    GameId = item.GameId,
                    Price = game.Price
                };

                db.OrderGames.Add(orderGames);
            }

            db.SaveChanges();

            Session["Cart"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}
