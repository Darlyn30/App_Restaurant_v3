using Microsoft.EntityFrameworkCore;
using UberEats.Core.Domain.Entities;

namespace UberEats.Infrastructure.Persistence.Contexts
{
    public class VerifyContext : DbContext
    {
        public VerifyContext(DbContextOptions<VerifyContext> options) : base(options)
        {
        }

        public DbSet<UnverifiedAccount> unverifiedAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnverifiedAccount>()
                .ToTable(tb => tb.HasTrigger("changeStatus"));
        }
    }
}
