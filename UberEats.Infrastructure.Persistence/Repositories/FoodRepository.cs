
using Microsoft.EntityFrameworkCore;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationContext _context;

        public FoodRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Food>> GetByRestaurant(int restaurantId)
        {
            var foods = await _context.Foods
                .Where(f => f.RestaurantId == restaurantId)
                .ToListAsync();

            return foods;
        }
    }
}
