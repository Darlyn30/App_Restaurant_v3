using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Infrastructure.Persistence.Contexts;
using UberEats.Infrastructure.Persistence.Repositories;
using UberEats.Infrastructure.Shared.Services;

namespace UberEats.Infrastructure.Persistence
{
    public static class ServiceRegister
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region contexts
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            #endregion

            #region repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRestaurantRepository,  RestaurantRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<IVerifyAccountRepository, VerifyAccountRepository>();
            #endregion
        }
    }
}
