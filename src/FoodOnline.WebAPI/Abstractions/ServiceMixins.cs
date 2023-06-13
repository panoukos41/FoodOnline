using FoodOnline.Abstractions;

namespace FoodOnline.Commons;

public static class ServiceMixins
{
    public static void Add<TWebModule>(this WebApplicationBuilder builder)
        where TWebModule : IWebModule
    {
        Log.Debug("Add WebModule: {Module}", typeof(TWebModule).Name);
        TWebModule.Add(builder);
    }

    public static void Use<TWebModule>(this WebApplication app)
        where TWebModule : IWebModule
    {
        Log.Debug("Use WebModule: {Module}", typeof(TWebModule).Name);
        TWebModule.Use(app);
    }
}
