using Microsoft.Extensions.DependencyInjection;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;
using AspNet7.Core.Repository.Base;
using AspNet7.Infrastructure.Logging;
using AspNet7.Infrastructure.Repository;
using AspNet7.Infrastructure.Repository.Base;

namespace AspNet7.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IAspNet7Logger<>), typeof(AspNet7Logger<>));
            return services;
        }
    }
}
