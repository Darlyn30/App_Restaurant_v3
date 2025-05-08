
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class VerifyAccountService : IVerifyAccountService
    {
        private readonly IVerifyAccountRepository _verifyAccountRepository;
        public VerifyAccountService(IVerifyAccountRepository verifyAccountRepository)
        {
            _verifyAccountRepository = verifyAccountRepository;
        }

        public List<UnverifiedAccount> GetUnverifiedAccounts()
        {
            var result = _verifyAccountRepository.GetUnverifiedAccounts();
            return result;
        }

        public void VerifyAccount(string email)
        {
            _verifyAccountRepository.verifyAccount(email);
        }
    }
}
