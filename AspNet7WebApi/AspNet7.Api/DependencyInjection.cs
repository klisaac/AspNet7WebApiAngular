using Microsoft.Extensions.DependencyInjection;
using AspNet7.Api.Common.Identity;
using AspNet7.Application.Common.Identity;

namespace AspNet7.Api
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
