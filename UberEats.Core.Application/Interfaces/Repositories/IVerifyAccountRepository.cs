
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface IVerifyAccountRepository
    {
        public void verifyAccount(string email);
        public List<UnverifiedAccount> GetUnverifiedAccounts();
    }
}
