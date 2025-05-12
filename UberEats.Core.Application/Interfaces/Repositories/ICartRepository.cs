
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCarts(int userId);
    }
}
