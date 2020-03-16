using System.Collections.Generic;
using System.Linq;
using KhromovGameStore.Models;

namespace KhromovGameStore.Services
{
    public class CartService
    {
        public string Add(CartAddView model, string cartJson)
        {
            if (string.IsNullOrEmpty(cartJson))
            {
                Cart cart = new Cart();
                CartItem cartItem = new CartItem
                {
                    GameId = int.Parse(model.GameId),
                };

                cart.CartItems.Add(cartItem);

                return Newtonsoft.Json.JsonConvert.SerializeObject(cart);
            }
            else
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

                foreach(CartItem item in cart.CartItems) { 
                    if (item.GameId == int.Parse(model.GameId))
                    {
                        return Newtonsoft.Json.JsonConvert.SerializeObject(cart);
                    }
                }
         
                CartItem cartItem = new CartItem
                {
                    GameId = int.Parse(model.GameId),
                };

                cart.CartItems.Add(cartItem);

                return Newtonsoft.Json.JsonConvert.SerializeObject(cart);
            }
        }

        public int[] getGamesIds(string cartJson)
        {
            List<int> ids = new List<int>();

            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

                foreach (CartItem item in cart.CartItems)
                {
                    ids.Add(item.GameId);
                }
            }

            return ids.ToArray();
        }

        public int GetCount(string cartJson)
        {
            int count = 0;

            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);
                count = cart.CartItems.Count();
            }

            return count;
        }

        public int GetPrice(string cartJson)
        {
            int price = 0;

            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);
                DataBaseContext db = new DataBaseContext();

                foreach (CartItem item in cart.CartItems)
                {
                    Game game = db.Games.Find(item.GameId);
                    price += game.Price;
                }
            }

            return price;
        }

        public string Delete(int gameId, string cartJson)
        {
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson);

                for (var i = 0; i < cart.CartItems.Count; i++)
                {
                    var item = cart.CartItems.ElementAt(i);
                    if (item.GameId == gameId)
                    {
                        cart.CartItems.Remove(item);
                        break;
                    }
                }

                if (cart.CartItems.Count == 0)
                {
                    return null;
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(cart);
            }

            return cartJson;
        }
    }
}
