using System.Collections.Generic;

namespace KhromovGameStore.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
