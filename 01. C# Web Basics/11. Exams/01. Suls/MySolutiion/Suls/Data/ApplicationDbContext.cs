using Microsoft.EntityFrameworkCore;
using Suls.Data;

namespace SulsApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=.;Database=Suls02;Integrated Security=true;");
            } 

            base.OnConfiguring(optionsBuilder);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Problem> Problems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}