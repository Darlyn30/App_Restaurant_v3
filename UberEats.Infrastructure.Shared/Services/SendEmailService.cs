
using System.Net.Mail;
using System.Net;
using UberEats.Core.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using UberEats.Core.Domain.Settings;
using UberEats.Core.Domain.Entities;

namespace UberEats.Infrastructure.Shared.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _config;
        PinRandomService _pinRandom = PinRandomService.Instance();
        MailModel _model = new MailModel();

        public SendEmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task sendEmail(string to, string subject, string body)
        {
            /*
            estas variables serian "variables de entorno", definidos los valores en el appsettings.json
            */
            var email = _config.GetSection("EmailSettings:Email").Value;
            var password = _config.GetSection("EmailSettings:Password").Value;
            var host = _config.GetSection("EmailSettings:Host").Value;
            var port = _config.GetSection("EmailSettings:Port").Value;

            var smtpClient = new SmtpClient(host, int.Parse(port));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;


            smtpClient.Credentials = new NetworkCredential(email, password);
            var mensaje = new MailMessage(email!, to, subject, body)
            {
                IsBodyHtml = true // habilitamos el cuerpo con estilo html
            };
            await smtpClient.SendMailAsync(mensaje);
        }
    }

}
