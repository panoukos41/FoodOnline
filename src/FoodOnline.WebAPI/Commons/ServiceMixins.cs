using FoodOnline.Abstractions;

namespace FoodOnline.Commons;

public static class ServiceMixins
{
    public static void ConfigureWebModule<TModule>(this WebApplicationBuilder builder)
        where TModule : IWebModule
    {
        Log.Debug("WebModule Configure: {Module}", typeof(TModule).Name);
        TModule.Configure(builder);
    }

    public static void UseWebModule<TModule>(this WebApplication app)
        where TModule : IWebModule
    {
        Log.Debug("WebModule Use: {Module}", typeof(TModule).Name);
        TModule.Use(app);
    }
}
