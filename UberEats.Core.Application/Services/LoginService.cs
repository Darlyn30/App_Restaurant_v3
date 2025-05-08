 using Microsoft.Extensions.Configuration;
using UberEats.Core.Application.DTO.Account;
using UberEats.Core.Application.Helpers;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.User;

namespace UberEats.Core.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public LoginService(IUserRepository userRepository, IPasswordHasher passwordHasher, IConfiguration configuration, IAuthService authService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _authService = authService;
        }

        public async Task<LoginResult> LoginAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null)
                return new LoginResult { IsSuccess = false, Message = "Usuario no encontrado" };

            if (!_passwordHasher.VerifyHashedPassword(model.Password, user.PasswordHash))
                return new LoginResult { IsSuccess = false, Message = "Correo o contraseña incorrecta" };

            if (!user.IsActive)
                return new LoginResult { IsSuccess = false, Message = "Usuario no verificado, por favor revise su correo" };

            // solo si el usuario esta verificado
            // Generar JWT
            var token = await _authService.GenerateJwtTokenAsync(user);
            return new LoginResult 
            { 
                IsSuccess = true,
                Token = token,
                UserVm = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Role = user.Role
                }
            };
        }
    }
}
