using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AspNet5.Application.Common.Mappings;
using AspNet5.Application.Handlers;

namespace AspNet5.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(typeof(UpdateProductCommandHandler).GetTypeInfo().Assembly);
            return services;
        }
    }
}
