using Microsoft.Extensions.DependencyInjection;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Domain.Settings;
using UberEats.Infrastructure.Shared.Services;

namespace UberEats.Infrastructure.Shared
{
    public static class ServiceRegister
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISendEmailService, SendEmailService>();
        }
    }
}
