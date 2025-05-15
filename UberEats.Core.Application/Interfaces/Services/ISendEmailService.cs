using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Interfaces.Services
{
    public interface ISendEmailService
    {
        Task sendEmail(string to, string subject, string body);
    }
}
