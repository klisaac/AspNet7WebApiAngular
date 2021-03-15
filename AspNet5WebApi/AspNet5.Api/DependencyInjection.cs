using Microsoft.Extensions.DependencyInjection;
using AspNet5.Application.Common.Identity;
using AspNet5.Api.Common.Identity;

namespace AspNet5.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUser, CurrentUser>();
            return services;
        }
    }
}
