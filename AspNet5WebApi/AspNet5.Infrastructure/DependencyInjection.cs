using Microsoft.Extensions.DependencyInjection;
using AspNet5.Core.Logging;
using AspNet5.Core.Repository;
using AspNet5.Core.Repository.Base;
using AspNet5.Infrastructure.Logging;
using AspNet5.Infrastructure.Repository;
using AspNet5.Infrastructure.Repository.Base;

namespace AspNet5.Infrastructure
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
            services.AddScoped(typeof(IAspNet5Logger<>), typeof(AspNet5Logger<>));
            return services;
        }
    }
}
