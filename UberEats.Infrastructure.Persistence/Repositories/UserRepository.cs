using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Domain.Entities;
using UberEats.Core.Domain.Settings;
using UberEats.Infrastructure.Persistence.Contexts;
using UberEats.Infrastructure.Shared.Services;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            return result;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            return result;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }
    }
}
