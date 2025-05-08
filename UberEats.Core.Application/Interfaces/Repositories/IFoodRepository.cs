using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetByRestaurant(int restaurantId);
        Task AddCart(Cart cart);
    }
}
