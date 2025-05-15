using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UberEats.Core.Application.Helpers;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Entities;
using UberEats.Core.Domain.Settings;
using UberEats.Infrastructure.Persistence.Contexts;
using UberEats.Infrastructure.Shared.Services;

namespace UberEats.Infrastructure.Persistence.Repositories
{
    public class UserRepository :GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<User> AddAsync(User user)
        {
            user.PasswordHash = PasswordEncryptation.ComputeSha256Hash(user.PasswordHash);
            await base.AddAsync(user);
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            return result;
        }

        public async Task<User> LoginAsync(LoginViewModel vm)
        {
            string passwordEncrypt = PasswordEncryptation.ComputeSha256Hash(vm.Password);
            User user = await _context.Set<User>()
                .FirstOrDefaultAsync(user => user.Email == vm.Email &&  user.PasswordHash == passwordEncrypt);

            return user;
        }
    }
}
