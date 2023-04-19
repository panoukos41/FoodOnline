using FoodOnline.Abstractions;

namespace FoodOnline.Authentications;

public sealed class AuthenticationsWebModule : IWebModule
{
    public static bool Configured { get; private set; }

    public static bool Used { get; private set; }

    public static void Configure(WebApplicationBuilder builder)
    {
        Configured = true;

        var services = builder.Services;
    }

    public static void Use(WebApplication app)
    {
        Used = true;

        var group = app.MapGroup("auth");
    }
}
