using Microsoft.EntityFrameworkCore;
using Panda.Data.Models;

namespace Panda.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        { }
        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=.;Database=Panda;Integrated Security=true;");
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Package> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Packages)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Receipts)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}