using FrituurOpDeHoekMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrituurOpDeHoekMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OldOrder> OldOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OldOrder>().Property(x => x.Price).HasPrecision(10, 2);
            modelBuilder.Entity<Order>().Property(x => x.Price).HasPrecision(10, 2);
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(10, 2);
        }
    }
}