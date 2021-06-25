using Microsoft.EntityFrameworkCore;
using MUSACA.Data.Models;

namespace MUSACA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        { }
        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        { }


        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasKey(x => x.Id);

            //    entity
            //        .HasOne(x=>x.Order)
            //        .WithOne(x=>x.)
            //})
        }
    }
}