global using Mediator;
using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceMixins
{
    public static void Add<TInfraModule>(this IServiceCollection services, IConfiguration configuration)
        where TInfraModule : IInfraModule
    {
        Log.Debug("Add InfraModule: {Module}", typeof(TInfraModule).Name);
        TInfraModule.Add(services, configuration);
    }
}
