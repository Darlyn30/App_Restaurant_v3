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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable(tb => tb.HasTrigger("GetPIN"));

            modelBuilder.Entity<UnverifiedAccount>()
                .ToTable(tb => tb.HasTrigger("changeStatus"));
        }
    }
}
