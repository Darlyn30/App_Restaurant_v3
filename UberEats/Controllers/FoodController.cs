 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetByRestaurant(int restaurantId)
        {
            var result = await _foodService.GetByRestaurant(restaurantId);
            if (result == null || !result.Any())
                return NotFound("No foods found for this restaurant.");

            return Ok(result);
        }
    }
}
