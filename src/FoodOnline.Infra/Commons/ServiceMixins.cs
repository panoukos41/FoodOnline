global using Mediator;
using FoodOnline.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceMixins
{
    public static void ConfigureInfraModule<TModule>(this IServiceCollection services, IConfiguration configuration)
        where TModule : IInfraModule
    {
        if (TModule.Configured) return;

        Log.Debug("InfraModule Configure: {Module}", typeof(TModule).Name);
        TModule.Configure(services, configuration);
    }

    public static void ConfigureInfraModules(this IServiceCollection services, IConfiguration configuration)
    {
        var args = new object[] { services, configuration };

        var modules = typeof(IInfraModule)
            .Assembly
            .ExportedTypes
            .Where(static t => t.GetInterface(nameof(IInfraModule)) is { })
            .ToList();

        var configure = typeof(ServiceMixins).GetMethod(nameof(ConfigureInfraModule), BindingFlags.Public | BindingFlags.Static)!;

        foreach (var module in modules)
        {
            configure.MakeGenericMethod(module)?.Invoke(null, args);
        }
    }
}
