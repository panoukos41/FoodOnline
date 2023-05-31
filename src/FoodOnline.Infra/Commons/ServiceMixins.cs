global using Mediator;
using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceMixins
{
    public static void Add<TModule>(this IServiceCollection services, IConfiguration configuration)
        where TModule : IInfraModule
    {
        Log.Debug("InfraModule Configure: {Module}", typeof(TModule).Name);
        TModule.Configure(services, configuration);
    }
}
