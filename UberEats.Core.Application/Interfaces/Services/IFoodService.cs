using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IFoodService
    {
        Task<IEnumerable<Food>> GetByRestaurant(int restaurantId);
        Task AddCart(Cart cart);
    }
}
