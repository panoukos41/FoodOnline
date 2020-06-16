using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BlazorStrap;
using Flurl.Http;
using FoodOnline.Domain.Models;
using FoodOnline.WebClient.Helpers;
using FoodOnline.WebClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOnline.WebClient
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the following services: <br />
        /// <br />
        /// <see cref="IFlurlClient"/> (Singleton) <br />
        /// <see cref="ILocalStorageService"/> (Scoped) <br />
        /// <see cref="ISessionStorageService"/> (Scoped) <br />
        /// <see cref="IFlurlNavigationManager"/> (Scoped) <br />
        /// AddAuthorizationCore() <br />
        /// <see cref="YamlDotNet.Serialization.ISerializer"/> (Scoped) <br />
        /// <see cref="YamlDotNet.Serialization.IDeserializer"/> (Scoped) <br />
        /// <br />
        /// Also Configures <see cref="FlurlClient"/> with <see cref="HttpClientFactoryForBlazor"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddSingleton<IFlurlClient>(f => new FlurlClient("http://localhost:4000"));
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddBlazoredSessionStorage(options =>
                options.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddScoped<IFlurlNavigationManager, FlurlNavigationManager>();
            services.AddBootstrapCss();
            services.AddAuthorizationCore();

            services.AddScoped(f => Yaml.NewSerializer);
            services.AddScoped(f => Yaml.NewDeserializer);

            FlurlHttp.Configure(settings =>
            {
                settings.HttpClientFactory = new HttpClientFactoryForBlazor();
            });

            return services;
        }
    }
}