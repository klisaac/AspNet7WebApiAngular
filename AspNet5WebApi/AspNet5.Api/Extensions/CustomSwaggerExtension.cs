using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AspNet5.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomSwaggerExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "AspNet5 WebApi V1",
                    Description = "AspNet5 WebAPI Version 1",
                    TermsOfService = new System.Uri("https://docs.microsoft.com/"),
                    Contact = new OpenApiContact() { Name = "Isaac", Email = "isaacbaby.t@gmail.com" },
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "1.0.1",
                    Title = "AspNet5 WebApi V2",
                    Description = "AspNet5 WebAPI Version 2",
                    TermsOfService = new System.Uri("https://docs.microsoft.com/"),
                    Contact = new OpenApiContact() { Name = "Isaac", Email = "isaacbaby.t@gmail.com" }
                });

                //var xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"PlatformAPI.xml";
                //c.IncludeXmlComments(xmlPath);

                //First we define the security scheme
                c.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
                //c.DescribeAllEnumsAsStrings();
                //c.DescribeStringEnumsInCamelCase();
            });
            return services;
        }
        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    //swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "/api" } };
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });
            //app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "AspNet5 WebApi Swagger UI";
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNet5 WebApi V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "AspNet5 WebApi V2");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
