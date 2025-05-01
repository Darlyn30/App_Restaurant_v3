
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _restaurantRepository.AddRestaurantAsync(restaurant);
        }

        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            await _restaurantRepository.DeleteRestaurantAsync(restaurantId);
        }

        public Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            var result = _restaurantRepository.GetAllRestaurantsAsync();
            return result;
        }

        public Task<Restaurant> GetRestaurantByIdAsync(int restaurantId)
        {
            var result = _restaurantRepository.GetRestaurantByIdAsync(restaurantId);
            return result;
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByCategoryIdAsync(int categoryId)
        {
            var result = await _restaurantRepository.GetRestaurantsByCategoryIdAsync(categoryId);
            return result;
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            var result = await _restaurantRepository.GetRestaurantByIdAsync(restaurant.Id);
            if (result != null)
            {
                result.Name = restaurant.Name;
                result.Description = restaurant.Description;
                result.CategoryId = restaurant.CategoryId;
                result.Active = restaurant.Active;
                await _restaurantRepository.UpdateRestaurantAsync(result);
            }
            else
            {
                throw new Exception("Restaurant not found");
            }
        }
    }
}
