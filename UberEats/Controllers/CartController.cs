using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet]

        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserIdFromToken();

            var cart = await _cartService.GetCarts(userId);

            return Ok(cart);
        }

        private int GetUserIdFromToken()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
