using System.Net.Mime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AspNet5.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomConfigurationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services)
        {
            // Add framework services.
             services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml },
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                };
            });

            return services;
        }
    }
}
