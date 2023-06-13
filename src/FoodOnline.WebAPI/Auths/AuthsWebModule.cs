using FoodOnline.Abstractions;

namespace FoodOnline.Auths;

/// <summary>
/// This already adds <see cref="AuthsInfraModule"/>
/// </summary>
public sealed class AuthsWebModule : IWebModule
{
    public static void Add(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.Add<AuthsInfraModule>(configuration);
    }

    public static void Use(WebApplication app)
    {
        var group = app.MapGroup("auth");
    }
}
