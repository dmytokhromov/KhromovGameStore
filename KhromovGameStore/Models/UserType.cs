namespace KhromovGameStore.Models
{
    public class UserType
    {
        public const int USER = 1;
        public const int ADMIN = 2;

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
