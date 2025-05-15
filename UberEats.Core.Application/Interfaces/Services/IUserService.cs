
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
        Task<List<UserViewModel>> GetByName(string name);
    }
}
