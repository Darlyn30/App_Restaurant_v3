
using UberEats.Core.Application.Helpers;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> CreateUserAsync(SaveUserViewModel model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return false; // User already exists
            }

            var user = new User
            {
                Name = model.Name,
                PasswordHash = _passwordHasher.HashPassword(model.Password),
                Email = model.Email,
                Role = model.Role
            };

            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {

            
            await _userRepository.DeleteAsync(userId);

            return true;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var result = await _userRepository.GetByIdAsync(userId);
            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var result = await _userRepository.GetByEmailAsync(email);
            return result;
        }
    }
}
