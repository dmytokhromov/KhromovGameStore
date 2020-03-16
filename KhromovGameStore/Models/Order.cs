using System;
namespace KhromovGameStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Sum { get; set; }
    }
}
