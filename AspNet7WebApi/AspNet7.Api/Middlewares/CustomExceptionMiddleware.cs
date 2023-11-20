using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AspNet7.Application.Common.Exceptions;
using AspNet7.Core.Logging;

namespace AspNet7.Api.Middlewares
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
            var logger = context.RequestServices.GetRequiredService<IAspNet7Logger<Startup>>();

            switch (exception)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    logger.Error($"Bad request: {badRequestException.Message}");
                    break;
                case ArgumentNullException argNullException:
                    logger.Error($"Argument null: {argNullException.Message}");
                    break;
                case AspNet7Exception aspNet7Exception:
                    logger.Error($"Application exception: {aspNet7Exception.Message}");
                    break;
                case SqlException sqlException:
                    logger.Error($"Database Exception: {sqlException.Message}");
                    break;
                default:
                    logger.Error($"{nameof(exception)}: { exception.Message} | {exception.StackTrace}");
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(statusCode.ToString());
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
