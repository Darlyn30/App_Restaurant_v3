using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberEats.Core.Application.Interfaces.Services;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]

        public IActionResult AddToCart(string userEmail, int foodId, int quantity)
        {
            _cartService.AddToCart(userEmail, foodId, quantity);
            return Ok("Item added to cart");
        }

        [HttpGet("{email}")]

        public IActionResult GetCartByUser(string email)
        {
            var result = _cartService.GetCartByUser(email);
            return Ok(result);
        }

        [HttpDelete("/item/{itemId}")]

        public IActionResult RemoveFromCart(int itemId)
        {
            _cartService.RemoveFromCart(itemId);
            return Ok("Item removed from cart");
        }
    }
}
