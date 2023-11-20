using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AspNet7.Core.Configuration;
using AspNet7.Api;
using AspNet7.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AspNet7.Integration.Tests.Infrastructure
{
    public class StartupTest : Startup
    {
        public StartupTest(IWebHostEnvironment env, IConfiguration configuration) : base (env, configuration)
        {
            WebHostEnvironemnt = env;
            Configuration = configuration;
        }

        private IWebHostEnvironment WebHostEnvironemnt { get; }
        private IConfiguration Configuration { get; }
        protected override void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<AspNet7DataContext>(c => c.UseInMemoryDatabase("AspNet5WebApiTest").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var aspNet5DataContext = scopedServices.GetRequiredService<AspNet7DataContext>();
                aspNet5DataContext.Database.EnsureCreated();
                AspNet7DataSeed.SeedAsync(aspNet5DataContext, false).Wait();
            }
        }
    }
}
