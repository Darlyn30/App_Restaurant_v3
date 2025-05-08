
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        void AddToCart(string userEmail, int foodId, int quantity);
        IEnumerable<object> GetCartByUser(string userEmail);
        void RemoveFromCart(int itemId);
    }
}
