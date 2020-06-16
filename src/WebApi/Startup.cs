using FoodOnline.Application;
using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Infrastructure;
using FoodOnline.WebApi.Hubs;
using FoodOnline.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodOnline.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUserService>();

            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddSignalR();
            services.AddControllers();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseBlazorFrameworkFiles("/Business");
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OrderHub>("/api/order");
                // Map Business SPA
                endpoints.MapFallbackToFile("/business/{*path:nonfile}", "Business/index.html");
                // Map SPA
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}