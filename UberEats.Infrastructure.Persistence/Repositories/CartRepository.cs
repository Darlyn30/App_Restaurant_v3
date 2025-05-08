using Microsoft.EntityFrameworkCore;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Infrastructure.Persistence.Contexts;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;
        public CartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void AddToCart(string userEmail, int foodId, int quantity)
        {
            if(quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            //buscar el producto para obtener su precio
            var food = _context.Foods.FirstOrDefault(f => f.Id == foodId);

            if(food == null)
                throw new ArgumentException("Food not found.");

            //verificar si el usuario ya tiene un carrito

            var cart = _context.Carts.FirstOrDefault(c => c.UserEmail == userEmail);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserEmail = userEmail,
                    CreationAt = DateTime.Now
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            var existingItem = _context.CartItems
                .FirstOrDefault(ci => ci.CartId == cart.Id && ci.FoodId == foodId);

            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.Price = food.Price * existingItem.Quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    FoodId = foodId,
                    Quantity = quantity,
                    Price = food.Price
                };

                _context.CartItems.Add(newItem);
            }

            _context.SaveChanges();
        }

        public IEnumerable<object> GetCartByUser(string userEmail)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.UserEmail == userEmail);

            if (cart == null)
                throw new ArgumentException("Cart not found.");

            var cartItems = _context.CartItems
                .Where(ci => ci.CartId == cart.Id)
                .Join(_context.Foods,
                    ci => ci.FoodId,
                    f => f.Id,
                    (ci, f) => new 
                    {
                        ci.Id,
                        ci.Quantity,
                        ci.Price,
                        FoodId = f.Id,
                        f.Name,
                        f.Description,
                        f.ImgUrl
                    })
                .ToList();

            return cartItems;
        }

        public void RemoveFromCart(int itemId)
        {
            var item = _context.CartItems.FirstOrDefault(i => i.Id == itemId);

            if(item != null)
                throw new ArgumentException("Item not found.");

            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }
    }
}
