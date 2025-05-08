
namespace UberEats.Core.Application.Interfaces.Services
{
    public interface ICartService
    {
        void AddToCart(string userEmail, int foodId, int quantity);
        IEnumerable<object> GetCartByUser(string userEmail);
        void RemoveFromCart(int itemId);
    }
}
