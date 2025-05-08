
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class VerifyAccountRepository : IVerifyAccountRepository
    {
        private readonly ApplicationContext _context;
        public VerifyAccountRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<UnverifiedAccount> GetUnverifiedAccounts()
        {
            var result = _context.UnverifiedAccounts.ToList();
            return result;
        }

        public void verifyAccount(string email)
        {
            var account = _context.UnverifiedAccounts.FirstOrDefault(x => x.Email == email);
            if (account != null)
            {
                _context.UnverifiedAccounts.Remove(account);
                _context.SaveChanges();
            }
        }
    }
}
