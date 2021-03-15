using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.AzureAppServices;
using AspNet5.Core.Configuration;
using AspNet5.Core.Logging;
using AspNet5.Infrastructure.Data;
using System.Net;

namespace AspNet5.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedData(host);
            host.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.Configure<AzureFileLoggerOptions>(context.Configuration.GetSection(Constants.AzureFileLoggerOptions));
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config.SetBasePath(env.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                })
                .ConfigureLogging((context, logging) =>
                {
                    // clear all previously registered providers
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection(Constants.LoggingKey));
                    if (context.HostingEnvironment.IsDevelopment())
                        logging.AddDebug();
                    logging.AddConsole();
                    logging.AddAzureWebAppDiagnostics();

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel((context, serverOptions) =>
                    {
                        serverOptions.Configure(context.Configuration.GetSection(Constants.KestrelKey));
                    });
                    webBuilder.UseIIS();
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        private static void SeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<IAspNet5Logger<AspNet5DataSeed>>();
                try
                {
                    var aspNet5DataContext = services.GetRequiredService<AspNet5DataContext>();
                    AspNet5DataSeed.SeedAsync(aspNet5DataContext, true).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError("An error occurred seeding the DB.", ex);
                }
            }
        }
    }
}
