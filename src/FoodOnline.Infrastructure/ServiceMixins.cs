global using Mediator;
using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceMixins
{
    public static void AddInfrastructureModule<TModule>(this IServiceCollection services, IConfiguration configuration)
        where TModule : IInfrastructureModule
    {
        TModule.Configure(services, configuration);
    }

    public static void AddInfrastructureModules(this IServiceCollection services, IConfiguration configuration)
    {
        var args = new object[] { services, configuration };

        var types = typeof(IInfrastructureModule)
            .Assembly
            .ExportedTypes
            .Where(static t => t.GetInterface(nameof(IInfrastructureModule)) is { })
            .ToList();

        foreach (var type in types)
        {
            Log.Debug("InfrastructureModule Configure: {Module}", type.Name);

            var method = type.GetMethod(nameof(IInfrastructureModule.Configure), BindingFlags.Public | BindingFlags.Static);
            method?.Invoke(null, args);
        }
    }
}
