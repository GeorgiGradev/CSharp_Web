using Microsoft.EntityFrameworkCore;
using Suls.Data.Models;

namespace SulsApp.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        { }
        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        public DbSet<Problem> Problems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=.;Database=Suls_Final;Integrated Security=true;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Problems)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Submissions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}