
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IVerifyAccountService
    {
        void VerifyAccount(string email);
        List<UnverifiedAccount> GetUnverifiedAccounts();
    }
}
