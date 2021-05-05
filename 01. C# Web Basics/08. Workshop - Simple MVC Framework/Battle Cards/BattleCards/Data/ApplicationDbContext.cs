namespace BattleCards.Data
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {}

        public ApplicationDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCard>(entity =>
            {
                entity.HasKey(sc => new
                {
                    sc.UserId,
                    sc.CardId
                });
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
