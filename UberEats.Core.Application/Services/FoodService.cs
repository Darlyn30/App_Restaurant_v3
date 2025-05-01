using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;

        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<IEnumerable<Food>> GetByRestaurant(int restaurantId)
        {
            var result = await _foodRepository.GetByRestaurant(restaurantId);
            return result;
        }
    }
}
