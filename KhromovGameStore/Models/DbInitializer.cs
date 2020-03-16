using System.Data.Entity;
using KhromovGameStore.Services;

namespace KhromovGameStore.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<DataBaseContext>
    {
        protected override void Seed(DataBaseContext db)
        {
            db.UserTypes.Add(new UserType { Id = UserType.USER, Name = "User" });
            db.UserTypes.Add(new UserType { Id = UserType.ADMIN, Name = "Admin" });

            db.Users.Add(new User
            {
                Email = "admin@mail.com",
                Password = HashFacade.Hash("admin@mail.com"),
                UserTypeId = UserType.ADMIN
            });

            db.Genres.Add(new Genre { Id = 1, Name = "MMORPG" });
            db.Genres.Add(new Genre { Id = 2, Name = "Strategy" });

            for (int i = 0; i < 10; i++)
            {
                db.Games.Add(new Game
                {
                    Name = "Game #" + i.ToString(),
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    GenreId = 1,
                    Price = 1000
                });
            }

            for (int i = 10; i < 20; i++)
            {
                db.Games.Add(new Game
                {
                    Name = "Game #" + i.ToString(),
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    GenreId = 2,
                    Price = 1500
                });
            }

            base.Seed(db);
        }
    }
}
