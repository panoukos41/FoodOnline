using Blazored.LocalStorage;
using Flurl.Http;
using FoodOnline.WebClient.Business.Services;
using FoodOnline.WebClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FoodOnline.WebClient.Business
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var services = builder.Services;
            builder.RootComponents.Add<App>("app");

            services.AddSharedServices();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>(f =>
                new ApiAuthenticationStateProvider(f.GetService<IFlurlClient>(), f.GetService<ILocalStorageService>(), "businessToken"));
            services.AddScoped<IAuthService, AuthService>();

            await builder.Build().RunAsync();
        }
    }
}