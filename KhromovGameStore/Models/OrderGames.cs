using System;
namespace KhromovGameStore.Models
{
    public class OrderGames
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int Price { get; set; }
    }
}
