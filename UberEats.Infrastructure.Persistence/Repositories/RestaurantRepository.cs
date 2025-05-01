using Microsoft.EntityFrameworkCore;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationContext _context;
        public RestaurantRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Restaurant> GetRestaurantByIdAsync(int restaurantId)
        {
            return await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId);
        }
        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {

            return await _context.Restaurants
                .ToListAsync();
        }
        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var restaurant = await GetRestaurantByIdAsync(restaurantId);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantsByCategoryIdAsync(int categoryId)
        {
            var result = await _context.Restaurants
                .Where(r => r.CategoryId == categoryId && r.Active)
                .ToListAsync();

            return result;
        }
    }
}
