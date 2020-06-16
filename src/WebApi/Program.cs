using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FoodOnline.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //Initialize the database
            //Seeded: true
            //var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            //using (var scope = scopeFactory.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            //    SeedData.Initialize(db).GetAwaiter().GetResult();
            //}

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseKestrel(options =>
                    {
                        options.ListenLocalhost(4000);
                    });
                });
        }
    }
}