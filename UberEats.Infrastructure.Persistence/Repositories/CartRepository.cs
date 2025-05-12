using Microsoft.EntityFrameworkCore;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;
        private readonly IUserRepository _userRepository;
        public CartRepository(ApplicationContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Cart> GetCarts(int userId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if(cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreationAt = DateTime.UtcNow
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}
