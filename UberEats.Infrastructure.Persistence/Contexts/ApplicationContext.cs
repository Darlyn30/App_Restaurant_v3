using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UberEats.Core.Domain.Entities;

namespace UberEats.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UnverifiedAccount> UnverifiedAccounts { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCar> ShoppingCars { get; set; }
        public DbSet<ShoppingCarItem> ShoppingCarItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable(tb => tb.HasTrigger("GetPIN"));

            modelBuilder.Entity<UnverifiedAccount>()
                .ToTable(tb => tb.HasTrigger("changeStatus"));
        }
    }
}
