using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using AspNet5.Core.Configuration;

namespace AspNet5.Integration.Tests.Infrastructure
{
    public class ApplicationFactoryTest<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null)
                .UseStartup<TEntryPoint>()
                .ConfigureAppConfiguration((context, conf) =>
                {
                    var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                    conf.AddJsonFile(configPath);
                })
                .ConfigureLogging((context, logging) =>
                {
                    // clear all previously registered providers
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection(Constants.LoggingKey));
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    }
                })
                .UseNLog();
        }
        protected override void ConfigureWebHost(IWebHostBuilder webBuilder)
        {
            webBuilder.UseSolutionRelativeContentRoot(@"Tests\AspNet5.Integration.Tests");
        }
    }
}