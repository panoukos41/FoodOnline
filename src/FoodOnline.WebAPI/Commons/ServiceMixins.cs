using FoodOnline.Abstractions;
using System.Reflection;

namespace FoodOnline.Commons;

public static class ServiceMixins
{
    public static void ConfigureWebModule<TModule>(this WebApplicationBuilder builder)
        where TModule : IWebModule
    {
        if (TModule.Configured) return;

        Log.Debug("WebModule Configure: {Module}", typeof(TModule).Name);
        TModule.Configure(builder);
    }

    public static void UseWebModule<TModule>(this WebApplication app)
        where TModule : IWebModule
    {
        if (TModule.Used) return;

        Log.Debug("WebModule Use: {Module}", typeof(TModule).Name);
        TModule.Use(app);
    }

    private static Type[] Modules { get; set; } = Array.Empty<Type>();

    public static void ConfigureWebModules(this WebApplicationBuilder builder)
    {
        var args = new object[] { builder };

        Modules = typeof(IWebModule)
            .Assembly
            .ExportedTypes
            .Where(static t => t.GetInterface(nameof(IWebModule)) is { })
            .ToArray();

        var configure = typeof(ServiceMixins).GetMethod(nameof(ConfigureWebModule), BindingFlags.Public | BindingFlags.Static)!;

        foreach (var module in Modules)
        {
            configure.MakeGenericMethod(module)!.Invoke(null, args);
        }
    }

    public static void UseWebModules(this WebApplication app)
    {
        var args = new object[] { app };

        var use = typeof(ServiceMixins).GetMethod(nameof(UseWebModule), BindingFlags.Public | BindingFlags.Static)!;

        foreach (var module in Modules)
        {
            use.MakeGenericMethod(module)?.Invoke(null, args);
        }
    }
}
