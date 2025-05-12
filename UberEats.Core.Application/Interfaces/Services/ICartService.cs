
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<Cart> GetCarts(int userId);
    }
}
