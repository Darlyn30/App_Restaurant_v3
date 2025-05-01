using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.Services;
using UberEats.Core.Domain.Entities;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetRestaurantsByCategoryId(int categoryId)
        {
            var restaurants = await _restaurantService.GetRestaurantsByCategoryIdAsync(categoryId);
            if (restaurants == null || !restaurants.Any())
                return NotFound();
            return Ok(restaurants);
        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurant([FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _restaurantService.AddRestaurantAsync(restaurant);
            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            if (id != restaurant.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _restaurantService.UpdateRestaurantAsync(restaurant);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
                return NotFound();

            await _restaurantService.DeleteRestaurantAsync(id);
            return NoContent();
        }
    }
}
