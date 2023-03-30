using FoodOnline.Abstractions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceMixins
{
    public static void AddWebModule<TModule>(this WebApplicationBuilder builder)
        where TModule : IWebModule
    {
        TModule.Configure(builder);
    }
    
    public static void UseWebModule<TModule>(this WebApplication app)
        where TModule : IWebModule
    {
        TModule.Use(app);
    }

    private static Type[] Modules { get; set; } = Array.Empty<Type>();

    public static void AddWebModules(this WebApplicationBuilder builder)
    {
        var args = new object[] { builder };

        Modules = typeof(IWebModule)
            .Assembly
            .ExportedTypes
            .Where(static t => t.GetInterface(nameof(IWebModule)) is { })
            .ToArray();

        foreach (var module in Modules)
        {
            Log.Debug("WebModule Configure: {Module}", module.Name);

            var method = module.GetMethod(nameof(IWebModule.Configure), BindingFlags.Public | BindingFlags.Static);
            method?.Invoke(null, args);
        }
    }

    public static void UseAllWebModules(this WebApplication app)
    {
        var args = new object[] { app };

        foreach (var module in Modules)
        {
            Log.Debug("WebModule Use: {Module}", module.Name);

            var method = module.GetMethod(nameof(IWebModule.Use), BindingFlags.Public | BindingFlags.Static);
            method?.Invoke(null, args);
        }
    }
}
