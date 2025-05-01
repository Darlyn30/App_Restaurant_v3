
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantByIdAsync(int restaurantId);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task AddRestaurantAsync(Restaurant restaurant);
        Task UpdateRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(int restaurantId);
        Task<IEnumerable<Restaurant>> GetRestaurantsByCategoryIdAsync(int categoryId);

    }
}
