
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;

namespace UberEats.Core.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddToCart(string userEmail, int foodId, int quantity)
        {
            _cartRepository.AddToCart(userEmail, foodId, quantity);
        }

        public IEnumerable<object> GetCartByUser(string userEmail)
        {
            var result = _cartRepository.GetCartByUser(userEmail);
            return result;
        }

        public void RemoveFromCart(int itemId)
        {
            _cartRepository.RemoveFromCart(itemId);
        }
    }
}
