using IRunes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        { 
        }
        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=.;Database=IRunes;Integrated Security=true;");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}