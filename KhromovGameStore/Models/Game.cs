using System;
namespace KhromovGameStore.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
