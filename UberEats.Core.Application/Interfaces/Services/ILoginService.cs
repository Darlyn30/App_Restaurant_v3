
using UberEats.Core.Application.DTO;
using UberEats.Core.Application.ViewModels.User;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginViewModel model);
    }
}
