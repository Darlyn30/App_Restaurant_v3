
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(SaveUserViewModel model);
        //Task<bool> UpdateUserAsync(UpdateUserViewModel model);
        Task<bool> DeleteUserAsync(int userId);
    }
}
