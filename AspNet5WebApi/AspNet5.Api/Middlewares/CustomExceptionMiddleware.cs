using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using AspNet5.Core.Logging;
using AspNet5.Application.Common.Exceptions;

namespace AspNet5.Api.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var logger = context.RequestServices.GetRequiredService<IAspNet5Logger<Startup>>();

            switch (exception)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    logger.LogInformation($"Bad request: {badRequestException.Message}");
                    break;
                case ArgumentNullException argNullException:
                    logger.LogInformation($"Argument null: {argNullException.Message}");
                    break;
                case AspNet5Exception aspNet5Exception:
                    logger.LogInformation($"Application exception: {aspNet5Exception.Message}");
                    break;
                case SqlException sqlException:
                    logger.LogInformation($"Database Exception: {sqlException.Message}");
                    break;
                default :
                    logger.LogInformation($"{nameof(exception)}: {exception.Message + exception.StackTrace}");
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = exception.Message }));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
