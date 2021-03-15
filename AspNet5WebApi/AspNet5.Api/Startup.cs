using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AspNet5.Core.Configuration;
using AspNet5.Infrastructure;
using AspNet5.Infrastructure.Data;
using AspNet5.Application;
using AspNet5.Api.Extensions;
using AspNet5.Api.Middlewares;

namespace AspNet5.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            WebHostEnvironemnt = env;
            Configuration = configuration;
        }
        private IWebHostEnvironment WebHostEnvironemnt { get; }
        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddCustomConfiguration();
            services.AddControllers();
            services.AddCustomSwagger();
            services.AddCustomAuthentication(Configuration);
            services.AddApiServices();
            // Add Infrastructure Layer
            services.AddInfrastructureServices();
            // Add Application Layer
            services.AddApplicationServices();
            // Add Miscellaneous
            services.AddHttpContextAccessor();
            ConfigureDatabases(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            app.UseCustomExceptionMiddleware();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomSwagger();
            
            //var seed = _configuration.GetValue<bool>("seed");
            //if (seed)
            //    SeedData.Initialize(app);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureDatabases(IServiceCollection services)
        {
            if (WebHostEnvironemnt.IsDevelopment() || WebHostEnvironemnt.IsStaging() || WebHostEnvironemnt.IsProduction())
            {
                services.AddDbContext<AspNet5DataContext>(c => c.UseSqlServer(Configuration.GetConnectionString(Constants.DbConnectionStringKey)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);
            }
            else 
            {
                services.AddDbContext<AspNet5DataContext>(c => c.UseInMemoryDatabase("AspNet5WebApiTest").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);
            }
        }
    }
}
