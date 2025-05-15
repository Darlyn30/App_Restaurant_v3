
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> LoginAsync(LoginViewModel vm);
    }
}
