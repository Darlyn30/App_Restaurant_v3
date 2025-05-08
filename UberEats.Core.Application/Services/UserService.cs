
using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;
using UberEats.Core.Application.Helpers;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Entities;
using UberEats.Core.Domain.Settings;
using UberEats.Infrastructure.Shared.Services;

namespace UberEats.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISendEmailService _emailService;
        PinRandomService _random = PinRandomService.Instance();
        MailModel _mailModel = new MailModel();
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ISendEmailService emailService, IConfiguration config)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _config = config;
        }

        public async Task<bool> CreateUserAsync(SaveUserViewModel model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return false; // User already exists
            }

            var pin = _random.pinRandom();
            var user = new User
            {
                Name = model.Name,
                PasswordHash = _passwordHasher.HashPassword(model.Password),
                Email = model.Email,
                Pin = pin,
                Role = "Client"
            };


            _mailModel.To = model.Email;
            _mailModel.SupportEmail = _config.GetSection("EmailSettings:Email").Value;

            string body = $"<html><body>" +
             $"<p>Estimado/a {model.Name},</p>" +
             $"<p>Gracias por registrarte en {_mailModel.EnterpriseName}. Para garantizar la seguridad de tu cuenta, hemos generado un código de verificación único.</p>" +
             $"<p><strong>Tu código de verificación es:</strong></p>" +
             $"<p style=\"font-size: 18px; font-weight: bold; color: #2C3E50;\">{pin}</p>" +
             $"<p>Por favor, ingresa este código en el formulario de nuestra aplicación para completar el proceso de verificación.</p>" +
             $"<p>Si no solicitaste este código, por favor ignora este correo.</p>" +
             $"<p>En caso de cualquier duda, no dudes en ponerte en contacto con nosotros a través de <a href=\"mailto:{_mailModel.SupportEmail}\">{_mailModel.SupportEmail}</a>.</p>" +
             $"<p>¡Gracias por confiar en {_mailModel.EnterpriseName}!</p>" +
             $"<br><p>Atentamente,</p>" +
             $"<p>El equipo de {_mailModel.EnterpriseName}</p>" +
             $"<p><a href=\"{_mailModel.WebSite}\" target=\"_blank\">Visita nuestro sitio web</a></p>" +
             $"</body></html>";

            await _userRepository.AddAsync(user);
            await _emailService.sendEmail(_mailModel.To, _mailModel.Subject, body);
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var result = await _userRepository.GetAllUserAsync();
            return result;
        }
    }
}
