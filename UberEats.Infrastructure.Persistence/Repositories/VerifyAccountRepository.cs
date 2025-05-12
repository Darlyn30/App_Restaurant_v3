
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
            var result = _context.unverifiedAccounts.ToList();
            return result;
        }

        public void verifyAccount(string email)
        {
            var registro = _context.unverifiedAccounts.Where(user => user.Email == email).FirstOrDefault();
            _context.unverifiedAccounts.Remove(registro);
            _context.SaveChanges();
        }
    }
}
