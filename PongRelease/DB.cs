using KeyboardMenu;
using Microsoft.EntityFrameworkCore;

namespace Menu
{
    static class DataBase
    {
        public class ApplicationContext : DbContext
        {

            public DbSet<Player> Players { get; set; } = null;

            public ApplicationContext() => Database.EnsureCreated();

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=helloapp.db");
            }

            //protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{
            //    modelBuilder.Entity<Player>().HasNoKey().
            //}


        }

        public static List<Player> GetPlayersData()
        {
            using (ApplicationContext db = new())
            {
                var Players = db.Players.ToList();
                return Players;
            }
        }

        public static void WriteDataInDatabase(Session session)
        {
            using (ApplicationContext db = new())
            {
                db.Database.EnsureCreated();
                Player LeftPlayer = new Player { Name = session.LeftPlayer.Name, Points = session.LeftPlayer.Points };
                Player RightPlayer = new Player { Name = session.RightPlayer.Name, Points = session.RightPlayer.Points };
                db.Players.AddRange(LeftPlayer, RightPlayer);
                db.SaveChanges();
            }
        }

        // Константы
        public const string Preview = @"

██████╗ ██╗███╗   ██╗ ██████╗     ██████╗  ██████╗ ███╗   ██╗ ██████╗      ██████╗  █████╗ ███╗   ███╗███████╗
██╔══██╗██║████╗  ██║██╔════╝     ██╔══██╗██╔═══██╗████╗  ██║██╔════╝     ██╔════╝ ██╔══██╗████╗ ████║██╔════╝
██████╔╝██║██╔██╗ ██║██║  ███╗    ██████╔╝██║   ██║██╔██╗ ██║██║  ███╗    ██║  ███╗███████║██╔████╔██║█████╗  
██╔═══╝ ██║██║╚██╗██║██║   ██║    ██╔═══╝ ██║   ██║██║╚██╗██║██║   ██║    ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  
██║     ██║██║ ╚████║╚██████╔╝    ██║     ╚██████╔╝██║ ╚████║╚██████╔╝    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗
╚═╝     ╚═╝╚═╝  ╚═══╝ ╚═════╝     ╚═╝      ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝      ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝
                ";
        public const string About = @"
 ██████╗ ██╗   ██╗████████╗███████╗██████╗     ██╗  ██╗███████╗ █████╗ ██╗   ██╗███████╗███╗   ██╗
██╔═══██╗██║   ██║╚══██╔══╝██╔════╝██╔══██╗    ██║  ██║██╔════╝██╔══██╗██║   ██║██╔════╝████╗  ██║
██║   ██║██║   ██║   ██║   █████╗  ██████╔╝    ███████║█████╗  ███████║██║   ██║█████╗  ██╔██╗ ██║
██║   ██║██║   ██║   ██║   ██╔══╝  ██╔══██╗    ██╔══██║██╔══╝  ██╔══██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║
╚██████╔╝╚██████╔╝   ██║   ███████╗██║  ██║    ██║  ██║███████╗██║  ██║ ╚████╔╝ ███████╗██║ ╚████║
 ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝                                             

                                  
Directed by:

Milushev Timur

Nekrasov Vladislav

Arseniy Mazurenko

Borisov Ivan

";

        // Пользовательское перечисление (enum-ы)
        public enum GameSettings
        {
            fieldLength = 50,
            fieldWidth = 15,
            speed = 80
        }
    }
    

}

