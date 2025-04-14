using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Settings;

namespace UberEats.Infrastructure.Shared.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _config;
        private readonly PinRandomService _PinRandom;
        private readonly MailModel _model;
        private readonly UserViewModel _user;
        public SendEmailService(IConfiguration _config)
        {
            this._config = _config;
            _model = new MailModel();
            _PinRandom = PinRandomService.Instance;
            _user = new UserViewModel();
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {


            var email = _config.GetSection("EmailSettings:Email").Value;
            var password = _config.GetSection("EmailSettings:Password").Value;
            var host = _config.GetSection("EmailSettings:Host").Value;
            var port = _config.GetSection("EmailSettings:Port").Value;

            var smtpClient = new SmtpClient(host, int.Parse(port));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            var pin = _PinRandom.pinRandom();
            _model.EnterpriseName = "DDA Company";
            _model.WebSite = "https://ddacompany.com";
            _model.SupportEmail = email;

            subject = "Código de Verificación - Completa tu Solicitud";

            body = $"<html><body>" +
                        $"<p>Estimado/a {_user.Name},</p>" +
                        $"<p>Gracias por registrarte en {_model.EnterpriseName}. Para garantizar la seguridad de tu cuenta, hemos generado un código de verificación único.</p>" +
                        $"<p><strong>Tu código de verificación es:</strong></p>" +
                        $"<p style=\"font-size: 18px; font-weight: bold; color: #2C3E50;\">{pin}</p>" +
                        $"<p>Por favor, ingresa este código en el formulario de nuestra aplicación para completar el proceso de verificación.</p>" +
                        $"<p>Si no solicitaste este código, por favor ignora este correo.</p>" +
                        $"<p>En caso de cualquier duda, no dudes en ponerte en contacto con nosotros a través de <a href=\"mailto:{_model.SupportEmail}\">{_model.SupportEmail}</a>.</p>" +
                        $"<p>¡Gracias por confiar en {_model.EnterpriseName}!</p>" +
                        $"<br><p>Atentamente,</p>" +
                        $"<p>El equipo de {_model.EnterpriseName}</p>" +
                        $"<p><a href=\"{_model.WebSite}\" target=\"_blank\">Visita nuestro sitio web</a></p>" +
                        $"</body></html>";

            //DATA Enterprise
            


            smtpClient.Credentials = new NetworkCredential(email, password);
            var message = new MailMessage(email!, to, subject, body)
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
