namespace UberEats.Core.Application.Interfaces.Services
{
    public interface ISendEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
